using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarVendedores : ObservableCollection<Vendedor>
    {
        // Obtener todos los vendedores:
        public static DataTable obtenerVendedores()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Vendedor";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        // Insertar un vendedor:
        public static void insertarVendedor(Vendedor vendedor)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Vendedor (Vend_Legajo, Vend_Apellido, Vend_Nombre) VALUES (@legajo, @apellido, @nombre)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@legajo", vendedor.Legajo);
            cmd.Parameters.AddWithValue("@apellido", vendedor.Apellido);
            cmd.Parameters.AddWithValue("@nombre", vendedor.Nombre);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Eliminar un vendedor:
        public static void eliminarVendedor(int id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Vendedor WHERE Vend_ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@id", id);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Modificar un vendedor:
        public static void modificarVendedor(Vendedor vendedor)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Vendedor SET  Vend_Legajo = @legajo, Vend_Apellido = @apellido, Vend_Nombre = @nombre WHERE Vend_ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@id", vendedor.ID);
            cmd.Parameters.AddWithValue("@legajo", vendedor.Legajo);
            cmd.Parameters.AddWithValue("@apellido", vendedor.Apellido);
            cmd.Parameters.AddWithValue("@nombre", vendedor.Nombre);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Obtener vendedor por legajo:
        public static Vendedor obtenerVendedorPorLegajo(string legajo)
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Vendedor WHERE Vend_Legajo = @legajo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            // Paramatros
            cmd.Parameters.AddWithValue("@legajo", legajo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Vendedor oVendedor = new Vendedor();
                oVendedor.ID = Convert.ToInt32(row["Vend_ID"]);
                oVendedor.Legajo = row["Vend_Legajo"].ToString();
                oVendedor.Apellido = row["Vend_Apellido"].ToString();
                oVendedor.Nombre = row["Vend_Nombre"].ToString();
                return oVendedor;
            }

            return null;
        }

        public ObservableCollection<Vendedor> obtenerVendedoresCollection() {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Vendedor";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Vendedor> vendedores = new ObservableCollection<Vendedor>();
            foreach (DataRow row in dt.Rows) {
                Vendedor oVendedor = new Vendedor();
                oVendedor.ID = Convert.ToInt32(row["Vend_ID"]);
                oVendedor.Apellido = row["Vend_Apellido"].ToString();
                oVendedor.Nombre = row["Vend_Nombre"].ToString();
                oVendedor.Legajo = row["Vend_Legajo"].ToString();

                vendedores.Add(oVendedor);
            }
            return vendedores;
        }
    }
}
