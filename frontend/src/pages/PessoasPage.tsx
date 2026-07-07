// Página de gerenciamento de pessoas
// Responsabilidades: listar, cadastrar e deletar pessoas
// Regra de negócio: ao deletar uma pessoa, o backend apaga suas transações também

import { useState, useEffect } from "react"
import { pessoaService } from "../services/pessoaService"
import type { PessoaResponse, CriarPessoaRequest } from "../types/Pessoa"

const PessoasPage = () => {
  // ── Estado da listagem ──────────────────────────────────
  const [pessoas, setPessoas] = useState<PessoaResponse[]>([])
  const [carregando, setCarregando] = useState(true)

  // ── Estado do formulário ────────────────────────────────
  const [nome, setNome] = useState("")
  const [idade, setIdade] = useState("")

  // ── Estado de feedback para o usuário ──────────────────
  const [enviando, setEnviando] = useState(false)
  const [erro, setErro] = useState("")
  const [sucesso, setSucesso] = useState("")

  // ── Carrega a lista ao abrir a página ──────────────────
  useEffect(() => {
    carregarPessoas()
  }, [])

  const carregarPessoas = async () => {
    try {
      setCarregando(true)
      const dados = await pessoaService.listar()
      setPessoas(dados)
    } catch {
      setErro("Erro ao carregar pessoas. Verifique se o backend está rodando.")
    } finally {
      setCarregando(false)
    }
  }

  // ── Submissão do formulário de cadastro ────────────────
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault() // impede o recarregamento padrão do formulário
    setErro("")
    setSucesso("")

    // Validação básica no frontend (o backend também valida)
    if (!nome.trim()) {
      setErro("O nome é obrigatório.")
      return
    }
    const idadeNum = Number(idade)
    if (!idade || isNaN(idadeNum) || idadeNum < 0 || idadeNum > 150) {
      setErro("Informe uma idade válida entre 0 e 150.")
      return
    }

    const dados: CriarPessoaRequest = { nome: nome.trim(), idade: idadeNum }

    try {
      setEnviando(true)
      await pessoaService.criar(dados)
      setSucesso(`Pessoa "${nome.trim()}" cadastrada com sucesso!`)
      // Limpa o formulário após sucesso
      setNome("")
      setIdade("")
      // Recarrega a lista para mostrar o novo registro
      await carregarPessoas()
    } catch (err: unknown) {
      const data = (err as { response?: { data?: { erro?: string } } })
        ?.response?.data

      setErro(data?.erro ?? "Erro ao cadastrar pessoa. Tente novamente.")
    } finally {
      setEnviando(false)
    }
  }

  // ── Deletar pessoa ─────────────────────────────────────
  const handleDeletar = async (pessoa: PessoaResponse) => {
    // Confirmação para evitar exclusão acidental
    const confirmado = window.confirm(
      `Deseja remover "${pessoa.nome}"?\nTodas as transações desta pessoa também serão apagadas.`,
    )
    if (!confirmado) return

    try {
      await pessoaService.deletar(pessoa.id)
      // Atualiza a lista removendo localmente (sem nova chamada à API)
      setPessoas((prev) => prev.filter((p) => p.id !== pessoa.id))
      setSucesso(`Pessoa "${pessoa.nome}" removida com sucesso.`)
      setErro("")
    } catch {
      setErro("Erro ao remover pessoa. Tente novamente.")
    }
  }

  // ── Renderização ───────────────────────────────────────
  return (
    <div>
      {/* ── Formulário de cadastro ── */}
      <div className="card">
        <h2>Cadastrar Pessoa</h2>
        <form onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="nome">Nome</label>
            <input
              id="nome"
              type="text"
              placeholder="Nome completo"
              value={nome}
              onChange={(e) => setNome(e.target.value)}
              maxLength={150}
            />
          </div>

          <div className="form-group">
            <label htmlFor="idade">Idade</label>
            <input
              id="idade"
              type="text"
              inputMode="numeric"
              placeholder="Idade"
              value={idade}
              onChange={(e) => {
                const val = e.target.value
                if (val === "" || /^\d+$/.test(val)) setIdade(val)
              }}
              maxLength={3}
            />
          </div>

          {/* Mensagens de feedback */}
          {erro && <p className="msg-erro">{erro}</p>}
          {sucesso && <p className="msg-sucesso">{sucesso}</p>}

          <button type="submit" className="btn btn-primary" disabled={enviando}>
            {enviando ? "Cadastrando..." : "Cadastrar"}
          </button>
        </form>
      </div>

      {/* ── Listagem de pessoas ── */}
      <div className="card">
        <h2>Pessoas Cadastradas</h2>

        {carregando ? (
          <p className="msg-vazio">Carregando...</p>
        ) : pessoas.length === 0 ? (
          <p className="msg-vazio">Nenhuma pessoa cadastrada.</p>
        ) : (
          <table>
            <thead>
              <tr>
                <th>Nome</th>
                <th>Idade</th>
                <th>Situação</th>
                <th>Ação</th>
              </tr>
            </thead>
            <tbody>
              {pessoas.map((pessoa) => (
                <tr key={pessoa.id}>
                  <td>{pessoa.nome}</td>
                  <td>{pessoa.idade} anos</td>
                  {/* Exibe visualmente se a pessoa é menor de idade — regra de negócio do desafio */}
                  <td>
                    {pessoa.idade < 18 ? (
                      <span className="badge badge-despesa">
                        Menor de idade
                      </span>
                    ) : (
                      <span className="badge badge-receita">
                        Maior de idade
                      </span>
                    )}
                  </td>
                  <td>
                    <button
                      className="btn btn-danger"
                      onClick={() => handleDeletar(pessoa)}
                    >
                      Remover
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  )
}

export default PessoasPage
