using Nullbank.Contas;
using Nullbank.Arquivos;
using Nullbank.Usuarios;
class Program
{

    //Leitura segura no console
    public static string lerLinha(string mensagem)
    {
        Console.Write(mensagem);
        var leitura = Console.ReadLine();
        
        if(leitura is string)
        {
            return leitura.ToString();
        }

        return "";
    }

    //Leitura de digitos segura no console
    public static int lerDigito(string mensagem)
    {
        int digito = -1;

        do
        {
            string leitura = lerLinha(mensagem);
            try
            {
                digito = int.Parse(leitura);
            }
            catch
            {
                Console.WriteLine("Valor não numerico ou menor que zero! Digite novamente...");
                digito = -1;
            }

        } while (digito == -1 || digito < 0);

        return digito;
    }

    //Cria um formulario de cadastro de funcionario no console
    public static Funcionario cadastrarFuncionario()
    {
        string cpf = lerLinha("CPF >> ");
        string nome = lerLinha("NOME >> ");
        string cep = lerLinha("CEP >> ");
        int num = lerDigito("NUMERO >> ");
        string rua = lerLinha("RUA >> ");
        string cidade = lerLinha("CIDADE >> ");
        string estado = lerLinha("ESTADO >> ");
        int agencia = lerDigito("AGENCIA >> ");
        string senha = lerLinha("SENHA >> ");
        Endereço funcionarioEndereço = new Endereço(cep, num, rua, cidade, estado);
        Funcionario nFuncionario = new Funcionario(nome, cpf, funcionarioEndereço, agencia, senha);

        return nFuncionario;
    }

    //Cria um formulario de cadastro de Cliente no console
    public static Cliente cadastrarCliente()
    {
        string cpf = lerLinha("CPF >> ");
        string nome = lerLinha("NOME >> ");
        string cep = lerLinha("CEP >> ");
        int num = lerDigito("NUMERO >> ");
        string rua = lerLinha("RUA >> ");
        string cidade = lerLinha("CIDADE >> ");
        string estado = lerLinha("ESTADO >> ");
        int agencia = lerDigito("AGENCIA >> ");
        string senha = lerLinha("SENHA >> ");
        Endereço funcionarioEndereço = new Endereço(cep, num, rua, cidade, estado);
        Cliente nCliente = new Cliente(nome, cpf, funcionarioEndereço, agencia, senha);

        return nCliente;
    }

