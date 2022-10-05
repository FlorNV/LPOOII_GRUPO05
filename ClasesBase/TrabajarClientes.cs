using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarClientes
    {
        // Obtener todos los clientes:
        public ObservableCollection<Cliente> obtenerClientes()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Cliente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
            foreach(DataRow row in dt.Rows)
            {
                Cliente oCliente = new Cliente();
                oCliente.DNI = row["Cli_DNI"].ToString();
                oCliente.Apellido = row["Cli_Apellido"].ToString();
                oCliente.Nombre = row["Cli_Nombre"].ToString();
                oCliente.Direccion = row["Cli_Direccion"].ToString();

                clientes.Add(oCliente);
            }
            return clientes;
        }
    }
}
