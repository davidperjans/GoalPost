import axios from 'axios';

const API_URL = 'http://localhost:5132/api';

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface RegisterData {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface User {
  id: string;
  username: string;
  email: string;
  profileImage?: string;
  token?: string;
}

export interface AuthResponse {
  token: string;
  user: User;
  error?: string;
}

export const authApi = {
  login: async (credentials: LoginCredentials): Promise<AuthResponse> => {
    const response = await axios.post(`${API_URL}/auth/login`, credentials);
    const { token, user, error } = response.data;
    
    if (error) {
      throw new Error(error);
    }
    
    // Spara token i localStorage
    localStorage.setItem('token', token);
    
    // Sätt token i axios headers för framtida requests
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    
    return { token, user: { ...user, token } };
  },

  register: async (data: RegisterData): Promise<AuthResponse> => {
    const response = await axios.post(`${API_URL}/auth/register`, {
      username: data.username,
      email: data.email,
      password: data.password,
      confirmPassword: data.confirmPassword
    });
    const { token, user, error } = response.data;
    
    if (error) {
      throw new Error(error);
    }
    
    // Spara token i localStorage
    localStorage.setItem('token', token);
    
    // Sätt token i axios headers för framtida requests
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    
    return { token, user: { ...user, token } };
  },

  logout: async (): Promise<void> => {
    localStorage.removeItem('token');
    delete axios.defaults.headers.common['Authorization'];
  },

  getCurrentUser: async (): Promise<User | null> => {
    try {
      const response = await axios.get(`${API_URL}/auth/me`);
      return response.data;
    } catch (error) {
      return null;
    }
  }
}; 