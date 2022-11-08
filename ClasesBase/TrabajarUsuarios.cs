using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase {
    public class TrabajarUsuarios {
        public static void InsertarUsuario(Usuario usuario) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO Usuario (Usu_Apellido, Usu_Nombre, Usu_Rol, Usu_Username, Usu_Password) VALUES (@apellido, @nombre, @rol, @username, @password)";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@username", usuario.Username);
            cmd.Parameters.AddWithValue("@password", usuario.Password);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void EliminarUsuario(int legajo) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Usuario WHERE Usu_Legajo = @legajo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@legajo", legajo);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void ModificarUsuario(Usuario usuario) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Usuario SET Usu_Apellido = @apellido, Usu_Nombre = @nombre, Usu_Rol = @rol, Usu_Username = @username, Usu_Password = @password WHERE Usu_legajo = @legajo";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            // Paramatros
            cmd.Parameters.AddWithValue("@legajo", usuario.Legajo);
            cmd.Parameters.AddWithValue("@apellido", usuario.Apellido);
            cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@rol", usuario.Rol);
            cmd.Parameters.AddWithValue("@username", usuario.Username);
            cmd.Parameters.AddWithValue("@password", usuario.Password);

            // Ejecutar la query
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static ObservableCollection<Usuario> ObtenerUsuariosObservable() {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Usuario";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Usuario> usuarios = new ObservableCollection<Usuario>();
            foreach (DataRow row in dt.Rows) {
                Usuario usr = new Usuario();
                usr.Legajo = Convert.ToInt32(row["Usu_Legajo"]);
                usr.Apellido = row["Usu_Apellido"].ToString();
                usr.Nombre = row["Usu_Nombre"].ToString();
                usr.Rol = row["Usu_Rol"].ToString();
                usr.Username = row["Usu_Username"].ToString();
                usr.Password = row["Usu_Password"].ToString();

                usuarios.Add(usr);
            }
            return usuarios;
        }

        public static DataTable ObtenerUsuarios() {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Usuario";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static Usuario VerificarUsuario(string username, string password) {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Usuario WHERE Usu_Username = @username AND Usu_Password = @password";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows) {
                Usuario usuario = new Usuario();
                usuario.Username = row["Usu_Username"].ToString();
                usuario.Rol = row["Usu_Rol"].ToString();
                return usuario;
            }

            return null;
        }

        public static Usuario ObtenerUsuarioPorUsername(string username)
        {
            SqlConnection cnn = new SqlConnection(ClasesBase.Properties.Settings.Default.muebleriaConnectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Usuario WHERE Usu_Username = @username";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;
            cmd.Parameters.AddWithValue("@username", username);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                Usuario usuario = new Usuario();
                usuario.Username = row["Usu_Username"].ToString();
                usuario.Rol = row["Usu_Rol"].ToString();
                return usuario;
            }

            return null;
        }
    }
}
