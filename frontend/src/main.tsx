// Ponto de entrada da aplicação React
// O ReactDOM.createRoot "monta" o React dentro do <div id="root"> do index.html

import { StrictMode } from "react"
import { createRoot } from "react-dom/client"
import "./index.css"
import App from "./App.tsx"

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
