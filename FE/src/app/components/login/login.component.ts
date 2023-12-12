import { Component, OnInit } from '@angular/core';
import { IdentityService } from 'src/app/apis/identity/identity.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  private _register: boolean = false;

  constructor(private _identityService: IdentityService) {}

  ngOnInit(): void {
    this._identityService
      .registerUser('venter109@gmail.com', 'Test@123')
      .subscribe((val) => {
        console.log(val);
      });
  }
}
