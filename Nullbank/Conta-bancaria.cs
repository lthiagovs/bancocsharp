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
        public string Titular;
        public double Saldo;
        public List<string> historico;
        public int idade;

        public Conta(int numeroConta, string titular, double saldoInicial)
        {
            NumeroConta = numeroConta;
            Titular = titular;
            Saldo = saldoInicial;
            historico = new List<string>();

        }
        public void ConsultarSaldo()
        {
            Console.WriteLine($"Saldo da conta {NumeroConta} de {Titular}: {Saldo} reais");
        }








    }
}
