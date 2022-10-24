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

namespace Vistas {
    /// <summary>
    /// Lógica de interacción para FormInicio.xaml
    /// </summary>
    public partial class FormInicio : Window {
        public FormInicio() {
            InitializeComponent();
        }

        private void btnProductos_Click(object sender, RoutedEventArgs e) {
            FormProductos frm = new FormProductos();
            frm.Show();
        }

        private void btnVentas_Click(object sender, RoutedEventArgs e) {
            DataContext = new UserControlAltaVenta();
        }
    }
}
