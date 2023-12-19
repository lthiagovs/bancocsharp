namespace Nullbank.Usuarios
{
    class Cliente : Usuario
    {

        //Construtor da classe Cliente  - @thiago
        public Cliente(string nome, string cpf, Endereço endereço, int agencia, string senha) : base(nome,cpf,endereço,agencia,senha)
        {

        }

        //Permite criar um Cliente vazio
        public Cliente()
        {

        }


        //Acessa uma das contas do Cliente  - @thiago
        public void acessaConta(string senha)
        {

        }


    }
}
