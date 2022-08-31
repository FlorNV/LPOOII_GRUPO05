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
using ClasesBase;
namespace Vistas
{
    /// <summary>
    /// Interaction logic for FormVendedores.xaml
    /// </summary>
    public partial class FormVendedores : Window
    {
        public FormVendedores()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(true);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea agregar este vendedor?",
                  "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Vendedor oVendedor = new Vendedor();
                oVendedor.Apellido = txtApellido.Text;
                oVendedor.Nombre = txtNombre.Text;
                oVendedor.Legajo = txtLegajo.Text;

                MessageBox.Show("Legajo: " + oVendedor.Legajo +
                    "\nApellido: " + oVendedor.Apellido +
                    "\nNombre: " + oVendedor.Nombre + "Datos del Vendedor");
                HabilitarDeshabilitarTextBox(false);
                HabilitarDeshabilitarBotones(false);
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarBotones(false);
            HabilitarDeshabilitarTextBox(false);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LimpiarCampos()
        {
            txtLegajo.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtNombre.Text = String.Empty;
        }

        private void HabilitarDeshabilitarTextBox(bool b)
        {
            txtLegajo.IsEnabled = b;
            txtApellido.IsEnabled = b;
            txtNombre.IsEnabled = b;
        }

        private void HabilitarDeshabilitarBotones(bool b)
        {
            btnGuardar.IsEnabled = b;
            btnCancelar.IsEnabled = b;

            btnNuevo.IsEnabled = !b;
            btnModificar.IsEnabled = !b;
            btnEliminar.IsEnabled = !b;
            btnPrimero.IsEnabled = !b;
            btnSiguiente.IsEnabled = !b;
            btnAnterior.IsEnabled = !b;
            btnUltimo.IsEnabled = !b;
            btnCancelar.IsEnabled = b;
        }

    }
}
