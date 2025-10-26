// Product types
export interface Product {
  id: number;
  name: string;
  description?: string;
  price: number;
  stockQuantity: number;
  category?: string;
  createdAt: string;
  updatedAt: string;
}

export interface AddOrUpdateProduct {
  name: string;
  description?: string;
  price: number;
  stockQuantity: number;
  category?: string;
}

// Order types
export interface Order {
  id: number;
  orderDate: string;
  totalAmount: number;
  status: string;
  customerName?: string;
  customerEmail?: string;
  createdAt: string;
  updatedAt: string;
}

export interface AddOrUpdateOrder {
  orderDate: string;
  totalAmount: number;
  status: string;
  customerName?: string;
  customerEmail?: string;
}

// Store types
export interface StationaryStore {
  id: number;
  name: string;
  address: string;
  phoneNumber?: string;
  email?: string;
  createdAt: string;
  updatedAt: string;
}

export interface AddOrUpdateStationaryStore {
  name: string;
  address: string;
  phoneNumber?: string;
  email?: string;
}

// Address types
export interface Address {
  id: number;
  street: string;
  city: string;
  postalCode: string;
  country: string;
  createdAt: string;
  updatedAt: string;
}

export interface AddOrUpdateAddress {
  street: string;
  city: string;
  postalCode: string;
  country: string;
}

// Invoice types
export interface Invoice {
  id: number;
  invoiceNumber: string;
  issueDate: string;
  dueDate: string;
  totalAmount: number;
  status: string;
  createdAt: string;
  updatedAt: string;
}

export interface AddOrUpdateInvoice {
  invoiceNumber: string;
  issueDate: string;
  dueDate: string;
  totalAmount: number;
  status: string;
}
