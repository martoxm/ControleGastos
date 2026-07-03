namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// DTO focado estritamente no retorno de dados básicos de pessoas cadastradas.
    /// </summary>
    public class PessoaExibicaoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
    }
}