import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Address } from 'src/app/apis/customer/models/customer.type';

@Component({
  selector: 'app-customers-address',
  templateUrl: './customers-address.component.html',
  styleUrls: ['./customers-address.component.scss'],
})
export class CustomersAddressComponent {
  viewOnly: boolean = false;

  addressForm: FormGroup = new FormGroup({
    id: new FormControl(null),
    addressName: new FormControl('', Validators.required),
    addressLine1: new FormControl('', Validators.required),
    addressLine2: new FormControl(''),
    city: new FormControl('', Validators.required),
    postalCode: new FormControl(
      '',
      Validators.compose([
        Validators.required,
        Validators.pattern(/^[0-9]{4,5}/),
      ])
    ),
    province: new FormControl('', Validators.required),
    active: new FormControl(true),
    customerId: new FormControl(null),
  });

  constructor(
    public dialogRef: MatDialogRef<CustomersAddressComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: {
      address: Address;
      customerId?: number;
      viewOnly?: boolean;
    }
  ) {}

  ngAfterContentInit(): void {
    this.viewOnly = this.data.viewOnly ? this.data.viewOnly : false;
    if (this.data.address) {
      this.addressForm.setValue({
        id: this.data.address.id,
        addressName: this.data.address.addressName,
        addressLine1: this.data.address.addressLine1,
        addressLine2: this.data.address.addressLine2,
        city: this.data.address.city,
        postalCode: this.data.address.postalCode,
        province: this.data.address.province,
        active: this.data.address.active,
        customerId: this.data.address.customerId,
      });
    }

    this.addressForm.controls['customerId'].setValue(this.data.customerId);

    Object.keys(this.addressForm.controls).map((control) => {
      if (this.viewOnly) this.addressForm.controls[control].disable();
      this.addressForm.controls[control].markAsTouched();
    });
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  dialogSuccessClose() {
    this.dialogRef.close(this.addressForm.value);
  }
}
