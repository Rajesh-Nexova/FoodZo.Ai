import { useState, useCallback } from "react";
import axiosInstance from "../utils/AxiosConfig";

const useApi = (endpoint, method = "GET") => {
  const [loading, setLoading] = useState(false);
  const [data, setData] = useState(null);
  const [error, setError] = useState(null);

  const callApi = useCallback(
    async ({
      params = {},
      data: requestData = {},
      baseURLOverride = "",
      responseType = "json", // Default to JSON response
    } = {}) => {
      setLoading(true);
      setError(null);

      try {
        const response = await axiosInstance({
          url: endpoint,
          method: method.toUpperCase(),
          params, // Query parameters for GET requests
          data: requestData, // Body for POST/PUT requests
          baseURL: baseURLOverride || undefined, // Override baseURL dynamically if provided
          responseType, // Support binary data handling
        });

        setData(response);
        return response; // Return response to allow further processing
      } catch (err) {
        const errorMessage = err;
        setError(errorMessage);
        throw err;
      } finally {
        setLoading(false);
      }
    },
    [endpoint, method]
  );

  return { callApi, loading, data, error };
};

export default useApi;
