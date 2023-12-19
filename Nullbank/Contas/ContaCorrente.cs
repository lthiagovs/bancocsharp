using Nullbank.Usuarios;

namespace Nullbank.Contas
{
    class ContaCorrente : Conta
    {
        public double limitecredito;
        public ContaCorrente(int numeroConta, Cliente titular, double saldoInicial, double limitecredito, string senha) : base(numeroConta, titular, saldoInicial, senha)
        {

            this.limitecredito = limitecredito;
        }
        
        //Permite criar uma conta corrente vazia
        public ContaCorrente()
        {

        }

        public new void ConsultarSaldo()
        {
            Console.WriteLine($"saldo da conta corrente {numeroConta} de {titular.nome}: R${saldo}, Limite de Crédito: R${limitecredito}");
        }
        public override void Sacar(double valor)
        {
            if (valor > 0)
            {
                double valorTotal = saldo + limitecredito;
                if (valorTotal >= valor)
                {
                    saldo -= valor;
                    string transacao = $"Saque de R${valor} (conta corrente)";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("saldo e limite de crédito insuficientes para realizar o saque.");
                }
            }
            else
            {
                Console.WriteLine("O valor do saque deve ser maior que zero.");
            }
        }
        public override void Transferir(Conta contaDestino, double valor)
        {
            if (valor > 0)
            {
                double valorTotal = saldo + limitecredito;
                if (valorTotal >= valor)
                {
                    saldo -= valor;
                    contaDestino.saldo += valor;

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.numeroConta} (conta corrente)";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("saldo e limite de crédito insuficientes para realizar a transferência.");
                }
            }
            else
            {
                Console.WriteLine("O valor da transferência deve ser maior que zero.");
            }
        }
    }
}
