// Baseado em RelatorioFinanceiroGeralDto e TotalPorPessoaDto do backend
// Usado exclusivamente na página de Consulta de Totais

export interface TotalPorPessoa {
  pessoaId: string
  nome: string
  idade: number
  totalReceitas: number
  totalDespesas: number
  saldo: number // Calculado pelo backend: totalReceitas - totalDespesas
}

export interface RelatorioFinanceiroGeral {
  pessoas: TotalPorPessoa[]
  totalGeralReceitas: number
  totalGeralDespesas: number
  saldoLiquidoGeral: number
}
