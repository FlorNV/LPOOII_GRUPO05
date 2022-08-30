using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Venta
    {
        private int ven_NroFactura;
        public int Ven_NroFactura
        {
            get { return ven_NroFactura; }
            set { ven_NroFactura = value; }
        }

        private DateTime ven_FechaFactura;
        public DateTime Ven_FechaFactura
        {
            get { return ven_FechaFactura; }
            set { ven_FechaFactura = value; }
        }

        private string ven_Legajo;
        public string Ven_Legajo
        {
            get { return ven_Legajo; }
            set { ven_Legajo = value; }
        }

        private string cli_DNI;
        public string Cli_DNI
        {
            get { return cli_DNI; }
            set { cli_DNI = value; }
        }

        private string prod_CodProducto;
        public string Prod_CodProducto
        {
            get { return prod_CodProducto; }
            set { prod_CodProducto = value; }
        }

        private decimal prod_Precio;
        public decimal Prod_Precio
        {
            get { return prod_Precio; }
            set { prod_Precio = value; }
        }

        private int ven_Cantidad;
        public int Ven_Cantidad
        {
            get { return ven_Cantidad; }
            set { ven_Cantidad = value; }
        }

        private decimal ven_Importe;
        public decimal Ven_Importe
        {
            get { return ven_Importe; }
            set { ven_Importe = value; }
        }
    }
}
