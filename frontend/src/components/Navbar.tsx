// Componente de navegação principal da aplicação
// Usa NavLink do React Router para destacar automaticamente a rota ativa

import { NavLink } from "react-router-dom"

const Navbar = () => {
  return (
    <nav className="navbar">
      <div className="navbar-brand">
        {/* Nome do sistema — sem mencionar empresa conforme regras do desafio */}
        <span>💰 Controle de Gastos</span>
      </div>
      <ul className="navbar-links">
        <li>
          {/* NavLink adiciona a classe "active" automaticamente na rota atual */}
          <NavLink
            to="/pessoas"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Pessoas
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/transacoes"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Transações
          </NavLink>
        </li>
        <li>
          <NavLink
            to="/totais"
            className={({ isActive }) => (isActive ? "active" : "")}
          >
            Totais
          </NavLink>
        </li>
      </ul>
    </nav>
  )
}

export default Navbar
