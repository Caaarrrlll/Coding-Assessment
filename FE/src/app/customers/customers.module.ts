import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { CustomersListComponent } from 'src/app/customers/customers-list/customers-list.component';
import { CustomersDetailComponent } from './customers-detail/customers-detail.component';
import { CustomersRoutingModule } from './customers-routing.module';

@NgModule({
  declarations: [CustomersListComponent, CustomersDetailComponent],
  imports: [
    CommonModule,
    CustomersRoutingModule,
    MatTableModule,
    MatButtonModule,
  ],
})
export class CustomersModule {}
