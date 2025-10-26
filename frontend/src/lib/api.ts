import { Product, Order, StationaryStore, Address, Invoice } from '@/types';

const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7000/api';

class ApiClient {
  private baseUrl: string;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {},
  ): Promise<T> {
    const url = `${this.baseUrl}${endpoint}`;
    const config: RequestInit = {
      headers: {
        'Content-Type': 'application/json',
        ...options.headers,
      },
      ...options,
    };

    const response = await fetch(url, config);

    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response.json();
  }

  // Products
  async getProducts(): Promise<Product[]> {
    return this.request<Product[]>('/products');
  }

  async getProduct(id: number): Promise<Product> {
    return this.request<Product>(`/products/${id}`);
  }

  async createProduct(product: any): Promise<Product> {
    return this.request<Product>('/products', {
      method: 'POST',
      body: JSON.stringify(product),
    });
  }

  async updateProduct(id: number, product: any): Promise<Product> {
    return this.request<Product>(`/products/${id}`, {
      method: 'PUT',
      body: JSON.stringify(product),
    });
  }

  // Orders
  async getOrders(): Promise<Order[]> {
    return this.request<Order[]>('/orders');
  }

  async getOrder(id: number): Promise<Order> {
    return this.request<Order>(`/orders/${id}`);
  }

  async createOrder(order: any): Promise<Order> {
    return this.request<Order>('/orders', {
      method: 'POST',
      body: JSON.stringify(order),
    });
  }

  async updateOrder(id: number, order: any): Promise<Order> {
    return this.request<Order>(`/orders/${id}`, {
      method: 'PUT',
      body: JSON.stringify(order),
    });
  }

  // Stores
  async getStores(): Promise<StationaryStore[]> {
    return this.request<StationaryStore[]>('/stores');
  }

  async getStore(id: number): Promise<StationaryStore> {
    return this.request<StationaryStore>(`/stores/${id}`);
  }

  async createStore(store: any): Promise<StationaryStore> {
    return this.request<StationaryStore>('/stores', {
      method: 'POST',
      body: JSON.stringify(store),
    });
  }

  async updateStore(id: number, store: any): Promise<StationaryStore> {
    return this.request<StationaryStore>(`/stores/${id}`, {
      method: 'PUT',
      body: JSON.stringify(store),
    });
  }

  // Addresses
  async getAddresses(): Promise<Address[]> {
    return this.request<Address[]>('/addresses');
  }

  async getAddress(id: number): Promise<Address> {
    return this.request<Address>(`/addresses/${id}`);
  }

  async createAddress(address: any): Promise<Address> {
    return this.request<Address>('/addresses', {
      method: 'POST',
      body: JSON.stringify(address),
    });
  }

  async updateAddress(id: number, address: any): Promise<Address> {
    return this.request<Address>(`/addresses/${id}`, {
      method: 'PUT',
      body: JSON.stringify(address),
    });
  }

  // Invoices
  async getInvoices(): Promise<Invoice[]> {
    return this.request<Invoice[]>('/invoices');
  }

  async getInvoice(id: number): Promise<Invoice> {
    return this.request<Invoice>(`/invoices/${id}`);
  }

  async createInvoice(invoice: any): Promise<Invoice> {
    return this.request<Invoice>('/invoices', {
      method: 'POST',
      body: JSON.stringify(invoice),
    });
  }

  async updateInvoice(id: number, invoice: any): Promise<Invoice> {
    return this.request<Invoice>(`/invoices/${id}`, {
      method: 'PUT',
      body: JSON.stringify(invoice),
    });
  }
}

export const apiClient = new ApiClient(API_BASE_URL);
