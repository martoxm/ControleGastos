using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities
{
    /// <summary>Entidade que representa uma Pessoa no sistema.</summary>
    public class Pessoa
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public int Idade { get; private set; }

        private Pessoa()
        { }

        public Pessoa(string nome, int idade)
        {
            var erros = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(nome))
                erros["Nome"] = ["O nome não pode ser vazio ou nulo."];

            if (idade < 1)
                erros["Idade"] = ["A idade deve ser maior que zero."];

            if (erros.Count > 0)
                throw new ErrosDeValidacaoException(erros);

            Id = Guid.NewGuid();
            Nome = nome.Trim();
            Idade = idade;
        }

        public bool EhMenorDeIdade() => Idade < 18;
    }
}