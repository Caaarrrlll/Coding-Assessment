import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { CustomersListComponent } from 'src/app/customers/customers-list/customers-list.component';
import { CustomersRoutingModule } from './customers-routing.module';
import { CustomersDetailComponent } from './customers-detail/customers-detail.component';

@NgModule({
  declarations: [CustomersListComponent, CustomersDetailComponent],
  imports: [CommonModule, CustomersRoutingModule],
})
export class CustomersModule {}
