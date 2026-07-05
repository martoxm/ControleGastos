namespace ControleGastos.Application.DTOs
{
    /// <summary>
    /// Representa uma resposta padronizada de erro da API.
    /// </summary>
    public class ResponseErrorDto
    {
        /// <summary>
        /// Mensagem principal do erro.
        /// </summary>
        public string Erro { get; set; } = string.Empty;

        /// <summary>
        /// Código de status HTTP.
        /// </summary>
        public int Status { get; set; }
    }
}