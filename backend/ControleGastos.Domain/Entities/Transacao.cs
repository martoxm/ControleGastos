using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma movimentação financeira (Transação).
    /// Possui regras rígidas acopladas à idade da Pessoa vinculada.
    /// </summary>
    public class Transacao
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public Guid PessoaId { get; private set; }

        protected Transacao()
        {
            Descricao = string.Empty;
        }

        public Transacao(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa)
        {
            if (pessoa == null)
                throw new RegraDeNegocioException("A transação precisa estar vinculada a uma pessoa existente.");

            ValidarDados(descricao, valor, tipo, pessoa);

            Id = Guid.NewGuid();
            Descricao = descricao.Trim();
            Valor = valor;
            Tipo = tipo;
            PessoaId = pessoa.Id;
        }

        private static void ValidarDados(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new RegraDeNegocioException("A descrição da transação é obrigatória.");

            if (valor <= 0)
                throw new RegraDeNegocioException("O valor da transação deve ser maior que zero.");

            if (!Enum.IsDefined(tipo))
                throw new RegraDeNegocioException("O tipo da transação informado é inválido.");

            if (pessoa.EhMenorDeIdade() && tipo == TipoTransacao.Receita)
                throw new RegraDeNegocioException("Menores de 18 anos só podem cadastrar transações do tipo Despesa.");
        }
    }
}