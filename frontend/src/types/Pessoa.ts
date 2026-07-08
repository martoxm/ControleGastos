// - PessoaResponse  → interface Pessoa (retorno da API)
// - CriarPessoaRequest  → interface CriarPessoaRequest (envio para API)

export interface PessoaResponse {
  id: string
  nome: string
  idade: number
}

export interface CriarPessoaRequest {
  nome: string
  idade: number
}
