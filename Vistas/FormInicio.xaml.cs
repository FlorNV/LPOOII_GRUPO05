using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vistas.Views;
using ClasesBase;

namespace Vistas {
    /// <summary>
    /// Lógica de interacción para FormInicio.xaml
    /// </summary>
    public partial class FormInicio : Window {
        private Usuario user;
        public FormInicio(Usuario userLog) {
            InitializeComponent();
            textUsername.Text = userLog.Username;
            textRol.Text = userLog.Rol;
            DataContext = new UserControlInicio();

            if (userLog.Rol == "Vendedor") {
                btnUsuarios.IsEnabled = false;
            }

            user = userLog;
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlProductos();
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlAltaVenta();
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlClientes();
        }

        private void btnInicio_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlInicio();
        }

        private void btnProveedores_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlProveedores();
        }

        private void btnUsuarios_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlUsuarios(user);
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e) {
            CerrarSesion();
        }

        private void CerrarSesion() {
            var res = MessageBox.Show("¿Cerrar Sesión?", "Logout", MessageBoxButton.YesNo);
            if (MessageBoxResult.Yes == res) {
                FormLogin frm = new FormLogin();
                frm.Show();
                this.Close();
            }
        }
    }
}
