using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.ComponentModel;

namespace ClasesBase {
    public class Producto : IDataErrorInfo {
        private string codProducto;

        public string CodProducto {
            get { return codProducto; }
            set { codProducto = value; }
        }
        private string categoria;

        public string Categoria {
            get { return categoria; }
            set { categoria = value; }
        }
        private string color;

        public string Color {
            get { return color; }
            set { color = value; }
        }
        private string descripcion;

        public string Descripcion {
            get { return descripcion; }
            set { descripcion = value; }
        }
        private decimal precio;

        public decimal Precio {
            get { return precio; }
            set { precio = value; }
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName] {
            get {
                string result = null;
                if (columnName == "CodProducto") { 
                    if(!String.IsNullOrEmpty(CodProducto)) {
                        result = "Debe ingresar el Código del Producto";
                    } else if (CodProducto.Length < 3) {
                        result = "Debe tener al menos 3 letras";
                    }
                } else if (columnName == "Categoria") {
                    if (String.IsNullOrEmpty(Categoria)) {
                        result = "Debe ingresar la Categoría del Producto";
                    }
                } else if (columnName == "Color") {
                    if (String.IsNullOrEmpty(Color)) {
                        result = "Debe ingresar el Color del Producto";
                    }
                } else if (columnName == "Descripcion") {
                    if (String.IsNullOrEmpty(Descripcion)) {
                        result = "Debe ingresar la Descripción del Producto";
                    }
                } 
                /*else if (columnName == "Precio") {
                    decimal num;
                    if (Precio == null) {
                        result = "Debe ingresar el Precio del Producto";
                    } else if (!decimal.TryParse(Precio.ToString(), out num)) {
                        result = "Debe ingresar un número";
                    } else if (Precio < 0) {
                        result = "El Precio debe ser mayor a $0.00";
                    }
                } */

                return result;
            }
        }
    }
}
