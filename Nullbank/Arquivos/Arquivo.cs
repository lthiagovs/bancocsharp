using Nullbank.Usuarios;
using static System.Net.Mime.MediaTypeNames;

namespace Nullbank.Arquivos
{
    class Arquivo
    {

        //Variaveis estaticas referenciando os diretorios - @thiago
        private static string caminhoUsuarios = "Data\\Usuarios\\";
        private static string caminhoContas = "Data\\Contas\\";

        //Caminho para os arquivos - @thiago
        private string caminho;

        //Variaveis nescessárias para alterar arquivos - @thiago
        private Stream entrada;
        private StreamReader leitor;
        private StreamWriter escritor;

        //Construtor da classe Arquivo - @thiago
        private Arquivo(string caminho)
        {
            this.caminho = caminho;

            //Verifica se o diretorio caminhoContas existe
            if (!Directory.Exists(Arquivo.caminhoContas))
            {
                Directory.CreateDirectory(Arquivo.caminhoContas);
            }

            //Verifica se os diretorios caminhoUsuarios existe
            if (!Directory.Exists(Arquivo.caminhoUsuarios))
            {
                Directory.CreateDirectory(Arquivo.caminhoUsuarios);
                Directory.CreateDirectory(Arquivo.caminhoUsuarios+"\\Clientes");
                Directory.CreateDirectory(Arquivo.caminhoUsuarios+"\\Gerentes");
                Directory.CreateDirectory(Arquivo.caminhoUsuarios+"\\Funcionarios");
            }
        }

        //Verifica se o arquivo existe - @thiago
        private bool arquivoExiste()
        {
            if (!File.Exists(this.caminho))
            {
                return false;
            }
            return true;
        }

        //Cria um arquivo no caminho especificado - @thiago
        private void criaArquivo()
        {
            try
            {
                this.entrada = File.Open(caminho, FileMode.Create);
                this.entrada.Close();
            }
            catch
            {
                throw new Exception("Falha ao criar arquivo.");
            }
        }

        //Apaga um arquivo no caminho especificado - @thiago
        private bool deletaArquivo()
        {
            if (this.arquivoExiste())
            {
                try
                {
                    File.Delete(this.caminho);
                    return true;
                }
                catch
                {
                    throw new Exception("Falha ao apagar arquivo.");
                }
            }
            return false;
        }

        //Abre um arquivo para leitura/escrita - @thiago
        private bool abrirArquivo()
        {
            if (this.arquivoExiste())
            {
                try
                {
                    this.entrada = File.Open(this.caminho, FileMode.Open);
                    return true;
                }
                catch
                {
                    throw new Exception("Não foi possivel acessar o arquivo.");
                }
            }
            return false;
        }

        //Encerra o arquivo - @thiago
        private void fecharArquivo()
        {
            this.entrada.Close();
        }

        //Escreve dados em um arquivo - @thiago
        private void escreverArquivo(IList<string> dadosLista)
        {
            try
            {
                this.criaArquivo();
                this.abrirArquivo();
                this.escritor = new StreamWriter(this.entrada);

                foreach (string dado in dadosLista)
                {
                    escritor.Write(dado + ",");
                }

                this.escritor.Close();
            }
            catch
            {
                throw new Exception("Não foi possivel realizar a escrita.");
            }
            finally
            {
                    
                this.fecharArquivo();
            }
        }

        //Lê dados de um arquivo - @thiago
        private List<string> lerArquivo()
        {

            List<string> data = new List<string>();

            if (this.abrirArquivo())
            {
                try
                {
                    this.leitor = new StreamReader(this.entrada);
                    var info = leitor.ReadLine();
                    if(info != null) {
                        data = info.ToString().Split(',').ToList();
                    }

                    this.leitor.Close();

                }
                catch
                {
                    throw new Exception("Falha ao tentar ler o arquivo.");
                }
                finally
                {
                    this.fecharArquivo();
                }
            }

            return data;
        }

        //Cria o arquivo de uma conta - @thiago
        public static void criarArquivoConta(Conta conta)
        {
            try
            {
                Arquivo aConta = new Arquivo(Arquivo.caminhoContas + conta.NumeroConta + ".data");
                List<string> dadosLista = new List<string>();

                dadosLista.Add(conta.Usuario);
                dadosLista.Add(conta.Saldo.ToString());
                dadosLista.Add(conta.idade.ToString());
                dadosLista.Add(conta.NumeroConta.ToString());
                dadosLista.Add(conta.senha);

                aConta.escreverArquivo(dadosLista);


            }
            catch
            {
                throw new Exception("Falha ao criar conta.");
            }
        }

        //Cria o arquivo de um cliente - @thiago
        public static void criaArquivoUsuario(Usuario usuario)
        {
            try
            {

                //Selecionar diretorio de usuario correto de acordo com o tipo do usuario
                string path;
                switch (usuario)
                {
                    case Cliente:
                        path = "Clientes\\";
                        break;
                    case Gerente:
                        path = "Funcionarios\\";
                        break;
                    case Funcionario:
                        path = "Gerentes\\";
                        break;
                    default:
                        path = "";
                        break;
                }


                Arquivo aConta = new Arquivo(Arquivo.caminhoUsuarios + path + usuario.nome + ".data");
                List<string> dadosLista = new List<string>();

                dadosLista.Add(usuario.nome);
                dadosLista.Add(usuario.agencia.ToString());
                dadosLista.Add(usuario.senha);
                dadosLista.Add(usuario.cpf);
                dadosLista.Add(usuario.EndUsuario);

                aConta.escreverArquivo(dadosLista);


            }
            catch
            {
                throw new Exception("Falha ao criar arquivo de usuario.");
            }
        }

        //Procura uma conta nos arquivos - @thiago
        public static Conta buscaConta(int numero)
        {
            Arquivo contaArquivo = new Arquivo(Arquivo.caminhoContas+numero+".data");
            Conta_corrente conta = new Conta_corrente(0, "", 0, 0);

            if (contaArquivo.arquivoExiste())
            {
                List<string> infoConta = contaArquivo.lerArquivo();
                conta = new Conta_corrente(numero, infoConta[0], 1,0);
                conta.senha = infoConta[4];

            }
            return conta;
        }

        //Procura um usuario nos arquivos - @thiago
        public static Usuario buscaUsuario(Usuario usuario)
        {
            Usuario user = new Cliente("","",new Endereço("",0,"","",""),0,"");
            string path = "";

            //Procurar diretorio por tipo de usuario
            switch (usuario)
            {
                case Cliente:
                    path = "Clientes\\";
                    break;
                case Gerente:
                    path = "Gerentes\\";
                    break;
                case Funcionario:
                    path = "Funcionarios\\";
                    break;
            }

            Arquivo userAqr = new Arquivo(Arquivo.caminhoUsuarios+path+usuario.nome+".data");
            if (userAqr.arquivoExiste())
            {
                List<string> infos = userAqr.lerArquivo();
                Endereço end = new Endereço(infos[8], int.Parse(infos[7]), infos[6], infos[5], infos[4]);
                user = new Cliente(infos[0], infos[3], end, int.Parse(infos[1]), infos[2]);
            }


            return user;

        }


    }
}
