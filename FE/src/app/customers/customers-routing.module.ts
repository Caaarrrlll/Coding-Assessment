import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersDetailComponent } from 'src/app/customers/customers-detail/customers-detail.component';
import { CustomersListComponent } from 'src/app/customers/customers-list/customers-list.component';

const routes: Routes = [
  {
    path: '',
    component: CustomersListComponent,
  },
  {
    path: ':id',
    component: CustomersDetailComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CustomersRoutingModule {}
