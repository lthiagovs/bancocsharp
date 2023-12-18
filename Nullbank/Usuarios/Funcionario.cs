namespace Nullbank.Usuarios
{
    class Funcionario : Usuario
    {

        //Construtor da classe Funcionario - @thiago
        public Funcionario(string nome, string cpf, Endereço endereço, int agencia, string senha) : base(nome, cpf, endereço, agencia, senha)
        {

        }

    }
}
