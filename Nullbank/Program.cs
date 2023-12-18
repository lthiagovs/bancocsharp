using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{
    public static void Main()
    {

        bool rodar = true;
        bool logado = false;
        Usuario usuario = null;
        int tipo = 0;
        string escolha;

        //Main Loop
        while (rodar)
        {
            Console.WriteLine("Bem vindo ao NullBank!");

            //Usuario está logado
            if (logado)
            {
                Console.WriteLine("Logado como: " + usuario.nome);
                switch (tipo)
                {
                    //Opções do Gerente
                    case 3:
                        Console.WriteLine("0 - Sair\n1 - Criar Funcionario\n2 - Buscar Funcionario\n3 - Editar Funcionario");
                        escolha = Console.ReadLine();

                        switch (int.Parse(escolha))
                        {

                            //LogOff
                            case 0:
                                logado = false;
                                break;

                            //Criar Funcionario
                            case 1:
                                Console.Write("CPF >> ");
                                string cpf = Console.ReadLine();

                                Console.Write("NOME >> ");
                                string nome = Console.ReadLine();

                                Console.Write("CEP >> ");
                                string cep = Console.ReadLine();

                                Console.Write("NUMERO >> ");
                                int num = int.Parse(Console.ReadLine());

                                Console.Write("RUA >> ");
                                string rua = Console.ReadLine();

                                Console.Write("CIDADE >> ");
                                string cidade = Console.ReadLine();

                                Console.Write("ESTADO >> ");
                                string estado = Console.ReadLine();

                                Console.Write("AGENCIA >> ");
                                int agencia = int.Parse(Console.ReadLine());

                                Console.Write("SENHA >> ");
                                string senha = Console.ReadLine();

                                Endereço funcionarioEndereço = new Endereço(cep, num, rua, cidade, estado);
                                Funcionario nFuncionario = new Funcionario(nome, cpf, funcionarioEndereço, agencia, senha);

                                Arquivo.criaArquivoUsuario(nFuncionario);
                                break;

                            //Buscar Funcionario
                            case 2 or 3:

                                Console.Write("NOME >> ");
                                string nomeFuncionario = Console.ReadLine();

                                Usuario funcBusca = new Funcionario(nomeFuncionario,"",null,1,"");
                                funcBusca = Arquivo.buscaUsuario(funcBusca);

                                if (!funcBusca.cpf.Equals(""))
                                {
                                    Console.WriteLine("\nFuncionario encontrado: ");
                                    Console.WriteLine("Nome: "+funcBusca.nome);
                                    Console.WriteLine("CPF:" + funcBusca.cpf);
                                    Console.WriteLine("Agencia: " + funcBusca.agencia);
                                }
                                else
                                {
                                    Console.WriteLine("Nenhum funcionario com este nome foi encontrado!");
                                }

                                if (int.Parse(escolha)==3)
                                {
                                    Console.WriteLine("\nAlterando Funcionario: ");

                                    Console.Write("CPF >> ");
                                    string cpfFunc = Console.ReadLine();

                                    Console.Write("AGENCIA >> ");
                                    int agenciaFunc = int.Parse(Console.ReadLine());

                                    Console.WriteLine("\nFuncionario alterado! ");

                                }

                                Console.ReadLine();

                                break;

                        }

                        break;

                    //Opções do Cliente
                    case 1:
                        Console.WriteLine("Cliente");
                        escolha = Console.ReadLine();
                        break;

                    //Opções do Funcionario
                    case 2:
                        Console.WriteLine("Funcionario");
                        escolha = Console.ReadLine();
                        break;
                    default:
                        break;
                }
            }
            //Usuario não está logado
            else
            {
                //Sistema de Login
                Console.WriteLine("Realize o login:\n0 - Sair\n1 - Clientes\n2 - Funcionarios\n3 - Gerentes");
                Console.Write("Opção >> ");
                escolha = Console.ReadLine();
                if (int.Parse(escolha) != 0)
                {
                    Console.Write("Login >> ");
                    string login = Console.ReadLine();
                    Console.Write("Senha >> ");
                    string senha = Console.ReadLine();
                    Usuario user = new Cliente(login, "", new Endereço("", 0, "", "", ""), 0, "");

                    //Instanciando usuario vazio para iniciar busca / baseado no tipo
                    switch (int.Parse(escolha))
                    {
                        case 2:
                            user = new Funcionario(login, "", new Endereço("", 0, "", "", ""), 0, "");
                            tipo = 2;
                            break;
                        case 3:
                            user = new Gerente(login, "", new Endereço("", 0, "", "", ""), 0, "");
                            tipo = 3;
                            break;
                        default:
                            tipo = 1;
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
                else
                {
                    rodar = false;
                }

            }

            Console.Clear();
        }

    }

}

