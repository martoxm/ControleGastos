// Contratos de dados baseados nos DTOs reais do backend:
// - PessoaExibicaoDto  → interface Pessoa (retorno da API)
// - PessoaCadastroDto  → interface CriarPessoaDTO (envio para API)

export interface Pessoa {
  id: string // Guid do C# serializa como string UUID no JSON
  nome: string
  idade: number
}

export interface CriarPessoaDTO {
  nome: string // Obrigatório, entre 2 e 150 caracteres
  idade: number // Entre 0 e 150
}
