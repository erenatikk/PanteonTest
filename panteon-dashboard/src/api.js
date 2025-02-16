import axios from "axios";


const token = localStorage.getItem('token'); 

const api = axios.create({
  baseURL: "https://3.120.205.237:5100",
});

api.interceptors.request.use(
  config => {
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

export const login = (credentials) => {
  return api.post("/identity/login", credentials);
};

export const register = (user) => {
  return api.post("/identity/CreateUser", user);
};

export const getConfigurations = async () => await api.get("/config/GetConfig");

export const addConfiguration = async (configuration) =>
  await api.post("/config/CreateConfig", configuration);

export const updateConfiguration = async (configuration) =>
  await api.patch("/config/UpdateConfig", configuration);

export default api;
