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

namespace Vistas {
    /// <summary>
    /// Lógica de interacción para FormPrincipal.xaml
    /// </summary>
    public partial class FormPrincipal : Window {
        public static string username;

        public FormPrincipal() {
            InitializeComponent();
            //verificarRol();
        }

        public void verificarRol() {
            if (lblUsername.Content.ToString() == "admin") {
                btnVendedores.IsEnabled = true;
            }
        }

        private void btnProveedores_Click(object sender, RoutedEventArgs e) {
            FormProveedores frm = new FormProveedores();
            frm.Show();
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e) {
            FormClientes frm = new FormClientes();
            frm.Show();
        }

        private void btnVendedores_Click(object sender, RoutedEventArgs e) {
            FormVendedores frm = new FormVendedores();
            frm.Show();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e) {
            FormProductos frm = new FormProductos();
            frm.Show();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e) {
            var res = MessageBox.Show("¿Cerrar Sesión?", "Logout", MessageBoxButton.YesNo);
            if (MessageBoxResult.Yes == res) {
                FormLogin frm = new FormLogin();
                frm.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            verificarRol();
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            ListaDeEstados le = new ListaDeEstados();
            le.Show();
        }
    }
}
