namespace ControleGastos.Domain.Entities
{
    /// <summary>
    /// Entidade que representa uma Pessoa no sistema.
    /// Responsável por gerenciar dados básicos de identificação e regras de idade.
    /// </summary>
    public class Pessoa
    {
        // Identificador único gerado automaticamente
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public int Idade { get; private set; }

        public Pessoa(string nome, int idade)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome não pode ser vazio ou nulo.");

            if (idade < 0)
                throw new ArgumentException("A idade não pode ser um valor negativo.");

            Id = Guid.NewGuid();
            Nome = nome;
            Idade = idade;
        }

        /// <summary>
        /// Regra de negócio isolada na própria entidade para verificar a maioridade.
        /// </summary>
        public bool EhMenorDeIdade()
        {
            return Idade < 18;
        }
    }
}