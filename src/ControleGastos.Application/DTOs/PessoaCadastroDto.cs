namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado exclusivamente na entrada de dados para o cadastro de uma nova pessoa.
    /// </summary>
    public class PessoaCadastroDto
    {
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}