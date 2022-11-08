using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarProveedores
    {
        // Obtener todos los proveedores:
        public static ObservableCollection<Proveedor> ObtenerProveedores()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Proveedor";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Proveedor> proveedores = new ObservableCollection<Proveedor>();
            foreach (DataRow row in dt.Rows)
            {
                Proveedor oProveedor = new Proveedor();
                oProveedor.ID = Convert.ToInt32(row["Prov_ID"].ToString());
                oProveedor.CUIT = row["Prov_CUIT"].ToString();
                oProveedor.RazonSocial = row["Prov_RazonSocial"].ToString();
                oProveedor.Domicilio = row["Prov_Domicilio"].ToString();
                oProveedor.Telefono = row["Prov_Telefono"].ToString();

                proveedores.Add(oProveedor);
            }
            return proveedores;
        }

        // Insertar un proveedor
        public static void InsertarProveedor(Proveedor proveedor)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Proveedor (Prov_CUIT, Prov_RazonSocial, Prov_Domicilio, Prov_Telefono) VALUES (@cuit, @razonSocial, @domicilio, @telefono)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@cuit", proveedor.CUIT);
            cmd.Parameters.AddWithValue("@razonSocial", proveedor.RazonSocial);
            cmd.Parameters.AddWithValue("@domicilio", proveedor.Domicilio);
            cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Eliminar un proveedor
        public static void EliminarProveedor(int id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Proveedor WHERE Prov_ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@id", id);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Modificar un proveedor
        public static void ModificarProveedor(Proveedor proveedor, int id)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Proveedor SET Prov_CUIT = @cuit, Prov_RazonSocial = @razonSocial, Prov_Domicilio = @domicilio, Prov_Telefono = @telefono WHERE Prov_ID = @id";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@cuit", proveedor.CUIT);
            cmd.Parameters.AddWithValue("@razonSocial", proveedor.RazonSocial);
            cmd.Parameters.AddWithValue("@domicilio", proveedor.Domicilio);
            cmd.Parameters.AddWithValue("@telefono", proveedor.Telefono);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
