import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { IdentityService } from 'src/app/apis/identity/identity.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  employeeForm: FormGroup = new FormGroup({
    email: new FormGroup(
      '',
      Validators.compose([Validators.required, Validators.email])
    ),
    password: new FormGroup(
      '',
      Validators.compose([
        Validators.required,
        Validators.minLength(6),
        Validators.pattern(
          /^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?\/~_+-=|\])$/
        ),
      ])
    ),
  });

  get email() {
    return this.employeeForm.get('email')?.value;
  }

  set email(value: string) {
    this.employeeForm.get('email')?.setValue(value);
  }

  get password() {
    return this.employeeForm.get('password')?.value;
  }

  set password(value: string) {
    this.employeeForm.get('password')?.setValue(value);
  }

  constructor(
    private _identityService: IdentityService,
    private _snackBar: MatSnackBar,
    private _router: Router
  ) {}

  ngOnInit(): void {}

  login(): void {
    const email = this.email;
    const password = this.password;

    const token = JSON.parse(sessionStorage.getItem('token') ?? '');
    const now = new Date().getTime();

    if (token == '') {
      this._identityService.loginUser(email, password).subscribe({
        next: (val: any) => {
          const token = {
            accessToken: val['accessToken'],
            refreshToken: val['refreshToken'],
            expireTime: new Date().getTime() + val['expiresIn'] * 1000,
            userName: email,
          };

          sessionStorage.setItem('token', JSON.stringify(token));

          this._snackBar.open('User logged in', 'Close', { duration: 3000 });

          this._router.navigate(['/user-list']);
        },
        error: () => {
          this._snackBar.open('User could not be logged in', 'Close', {
            duration: 5000,
          });
        },
      });
    }

    if (token != '' && +token['expireTime'] > now) {
      this._identityService.refreshToken(token['refreshToken']).subscribe({
        next: (val: any) => {
          const token = {
            accessToken: val['accessToken'],
            refreshToken: val['refreshToken'],
            expireTime: new Date().getTime() + val['expiresIn'] * 1000,
            userName: email,
          };

          sessionStorage.setItem('token', JSON.stringify(token));

          this._snackBar.open('User logged in', 'Close', { duration: 3000 });

          this._router.navigate(['/user-list']);
        },
        error: () => {
          this._snackBar.open('User could not be logged in', 'Close', {
            duration: 5000,
          });
        },
      });
    }
  }

  register(email: string, password: string): void {
    this._identityService.registerUser(email, password).subscribe({
      next: () => {
        this._snackBar.open('User registered', 'Close', { duration: 3000 });
        this.email = email;
        this.password = password;
        this.login();
      },
      error: (failed) => {
        Object.keys(failed['error']['errors']).map((error) => {
          this._snackBar.open(
            `Error: ${failed['error']['errors'][`${error}`]}`,
            'Close',
            {
              duration: 5000,
            }
          );
        });
      },
    });
  }
}
