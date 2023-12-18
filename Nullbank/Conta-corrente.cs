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
    }
}
