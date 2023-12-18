namespace Nullbank.Usuarios
{
    class Gerente : Usuario, IGerente
    {

        //Construtor da classe Gerente - @thiago
        public Gerente(string nome, string cpf, Endereço endereço, int agencia, string senha) : base(nome, cpf, endereço, agencia, senha)
        {

        }

        //#IGerente
        public void criaConta()
        {

        }

        //#IGerente
        public void deletaConta()
        {

        }

        //#IGerente
        public void alteraConta()
        {

        }

        //#IGerente
        public void criaFuncionario(Funcionario funcionario)
        {

        }

        //#IGerente
        public void deletaFuncionario(string nome)
        {

        }

        //#IGerente
        public void alteraFuncionario(string nome)
        {

        }
    }
}
