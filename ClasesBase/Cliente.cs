using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Cliente : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notificador(string nombrePropiedad)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nombrePropiedad));
            }
        }
    }
}
