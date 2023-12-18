using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullbank
{
    abstract class Conta
    {
        public int NumeroConta;
        public string Usuario;
        public double Saldo;
        public List<string> historico;
        public int idade;
        public string senha;
        public int cpf;

        public Conta(int numeroConta, string titular, double saldoInicial)
        {
            NumeroConta = numeroConta;
            Usuario = titular;
            Saldo = saldoInicial;
            historico = new List<string>();

        }

        public void ConsultarSaldo()
        {
            Console.WriteLine($"Saldo da conta {NumeroConta} de {Usuario}: {Saldo} reais");
        }
        public virtual void Depositar(double valor)
        {
            if (valor > 0)
            {
                Saldo += valor;
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
                if (Saldo >= valor)
                {
                    Saldo -= valor;
                    string transacao = $"Saque de R${valor}";
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
        public virtual void Transferir(Conta contaDestino, double valor)
        {
            if (valor > 0)
            {
                if (Saldo >= valor)
                {
                    Saldo -= valor;
                    contaDestino.Saldo += valor;

                    string transacao = $"Transferência de R${valor} para a conta {contaDestino.NumeroConta}";
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
