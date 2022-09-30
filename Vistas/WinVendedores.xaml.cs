using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClasesBase;

namespace Vistas
{
    /// <summary>
    /// Interaction logic for WinVendedores.xaml
    /// </summary>
    public partial class WinVendedores : Window
    {
        public Window Owner { get; set; }

        public WinVendedores()
        {
            InitializeComponent();
        }

        // Obtiene el legajo del vendedor seleccionado
        private void getLegajo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = Vendedor.SelectedItem as DataRowView;
            string szLegajo = dataRowView[0].ToString();

            Vendedor oVendedor = TrabajarVendedores.obtenerVendedorPorLegajo(szLegajo);

            ((FormVendedores)this.Owner).txtLegajo.Text = oVendedor.Legajo;
            ((FormVendedores)this.Owner).txtApellido.Text = oVendedor.Apellido;
            ((FormVendedores)this.Owner).txtNombre.Text = oVendedor.Nombre;

            ((FormVendedores)this.Owner).txtLegajo.IsEnabled = false;
            ((FormVendedores)this.Owner).txtApellido.IsEnabled = false;
            ((FormVendedores)this.Owner).txtNombre.IsEnabled = false;

            this.Close();
        }

    }
}
