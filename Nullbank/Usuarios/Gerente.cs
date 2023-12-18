namespace Nullbank.Usuarios
{
    class Gerente : Usuario, IGerente
    {

        //Construtor da classe Gerente
        public Gerente(string nome, string cpf, Endereço endereço, int agencia, string senha) : base(nome, cpf, endereço, agencia, senha)
        {

        }

        public void criaConta()
        {

        }

        public void deletaConta()
        {

        }

        public void alteraConta()
        {

        }

    }
}
