namespace Nullbank.Arquivos
{
    class Arquivo
    {

        //Varias estaticas referenciando - @thiago
        public static string caminhoUsuarios = "Data\\Usuarios\\";
        public static string caminhoContas = "Data\\Contas\\";

        //Caminho para os arquivos - @thiago
        public string caminho;

        //Variaveis nescessárias para alterar arquivos - @thiago
        private Stream entrada;
        private StreamReader leitor;
        private StreamWriter escritor;

        //Construtor da classe Arquivo - @thiago
        public Arquivo(string caminho)
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
        private bool criaArquivo()
        {

            if (!arquivoExiste())
            {
                try
                {
                    this.entrada = File.Open(caminho, FileMode.Create);
                    this.entrada.Close();
                    return true;
                }
                catch
                {

                }
            }
            return false;
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

        }

        //Lê dados de um arquivo - @thiago
        private IList<string> lerArquivo(string dados)
        {
            return new List<string>();
        }

        //Cria o arquivo de uma conta - @thiago
        public static bool criarArquivoConta(Conta conta)
        {
            Arquivo aConta = new Arquivo(Arquivo.caminhoContas+conta.NumeroConta+".data");
            if (!aConta.arquivoExiste())
            {
                if (aConta.criaArquivo())
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Esta conta já existe!");
            }
            return false;
        }


    }
}
