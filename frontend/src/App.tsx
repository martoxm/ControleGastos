// Raiz da aplicação: define o layout geral e as rotas do sistema
// BrowserRouter  → habilita a navegação por URL
// Routes + Route → mapeia cada URL para um componente de página

import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom"
import Navbar from "./components/Navbar"
import PessoasPage from "./pages/PessoasPage"
import TransacoesPage from "./pages/TransacoesPage"
import TotaisPage from "./pages/TotaisPage"

const App = () => {
  return (
    <BrowserRouter>
      {/* Navbar aparece em todas as páginas, fora das Routes */}
      <Navbar />

      <main className="main-content">
        <Routes>
          {/* Redireciona a raiz "/" para "/pessoas" como página inicial */}
          <Route path="/" element={<Navigate to="/pessoas" replace />} />
          <Route path="/pessoas" element={<PessoasPage />} />
          <Route path="/transacoes" element={<TransacoesPage />} />
          <Route path="/totais" element={<TotaisPage />} />
        </Routes>
      </main>
    </BrowserRouter>
  )
}

export default App
