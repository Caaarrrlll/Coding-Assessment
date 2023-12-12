import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Customer } from 'src/app/apis/customer/models/customer.type';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  customerUrl: string = environment.apiUrl + '/customers';

  constructor(private _http: HttpClient) {}

  getCustomers(): Observable<Customer[]> {
    return this._http.get<Customer[]>(`${this.customerUrl}/Customers`);
  }

  getCustomerById(id: number): Observable<Customer> {
    return this._http.get<Customer>(
      `${this.customerUrl}/CustomerDetails?id=${id}`
    );
  }

  createCustomer(customer: Customer): Observable<Customer> {
    delete customer.id;
    return this._http.post<Customer>(
      `${this.customerUrl}/CreateCustomer`,
      customer
    );
  }

  editCustomer(customer: Customer): Observable<Customer> {
    return this._http.patch<Customer>(
      `${this.customerUrl}/EditCustomer`,
      customer
    );
  }

  deleteCustomer(id: number): Observable<boolean> {
    return this._http.delete<boolean>(
      `${this.customerUrl}/DeleteCustomer?id=${id}`
    );
  }
}
