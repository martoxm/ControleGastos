namespace ControleGastos.Domain.Exceptions
{
    /// <summary>
    /// Exceção usada quando uma regra de negócio do domínio é violada.
    /// Esse tipo de erro representa uma operação inválida do ponto de vista do sistema,
    /// mas não necessariamente uma falha interna da aplicação.
    /// </summary>
    public class RegraDeNegocioException(string message) : Exception(message)
    {
    }
}