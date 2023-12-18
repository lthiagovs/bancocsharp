using Nullbank.Usuarios;

namespace Nullbank.Contas
{
    internal class ContaCompartilhada : Conta
    {
        public List<string> UsuariosSecundarios { get; }

        public ContaCompartilhada(int numeroConta, Cliente titular, double saldoInicial, List<string> UsuariosSecundarios) : base(numeroConta, titular, saldoInicial)
        {
            this.numeroConta = numeroConta;
            this.UsuariosSecundarios = UsuariosSecundarios;
        }

        public override void Sacar(double valor)
        {
            if (valor > 0)
            {
                if (Saldo >= valor)
                {
                    Saldo -= valor;
                    string transacao = $"Saque de R${valor} (conta compartilhada)";
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

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.numeroConta} (conta compartilhada)";
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
