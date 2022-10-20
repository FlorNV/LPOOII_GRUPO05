using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ClasesBase {
    public class TrabajarVentas {
        public static int insertarVenta(Venta venta) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Venta (Ven_FechaFactura, Ven_Legajo, Ven_DNI, Ven_CodProducto," +
                "Ven_Precio, Ven_Cantidad, Ven_Importe) VALUES (@fecha, @legajo, @dni, @producto, @precio, @cantidad, @importe)" +
                "; SELECT SCOPE_IDENTITY()";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@fecha", venta.FechaFactura);
            cmd.Parameters.AddWithValue("@legajo", venta.Legajo);
            cmd.Parameters.AddWithValue("@dni", venta.DNI);
            cmd.Parameters.AddWithValue("@producto", venta.CodProducto);
            cmd.Parameters.AddWithValue("@precio", venta.Precio);
            cmd.Parameters.AddWithValue("@cantidad", venta.Cantidad);
            cmd.Parameters.AddWithValue("@importe", venta.Importe);

            // Ejecutar la query

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            cnn.Open();
            int iVentaNro = Convert.ToInt32(cmd.ExecuteScalar());
            cnn.Close();

            return iVentaNro;
        }
    }
}
