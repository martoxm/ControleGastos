// Página de consulta de totais financeiros
//
// Exibe por pessoa: total de receitas, total de despesas e saldo
// Exibe ao final: totais gerais consolidados de todas as pessoas
//
// Regra de negócio: saldo = totalReceitas - totalDespesas
// O cálculo é feito pelo backend — o frontend apenas exibe

import { useState, useEffect } from "react"
import { pessoaService } from "../services/pessoaService"
import type { RelatorioFinanceiroGeral } from "../types/Relatorio"

const TotaisPage = () => {
  // ── Estado ──────────────────────────────────────────────
  const [relatorio, setRelatorio] = useState<RelatorioFinanceiroGeral | null>(
    null,
  )
  const [carregando, setCarregando] = useState(true)
  const [erro, setErro] = useState("")

  // ── Carrega o relatório ao abrir a página ──────────────
  useEffect(() => {
    carregarTotais()
  }, [])

  const carregarTotais = async () => {
    try {
      setCarregando(true)
      const dados = await pessoaService.obterTotais()
      setRelatorio(dados)
    } catch {
      setErro("Erro ao carregar totais. Verifique se o backend está rodando.")
    } finally {
      setCarregando(false)
    }
  }

  // ── Helper: formata valor em Real brasileiro ───────────
  const formatarMoeda = (valor: number): string =>
    valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })

  // ── Helper: retorna a classe CSS correta para o saldo ──
  // Saldo positivo → verde | negativo → vermelho | zero → cinza
  const classeSaldo = (saldo: number): string => {
    if (saldo > 0) return "valor-positivo"
    if (saldo < 0) return "valor-negativo"
    return "valor-neutro"
  }

  // ── Renderização ───────────────────────────────────────
  return (
    <div>
      <div className="card">
        <h2>Consulta de Totais por Pessoa</h2>

        {/* Estado de carregamento */}
        {carregando && <p className="msg-vazio">Carregando...</p>}

        {/* Estado de erro */}
        {erro && <p className="msg-erro">{erro}</p>}

        {/* Tabela de totais — só exibe quando o relatório estiver carregado */}
        {!carregando && !erro && relatorio && (
          <>
            {relatorio.pessoas.length === 0 ? (
              <p className="msg-vazio">
                Nenhuma pessoa cadastrada. Cadastre pessoas e transações para
                ver os totais.
              </p>
            ) : (
              <table>
                <thead>
                  <tr>
                    <th>Nome</th>
                    <th>Idade</th>
                    <th>Total Receitas</th>
                    <th>Total Despesas</th>
                    <th>Saldo</th>
                  </tr>
                </thead>
                <tbody>
                  {/* ── Linhas por pessoa ── */}
                  {relatorio.pessoas.map((pessoa) => (
                    <tr key={pessoa.pessoaId}>
                      <td>{pessoa.nome}</td>
                      <td>{pessoa.idade} anos</td>
                      <td className="valor-positivo">
                        {formatarMoeda(pessoa.totalReceitas)}
                      </td>
                      <td className="valor-negativo">
                        {formatarMoeda(pessoa.totalDespesas)}
                      </td>
                      <td className={classeSaldo(pessoa.saldo)}>
                        {formatarMoeda(pessoa.saldo)}
                      </td>
                    </tr>
                  ))}

                  {/* ── Linha de total geral (destaque visual) ── */}
                  {/* Regra do desafio: exibir consolidado geral ao final da listagem */}
                  <tr className="row-total-geral">
                    <td colSpan={2}>TOTAL GERAL</td>
                    <td>{formatarMoeda(relatorio.totalGeralReceitas)}</td>
                    <td>{formatarMoeda(relatorio.totalGeralDespesas)}</td>
                    <td>{formatarMoeda(relatorio.saldoLiquidoGeral)}</td>
                  </tr>
                </tbody>
              </table>
            )}

            {/* ── Botão de atualizar ── */}
            <div style={{ marginTop: "1rem" }}>
              <button
                className="btn btn-primary"
                onClick={carregarTotais}
                disabled={carregando}
              >
                🔄 Atualizar
              </button>
            </div>
          </>
        )}
      </div>
    </div>
  )
}

export default TotaisPage
