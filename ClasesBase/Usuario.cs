using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase {
    public class Usuario : IDataErrorInfo, INotifyPropertyChanged {
        private int legajo;

        public int Legajo {
            get { return legajo; }
            set { legajo = value; this.NotifyPropertyChanged("Legajo"); }
        }
        private string username;

        public string Username {
            get { return username; }
            set { username = value; this.NotifyPropertyChanged("Username"); }
        }
        private string password;

        public string Password {
            get { return password; }
            set { password = value; this.NotifyPropertyChanged("Password"); }
        }
        private string rol;

        public string Rol {
            get { return rol; }
            set { rol = value; this.NotifyPropertyChanged("Rol"); }
        }

        private string apellido;

        public string Apellido {
            get { return apellido; }
            set { apellido = value; this.NotifyPropertyChanged("Apellido"); }
        }

        private string nombre;

        public string Nombre {
            get { return nombre; }
            set { nombre = value; this.NotifyPropertyChanged("Nombre"); }
        }

        public string Error {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName] {
            get {
                string result = null;
                if (columnName == "Username") {
                    if (String.IsNullOrEmpty(Username)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Password") {
                    if (String.IsNullOrEmpty(Password)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Rol") {
                    if (String.IsNullOrEmpty(Rol)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Apellido") {
                    if (String.IsNullOrEmpty(Apellido)) {
                        result = "Campo requerido.";
                    }
                } else if (columnName == "Nombre") {
                    if (String.IsNullOrEmpty(Nombre)) {
                        result = "Campo requerido.";
                    }
                }

                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }
}
