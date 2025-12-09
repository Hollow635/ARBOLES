using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLAS
{
    internal class Nodo:ICloneable
    {
        public Nodo (string codigo = "", decimal importe = 0, Nodo siguiente = null) {
            codVendedor = codigo;
            impVenta = importe;
            Siguiente = siguiente;
        }

        public string codVendedor { get; set; }   
        public decimal impVenta { get; set; }
        public Nodo Siguiente { get; set; }

        public Object Clone()
        {
            Nodo copia = (Nodo)this.MemberwiseClone();
            copia.Siguiente = null;
            return null;
        }

        public Nodo CloneTipado() => (Nodo)this.Clone();
    }
}
