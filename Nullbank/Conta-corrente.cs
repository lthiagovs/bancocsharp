using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullbank
{
    internal class Conta_corrente : Conta
    {
        public double limitecredito;
        public Conta_corrente(int NumeroConta, string Usuario, double saldoInicial, double limitecredito) : base(NumeroConta, Usuario, saldoInicial)
        {
            this.limitecredito = limitecredito;
        }
        public new void ConsultarSaldo()
        {
            Console.WriteLine($"Saldo da conta corrente {NumeroConta} de {Usuario}: R${Saldo}, Limite de Crédito: R${limitecredito}");
        }
        public override void Sacar(double valor)
        {
            if (valor > 0)
            {
                double valorTotal = Saldo + limitecredito;
                if (valorTotal >= valor)
                {
                    Saldo -= valor;
                    string transacao = $"Saque de R${valor} (conta corrente)";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("Saldo e limite de crédito insuficientes para realizar o saque.");
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
                double valorTotal = Saldo + limitecredito;
                if (valorTotal >= valor)
                {
                    Saldo -= valor;
                    contaDestino.Saldo += valor;

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.NumeroConta} (conta corrente)";
                    historico.Add(transacao);
                    Console.WriteLine(transacao);
                }
                else
                {
                    Console.WriteLine("Saldo e limite de crédito insuficientes para realizar a transferência.");
                }
            }
            else
            {
                Console.WriteLine("O valor da transferência deve ser maior que zero.");
            }
        }
    }
}
