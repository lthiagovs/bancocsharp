using System.Net.Http.Headers;
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
                    case Funcionario:
                        Console.WriteLine("0 - sair\n1 - 1 criar Cliente\n2 - 2 Buscar cliente\n3 - 3 Editar cliente");
                        escolha = Console.ReadLine();
                        switch (int.Parse(escolha)) 
                        {
                            case 0:
                                logado=false ;
                                break;
                            case 1:
                                Console.WriteLine("Criando novo cliente...");

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

                                Endereço clienteEndereço = new Endereço(cep, num, rua, cidade, estado);

                                Cliente novoCliente = new Cliente( cpf, nome, clienteEndereço ,agencia,senha);

                                Arquivo.criaArquivoUsuario(novoCliente);
                                break; 
                            case 2:
                                Console.WriteLine("Buscando cliente...");
                                Console.Write("CPF do Cliente: ");
                                string cpf = Console.ReadLine();
                                Cliente clienteEncontrado = new Cliente(cpf, nome, clienteEndereço, agencia, senha);
                                clienteEncontrado = Arquivo.criaArquivoUsuario(clienteEncontrado);
                                if (clienteEncontrado.cpf.Equals(""))
                                {
                                    Console.WriteLine($"Cliente encontrado:\nNome: {clienteEncontrado.nome}\nCPF: {clienteEncontrado.cpf}\nEndereço: {clienteEncontrado.clienteEndereço}\nAgencia: {clienteEncontrado.agencia}\nsenha: {clienteEncontrado.senha}");
                                }
                                else
                                {
                                    Console.WriteLine("Cliente não encontrado.");
                                }
                        }
                        break; 
                            case 3:
                        Console.WriteLine("Editando cliente...");

                       
                        Console.Write("CPF do Cliente: ");
                        string cpf = Console.ReadLine();

                        
                        Cliente clienteParaEditar = new Cliente(cpf, nome, clienteEndereço, agencia, senha);

                       
                        clienteParaEditar = Arquivo.buscaCliente(clienteParaEditar);

                       
                        if (!clienteParaEditar.cpf.Equals(""))
                        {
                            
                            
                            Console.Write("editar CPF >> ");
                            clienteParaEditar.cpf = Console.ReadLine();

                            Console.Write(" editar NOME >> ");
                            clienteParaEditar.nome = Console.ReadLine();

                            Console.Write("editar CEP >> ");
                            clienteParaEditar.cep = Console.ReadLine();

                            Console.Write("editar NUMERO >> ");
                            clienteParaEditar.num = int.Parse(Console.ReadLine());

                            Console.Write("editar RUA >> ");
                            clienteParaEditar.rua = Console.ReadLine();

                            Console.Write("editar CIDADE >> ");
                            clienteParaEditar.cidade = Console.ReadLine();

                            Console.Write("editar ESTADO >> ");
                            clienteParaEditar.estado = Console.ReadLine();

                            Console.Write("editar AGENCIA >> ");
                            clienteParaEditar.agencia = int.Parse(Console.ReadLine());

                            Console.Write("editar SENHA >> ");
                            clienteParaEditar.senha = Console.ReadLine();


                            Arquivo.salvarCliente(clienteParaEditar);

                            Console.WriteLine("Cliente editado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Cliente não encontrado. Não é possível editar.");
                        }
                        break;
                            default: Console.WriteLine("opção invalida");
                                break;
                        }
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

