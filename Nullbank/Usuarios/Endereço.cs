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

    }
}
