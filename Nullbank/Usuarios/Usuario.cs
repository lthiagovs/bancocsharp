namespace Nullbank.Usuarios
{
    abstract class Usuario
    {

        public string nome;
        public string cpf { protected set; get; }
        private Endereço endereço;
        public int agencia { protected set; get; }
        public string senha { private set; get; }

        //Propertie do Endereço - @thiago
        public String EndUsuario
        {
            get
            {
                return endereço.estado + "," + endereço.cidade + "," + endereço.rua + "," + endereço.numero + "," + endereço.cep;
            }
        }

        //Construtor da classe Usuario - @thiago
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
