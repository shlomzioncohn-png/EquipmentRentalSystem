export interface Item {
  id: string;

  description: string;

  amount: string;

  price: number;
  comments:string;

  isReturnable: boolean;
  name: string;
  businessId: string;

  businessName?: string;
  businessCity?: string;
}