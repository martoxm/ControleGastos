namespace ControleGastos.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando um recurso solicitado não é encontrado. → HTTP 404
/// </summary>
public class NotFoundException(string mensagem) : ControleGastosException(mensagem)
{
}