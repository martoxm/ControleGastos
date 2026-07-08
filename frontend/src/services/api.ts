// Instância central do Axios — toda comunicação com o backend passa por aqui

import axios from "axios"

export const api = axios.create({
  baseURL: "/api", // O proxy do vite.config.ts redireciona para https://localhost:5176
  headers: {
    "Content-Type": "application/json",
  },
})
