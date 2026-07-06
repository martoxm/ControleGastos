namespace ControleGastos.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um recurso solicitado não é encontrado.
/// </summary>
public class NotFoundException(string mensagem) : Exception(mensagem)
{
}