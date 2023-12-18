using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{

    public static void Main()
    {

        bool rodar = true;
        bool logado = false;
        string escolha;

        //Main Loop
        while (rodar)
        {
            Console.WriteLine("Bem vindo ao NullBank!");

            if (logado)
            {

            }
            else
            {
                Console.WriteLine("Faça o login abaixo: ");
                Console.Write("Login: ");
                string login = Console.ReadLine();
                Console.Write("Senha: ");
                string senha = Console.ReadLine();

            }


        }

    }

}

