namespace ControleGastos.Domain.Exceptions;

/// <summary>Exceção base do domínio ControleGastos.
/// Todas as exceções de negócio devem herdar desta classe./// </summary>
public abstract class ControleGastosException(string mensagem) : Exception(mensagem)
{
}