using ControleGastos.Domain.Enums;

namespace ControleGastos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma movimentação financeira vinculada a uma pessoa.
    /// </summary>
    public class Transacao
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public TipoTransacao Tipo { get; private set; }

        // Chave estrangeira ligando a transação à pessoa
        public Guid PessoaId { get; private set; }

        public Transacao(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa)
        {
            if (pessoa == null)
                throw new ArgumentNullException(nameof(pessoa), "A transação precisa estar vinculada a uma pessoa existente.");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição da transação é obrigatória.");

            if (valor <= 0)
                throw new ArgumentException("O valor da transação deve ser maior que zero.");

            // REGRA DE NEGÓCIO EXIGIDA: Menores de 18 anos só registram despesas
            if (pessoa.EhMenorDeIdade() && tipo == TipoTransacao.Receita)
            {
                throw new InvalidOperationException("Menores de 18 anos só podem cadastrar transações do tipo Despesa.");
            }

            Id = Guid.NewGuid();
            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            PessoaId = pessoa.Id;
        }
    }
}