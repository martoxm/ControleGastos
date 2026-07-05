// vite.config.ts
// Configura o servidor de desenvolvimento do Vite
// O proxy redireciona chamadas de /api para o backend .NET, evitando erro de CORS

import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      // Toda requisição que começar com /api vai para o backend
      '/api': {
        target: 'https://localhost:7194',
        changeOrigin: true,
        secure: false, // necessário para aceitar o certificado local do .NET
      },
    },
  },
})