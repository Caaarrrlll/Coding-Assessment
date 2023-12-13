import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
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
    email: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/),
      ])
    ),
    password: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(
          /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,14}$/
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

  ngOnInit(): void {
    Object.keys(this.employeeForm.controls).map((control) =>
      this.employeeForm.controls[control].markAsTouched()
    );
  }

  login(): void {
    const email = this.email;
    const password = this.password;

    let token: any = '';
    let readSession = sessionStorage.getItem('token');
    if (readSession != null) {
      token = JSON.parse(readSession);
    }
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

          this._router.navigate(['/customers']);
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

          this._router.navigate(['/customers']);
        },
        error: () => {
          this._snackBar.open('User could not be logged in', 'Close', {
            duration: 5000,
          });
        },
      });
    }
  }

  register(): void {
    const email = this.email;
    const password = this.password;

    this._identityService.registerUser(email, password).subscribe({
      next: () => {
        this._snackBar.open('User registered', 'Close', { duration: 3000 });
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
