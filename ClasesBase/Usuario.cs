using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase {
    public class Usuario {
        private int id;

        public int Id {
            get { return id; }
            set { id = value; }
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
    }
}
