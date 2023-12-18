using Nullbank;
using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{

    public static void Main()
    {

        Usuario c1 = new Cliente("Thiago","",null,0,"");

        c1 = Arquivo.buscaUsuario(c1);

        Console.WriteLine(c1.EndUsuario);

    }

}

