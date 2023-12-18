namespace Nullbank.Usuarios
{
    abstract class Usuario
    {

        public string nome;
        protected string cpf;
        protected Endereço endereço;
        public int agencia;
        public string senha;

        //Construtor da classe Usuario
        public Usuario(string nome, string cpf, Endereço endereço, int agencia, string senha)
        {
            this.nome = nome;
            this.cpf = cpf;
            this.endereço = endereço;
            this.agencia = agencia;
            this.senha = senha;
        }



    }
}
