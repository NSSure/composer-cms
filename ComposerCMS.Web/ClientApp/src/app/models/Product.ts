export default class Product {
  id: string;
  title: string;
  description: string;
  hasVariants: boolean;
  price?: number;
  cost?: number;
  vendor: string;
  productTypeId?: string;
  sku: string;
  trackQuantity: boolean;
  isPhysical: boolean;
  allowOutOfStockPurchases: boolean;
  isPublished: boolean;
  quantity?: number;
  dateAdded: Date;
  dateLastUpdated: Date;
  userIdAdded: string;
  userIdLastUpdated: string;
}
