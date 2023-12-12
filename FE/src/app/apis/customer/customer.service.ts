import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Address, Customer } from 'src/app/apis/customer/models/customer.type';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  customerUrl: string = environment.apiUrl + '/customers';

  constructor(private _http: HttpClient) {}

  // *** Customer Details *** //
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

  // *** Customer Address *** //
  createAddress(address: Address): Observable<Address> {
    delete address.id;
    return this._http.post<Address>(
      `${this.customerUrl}/CreateAddress`,
      address
    );
  }

  editAddress(address: Address): Observable<Address> {
    return this._http.patch<Address>(
      `${this.customerUrl}/EditAddress`,
      address
    );
  }

  deleteAddress(id: number): Observable<boolean> {
    return this._http.delete<boolean>(
      `${this.customerUrl}/DeleteAddress?id=${id}`
    );
  }
}
