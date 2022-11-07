using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.ComponentModel;

namespace ClasesBase {
    public class Producto : IDataErrorInfo, INotifyPropertyChanged {
        private string codProducto;

        public string CodProducto {
            get { return codProducto; }
            set { 
                codProducto = value;
                this.NotifyPropertyChanged("CodProducto");
            }
        }
        private string categoria;

        public string Categoria {
            get { return categoria; }
            set { 
                categoria = value;
                this.NotifyPropertyChanged("Categoria");
            }
        }
        private string color;

        public string Color {
            get { return color; }
            set { 
                color = value;
                this.NotifyPropertyChanged("Color");
            }
        }
        private string descripcion;

        public string Descripcion {
            get { return descripcion; }
            set { 
                descripcion = value;
                this.NotifyPropertyChanged("Descripcion");
            }
        }
        private decimal precio;

        public decimal Precio {
            get { return precio; }
            set { 
                precio = value;
                this.NotifyPropertyChanged("Precio");
            }
        }

        private string imagen;

        public string Imagen
        {
            get { return imagen; }
            set
            {
                imagen = value;
                this.NotifyPropertyChanged("Imagen");
            }
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName] {
            get {
                string result = null;
                if (columnName == "CodProducto") { 
                    if(String.IsNullOrEmpty(CodProducto)) {
                        result = "Campo requerido.";
                    } /*
                    else if (CodProducto.Length < 3) {
                        result = "Debe tener al menos 3 letras";
                    }*/
                } else if (columnName == "Categoria") {
                    if (String.IsNullOrEmpty(Categoria)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Color") {
                    if (String.IsNullOrEmpty(Color)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Descripcion") {
                    if (String.IsNullOrEmpty(Descripcion)) {
                        result = "Campo requerido.";
                    }
                } else if(columnName == "Precio") {
                    try {
                        decimal num;
                        if (Precio == 0 || string.IsNullOrEmpty(Precio.ToString())) {
                            result = "Campo requerido.";
                        } else if (!decimal.TryParse(Precio.ToString(), out num)) {
                            result = "Debe ingresar un número";
                        } else if (Precio < 0) {
                            result = "Debe ser mayor a $0.00";
                        }
                    } catch (FormatException e) {
                        throw new FormatException("Debe ingresar un número");
                    }
                } else if (columnName == "Imagen") {
                    if (String.IsNullOrEmpty(Imagen)) {
                        result = "Campo requerido.";
                    }
                } 

                return result;
            }
        }

        public string isValid() {
            var allProperties = GetType().GetProperties();
            foreach (var property in allProperties) {
                string error = this[property.Name];
                if (error != null) return error;
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
