namespace Nullbank.Usuarios
{
    class Endereço
    {

        public string cep;
        public int numero;
        public string rua;
        public string cidade;
        public string estado;

        //Construtor da classe Endereço  - @thiago
        public Endereço(string cep, int numero, string rua, string cidade, string estado)
        {
            this.cep = cep;
            this.numero = numero;
            this.rua = rua;
            this.cidade = cidade;
            this.estado = estado;
        }

        //Permite criar um Endereço vazio
        public Endereço()
        {
            this.cep = "";
            this.numero = -1;
            this.rua = "";
            this.cidade = "";
            this.estado = "";
        }

    }
}
