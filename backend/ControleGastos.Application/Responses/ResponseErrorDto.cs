using System.Text.Json.Serialization;

namespace ControleGastos.Application.Responses
{
    /// <summary>Representa uma resposta padronizada de erro da API.</summary>
    public class ResponseErrorDto
    {
        public string Erro { get; set; } = string.Empty;

        public int Status { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string[]>? Erros { get; set; }
    }
}