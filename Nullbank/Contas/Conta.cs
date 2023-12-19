using Nullbank.Usuarios;

namespace Nullbank.Contas
{
    abstract class Conta
    {
        public int numeroConta;
        public Cliente titular;
        public double saldo;
        public List<string> historico;
        public int idade;
        public string senha;
        public int cpf;
        public static int totalContas = 0;

        public Conta(int numeroConta, Cliente titular, double saldoInicial, string senha)
        {
            this.numeroConta = numeroConta;
            this.titular = titular;
            this.saldo = saldoInicial;
            this.historico = new List<string>();

            Conta.totalContas++;

        }

        public void ConsultarSaldo()
        {
            Console.WriteLine($"saldo da conta {numeroConta} de {titular.nome}: {saldo} reais");
        }
        public virtual void Depositar(double valor)
        {
            if (valor > 0)
            {
                AplicarTaxa();
                saldo += valor;
                string transacao = $"Depósito de R${valor}";
                historico.Add(transacao);
                Console.WriteLine(transacao);
            }
            else
            {
                Console.WriteLine("O valor do depósito deve ser maior que zero.");
            };
        }
        public virtual void Sacar(double valor)
        {
            if (valor > 0)
            {
                if (saldo >= valor)
                {
                    AplicarTaxa();
                    saldo -= valor;
                    string transacao = $"Saque de R${valor}";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("saldo insuficiente para realizar o saque.");
                }
            }
            else
            {
                Console.WriteLine("O valor do saque deve ser maior que zero.");
            }
        }
        public virtual void Transferir(Conta contaDestino, double valor)
        {
            if (valor > 0)
            {
                if (saldo >= valor)
                {
                    AplicarTaxa();
                    saldo -= valor;
                    contaDestino.saldo += valor;

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.numeroConta}";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("saldo insuficiente para realizar a transferência.");
                }
            }
            else
            {
                Console.WriteLine("O valor da transferência deve ser maior que zero.");
            }
        }
        protected virtual void AplicarTaxa()
        {
          
        }

        protected void AdicionarHistorico(string transacao)
        {
            historico.Add(transacao);
        }

        public void ExibirHistorico()
        {
            Console.WriteLine($"Histórico de transações da conta {numeroConta} de {titular.nome}:");
            foreach (var transacao in historico)
            {
                Console.WriteLine(transacao);
            }
        }
    }
}
