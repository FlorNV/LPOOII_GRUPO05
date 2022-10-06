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
                oCliente.ID = Convert.ToInt32( row["Cli_ID"].ToString());

                clientes.Add(oCliente);
            }
            return clientes;
        }

        public static void InsertarCliente(Cliente cliente) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Cliente (Cli_DNI, Cli_Apellido, Cli_Nombre, Cli_Direccion) VALUES (@dni, @apellido, @nombre, @direccion)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            //cmd.Parameters.AddWithValue("@cod", prod.CodProducto);
            cmd.Parameters.AddWithValue("@dni", cliente.DNI);
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void EliminarCliente(int id) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Cliente WHERE Cli_ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@id", id);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void ModificarCliente(Cliente cliente, int id) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Cliente SET Cli_DNI = @dni, Cli_Apellido = @apellido, Cli_Nombre = @nombre, Cli_Direccion = @direccion WHERE Cli_ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@dni",cliente.DNI);
            cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
            cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
            cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static ObservableCollection<Cliente> ObtenerClientesStatic() {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Cliente";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Cliente> clientes = new ObservableCollection<Cliente>();
            foreach (DataRow row in dt.Rows) {
                Cliente oCliente = new Cliente();
                oCliente.DNI = row["Cli_DNI"].ToString();
                oCliente.Apellido = row["Cli_Apellido"].ToString();
                oCliente.Nombre = row["Cli_Nombre"].ToString();
                oCliente.Direccion = row["Cli_Direccion"].ToString();
                oCliente.ID = Convert.ToInt32(row["Cli_ID"].ToString());

                clientes.Add(oCliente);
            }
            return clientes;
        }
    }
}
