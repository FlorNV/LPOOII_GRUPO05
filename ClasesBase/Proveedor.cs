using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Proveedor : INotifyPropertyChanged
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
