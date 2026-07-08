namespace ControleGastos.Domain.Exceptions;

///<summary> Exceção lançada quando múltiplos erros de validação ocorrem simultaneamente.</summary>

public class ErrosDeValidacaoException(Dictionary<string, string[]> erros) : ControleGastosException("Existem erros de validação.")
{
    public Dictionary<string, string[]> Erros { get; } = erros;
}