using Nullbank.Contas;
using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{

    /// <summary>
    ///     Editar Clientes
    ///     Tipos de Conta nos Arquivos e na interface
    ///     Contas Conjuntas
    ///     Polimento de Interface
    ///     Remoção de Warnings e Comentarios
    /// </summary>

    //Cria um formulario de cadastro de funcionario no console
    public static Funcionario cadastrarFuncionario()
    {
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

        return nFuncionario;
    }

    //Cria um formulario de cadastro de Cliente no console
    public static Cliente cadastrarCliente()
    {
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
        Cliente nFuncionario = new Cliente(nome, cpf, funcionarioEndereço, agencia, senha);

        return nFuncionario;
    }

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
                                Arquivo.criaArquivoUsuario(cadastrarFuncionario());
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
                                    if (int.Parse(escolha) == 3)
                                    {
                                        Console.WriteLine("Digite agora as novas informações: ");
                                        Funcionario alterado = cadastrarFuncionario();
                                        Arquivo.alteraUsuario(funcBusca, alterado);
                                        Console.WriteLine("Alterações realizadas! (OBS: O nome é imutavel)");

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nenhum funcionario com este nome foi encontrado!");
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
                                    Console.WriteLine("LogOut efetuado!");
                                    logado = false;
                                    break;

                                case 1:

                                    Console.Write("NUMERO >> ");
                                    int numeroConta = int.Parse(Console.ReadLine());
                                    Conta buscaConta = new ContaCorrente(numeroConta,null,0,0,"");
                                    Console.Write("SENHA >> ");
                                    string senha = Console.ReadLine();
                                    Console.WriteLine("1 - Corrente\n2 - Poupança\n3 - Compartilhada\n");
                                    int tipoLogin = int.Parse(Console.ReadLine());

                                    switch (tipoLogin)
                                    {
                                        case 1:
                                            buscaConta = new ContaCorrente(numeroConta, null, 0, 0, "");
                                            break;
                                        case 2:
                                            buscaConta = new ContaPoupanca(numeroConta, null, 0, 0, "");
                                            break;
                                        case 3:
                                            buscaConta = new ContaCompartilhada(numeroConta, null, 0, new List<string>(), "");
                                            break;
                                        default:
                                            buscaConta = new ContaCorrente(numeroConta, null, 0, 0, "");
                                            break;
                                    }

                                    buscaConta = Arquivo.buscaConta(buscaConta);

                                    //Conta encontrada
                                    if (!(buscaConta.numeroConta == -1))
                                    {
                                        //Se encontrada verificar credenciais e se pertence ao titular logado
                                        if (usuario.nome.Equals(buscaConta.titular.nome)&&buscaConta.senha.Equals(senha))
                                        {
                                            conta = buscaConta;
                                            acessaConta = true;
                                            Console.WriteLine("Conta de cpf: "+buscaConta.titular.cpf+" acessada.");;
                                            Console.ReadLine();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Dados incorretos.");
                                        Console.ReadLine();
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                        //Conta acessada
                        else
                        {
                            Console.WriteLine("saldo da conta: R$"+conta.saldo+"\n0 - Sair\n1 - Sacar\n2 - Depositar\n3 - Transferir");
                            escolha = Console.ReadLine();

                            switch (int.Parse(escolha))
                            {
                                case 0:
                                    Arquivo.alteraConta(Arquivo.buscaConta(conta), conta);
                                    acessaConta = false;
                                    break;
                                case 1:
                                    Console.WriteLine("Digite o valor que deseja sacar: ");
                                    escolha = Console.ReadLine();
                                    conta.Sacar(double.Parse(escolha));
                                    break;
                                case 2:
                                    Console.WriteLine("Digite o valor que deseja Depositar: ");
                                    escolha = Console.ReadLine();
                                    conta.Depositar(int.Parse(escolha));
                                    break;
                                case 3:
                                    Console.WriteLine("Digite o valor que deseja transferir: ");
                                    escolha = Console.ReadLine();
                                    conta.Transferir(null, double.Parse(escolha));
                                    break;
                                default:
                                    break;

                            }

                        }
                        Console.Clear();
                        break;

                    //Opções do Funcionario
                    case 2:
                        Console.WriteLine("0 - Sair\n1 - Criar Cliente\n2 - Buscar cliente\n3 - Editar cliente\n4 - Criar Conta");
                        escolha = Console.ReadLine();
                        switch (int.Parse(escolha))
                        {
                            case 0:
                                Console.WriteLine("LogOut efetuado!");
                                logado = false;
                                break;
                            case 1:
                                Console.WriteLine("Criando novo cliente...");

                                Cliente novoCliente = cadastrarCliente();

                                Console.WriteLine("Cliente criado com sucesso!");
                                Arquivo.criaArquivoUsuario(novoCliente);
                                Console.ReadLine();
                                break;
                            case 2 or 3:

                                Console.Write("NOME >> ");
                                string nomeFuncionario = Console.ReadLine();

                                Usuario clienteBusca = new Cliente(nomeFuncionario, "", null, 1, "");
                                clienteBusca = Arquivo.buscaUsuario(clienteBusca);

                                if (!clienteBusca.cpf.Equals(""))
                                {
                                    Console.WriteLine("\nCliente encontrado: ");
                                    Console.WriteLine("Nome: " + clienteBusca.nome);
                                    Console.WriteLine("CPF:" + clienteBusca.cpf);
                                    Console.WriteLine("Agencia: " + clienteBusca.agencia);
                                    if (int.Parse(escolha) == 3)
                                    {
                                        Console.WriteLine("Digite agora as novas informações: ");
                                        Cliente alterado = cadastrarCliente();
                                        Arquivo.alteraUsuario(clienteBusca, alterado);
                                        Console.WriteLine("Alterações realizadas! (OBS: O nome é imutavel)");

                                    }

                                }
                                else
                                {
                                    Console.WriteLine("Nenhum cliente com este nome foi encontrado!");
                                }

                                Console.ReadLine();
                                break;
                            case 4:
                                Console.Write("NOME DO TITULAR >> ");
                                string nomeConta = Console.ReadLine();
                                Usuario usuarioConta = new Cliente(nomeConta,"",null,0,"");
                                usuarioConta = Arquivo.buscaUsuario(usuarioConta);

                                if (!usuarioConta.cpf.Equals(""))
                                {
                                    Console.WriteLine("Titular de cpf: "+usuarioConta.cpf+" encontrado!");
                                    Console.Write("SENHA >> ");
                                    string contaSenha = Console.ReadLine();
                                    Console.WriteLine("1 - Corrente\n2 - Poupança\n3 - Compartilhada\n");
                                    Console.Write("TIPO >> ");
                                    int tipoConta = int.Parse(Console.ReadLine());
                                    Conta contaCriar;

                                    switch (tipoConta)
                                    {
                                        case 1:
                                            contaCriar = new ContaCorrente(Conta.totalContas, usuarioConta as Cliente, 0, 0, contaSenha);
                                            break;
                                        case 2:
                                            contaCriar = new ContaPoupanca(Conta.totalContas, usuarioConta as Cliente, 0, 0, contaSenha);
                                            break;
                                        case 3:
                                            contaCriar = new ContaCompartilhada(Conta.totalContas, usuarioConta as Cliente, 0, new List<string>(), contaSenha);
                                            ContaCompartilhada contaCriarCompartilhada = contaCriar as ContaCompartilhada;
                                            do
                                            {
                                                Console.WriteLine("0 - Finalizar\nDigite o nome de um dos titulares: ");
                                                escolha = Console.ReadLine();

                                                Usuario titularBusca = new Cliente(escolha,"",null,0,"");
                                                titularBusca = Arquivo.buscaUsuario(titularBusca);

                                                if (!escolha.Equals("0")) {
                                                    if (!titularBusca.cpf.Equals(""))
                                                    {
                                                        Console.WriteLine("Titular adicionado à conta!");
                                                        contaCriarCompartilhada.usuariosSecundarios.Add(titularBusca.nome);

                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Titular não encontrado!");
                                                    }
                                                    Console.ReadLine();
                                                }


                                            } while (!escolha.Equals("0"));
                                            contaCriar = contaCriarCompartilhada;
                                            break;
                                        default:
                                            contaCriar = new ContaCorrente(Conta.totalContas, usuarioConta as Cliente, 0, 0, contaSenha);
                                            break;
                                    }

                                    Arquivo.criaArquivoConta(contaCriar);

                                    Console.WriteLine("Conta de numero: " + contaCriar.numeroConta + " criada com sucesso!");


                                }
                                else
                                {
                                    Console.WriteLine("Este titular não existe.");
                                }

                                Console.ReadLine();
                                break;
                            default:
                                Console.WriteLine("Opção invalida");
                                break;
                        }
           
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
                    if (!user.cpf.Equals("")&&user.senha.Equals(senha))
                    {
                        usuario = user;
                        logado = true;
                        Console.WriteLine("Login efetuado!");
                    }
                    else
                    {
                        Console.WriteLine("Credenciais incorretas!");
                    }
                    Console.ReadLine();
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