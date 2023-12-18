using Nullbank;
using Nullbank.Arquivos;
class Program
{

    public static void Main()
    {
        Arquivo teste = new Arquivo(Arquivo.caminhoContas + "teste.data");

        teste.criaArquivo();

        teste.deletaArquivo();

        


    }

}

