import { Product, Order, StationaryStore, Address, Invoice } from '@/types';

const API_BASE_URL =
  process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7000/api';

interface LoginRequest {
  Login: string;
  Password: string;
}

interface LoginResponse {
  access_token: string;
  expires_in: string;
}

class ApiClient {
  private baseUrl: string;
  private token: string | null = null;

  constructor(baseUrl: string) {
    this.baseUrl = baseUrl;
    // Load token from localStorage on initialization
    if (typeof window !== 'undefined') {
      this.token = localStorage.getItem('auth_token');
    }
  }

  setToken(token: string | null) {
    this.token = token;
    if (typeof window !== 'undefined') {
      if (token) {
        localStorage.setItem('auth_token', token);
      } else {
        localStorage.removeItem('auth_token');
      }
    }
  }

  getToken(): string | null {
    return this.token;
  }

  isAuthenticated(): boolean {
    return this.token !== null;
  }

  async login(credentials: LoginRequest): Promise<LoginResponse> {
    const formData = new FormData();
    formData.append('Login', credentials.Login);
    formData.append('Password', credentials.Password);

    const response = await fetch(`${this.baseUrl}/AccountApi/Login`, {
      method: 'POST',
      body: formData,
    });

    if (!response.ok) {
      throw new Error(`Login failed: ${response.status}`);
    }

    const loginResponse: LoginResponse = await response.json();
    this.setToken(loginResponse.access_token);
    return loginResponse;
  }

  logout() {
    this.setToken(null);
  }

  private async request<T>(
    endpoint: string,
    options: RequestInit = {},
  ): Promise<T> {
    const url = `${this.baseUrl}${endpoint}`;
    const headers: Record<string, string> = {
      'Content-Type': 'application/json',
      ...options.headers,
    };

    // Add Authorization header if token exists
    if (this.token) {
      headers['Authorization'] = `Bearer ${this.token}`;
    }

    const config: RequestInit = {
      headers,
      ...options,
    };

    const response = await fetch(url, config);

    if (!response.ok) {
      if (response.status === 401) {
        // Token expired or invalid, clear it
        this.logout();
        throw new Error('Authentication required');
      }
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

  async deleteProduct(id: number): Promise<boolean> {
    return this.request<boolean>(`/products/${id}`, {
      method: 'DELETE',
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

  async deleteOrder(id: number): Promise<boolean> {
    return this.request<boolean>(`/orders/${id}`, {
      method: 'DELETE',
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

  async deleteStore(id: number): Promise<boolean> {
    return this.request<boolean>(`/stores/${id}`, {
      method: 'DELETE',
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

  async deleteAddress(id: number): Promise<boolean> {
    return this.request<boolean>(`/addresses/${id}`, {
      method: 'DELETE',
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

  async deleteInvoice(id: number): Promise<boolean> {
    return this.request<boolean>(`/invoices/${id}`, {
      method: 'DELETE',
    });
  }
}

export const apiClient = new ApiClient(API_BASE_URL);
export type { LoginRequest, LoginResponse };
