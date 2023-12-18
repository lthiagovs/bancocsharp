using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nullbank
{
    internal class Conta_compartilhada : Conta
    {
        public List<string> UsuariosSecundarios { get; }

        public Conta_compartilhada(int NumeroConta, string Usuario, double saldoInicial, List<string> UsuariosSecundarios) : base(NumeroConta, Usuario, saldoInicial)
        {
            this.UsuariosSecundarios = UsuariosSecundarios;
        }
    }
}
