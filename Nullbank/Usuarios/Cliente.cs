namespace Nullbank.Usuarios
{
    class Cliente : Usuario
    {

        //Construtor da classe Cliente
        public Cliente(string nome, string cpf, Endereço endereço, int agencia, string senha) : base(nome,cpf,endereço,agencia,senha)
        {

        }

        //Acessa uma das contas do Cliente
        public void acessaConta(string senha)
        {

        }

    }
}
