// Única responsabilidade: comunicar com os endpoints do PessoasController
//
// Rotas mapeadas:
// GET    /api/pessoas          → listar todas as pessoas
// POST   /api/pessoas          → criar uma pessoa
// DELETE /api/pessoas/{id}     → deletar pessoa (apaga as transações também)
// GET    /api/pessoas/totais   → relatório financeiro geral

import { api } from "./api"
import type { PessoaResponse, CriarPessoaRequest } from "../types/Pessoa"
import type { RelatorioFinanceiroGeral } from "../types/Relatorio"

export const pessoaService = {
  listar: async (): Promise<PessoaResponse[]> => {
    const response = await api.get<PessoaResponse[]>("/pessoas")
    return response.data
  },

  criar: async (dados: CriarPessoaRequest): Promise<PessoaResponse> => {
    const response = await api.post<PessoaResponse>("/pessoas", dados)
    return response.data
  },

  // Ao deletar uma pessoa, suas transações são apagadas pelo backend
  deletar: async (id: string): Promise<void> => {
    await api.delete(`/pessoas/${id}`)
  },

  obterTotais: async (): Promise<RelatorioFinanceiroGeral> => {
    const response = await api.get<RelatorioFinanceiroGeral>("/pessoas/totais")
    return response.data
  },
}
