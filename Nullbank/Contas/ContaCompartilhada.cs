using Nullbank.Usuarios;

namespace Nullbank.Contas
{
    class ContaCompartilhada : Conta
    {
        public List<string> usuariosSecundarios;

        public ContaCompartilhada(int numeroConta, Cliente titular, double saldoInicial, List<string> UsuariosSecundarios, string senha) : base(numeroConta, titular, saldoInicial, senha)
        {
            this.numeroConta = numeroConta;
            this.usuariosSecundarios = UsuariosSecundarios;
        }

        public override void Sacar(double valor)
        {
            if (valor > 0)
            {
                if (saldo >= valor)
                {
                    saldo -= valor;
                    string transacao = $"Saque de R${valor} (conta compartilhada)";
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
        public override void Transferir(Conta contaDestino, double valor)
        {
            if (valor > 0)
            {
                if (saldo >= valor)
                {
                    saldo -= valor;
                    contaDestino.saldo += valor;

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.numeroConta} (conta compartilhada)";
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
    }
}
