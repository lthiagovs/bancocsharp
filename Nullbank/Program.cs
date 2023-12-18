using Nullbank.Contas;
using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{
    public static void Main()
    {

        bool rodar = true;
        bool logado = false;
        bool acessaConta = false;
        Usuario usuario = null;
        Conta conta = null;
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

                                Usuario funcBusca = new Funcionario(nomeFuncionario, "", null, 1, "");
                                funcBusca = Arquivo.buscaUsuario(funcBusca);

                                if (!funcBusca.cpf.Equals(""))
                                {
                                    Console.WriteLine("\nFuncionario encontrado: ");
                                    Console.WriteLine("Nome: " + funcBusca.nome);
                                    Console.WriteLine("CPF:" + funcBusca.cpf);
                                    Console.WriteLine("Agencia: " + funcBusca.agencia);
                                }
                                else
                                {
                                    Console.WriteLine("Nenhum funcionario com este nome foi encontrado!");
                                }

                                if (int.Parse(escolha) == 3)
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
                        //Nenhuma Conta acessada
                        if (!acessaConta)
                        {
                            Console.WriteLine("0 - Sair\n1 - Acessar Conta");
                            escolha = Console.ReadLine();
                            switch (int.Parse(escolha))
                            {
                                case 0:

                                    logado = false;
                                    break;

                                case 1:

                                    Console.Write("NUMERO >> ");
                                    int numeroConta = int.Parse(Console.ReadLine());
                                    Console.Write("SENHA >> ");
                                    string senha = Console.ReadLine();

                                    Conta buscaConta = Arquivo.buscaConta(numeroConta);

                                    if (!(buscaConta.numeroConta == 0))
                                    {
                                        conta = buscaConta;
                                        acessaConta = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Dados incorretos.");
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                        //Conta acessada
                        else
                        {
                            Console.WriteLine("Saldo da conta:"+conta.Saldo+"\n0 - Sair\n1 - Sacar\n2 - Depositar\n3 - Transferir");
                            escolha = Console.ReadLine();

                            switch (int.Parse(escolha))
                            {
                                case 0:
                                    acessaConta = false;
                                    break;
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                default: break;
                            }

                        }
                        break;

                    //Opções do Funcionario
                    case 2:
                        Console.WriteLine("0 - sair\n1 - Criar Cliente\n2 - Buscar cliente\n3 - Editar cliente\n4 - Criar Conta");
                        escolha = Console.ReadLine();
                        switch (int.Parse(escolha))
                        {
                            case 0:
                                Console.WriteLine("LogOut efetuado!");
                                logado = false;
                                break;
                            case 1:
                                Console.WriteLine("Criando novo cliente...");

                                Console.Write("CPF >> ");
                                string cpf1 = Console.ReadLine();

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

                                Endereço clienteEndereço = new Endereço(cep, num, rua, cidade, estado);

                                Cliente novoCliente = new Cliente(cpf1, nome, clienteEndereço, agencia, senha);

                                Arquivo.criaArquivoUsuario(novoCliente);
                                break;
                            case 2:
                                Console.WriteLine("Buscando cliente...");
                                Console.Write("Nome do Cliente: ");
                                string nome1 = Console.ReadLine();
                                Usuario clienteEncontrado = new Cliente(nome1, "", null, 0, "");
                                clienteEncontrado = Arquivo.buscaUsuario(clienteEncontrado);
                                if (clienteEncontrado.cpf.Equals(""))
                                {
                                    Console.WriteLine($"Cliente encontrado:\nNome: {clienteEncontrado.nome}\nCPF: {clienteEncontrado.cpf}\nEndereço: {clienteEncontrado.EndUsuario}\nAgencia: {clienteEncontrado.agencia}\nsenha: {clienteEncontrado.senha}");
                                }
                                else
                                {
                                    Console.WriteLine("Cliente não encontrado.");
                                }
                                break;
                            case 3:
                                Console.WriteLine("Editando cliente...");
                                Console.Write("Nome do Cliente: ");
                                string nome2 = Console.ReadLine();
                                Usuario clienteParaEditar = new Cliente(nome2, "", null, 0, "");
                                clienteParaEditar = Arquivo.buscaUsuario(clienteParaEditar);
                                if (!clienteParaEditar.cpf.Equals(""))
                                {
                                    //vazio á fazer
                                }
                                else
                                {
                                    Console.WriteLine("Cliente não encontrado. Não é possível editar.");
                                }
                                break;
                            case 4:
                                Console.Write("NOME DO TITULAR >> ");
                                string nomeConta = Console.ReadLine();
                                Usuario usuarioConta = new Cliente(nomeConta,"",null,0,"");
                                usuarioConta = Arquivo.buscaUsuario(usuarioConta);

                                if (!usuarioConta.cpf.Equals(""))
                                {
                                    Console.WriteLine("Titular de cpf: "+usuarioConta.cpf+" encontrado!");
                                    Conta contaCriar = new ContaCorrente(Conta.totalContas,usuarioConta as Cliente,0,0);

                                    Arquivo.criarArquivoConta(contaCriar);

                                    Console.WriteLine("Conta de numero: " + contaCriar.numeroConta + " criada com sucesso!");


                                }
                                else
                                {
                                    Console.WriteLine("Este titular não existe.");
                                }

                                break;
                            default:
                                Console.WriteLine("Opção invalida");
                                break;
                        }
                        Console.ReadLine();
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