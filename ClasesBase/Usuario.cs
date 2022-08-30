using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Usuario
    {
        private int usu_Id;
        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }

        private string usu_Username;
        public string Usu_Username
        {
            get { return usu_Username; }
            set { usu_Username = value; }
        }

        private string usu_Password;
        public string Usu_Password
        {
            get { return usu_Password; }
            set { usu_Password = value; }
        }

        private string usu_Rol;
        public string Usu_Rol
        {
            get { return usu_Rol; }
            set { usu_Rol = value; }
        }
    }
}
