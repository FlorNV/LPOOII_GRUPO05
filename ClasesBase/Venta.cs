using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace ClasesBase {
    public class Venta : IDataErrorInfo {
        private int nroFactura;

        public int NroFactura {
            get { return nroFactura; }
            set { nroFactura = value; }
        }
        private DateTime fechaFactura;

        public DateTime FechaFactura {
            get { return fechaFactura; }
            set { fechaFactura = value; }
        }
        private string legajo;

        public string Legajo {
            get { return legajo; }
            set { legajo = value; }
        }
        private string dni;

        public string DNI {
            get { return dni; }
            set { dni = value; }
        }
        private string codProducto;

        public string CodProducto {
            get { return codProducto; }
            set { codProducto = value; }
        }
        private decimal precio;

        public decimal Precio {
            get { return precio; }
            set { precio = value; }
        }
        private int cantidad;

        public int Cantidad {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private decimal importe;

        public decimal Importe {
            get { return importe; }
            set { importe = value; }
        }

        public string isValid() {
            var allProperties = GetType().GetProperties();
            foreach (var property in allProperties) {
                string error = this[property.Name];
                if (error != null) return error;
            }
            return null;
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName] {
            get {
                string result = null;
                if (columnName == "FechaFactura") {
                    if (String.IsNullOrEmpty(FechaFactura.ToString())) {
                        result = "Campo requerido.";
                    } else if(DateTime.Compare(FechaFactura, DateTime.Now) < 0) {
                        result = "Fecha no válida";
                    }
                    /*
                    else if (CodProducto.Length < 3) {
                        result = "Debe tener al menos 3 letras";
                    }*/
                } else if (columnName == "Legajo") {
                    if (String.IsNullOrEmpty(Legajo)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "DNI") {
                    if (String.IsNullOrEmpty(DNI)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "CodProducto") {
                    if (String.IsNullOrEmpty(CodProducto)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Cantidad") {
                    try {
                        int num;
                        if (Cantidad == 0 || string.IsNullOrEmpty(Cantidad.ToString())) {
                            result = "Campo requerido.";
                        } else if (!int.TryParse(Cantidad.ToString(), out num)) {
                            result = "Debe ingresar un número";
                        } else if (Cantidad < 1) {
                            result = "Debe ser mayor 1";
                        }
                    } catch (FormatException e) {
                        throw new FormatException("Debe ingresar un número");
                    }
                }

                return result;
            }
        }
    }
}
