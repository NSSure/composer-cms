export default class Product {
  id: string;
  title: string;
  description: string;
  sku: string;
  price: number;
  cost: number;
  trackQuantity: boolean;
  quantity: number;
  isPhysical: boolean;
  hasVariants: boolean;
  allowOutOfStockPurchases: boolean;
  isPublished: boolean;
  dateAdded: Date;
  dateLastUpdated: Date;
  userIdAdded: string;
  userIdLastUpdated: string;
}
