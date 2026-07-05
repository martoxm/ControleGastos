// Instância central do Axios — toda comunicação com o backend passa por aqui
// Evita repetir baseURL em cada service (princípio DRY)

import axios from "axios"

export const api = axios.create({
  baseURL: "/api", // O proxy do vite.config.ts redireciona para https://localhost:7194
  headers: {
    "Content-Type": "application/json",
  },
})
