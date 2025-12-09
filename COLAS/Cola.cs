using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COLAS
{
    internal class Cola
    {
        private Nodo cp;
        public Cola() { cp = new Nodo(); }

        public Nodo BuscarUltimo(){
            return BuscarUltimoRecursivo(cp.Siguiente);
        }

        public Nodo BuscarUltimoRecursivo(Nodo actual){
            Nodo ultimo = actual;
            if(ultimo.Siguiente != null)
            {
                ultimo = BuscarUltimoRecursivo(ultimo.Siguiente);
            }
            return ultimo;
        }

        public void Encolar(Nodo venta) {
            if (cp.Siguiente == null)
                cp.Siguiente = venta;
            else
                BuscarUltimo().Siguiente = venta;
        }

        public Nodo Desencolar()
        {
            Nodo eliminado = null;
            if(cp.Siguiente != null) {
                eliminado = cp.Siguiente;
                cp.Siguiente = eliminado.Siguiente;
                eliminado.Siguiente = null;
            }
            return eliminado;
        }

        public Nodo Ver()
        {
            Nodo actual = null;

            if (cp.Siguiente != null)
            {
                actual = cp.Siguiente;
            }

            return actual;
            //En lugar de escribir todo esto para el metodo tambien se puede hacer esto
            //tambien se puede escribir return cp.Siguiente; 
        }
    }
}
