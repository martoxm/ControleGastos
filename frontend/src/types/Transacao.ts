// TipoTransacao: enviado como número (0/1), recebido como string ("Despesa"/"Receita")

export type TipoTransacao = 0 | 1

// Constantes nomeadas evitam "números mágicos" no código
export const TIPO_TRANSACAO = {
  DESPESA: 0 as TipoTransacao,
  RECEITA: 1 as TipoTransacao,
} as const

// Enviado no POST /api/transacoes — baseado em CriarTransacaoRequest do backend
export interface CriarTransacaoRequest {
  descricao: string
  valor: number
  tipo: TipoTransacao
  pessoaId: string // UUID da pessoa selecionada
}

// Retornado pelo GET /api/transacoes
export interface TransacaoResponse {
  id: string
  descricao: string
  valor: number
  tipo: string // Backend retorna "Despesa" ou "Receita" como string
  pessoaId: string
}
