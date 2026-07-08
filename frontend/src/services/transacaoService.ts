// Única responsabilidade: comunicar com os endpoints do TransacoesController
//
// Rotas mapeadas:
// GET  /api/transacoes   → listar todas as transações
// POST /api/transacoes   → criar uma transação

import { api } from "./api"
import type {
  TransacaoResponse,
  CriarTransacaoRequest,
} from "../types/Transacao"

export const transacaoService = {
  listar: async (): Promise<TransacaoResponse[]> => {
    const response = await api.get<TransacaoResponse[]>("/transacoes")
    return response.data
  },

  // Se a pessoa for menor de 18, o backend rejeita receitas
  criar: async (dados: CriarTransacaoRequest): Promise<TransacaoResponse> => {
    const response = await api.post<TransacaoResponse>("/transacoes", dados)
    return response.data
  },
}
