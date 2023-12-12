export type Customer = {
  id: number;
  name: string;
  surname: string;
  email: string;
  phoneNumber: string;
  identityNumber: string;
  addresses: Address[];
};

export type Address = {
  id: number;
  addressName: string;
  addressType: string;
  addressLine1: string;
  addressLine2: string;
  city: string;
  postalCode: string;
  province: string;
  active: boolean;
  customerId: number;
  customer: string;
};
