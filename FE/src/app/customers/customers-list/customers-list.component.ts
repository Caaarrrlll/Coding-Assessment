import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CustomerService } from 'src/app/apis/customer/customer.service';
import { Address, Customer } from 'src/app/apis/customer/models/customer.type';
import { CustomersAddressComponent } from 'src/app/customers/customers-address/customers-address.component';
import { CustomersDetailComponent } from 'src/app/customers/customers-detail/customers-detail.component';

@Component({
  selector: 'app-customers-list',
  templateUrl: './customers-list.component.html',
  styleUrls: ['./customers-list.component.scss'],
})
export class CustomersListComponent {
  customerDataSource: MatTableDataSource<any> = new MatTableDataSource<any>();

  displayedColumns: string[] = [
    'name',
    'surname',
    'phoneNumber',
    'email',
    'identityNumber',
    'addressId',
    'addressActions',
    'actions',
  ];

  constructor(
    private _customerService: CustomerService,
    public dialog: MatDialog
  ) {
    this._customerService.getCustomers().subscribe((customers) => {
      this.customerDataSource.data = customers;
    });
  }

  deleteCustomer(customerId: number): void {
    this._customerService.deleteCustomer(customerId).subscribe(() => {
      this._customerService.getCustomers().subscribe((customers) => {
        this.customerDataSource.data = customers;
      });
    });
  }

  editCustomer(customer?: Customer): void {
    const dialogRef = this.dialog.open(CustomersDetailComponent, {
      width: '400px',
      data: customer,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        if (customer) {
          this._customerService.editCustomer(result).subscribe(() => {
            this._customerService.getCustomers().subscribe((customers) => {
              this.customerDataSource.data = customers;
            });
          });
        } else {
          this._customerService.createCustomer(result).subscribe(() => {
            this._customerService.getCustomers().subscribe((customers) => {
              this.customerDataSource.data = customers;
            });
          });
        }
      }
    });
  }

  editAddress(
    address?: Address | null,
    customerId?: number,
    viewOnly?: boolean
  ): void {
    const dialogRef = this.dialog.open(CustomersAddressComponent, {
      width: '400px',
      data: {
        address: address,
        customerId: customerId,
        viewOnly: viewOnly,
      },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        if (address) {
          this._customerService.editAddress(result).subscribe(() => {
            this._customerService.getCustomers().subscribe((customers) => {
              this.customerDataSource.data = customers;
            });
          });
        } else {
          this._customerService.createAddress(result).subscribe(() => {
            this._customerService.getCustomers().subscribe((customers) => {
              this.customerDataSource.data = customers;
            });
          });
        }
      }
    });
  }

  deleteAddress(addressId: number): void {
    this._customerService.deleteAddress(addressId).subscribe(() => {
      this._customerService.getCustomers().subscribe((customers) => {
        this.customerDataSource.data = customers;
      });
    });
  }
}
