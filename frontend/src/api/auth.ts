import axios from 'axios';

const API_URL = 'http://localhost:5132/api';

export interface LoginCredentials {
  email: string;
  password: string;
}

export interface RegisterData {
  username: string;
  email: string;
  firstName: string;
  lastName: string;
  password: string;
  confirmPassword: string;
}

export interface User {
  id: string;
  username: string;
  email: string;
  firstName: string;
  lastName: string;
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
    
    console.log('Login response:', response.data);
    
    if (error) {
      throw new Error(error);
    }
    
    // Spara token i localStorage
    localStorage.setItem('token', token);
    
    // Sätt token i axios headers för framtida requests
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    
    // Mappa backend userName/UserName till frontend username
    const mappedUser = { 
      ...user, 
      username: user.userName || user.UserName, // Hantera både userName och UserName
      token 
    };
    
    console.log('Mapped user:', mappedUser);
    
    return { 
      token, 
      user: mappedUser
    };
  },

  register: async (data: RegisterData): Promise<AuthResponse> => {
    const response = await axios.post(`${API_URL}/auth/register`, {
      username: data.username,
      email: data.email,
      firstName: data.firstName,
      lastName: data.lastName,
      password: data.password,
      confirmPassword: data.confirmPassword
    });
    const { token, user, error } = response.data;
    
    console.log('Register response:', response.data);
    
    if (error) {
      throw new Error(error);
    }
    
    // Spara token i localStorage
    localStorage.setItem('token', token);
    
    // Sätt token i axios headers för framtida requests
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    
    // Mappa backend userName/UserName till frontend username
    const mappedUser = { 
      ...user, 
      username: user.userName || user.UserName, // Hantera både userName och UserName
      token 
    };
    
    console.log('Mapped user:', mappedUser);
    
    return { 
      token, 
      user: mappedUser
    };
  },

  logout: async (): Promise<void> => {
    localStorage.removeItem('token');
    delete axios.defaults.headers.common['Authorization'];
  },

  getCurrentUser: async (): Promise<User | null> => {
    try {
      const response = await axios.get(`${API_URL}/auth/me`);
      console.log('Get current user response:', response.data);
      
      const user = response.data;
      const mappedUser = {
        ...user,
        username: user.userName || user.UserName // Hantera både userName och UserName
      };
      
      console.log('Mapped current user:', mappedUser);
      return mappedUser;
    } catch (error) {
      console.error('Get current user error:', error);
      return null;
    }
  }
}; 