using System.Text.Json.Serialization;

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

        /// <summary>
        /// Erros detalhados por campo.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string[]>? Erros { get; set; }
    }
}