import axios from 'axios';
import { toast } from 'react-toastify';
// Create a single Axios instance
const axiosInstance = axios.create();

// Request interceptor to dynamically set the baseURL
axiosInstance.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token'); // Fetch token from localStorage
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    // Dynamically override baseURL if baseURLOverride exists
    if (config.baseURLOverride) {
      config.baseURL = config.baseURLOverride;
      delete config.baseURLOverride; // Clean up to avoid unused property
    }

    return config;
  },
  (error) => Promise.reject(error)
);

axiosInstance.interceptors.response.use(
  (response) => response,
  (error) => {
    if (axios.isAxiosError(error)) {
      if (error.response) {
        // Extract status code and message
        const status = error.response.status;
        const errorMessage = error.response.data?.message || error.message;

        // Handle different status codes
        switch (status) {
          case 400:
            toast.error(`Bad Request: ${errorMessage}`);
            break;
          case 401:
            toast.error("Unauthorized: Please log in again!");
            break;
          case 403:
            toast.error("Forbidden: You don't have permission.");
            break;
          case 404:
            toast.error("Not Found: The requested resource was not found.");
            break;
          case 500:
            toast.error("Server Error: Something went wrong.");
            break;
          default:
            toast.error(`Error ${status}: ${errorMessage}`);
        }
      } else if (error.request) {
        // No response received
        toast.error("Network Error: Unable to reach server!");
      } else {
        // Any other errors
        toast.error("An unexpected error occurred!");
      }
    }
    
    return Promise.reject(error);
  }
);

export default axiosInstance;
