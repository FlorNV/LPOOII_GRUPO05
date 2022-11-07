using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Collections.ObjectModel;
//using System.

namespace ClasesBase
{
    public class TrabajarProductos : ObservableCollection<Producto>
    {
        // Obtener todos los productos:
        public static DataTable obtenerProductos()
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static ObservableCollection<Producto> ObtenerProductos() {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Producto";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Producto> productos = new ObservableCollection<Producto>();
            foreach (DataRow row in dt.Rows) {
                Producto obj = new Producto();
                obj.CodProducto = row["Prod_Codigo"].ToString();
                obj.Categoria = row["Prod_Categoria"].ToString();
                obj.Color = row["Prod_Color"].ToString();
                obj.Descripcion = row["Prod_Descripcion"].ToString();
                obj.Precio = Convert.ToDecimal(row["Prod_Precio"].ToString());
                obj.Imagen = row["Prod_Imagen"].ToString();

                productos.Add(obj);
            }
            return productos;
        }

        // Insertar un producto:
        public static void InsertarProducto(Producto prod) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Producto (Prod_Categoria, Prod_Color, Prod_Descripcion, Prod_Precio, Prod_Imagen) VALUES (@categoria, @color, @descripcion, @precio, @imagen)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            //cmd.Parameters.AddWithValue("@cod", prod.CodProducto);
            cmd.Parameters.AddWithValue("@categoria", prod.Categoria);
            cmd.Parameters.AddWithValue("@color", prod.Color);
            cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
            cmd.Parameters.AddWithValue("@precio", prod.Precio);
            cmd.Parameters.AddWithValue("@imagen", prod.Imagen);
            
            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Eliminar un producto:
        public static void eliminarProducto(String codigo)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Producto WHERE Prod_Codigo = @codigo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@codigo", codigo);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Modificar un producto:
        public static void ModificarProducto(Producto prod, string codigo)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Producto SET Prod_Categoria = @categoria, Prod_Color = @color, Prod_Descripcion = @descripcion, Prod_Precio = @precio, Prod_Imagen = @imagen WHERE Prod_Codigo = @cod";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@cod", codigo);
            cmd.Parameters.AddWithValue("@categoria", prod.Categoria);
            cmd.Parameters.AddWithValue("@color", prod.Color);
            cmd.Parameters.AddWithValue("@descripcion", prod.Descripcion);
            cmd.Parameters.AddWithValue("@precio", prod.Precio);
            cmd.Parameters.AddWithValue("@imagen", prod.Imagen);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        // Obtener producto por codigo
        public static Producto obtenerProductoPorCodigo(string codigo)
        {
            SqlConnection cn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Producto WHERE Prod_Codigo = @cod";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            // Paramatros
            cmd.Parameters.AddWithValue("@cod", codigo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Producto oProducto = new Producto();
                oProducto.CodProducto = row["Prod_Codigo"].ToString();
                oProducto.Categoria = row["Prod_Categoria"].ToString();
                oProducto.Color = row["Prod_Color"].ToString();
                oProducto.Descripcion = row["Prod_Descripcion"].ToString();
                oProducto.Imagen = row["Prod_Imagen"].ToString();
                oProducto.Precio = Convert.ToDecimal(row["Prod_Precio"].ToString());
                return oProducto;
            }

            return null;
        }

        public static DataTable TraerProductos() { 
            using (DataTable dt = new DataTable()) { 
                using (SqlConnection cn = new SqlConnection( ClasesBase.Properties.Settings.Default.muebleriaConnectionString)) {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Producto", cn)) { 
                        da.Fill(dt); 
                    } 
                } 
                return dt; 
            } 
        } 
    }
}
