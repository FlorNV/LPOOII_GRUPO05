using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace ClasesBase {
    public class Vendedor : INotifyPropertyChanged {

        private int id;

        public int ID {
            get { return id; }
            set {
                id = value;
                this.NotifyPropertyChanged("ID");
            }
        }

        private string legajo;

        public string Legajo {
            get { return legajo; }
            set { 
                legajo = value;
                this.NotifyPropertyChanged("Legajo");
            }
        }
        private string apellido;

        public string Apellido {
            get { return apellido; }
            set { 
                apellido = value;
                this.NotifyPropertyChanged("Apellido");
            }
        }
        private string nombre;

        public string Nombre {
            get { return nombre; }
            set { 
                nombre = value;
                this.NotifyPropertyChanged("Nombre");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propName) {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
