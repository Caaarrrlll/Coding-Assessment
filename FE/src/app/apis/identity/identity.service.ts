import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class IdentityService {
  constructor(private _http: HttpClient) {}

  registerUser(email: string, password: string) {
    return this._http.post(`${environment.apiUrl}/register`, {
      email,
      password,
    });
  }

  loginUser(email: string, password: string) {
    return this._http.post(`${environment.apiUrl}/login`, { email, password });
  }
}
