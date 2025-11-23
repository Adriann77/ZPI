'use client';

import { useState } from 'react';
import { apiClient, LoginRequest } from '@/lib/api';

interface LoginFormProps {
  onLogin: () => void;
}

export default function LoginForm({ onLogin }: LoginFormProps) {
  const [credentials, setCredentials] = useState<LoginRequest>({
    Login: '',
    Password: '',
  });
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    setError(null);

    try {
      await apiClient.login(credentials);
      onLogin();
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Login failed');
    } finally {
      setIsLoading(false);
    }
  };

  const handleLogout = () => {
    apiClient.logout();
    onLogin();
  };

  if (apiClient.isAuthenticated()) {
    return (
      <div className='bg-green-50 border border-green-200 rounded-md p-4'>
        <div className='flex items-center justify-between'>
          <div className='flex items-center'>
            <div className='shrink-0'>
              <svg
                className='h-5 w-5 text-green-400'
                viewBox='0 0 20 20'
                fill='currentColor'
              >
                <path
                  fillRule='evenodd'
                  d='M10 18a8 8 0 100-16 8 8 0 000 16zm3.707-9.293a1 1 0 00-1.414-1.414L9 10.586 7.707 9.293a1 1 0 00-1.414 1.414l2 2a1 1 0 001.414 0l4-4z'
                  clipRule='evenodd'
                />
              </svg>
            </div>
            <div className='ml-3'>
              <p className='text-sm font-medium text-green-800'>
                You are logged in
              </p>
            </div>
          </div>
          <button
            onClick={handleLogout}
            className='text-sm bg-green-100 text-green-800 px-3 py-1 rounded hover:bg-green-200'
          >
            Logout
          </button>
        </div>
      </div>
    );
  }

  return (
    <div className=' shadow sm:rounded-lg'>
      <div className='px-4 py-5 sm:p-6'>
        <h3 className='text-lg leading-6 font-medium text-gray-900 mb-4'>
          Login to WebStore
        </h3>
        <form
          onSubmit={handleSubmit}
          className='space-y-4'
        >
          <div>
            <label
              htmlFor='login'
              className='block text-sm font-medium text-gray-700'
            >
              Email
            </label>
            <input
              type='email'
              id='login'
              value={credentials.Login}
              onChange={(e) =>
                setCredentials({ ...credentials, Login: e.target.value })
              }
              className='mt-1 block w-full border-gray-300 rounded-md shadow-sm text-black focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
              required
            />
          </div>
          <div>
            <label
              htmlFor='password'
              className='block text-sm font-medium text-gray-700'
            >
              Password
            </label>
            <input
              type='password'
              id='password'
              value={credentials.Password}
              onChange={(e) =>
                setCredentials({ ...credentials, Password: e.target.value })
              }
              className='mt-1 block w-full border-gray-300 rounded-md shadow-sm text-black focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border'
              required
            />
          </div>
          {error && <div className='text-red-600 text-sm'>{error}</div>}
          <button
            type='submit'
            disabled={isLoading}
            className='w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50'
          >
            {isLoading ? 'Logging in...' : 'Login'}
          </button>
        </form>
        <div className='mt-4 text-sm text-gray-600'>
          <p>
            Note: This is a demo application. You can use any email and password
            to test the login functionality.
          </p>
        </div>
      </div>
    </div>
  );
}
