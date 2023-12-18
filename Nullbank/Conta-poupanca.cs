using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullbank
{
    internal class Conta_poupanca : Conta
    {
        public double TaxaRendimento { get; }

        public Conta_poupanca(int NumeroConta, string Usuario, double saldoInicial, double TaxaRendimento): base(NumeroConta, Usuario, saldoInicial)
        {
            this.TaxaRendimento = TaxaRendimento;
        }

        public new void ConsultarSaldo()
        {
            Console.WriteLine($"Saldo da conta poupança {NumeroConta} de {Usuario}: R${Saldo}, Taxa de Rendimento: {TaxaRendimento}%");
        }
            
    }
}
