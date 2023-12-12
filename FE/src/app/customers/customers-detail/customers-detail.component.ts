import { AfterContentInit, Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Customer } from 'src/app/apis/customer/models/customer.type';

@Component({
  selector: 'app-customers-detail',
  templateUrl: './customers-detail.component.html',
  styleUrls: ['./customers-detail.component.scss'],
})
export class CustomersDetailComponent implements AfterContentInit {
  customerForm: FormGroup = new FormGroup({
    id: new FormControl(null),
    name: new FormControl('', Validators.required),
    surname: new FormControl('', Validators.required),
    phoneNumber: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(/^[0-9]{10}/),
      ])
    ),
    email: new FormControl(
      '',
      Validators.compose([Validators.required, Validators.email])
    ),
    identityNumber: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(/^[0-9]{13}/),
      ])
    ),
    // addressId: new FormControl({}),
  });

  constructor(
    public dialogRef: MatDialogRef<CustomersDetailComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Customer
  ) {}

  ngAfterContentInit(): void {
    if (this.data) {
      this.customerForm.setValue({
        id: this.data.id,
        name: this.data.name,
        surname: this.data.surname,
        phoneNumber: this.data.phoneNumber,
        email: this.data.email,
        identityNumber: this.data.identityNumber,
        // addressId: this.data.addresses[0].id,
      });
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  dialogSuccessClose() {
    this.dialogRef.close(this.customerForm.value);
  }
}
