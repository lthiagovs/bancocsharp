namespace Nullbank.Arquivos
{
    class Arquivo
    {

        //Varias estaticas referenciando
        public static string caminhoUsuarios = "Data\\Usuarios\\";
        public static string caminhoContas = "Data\\Contas\\";

        //Caminho para os arquivos
        public string caminho;

        //Variaveis nescessárias para alterar arquivos.
        private Stream entrada;
        private StreamReader leitor;
        private StreamWriter escritor;

        //Construtor da classe Arquivo
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

        //Verifica se o arquivo existe
        private bool arquivoExiste()
        {
            if (!File.Exists(this.caminho))
            {
                return false;
            }
            return true;
        }

        //Cria um arquivo no caminho especificado
        public bool criaArquivo()
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

        //Apaga um arquivo no caminho especificado
        public bool deletaArquivo()
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

        //Abre um arquivo para leitura/escrita
        public bool abrirArquivo()
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

        //Encerra o arquivo
        public void fecharArquivo()
        {
            this.entrada.Close();
        }

    }
}
