namespace Nullbank.Usuarios
{
    //Interface dos Gerentes
    interface IGerente
    {

        //Cria uma conta - @thiago
        public void criaConta();

        //Deleta uma conta - @thiago
        public void deletaConta();

        //Altera uma conta - @thiago
        public void alteraConta();

        //Cria um funcionario - @thiago
        public void criaFuncionario(Funcionario funcionario);

        //Deleta um funcionario  - @thiago
        public void deletaFuncionario(string nome);

        //Altera um funcionario  - @thiago
        public void alteraFuncionario(string nome);

    }
}
