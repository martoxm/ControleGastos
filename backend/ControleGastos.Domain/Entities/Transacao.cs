using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities
{
    ///<summary> Entidade que representa uma movimentação financeira (Transação).
    /// Possui regras rígidas acopladas à idade da Pessoa vinculada.</summary>
    public class Transacao
    {
        public Guid Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public TipoTransacao Tipo { get; private set; }
        public Guid PessoaId { get; private set; }

        /// <summary>Construtor sem parâmetros exigido exclusivamente pelo Entity Framework Core.
        /// Declarado como 'protected' para impedir que a camada de Aplicação o use incorretamente.</summary>
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

            ValidarDados(descricao, valor, tipo, pessoa);

            Id = Guid.NewGuid();
            Descricao = descricao.Trim();
            Valor = valor;
            Tipo = tipo;
            PessoaId = pessoa.Id;
        }

        /// <summary>
        /// Centraliza as validações da transação para evitar repetição entre criação e atualização.
        /// </summary>
        private static void ValidarDados(string descricao, decimal valor, TipoTransacao tipo, Pessoa pessoa)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição da transação é obrigatória.");

            if (valor <= 0)
                throw new ArgumentException("O valor da transação deve ser maior que zero.");

            if (!Enum.IsDefined(tipo))
                throw new ArgumentException("O tipo da transação informado é inválido.");

            // Se a pessoa for menor de idade (menos de 18 anos),
            // apenas transações do tipo Despesa podem ser cadastradas.
            if (pessoa.EhMenorDeIdade() && tipo == TipoTransacao.Receita)
            {
                throw new RegraDeNegocioException("Menores de 18 anos só podem cadastrar transações do tipo Despesa.");
            }
        }
    }
}