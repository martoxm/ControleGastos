// Página de cadastro e listagem de transações
//
// Regra de negócio crítica do desafio:
// → Pessoas menores de 18 anos só podem ter transações do tipo "Despesa"
// → O frontend bloqueia a opção "Receita" e o backend também valida

import { useState, useEffect } from "react"
import { transacaoService } from "../services/transacaoService"
import { pessoaService } from "../services/pessoaService"
import type {
  TransacaoResponse,
  CriarTransacaoRequest,
} from "../types/Transacao"
import type { PessoaResponse } from "../types/Pessoa"
import { TIPO_TRANSACAO } from "../types/Transacao"

const TransacoesPage = () => {
  // ── Estado da listagem ──────────────────────────────────
  const [transacoes, setTransacoes] = useState<TransacaoResponse[]>([])
  const [pessoas, setPessoas] = useState<PessoaResponse[]>([])
  const [carregando, setCarregando] = useState(true)

  // ── Estado do formulário ────────────────────────────────
  const [descricao, setDescricao] = useState("")
  const [valor, setValor] = useState("")
  const [tipo, setTipo] = useState<0 | 1>(TIPO_TRANSACAO.DESPESA)
  const [pessoaId, setPessoaId] = useState("")

  // ── Estado de feedback ──────────────────────────────────
  const [enviando, setEnviando] = useState(false)
  const [erro, setErro] = useState("")
  const [sucesso, setSucesso] = useState("")

  // ── Carrega listas ao abrir a página ───────────────────
  useEffect(() => {
    carregarDados()
  }, [])

  const carregarDados = async () => {
    try {
      setCarregando(true)
      // Busca as duas listas em paralelo (mais eficiente que em sequência)
      const [dadosTransacoes, dadosPessoas] = await Promise.all([
        transacaoService.listar(),
        pessoaService.listar(),
      ])
      setTransacoes(dadosTransacoes)
      setPessoas(dadosPessoas)
    } catch {
      setErro("Erro ao carregar dados. Verifique se o backend está rodando.")
    } finally {
      setCarregando(false)
    }
  }

  // ── Regra de negócio: verifica se a pessoa selecionada é menor de idade ──
  const pessoaSelecionada = pessoas.find((p) => p.id === pessoaId)
  const eMenorDeIdade =
    pessoaSelecionada !== undefined && pessoaSelecionada.idade < 18

  // Quando selecionar um menor de idade, força o tipo para Despesa automaticamente
  const handleSelecionarPessoa = (id: string) => {
    setPessoaId(id)
    const pessoa = pessoas.find((p) => p.id === id)
    if (pessoa && pessoa.idade < 18) {
      setTipo(TIPO_TRANSACAO.DESPESA)
    }
  }

  // ── Submissão do formulário ─────────────────────────────
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setErro("")
    setSucesso("")

    // Validações no frontend
    if (!descricao.trim()) {
      setErro("A descrição é obrigatória.")
      return
    }
    const valorNum = Number(valor)
    if (!valor || isNaN(valorNum) || valorNum <= 0) {
      setErro("O valor deve ser maior que zero.")
      return
    }
    if (!pessoaId) {
      setErro("Selecione uma pessoa.")
      return
    }

    const dados: CriarTransacaoRequest = {
      descricao: descricao.trim(),
      valor: valorNum,
      tipo,
      pessoaId,
    }

    try {
      setEnviando(true)
      await transacaoService.criar(dados)
      setSucesso("Transação cadastrada com sucesso!")
      // Limpa o formulário
      setDescricao("")
      setValor("")
      setTipo(TIPO_TRANSACAO.DESPESA)
      setPessoaId("")
      // Recarrega a lista de transações
      const novasTransacoes = await transacaoService.listar()
      setTransacoes(novasTransacoes)
    } catch (err: unknown) {
      const msg = (err as { response?: { data?: { message?: string } } })
        ?.response?.data?.message
      setErro(msg ?? "Erro ao cadastrar transação. Tente novamente.")
    } finally {
      setEnviando(false)
    }
  }

  // ── Helpers de exibição ─────────────────────────────────

  // Busca o nome da pessoa pelo id para exibir na tabela
  const getNomePessoa = (id: string): string => {
    return pessoas.find((p) => p.id === id)?.nome ?? "Desconhecida"
  }

  // Formata valor monetário em Real brasileiro
  const formatarMoeda = (valor: number): string =>
    valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" })

  // ── Renderização ───────────────────────────────────────
  return (
    <div>
      {/* ── Formulário de cadastro ── */}
      <div className="card">
        <h2>Cadastrar Transação</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="pessoaId">Pessoa</label>
            <select
              id="pessoaId"
              value={pessoaId}
              onChange={(e) => handleSelecionarPessoa(e.target.value)}
            >
              <option value="">Selecione uma pessoa...</option>
              {pessoas.map((pessoa) => (
                <option key={pessoa.id} value={pessoa.id}>
                  {pessoa.nome} ({pessoa.idade} anos
                  {pessoa.idade < 18 ? " — menor de idade" : ""})
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="descricao">Descrição</label>
            <input
              id="descricao"
              type="text"
              placeholder="Ex: Conta de luz, Salário..."
              value={descricao}
              onChange={(e) => setDescricao(e.target.value)}
              maxLength={200}
            />
          </div>

          <div className="form-group">
            <label htmlFor="valor">Valor (R$)</label>
            <input
              id="valor"
              type="number"
              placeholder="0,00"
              value={valor}
              onChange={(e) => setValor(e.target.value)}
              min="0.01"
              step="0.01"
            />
          </div>

          <div className="form-group">
            <label htmlFor="tipo">Tipo</label>
            <select
              id="tipo"
              value={tipo}
              onChange={(e) => setTipo(Number(e.target.value) as 0 | 1)}
              // Bloqueia a seleção de tipo se for menor de idade
              disabled={eMenorDeIdade}
            >
              <option value={TIPO_TRANSACAO.DESPESA}>Despesa</option>
              {/* Opção Receita só aparece habilitada para maiores de idade */}
              <option value={TIPO_TRANSACAO.RECEITA} disabled={eMenorDeIdade}>
                Receita
              </option>
            </select>
            {/* Aviso explicativo quando a pessoa for menor de idade */}
            {eMenorDeIdade && (
              <p className="msg-erro">
                ⚠️ Menores de 18 anos só podem cadastrar despesas.
              </p>
            )}
          </div>

          {erro && <p className="msg-erro">{erro}</p>}
          {sucesso && <p className="msg-sucesso">{sucesso}</p>}

          <button type="submit" className="btn btn-primary" disabled={enviando}>
            {enviando ? "Cadastrando..." : "Cadastrar"}
          </button>
        </form>
      </div>

      {/* ── Listagem de transações ── */}
      <div className="card">
        <h2>Transações Cadastradas</h2>

        {carregando ? (
          <p className="msg-vazio">Carregando...</p>
        ) : transacoes.length === 0 ? (
          <p className="msg-vazio">Nenhuma transação cadastrada.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>Pessoa</th>
                <th>Descrição</th>
                <th>Tipo</th>
                <th>Valor</th>
              </tr>
            </thead>
            <tbody>
              {transacoes.map((transacao) => (
                <tr key={transacao.id}>
                  <td>{getNomePessoa(transacao.pessoaId)}</td>
                  <td>{transacao.descricao}</td>
                  <td>
                    {/* Badge colorido conforme o tipo retornado pelo backend */}
                    <span
                      className={
                        transacao.tipo === "Receita"
                          ? "badge badge-receita"
                          : "badge badge-despesa"
                      }
                    >
                      {transacao.tipo}
                    </span>
                  </td>
                  <td>{formatarMoeda(transacao.valor)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  )
}

export default TransacoesPage
