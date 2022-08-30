using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Producto
    {
        private string prod_CodProducto;
        public string Prod_CodProducto
        {
            get { return prod_CodProducto; }
            set { prod_CodProducto = value; }
        }

        private string prod_Categoria;
        public string Prod_Categoria
        {
            get { return prod_Categoria; }
            set { prod_Categoria = value; }
        }

        private string prod_Color;
        public string Prod_Color
        {
            get { return prod_Color; }
            set { prod_Color = value; }
        }

        private string prod_Descripcion;
        public string Prod_Descripcion
        {
            get { return prod_Descripcion; }
            set { prod_Descripcion = value; }
        }

        private decimal prod_Precio;
        public decimal Prod_Precio
        {
            get { return prod_Precio; }
            set { prod_Precio = value; }
        }
    }
}
