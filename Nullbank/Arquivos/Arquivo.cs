﻿using Nullbank.Usuarios;
using Nullbank.Contas;
using System.ComponentModel;

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
        private Stream? entrada;
        private StreamReader? leitor;
        private StreamWriter? escritor;

        //Construtor da classe Arquivo - @thiago
        private Arquivo(string caminho)
        {
            this.caminho = caminho;

            //Verifica se o diretorio caminhoContas existe
            if (!Directory.Exists(Arquivo.caminhoContas))
            {
                Directory.CreateDirectory(Arquivo.caminhoContas);
                Directory.CreateDirectory(Arquivo.caminhoContas+"\\Corrente");
                Directory.CreateDirectory(Arquivo.caminhoContas+"\\Compartilhada");
                Directory.CreateDirectory(Arquivo.caminhoContas+"\\Poupanca");
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
            if(this.entrada!=null) this.entrada.Close();
        }

        //Escreve dados em um arquivo - @thiago
        private void escreverArquivo(IList<string> dadosLista)
        {
            try
            {
                this.criaArquivo();
                this.abrirArquivo();

                if (this.entrada != null)
                {
                    this.escritor = new StreamWriter(this.entrada);
                }
                else
                {
                    throw new Exception("Não foi possivel realizar a escrita.");
                }

                if (this.escritor != null)
                {
                    foreach (string dado in dadosLista)
                    {
                        escritor.Write(dado + ",");
                    }

                    this.escritor.Close();
                }
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

            if (this.abrirArquivo() && this.entrada != null)
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
        public static void criaArquivoConta(Conta conta)
        {
            try
            {

                string path = "";
                switch (conta)
                {
                    case ContaCompartilhada:
                        path = "Compartilhada\\";
                        break;
                    case ContaCorrente:
                        path = "Corrente\\";
                        break;
                    case ContaPoupanca:
                        path = "Poupanca\\";
                        break;
                }

                Arquivo aConta = new Arquivo(Arquivo.caminhoContas + path + conta.numeroConta + ".data");
                List<string> dadosLista = new List<string>();

                dadosLista.Add(conta.titular.nome);
                dadosLista.Add(conta.saldo.ToString());
                dadosLista.Add(conta.idade.ToString());
                dadosLista.Add(conta.numeroConta.ToString());
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
                switch(usuario)
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
        public static Conta buscaConta(Conta conta)
        {
            string path = "";
            Conta contaBusca;

            switch (conta)
            {

                case ContaCompartilhada:
                    contaBusca = new ContaCompartilhada(-1, new Cliente(), 0, new List<string>(), "");
                    path = "Compartilhada\\";
                    break;
                case ContaCorrente:
                    contaBusca = new ContaCorrente(-1, new Cliente(), 0, 0, "");
                    path = "Corrente\\";
                    break;
                case ContaPoupanca:
                    contaBusca = new ContaPoupanca(-1, new Cliente(), 0, 0, "");
                    path = "Poupanca\\";
                    break;
                default:
                    contaBusca = new ContaCorrente(-1, new Cliente(), 0, 0, "");
                    path = "Corrente\\";
                    break;

            }

            Arquivo contaArquivo = new Arquivo(Arquivo.caminhoContas+path+conta.numeroConta+".data");

            if (contaArquivo.arquivoExiste())
            {
                List<string> infoConta = contaArquivo.lerArquivo();
                Cliente? buscaUsuario = new Cliente(infoConta[0],"",new Endereço(),0,"");

                buscaUsuario = Arquivo.buscaUsuario(buscaUsuario) as Cliente;

                if (buscaUsuario != null)
                {
                    switch (conta)
                    {

                        case ContaCompartilhada:
                            contaBusca = new ContaCompartilhada(int.Parse(infoConta[3]), buscaUsuario, double.Parse(infoConta[1]), new List<string>(), infoConta[4]);
                            path = "Compartilhada\\";
                            break;
                        case ContaCorrente:
                            contaBusca = new ContaCorrente(int.Parse(infoConta[3]), buscaUsuario, double.Parse(infoConta[1]), 0, infoConta[4]);
                            path = "Corrente\\";
                            break;
                        case ContaPoupanca:
                            contaBusca = new ContaPoupanca(int.Parse(infoConta[3]), buscaUsuario, double.Parse(infoConta[1]), 0, infoConta[4]);
                            path = "Poupanca\\";
                            break;
                        default:
                            contaBusca = new ContaCorrente(int.Parse(infoConta[3]), buscaUsuario, double.Parse(infoConta[1]), 0, infoConta[4]);
                            path = "Corrente\\";
                            break;

                    }
                }

            }
            return contaBusca;
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

        //Altera as informações de um usuario - @thiago
        public static bool alteraUsuario(Usuario alterar, Usuario alterado)
        {

            if (!Arquivo.buscaUsuario(alterar).cpf.Equals(""))
            {
                Usuario nAlterado = alterado;
                nAlterado.nome = alterar.nome;
                Arquivo.criaArquivoUsuario(nAlterado);
                return true;
            }
            return false;

        }

        //Altera as informações de uma conta - @thiago
        public static bool alteraConta(Conta alterar, Conta alterado)
        {

            if (Arquivo.buscaConta(alterar).numeroConta!=-1)
            {
                Conta nAlterado = alterado;
                nAlterado.numeroConta = alterar.numeroConta;
                Arquivo.criaArquivoConta(nAlterado);
                return true;
            }
            return false;
        }



    }
}