    //Função Principal
    public static void Main()
    {

        bool rodar = true;
        bool logado = false;
        bool acessaConta = false;
        Usuario usuario = new Cliente();
        Conta conta = new ContaCorrente();
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
                        int funcEscolha = lerDigito(" >> ");
                        switch (funcEscolha)
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
                                string nomeFuncionario = lerLinha("NOME >> ");

                                Usuario funcBusca = new Funcionario(nomeFuncionario, "", new Endereço(), 1, "");
                                funcBusca = Arquivo.buscaUsuario(funcBusca);

                                if (!funcBusca.cpf.Equals(""))
                                {
                                    Console.WriteLine("\nFuncionario encontrado: ");
                                    Console.WriteLine("Nome: " + funcBusca.nome);
                                    Console.WriteLine("CPF:" + funcBusca.cpf);
                                    Console.WriteLine("Agencia: " + funcBusca.agencia);
                                    if (funcEscolha == 3)
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
                            switch (lerDigito(" >> "))
                            {
                                case 0:
                                    Console.WriteLine("LogOut efetuado!");
                                    logado = false;
                                    break;

                                case 1:

                                    int numeroConta = lerDigito("NUMERO >> ");
                                    Conta buscaConta = new ContaCorrente(numeroConta,new Cliente(),0,0,"");
                                    string senha = lerLinha("SENHA >> ");
                                    Console.WriteLine("1 - Corrente\n2 - Poupança\n3 - Compartilhada\n");
                                    int tipoLogin = lerDigito(" >> ");

                                    switch (tipoLogin)
                                    {

                                        case 2:
                                            buscaConta = new ContaPoupanca(numeroConta, new Cliente(), 0, 0, "");
                                            break;
                                        case 3:
                                            buscaConta = new ContaCompartilhada(numeroConta, new Cliente(), 0, new List<string>(), "");
                                            break;
                                        default:
                                            buscaConta = new ContaCorrente(numeroConta, new Cliente(), 0, 0, "");
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
                            Console.WriteLine("Saldo da conta: R$"+conta.saldo+"\n0 - Sair\n1 - Sacar\n2 - Depositar\n3 - Transferir");

                            switch (lerDigito(" >> "))
                            {
                                case 0:
                                    Arquivo.alteraConta(Arquivo.buscaConta(conta), conta);
                                    acessaConta = false;
                                    break;
                                case 1:
                                    Console.WriteLine("Digite o valor que deseja sacar: ");
                                    conta.Sacar(lerDigito(" >> "));
                                    break;
                                case 2:
                                    Console.WriteLine("Digite o valor que deseja Depositar: ");
                                    conta.Depositar(lerDigito(" >> "));
                                    break;
                                case 3:
                                    Console.WriteLine("Digite o valor que deseja transferir: ");
                                    conta.Transferir(new ContaCorrente(), lerDigito(" >> "));
                                    break;
                                default:
                                    break;

                            }
                            Console.ReadLine();

                        }
                        Console.Clear();
                        break;

                    //Opções do Funcionario
                    case 2:
                        Console.WriteLine("0 - Sair\n1 - Criar Cliente\n2 - Buscar cliente\n3 - Editar cliente\n4 - Criar Conta");
                        int selec = lerDigito(" >> ");
                        switch (selec)
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
                                string nomeFuncionario = lerLinha("NOME >> ");

                                Usuario clienteBusca = new Cliente(nomeFuncionario, "", new Endereço(), 1, "");
                                clienteBusca = Arquivo.buscaUsuario(clienteBusca);

                                if (!clienteBusca.cpf.Equals(""))
                                {
                                    Console.WriteLine("\nCliente encontrado: ");
                                    Console.WriteLine("Nome: " + clienteBusca.nome);
                                    Console.WriteLine("CPF:" + clienteBusca.cpf);
                                    Console.WriteLine("Agencia: " + clienteBusca.agencia);
                                    if (selec == 3)
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
                                string nomeConta = lerLinha("NOME DO TITULAR >> ");
                                Cliente? usuarioConta = new Cliente(nomeConta,"",new Endereço(),0,"");
                                usuarioConta = Arquivo.buscaUsuario(usuarioConta) as Cliente;

                                if (usuarioConta != null)
                                {
                                    if (!usuarioConta.cpf.Equals("") && usuarioConta is Cliente)
                                    {
                                        Console.WriteLine("Titular de cpf: " + usuarioConta.cpf + " encontrado!");
                                        string contaSenha = lerLinha("SENHA >> ");
                                        Console.WriteLine("1 - Corrente\n2 - Poupança\n3 - Compartilhada\n");
                                        int tipoConta = lerDigito("TIPO >> ");
                                        Conta contaCriar = new ContaCorrente();

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
                                                ContaCompartilhada? contaCriarCompartilhada = contaCriar as ContaCompartilhada;
                                                if (contaCriarCompartilhada != null)
                                                {
                                                    do
                                                    {
                                                        Console.WriteLine("0 - Finalizar\nDigite o nome de um dos titulares: ");
                                                        escolha = lerLinha(" >> ");

                                                        Usuario titularBusca = new Cliente(escolha, "", new Endereço(), 0, "");
                                                        titularBusca = Arquivo.buscaUsuario(titularBusca);

                                                        if (!escolha.Equals("0"))
                                                        {
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
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Erro ao criar conta compartilhada...");
                                                }
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
                escolha = lerLinha("Opção >> ");
                if (int.Parse(escolha) != 0)
                {
                    string login = lerLinha("Login >> ");
                    string senha = lerLinha("Senha >> ");
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