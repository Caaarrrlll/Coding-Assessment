import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { CustomersListComponent } from 'src/app/customers/customers-list/customers-list.component';
import { CustomersDetailComponent } from './customers-detail/customers-detail.component';
import { CustomersRoutingModule } from './customers-routing.module';
import { CustomersAddressComponent } from './customers-address/customers-address.component';

@NgModule({
  declarations: [CustomersListComponent, CustomersDetailComponent, CustomersAddressComponent],
  imports: [
    CommonModule,
    CustomersRoutingModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatDialogModule,
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
  ],
})
export class CustomersModule {}
