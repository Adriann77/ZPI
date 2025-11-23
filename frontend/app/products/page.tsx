'use client';

import { useState, useEffect } from 'react';
import { Product, AddOrUpdateProduct } from '@/types';
import { apiClient } from '@/lib/api';

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [showAddForm, setShowAddForm] = useState(false);
  const [newProduct, setNewProduct] = useState<AddOrUpdateProduct>({
    name: '',
    description: '',
    price: 0,
    weight: 0,
    categoryId: 1,
    supplierId: 1,
  });

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await apiClient.getProducts();
        setProducts(data);
      } catch (err) {
        setError('Failed to fetch products');
        console.error('Error fetching products:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const handleAddProduct = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      setError(null);
      await apiClient.createProduct(newProduct);
      // Refresh products list
      const data = await apiClient.getProducts();
      setProducts(data);
      // Reset form
      setNewProduct({
        name: '',
        description: '',
        price: 0,
        weight: 0,
        categoryId: 1,
        supplierId: 1,
      });
      setShowAddForm(false);
    } catch (err) {
      setError('Failed to add product');
      console.error('Error adding product:', err);
    }
  };

  if (loading) {
    return (
      <div className='py-12'>
        <div className='max-w-7xl mx-auto px-4 sm:px-6 lg:px-8'>
          <div className='text-center'>
            <div className='animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600 mx-auto'></div>
            <p className='mt-4 text-gray-600'>Loading products...</p>
          </div>
        </div>
      </div>
    );
  }

  if (error && !products.length) {
    return (
      <div className='py-12'>
        <div className='max-w-7xl mx-auto px-4 sm:px-6 lg:px-8'>
          <div className='text-center'>
            <div className='bg-red-50 border border-red-200 rounded-md p-4'>
              <p className='text-red-600'>{error}</p>
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className='py-12'>
      <div className='max-w-7xl mx-auto px-4 sm:px-6 lg:px-8'>
        {error && (
          <div className='mb-4 bg-red-50 border border-red-200 rounded-md p-4'>
            <p className='text-red-600'>{error}</p>
          </div>
        )}
        <div className='sm:flex sm:items-center'>
          <div className='sm:flex-auto'>
            <h1 className='text-2xl font-semibold text-gray-900'>Products</h1>
            <p className='mt-2 text-sm text-gray-700'>
              A list of all products in your inventory.
            </p>
          </div>
          <div className='mt-4 sm:mt-0 sm:ml-16 sm:flex-none'>
            <button
              type='button'
              onClick={() => setShowAddForm(!showAddForm)}
              className='block rounded-md bg-blue-600 py-2 px-3 text-center text-sm font-semibold text-white shadow-sm hover:bg-blue-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600'
            >
              {showAddForm ? 'Cancel' : 'Add Product'}
            </button>
          </div>
        </div>

        {showAddForm && (
          <div className='mt-8 bg-white shadow sm:rounded-lg'>
            <div className='px-4 py-5 sm:p-6'>
              <h3 className='text-lg leading-6 font-medium text-gray-900 mb-4'>
                Add New Product
              </h3>
              <form onSubmit={handleAddProduct} className='space-y-4'>
                <div>
                  <label
                    htmlFor='name'
                    className='block text-sm font-medium text-gray-700'
                  >
                    Name
                  </label>
                  <input
                    type='text'
                    id='name'
                    required
                    value={newProduct.name}
                    onChange={(e) =>
                      setNewProduct({ ...newProduct, name: e.target.value })
                    }
                    className='mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
                  />
                </div>
                <div>
                  <label
                    htmlFor='price'
                    className='block text-sm font-medium text-gray-700'
                  >
                    Price ($)
                  </label>
                  <input
                    type='number'
                    step='0.01'
                    id='price'
                    required
                    value={newProduct.price}
                    onChange={(e) =>
                      setNewProduct({
                        ...newProduct,
                        price: parseFloat(e.target.value),
                      })
                    }
                    className='mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
                  />
                </div>
                <div>
                  <label
                    htmlFor='description'
                    className='block text-sm font-medium text-gray-700'
                  >
                    Description
                  </label>
                  <textarea
                    id='description'
                    rows={3}
                    required
                    value={newProduct.description}
                    onChange={(e) =>
                      setNewProduct({
                        ...newProduct,
                        description: e.target.value,
                      })
                    }
                    className='mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
                  />
                </div>
                <div className='grid grid-cols-1 gap-4 sm:grid-cols-3'>
                  <div>
                    <label
                      htmlFor='weight'
                      className='block text-sm font-medium text-gray-700'
                    >
                      Weight (kg)
                    </label>
                    <input
                      type='number'
                      step='0.01'
                      id='weight'
                      required
                      value={newProduct.weight}
                      onChange={(e) =>
                        setNewProduct({
                          ...newProduct,
                          weight: parseFloat(e.target.value),
                        })
                      }
                      className='mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
                    />
                  </div>
                  <div>
                    <label
                      htmlFor='categoryId'
                      className='block text-sm font-medium text-gray-700'
                    >
                      Category ID
                    </label>
                    <input
                      type='number'
                      id='categoryId'
                      required
                      value={newProduct.categoryId}
                      onChange={(e) =>
                        setNewProduct({
                          ...newProduct,
                          categoryId: parseInt(e.target.value),
                        })
                      }
                      className='mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
                    />
                  </div>
                  <div>
                    <label
                      htmlFor='supplierId'
                      className='block text-sm font-medium text-gray-700'
                    >
                      Supplier ID
                    </label>
                    <input
                      type='number'
                      id='supplierId'
                      required
                      value={newProduct.supplierId}
                      onChange={(e) =>
                        setNewProduct({
                          ...newProduct,
                          supplierId: parseInt(e.target.value),
                        })
                      }
                      className='mt-1 block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
                    />
                  </div>
                </div>
                <div className='flex justify-end'>
                  <button
                    type='submit'
                    className='inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500'
                  >
                    Add Product
                  </button>
                </div>
              </form>
            </div>
          </div>
        )}

        {products.length === 0 ? (
          <div className='text-center py-12'>
            <div className='text-gray-500'>
              <p className='text-lg'>No products found</p>
              <p className='text-sm'>
                Get started by adding your first product.
              </p>
            </div>
          </div>
        ) : (
          <div className='mt-8 flow-root'>
            <div className='-my-2 -mx-4 overflow-x-auto sm:-mx-6 lg:-mx-8'>
              <div className='inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8'>
                <table className='min-w-full divide-y divide-gray-300'>
                  <thead>
                    <tr>
                      <th
                        scope='col'
                        className='py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-gray-900 sm:pl-0'
                      >
                        Name
                      </th>
                      <th
                        scope='col'
                        className='px-3 py-3.5 text-left text-sm font-semibold text-gray-900'
                      >
                        Price
                      </th>
                      <th
                        scope='col'
                        className='px-3 py-3.5 text-left text-sm font-semibold text-gray-900'
                      >
                        Stock
                      </th>
                      <th
                        scope='col'
                        className='px-3 py-3.5 text-left text-sm font-semibold text-gray-900'
                      >
                        Category
                      </th>
                      <th
                        scope='col'
                        className='relative py-3.5 pl-3 pr-4 sm:pr-0'
                      >
                        <span className='sr-only'>Actions</span>
                      </th>
                    </tr>
                  </thead>
                  <tbody className='divide-y divide-gray-200'>
                    {products.map((product, index) => (
                      <tr key={`product-${product.id || index}`}>
                        <td className='whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-0'>
                          {product.name}
                        </td>
                        <td className='whitespace-nowrap px-3 py-4 text-sm text-gray-500'>
                          ${product.price.toFixed(2)}
                        </td>
                        <td className='whitespace-nowrap px-3 py-4 text-sm text-gray-500'>
                          {product.stockQuantity}
                        </td>
                        <td className='whitespace-nowrap px-3 py-4 text-sm text-gray-500'>
                          {product.category || 'N/A'}
                        </td>
                        <td className='relative whitespace-nowrap py-4 pl-3 pr-4 text-right text-sm font-medium sm:pr-0'>
                          <button className='text-blue-600 hover:text-blue-500 mr-4'>
                            Edit
                          </button>
                          <button className='text-red-600 hover:text-red-500'>
                            Delete
                          </button>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
