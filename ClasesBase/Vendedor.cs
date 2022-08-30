using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Vendedor
    {
        private string vend_Legajo;
        public string Vend_Legajo
        {
            get { return vend_Legajo; }
            set { vend_Legajo = value; }
        }

        private string vend_Apellido;
        public string Vend_Apellido
        {
            get { return vend_Apellido; }
            set { vend_Apellido = value; }
        }

        private string vend_Nombre;
        public string Vend_Nombre
        {
            get { return vend_Nombre; }
            set { vend_Nombre = value; }
        }
    }
}
