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
    /// Interaction logic for FormProveedores.xaml
    /// </summary>
    public partial class FormProveedores : Window
    {
        public FormProveedores()
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
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea agregar este elemento?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                Proveedor oProveedor = new Proveedor();
                oProveedor.CUIT = txtCUIT.Text;
                oProveedor.RazonSocial = txtRazonSocial.Text;
                oProveedor.Domicilio = txtDomicilio.Text;
                oProveedor.Telefono = txtTelefono.Text;

                MessageBox.Show("CUIT: " + oProveedor.CUIT +
                    "\nRazon Social: " + oProveedor.RazonSocial +
                    "\nDomicilio: " + oProveedor.Domicilio +
                    "\nTeléfono: " + oProveedor.Telefono, "Datos del Proveedor");
                HabilitarDeshabilitarTextBox(false);
                HabilitarDeshabilitarBotones(false);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarBotones(false);

        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LimpiarCampos()
        {
            txtCUIT.Text = String.Empty;
            txtRazonSocial.Text = String.Empty;
            txtDomicilio.Text = String.Empty;
            txtTelefono.Text = String.Empty;
        }

        private void HabilitarDeshabilitarTextBox(bool b)
        {
            txtCUIT.IsEnabled = b;
            txtRazonSocial.IsEnabled = b;
            txtDomicilio.IsEnabled = b;
            txtTelefono.IsEnabled = b;
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
