using ControleGastos.Domain.Enums;

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

        /// <summary>
        /// Construtor sem parâmetros exigido exclusivamente pelo Entity Framework Core.
        /// Declarado como 'protected' para impedir que a camada de Aplicação o use incorretamente.
        /// </summary>
        protected Transacao()
        {
            Descricao = string.Empty;
        }

        /// <summary>
        /// Construtor padrão do Domínio. Aplica as validações de negócio obrigatórias do sistema.
        /// </summary>
        public Transacao(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa)
        {
            if (pessoa == null)
                throw new ArgumentNullException(nameof(pessoa), "A transação precisa estar vinculada a uma pessoa existente.");

            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição da transação é obrigatória.");

            if (valor <= 0)
                throw new ArgumentException("O valor da transação deve ser maior que zero.");

            // REGRA DE NEGÓCIO: Se for menor de idade (menor de 18 anos), apenas despesas são permitidas
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