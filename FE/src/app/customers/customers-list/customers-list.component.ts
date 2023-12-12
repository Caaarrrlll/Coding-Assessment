import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { CustomerService } from 'src/app/apis/customer/customer.service';
import { Customer } from 'src/app/apis/customer/models/customer.type';
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

  deleteCustomer(customerId: number) {
    this._customerService.deleteCustomer(customerId).subscribe(() => {
      this._customerService.getCustomers().subscribe((customers) => {
        this.customerDataSource.data = customers;
      });
    });
  }

  editCustomer(customer?: Customer) {
    const dialogRef = this.dialog.open(CustomersDetailComponent, {
      width: '250px',
      data: customer,
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log('The dialog was closed');

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
    });
  }
}
