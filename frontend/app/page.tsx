'use client';

import Link from 'next/link';
import { useState } from 'react';
import LoginForm from '@/components/LoginForm';
import { apiClient } from '@/lib/api';

export default function Home() {
  const [isAuthenticated, setIsAuthenticated] = useState(() =>
    apiClient.isAuthenticated(),
  );

  const handleLogin = () => {
    setIsAuthenticated(apiClient.isAuthenticated());
  };
  return (
    <div className='py-12'>
      <div className='max-w-7xl mx-auto px-4 sm:px-6 lg:px-8'>
        <div className='text-center'>
          <h1 className='text-4xl font-bold text-gray-900 sm:text-5xl md:text-6xl'>
            Welcome to WebStore
          </h1>
          <p className='mt-3 max-w-md mx-auto text-base text-gray-500 sm:text-lg md:mt-5 md:text-xl md:max-w-3xl'>
            A modern web application for managing your stationary store
            operations. Manage products, orders, stores, addresses, and invoices
            all in one place.
          </p>
        </div>

        <div className='mt-10'>
          <div className='mb-8'>
            <LoginForm onLogin={handleLogin} />
          </div>

          {isAuthenticated && (
            <div className='grid grid-cols-1 gap-8 sm:grid-cols-2 lg:grid-cols-3'>
              <div className='bg-white overflow-hidden shadow rounded-lg'>
                <div className='p-5'>
                  <div className='flex items-center'>
                    <div className='flex-shrink-0'>
                      <div className='w-8 h-8 bg-blue-500 rounded-md flex items-center justify-center'>
                        <span className='text-white font-bold'>P</span>
                      </div>
                    </div>
                    <div className='ml-5 w-0 flex-1'>
                      <dl>
                        <dt className='text-sm font-medium text-gray-500 truncate'>
                          Products
                        </dt>
                        <dd className='text-lg font-medium text-gray-900'>
                          Manage inventory
                        </dd>
                      </dl>
                    </div>
                  </div>
                  <div className='mt-4'>
                    <Link
                      href='/products'
                      className='text-blue-600 hover:text-blue-500 font-medium'
                    >
                      View Products →
                    </Link>
                  </div>
                </div>
              </div>

              <div className='bg-white overflow-hidden shadow rounded-lg'>
                <div className='p-5'>
                  <div className='flex items-center'>
                    <div className='flex-shrink-0'>
                      <div className='w-8 h-8 bg-green-500 rounded-md flex items-center justify-center'>
                        <span className='text-white font-bold'>O</span>
                      </div>
                    </div>
                    <div className='ml-5 w-0 flex-1'>
                      <dl>
                        <dt className='text-sm font-medium text-gray-500 truncate'>
                          Orders
                        </dt>
                        <dd className='text-lg font-medium text-gray-900'>
                          Track sales
                        </dd>
                      </dl>
                    </div>
                  </div>
                  <div className='mt-4'>
                    <Link
                      href='/orders'
                      className='text-green-600 hover:text-green-500 font-medium'
                    >
                      View Orders →
                    </Link>
                  </div>
                </div>
              </div>

              <div className='bg-white overflow-hidden shadow rounded-lg'>
                <div className='p-5'>
                  <div className='flex items-center'>
                    <div className='flex-shrink-0'>
                      <div className='w-8 h-8 bg-purple-500 rounded-md flex items-center justify-center'>
                        <span className='text-white font-bold'>S</span>
                      </div>
                    </div>
                    <div className='ml-5 w-0 flex-1'>
                      <dl>
                        <dt className='text-sm font-medium text-gray-500 truncate'>
                          Stores
                        </dt>
                        <dd className='text-lg font-medium text-gray-900'>
                          Manage locations
                        </dd>
                      </dl>
                    </div>
                  </div>
                  <div className='mt-4'>
                    <Link
                      href='/stores'
                      className='text-purple-600 hover:text-purple-500 font-medium'
                    >
                      View Stores →
                    </Link>
                  </div>
                </div>
              </div>
            </div>
          )}

          {!isAuthenticated && (
            <div className='text-center py-8'>
              <p className='text-gray-600'>
                Please log in to access the WebStore features.
              </p>
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
