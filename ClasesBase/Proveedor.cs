using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Proveedor : INotifyPropertyChanged, IDataErrorInfo
    {
        private int id;

        public int ID
        {
            get { return id; }
            set
            {
                id = value;
                Notificador("ID");
            }
        }

        private string cuit;

        public string CUIT
        {
            get { return cuit; }
            set
            {
                cuit = value;
                Notificador("CUIT");
            }
        }
        private string razonSocial;

        public string RazonSocial
        {
            get { return razonSocial; }
            set
            {
                razonSocial = value;
                Notificador("RazonSocial");
            }
        }
        private string domicilio;

        public string Domicilio
        {
            get { return domicilio; }
            set
            {
                domicilio = value;
                Notificador("Domicilio");
            }
        }
        private string telefono;

        public string Telefono
        {
            get { return telefono; }
            set
            {
                telefono = value;
                Notificador("Telefono");
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

        public void Notificador(string nombrePropiedad)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
            }
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName] {
            get {
                string result = null;
                if (columnName == "CUIT") {
                    if (String.IsNullOrEmpty(CUIT)) {
                        result = "Campo requerido.";
                    } else if (!CUIT.All(char.IsDigit)) {
                        result = "Debe ingresar números";
                    }
                } else if (columnName == "RazonSocial") {
                    if (String.IsNullOrEmpty(RazonSocial)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Domicilio") {
                    if (String.IsNullOrEmpty(Domicilio)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Telefono") {
                    if (String.IsNullOrEmpty(Telefono)) {
                        result = "Campo requerido.";
                    } else if (!Telefono.All(char.IsDigit)) {
                        result = "Debe ingresar números";
                    }
                }

                return result;
            }
        }
    }
}
