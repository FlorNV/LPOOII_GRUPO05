using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase {
    public class Usuario {
        private int legajo;

        public int Legajo {
            get { return legajo; }
            set { legajo = value; }
        }
        private string username;

        public string Username {
            get { return username; }
            set { username = value; }
        }
        private string password;

        public string Password {
            get { return password; }
            set { password = value; }
        }
        private string rol;

        public string Rol {
            get { return rol; }
            set { rol = value; }
        }

        private string apellido;

        public string Apellido {
            get { return apellido; }
            set { apellido = value; }
        }

        private string nombre;

        public string Nombre {
            get { return nombre; }
            set { nombre = value; }
        }
    }
}
