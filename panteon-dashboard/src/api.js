import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:5100",
});

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
