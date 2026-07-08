// Usado exclusivamente na página de Consulta de Totais

export interface TotalPorPessoa {
  pessoaId: string
  nome: string
  idade: number
  totalReceitas: number
  totalDespesas: number
  saldo: number
}

export interface RelatorioFinanceiroGeral {
  pessoas: TotalPorPessoa[]
  totalGeralReceitas: number
  totalGeralDespesas: number
  saldoLiquidoGeral: number
}
