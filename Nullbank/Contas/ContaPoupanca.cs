using Nullbank.Usuarios;

namespace Nullbank.Contas
{
    internal class ContaPoupanca : Conta
    {
        public double TaxaRendimento { get; }

        public ContaPoupanca(int numeroConta, Cliente titular, double saldoInicial, double TaxaRendimento) : base(numeroConta, titular, saldoInicial)
        {
            this.numeroConta = numeroConta;
            this.TaxaRendimento = TaxaRendimento;
        }

        public new void ConsultarSaldo()
        {
            Console.WriteLine($"Saldo da conta poupança {numeroConta} de {titular.nome}: R${Saldo}, Taxa de Rendimento: {TaxaRendimento}%");
        }
        public override void Sacar(double valor)
        {
            if (valor > 0)
            {
                if (Saldo >= valor)
                {
                    Saldo -= valor;
                    string transacao = $"Saque de R${valor} (conta poupança)";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente para realizar o saque.");
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
                if (Saldo >= valor)
                {
                    Saldo -= valor;
                    contaDestino.Saldo += valor;

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.numeroConta} (conta poupança)";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("Saldo insuficiente para realizar a transferência.");
                }
            }
            else
            {
                Console.WriteLine("O valor da transferência deve ser maior que zero.");
            }
        }
    }
}
