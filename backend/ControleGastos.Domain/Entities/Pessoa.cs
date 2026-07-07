using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma Pessoa no sistema.
    /// Responsável por gerenciar dados básicos de identificação e regras de idade.
    /// </summary>
    public class Pessoa
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public int Idade { get; private set; }

        private Pessoa()
        { }

        public Pessoa(string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new RegraDeNegocioException("O nome não pode ser vazio ou nulo.");

            if (idade < 0)
                throw new RegraDeNegocioException("A idade não pode ser um valor negativo.");

            Id = Guid.NewGuid();
            Nome = nome.Trim();
            Idade = idade;
        }

        public bool EhMenorDeIdade() => Idade < 18;
    }
}