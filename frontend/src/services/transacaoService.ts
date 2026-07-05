// Única responsabilidade: comunicar com os endpoints do TransacoesController
//
// Rotas mapeadas:
// GET  /api/transacoes   → listar todas as transações
// POST /api/transacoes   → criar uma transação

import { api } from "./api"
import type { Transacao, CriarTransacaoDTO } from "../types/Transacao"

export const transacaoService = {
  listar: async (): Promise<Transacao[]> => {
    const response = await api.get<Transacao[]>("/transacoes")
    return response.data
  },

  // Regra do desafio: se a pessoa for menor de 18, o backend rejeita receitas
  criar: async (dados: CriarTransacaoDTO): Promise<Transacao> => {
    const response = await api.post<Transacao>("/transacoes", dados)
    return response.data
  },
}
