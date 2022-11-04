using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Cliente : INotifyPropertyChanged, IDataErrorInfo
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

        private string dni;

        public string DNI
        {
            get { return dni; }
            set
            {
                dni = value;
                Notificador("DNI");
            }
        }
        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set
            {
                apellido = value;
                Notificador("Apellido");
            }
        }
        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                Notificador("Nombre");
            }
        }
        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set
            {
                direccion = value;
                Notificador("Direccion");
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
                if (columnName == "DNI") {
                    if (String.IsNullOrEmpty(DNI)) {
                        result = "Campo requerido.";
                    } else if(!DNI.All(char.IsDigit)) {
                        result = "Debe ingresar números";
                    }else if (DNI.Length != 8) {
                        result = "Debe contener 8 números";
                    }
                } else if (columnName == "Apellido") {
                    if (String.IsNullOrEmpty(Apellido)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Nombre") {
                    if (String.IsNullOrEmpty(Nombre)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Direccion") {
                    if (String.IsNullOrEmpty(Direccion)) {
                        result = "Campo requerido.";
                    }
                }

                return result;
            }
        }
    }
}
