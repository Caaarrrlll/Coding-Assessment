import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class RequestInterceptor implements HttpInterceptor {
  constructor() {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    let token = sessionStorage.getItem('token');
    if (typeof token === 'string') {
      request = this.addHeaders(request);
    }

    return next.handle(request);
  }

  addHeaders(request: HttpRequest<any>) {
    return request.clone({
      setHeaders: {
        'Content-Type': 'application/json',
        Authorization:
          'Bearer ' +
          JSON.parse(sessionStorage.getItem('token')!)['accessToken'],
      },
    });
  }
}
