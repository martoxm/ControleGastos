namespace ControleGastos.Domain.Exceptions;

/// <summary>
/// Exceção usada quando uma regra de negócio do domínio é violada. → HTTP 400
/// </summary>
public class RegraDeNegocioException(string mensagem) : ControleGastosException(mensagem)
{
}