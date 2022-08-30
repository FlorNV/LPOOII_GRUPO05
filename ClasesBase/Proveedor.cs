using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Proveedor
    {
        private string prov_CUIT;
        public string Prov_CUIT
        {
            get { return prov_CUIT; }
            set { prov_CUIT = value; }
        }

        private string prov_RazonSocial;
        public string Prov_RazonSocial
        {
            get { return prov_RazonSocial; }
            set { prov_RazonSocial = value; }
        }

        private string prov_Domicilio;
        public string Prov_Domicilio
        {
            get { return prov_Domicilio; }
            set { prov_Domicilio = value; }
        }

        private string prov_Telefono;
        public string Prov_Telefono
        {
            get { return prov_Telefono; }
            set { prov_Telefono = value; }
        }
    }
}
