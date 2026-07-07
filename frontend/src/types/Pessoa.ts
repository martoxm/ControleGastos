// - PessoaResponse  → interface Pessoa (retorno da API)
// - CriarPessoaRequest  → interface CriarPessoaDTO (envio para API)

export interface PessoaResponse {
  id: string // Guid do C# serializa como string UUID no JSON
  nome: string
  idade: number
}

export interface CriarPessoaRequest {
  nome: string // Obrigatório, entre 2 e 150 caracteres
  idade: number // Entre 0 e 150
}
