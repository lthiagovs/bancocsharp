using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{ 
    public static void Main()
    {

        bool rodar = true;
        bool logado = false;
        Usuario usuario = null;
        string escolha;

        //Main Loop
        while (rodar)
        {
            Console.WriteLine("Bem vindo ao NullBank!");

            //Usuario está logado
            if (logado)
            {
                Console.WriteLine("Logado como: " + usuario.nome);
                switch(usuario)
                {
                    //Opções do Gerente
                    case Gerente: 
                        break;

                    //Opções do Cliente
                    case Cliente: break;

                    //Opções do Funcionario
                    case Funcionario: break;
                }
            }
            //Usuario não está logado
            else
            {
                //Sistema de Login
                Console.WriteLine("Realize o login:\n0 - Sair\n1 - Clientes\n2 - Funcionarios\n3 - Gerentes");
                Console.Write("Opção >> ");
                escolha = Console.ReadLine();
                Console.Write("Login >> ");
                string login = Console.ReadLine();
                Console.Write("Senha >> ");
                string senha = Console.ReadLine();
                Usuario user;

                //Instanciando usuario vazio para iniciar busca / baseado no tipo
                switch (int.Parse(escolha))
                {
                    case 2:
                        user = new Funcionario(login, "", new Endereço("", 0, "", "", ""), 0, ""); 
                        break;
                    case 3:
                        //Console.Write("GERENTE");
                        user = new Gerente(login, "", new Endereço("", 0, "", "", ""), 0, "");
                        break;
                    default:
                        user = new Cliente(login, "", new Endereço("", 0, "", "", ""), 0, "");
                        break;

                }

                //Verificar Login
                user = Arquivo.buscaUsuario(user);
                if (!user.cpf.Equals(""))
                {
                    usuario = user;
                    logado = true;
                }

            }

            Console.Clear();
        }

    }

}

