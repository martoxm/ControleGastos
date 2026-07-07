namespace ControleGastos.Domain.Enums
{
    /// <summary>Enum responsável por definir explicitamente o tipo da transação financeira.
    /// Mantém o código mais legível e evita o uso de valores mágicos.</summary>
    public enum TipoTransacao
    {
        Despesa = 0,
        Receita = 1
    }
}