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
    /// Interaction logic for FormClientes.xaml
    /// </summary>
    public partial class FormClientes : Window
    {
        public FormClientes()
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
            if (!ValidarTextBox())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea agregar este cliente?",
                   "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Cliente oCliente = new Cliente();
                    oCliente.DNI = txtDNI.Text;
                    oCliente.Apellido = txtApellido.Text;
                    oCliente.Nombre = txtNombre.Text;
                    oCliente.Direccion = txtDireccion.Text;

                    MessageBox.Show("DNI: " + oCliente.DNI +
                        "\nApellido: " + oCliente.Apellido +
                        "\nNombre: " + oCliente.Nombre +
                        "\nDireccion: " + oCliente.Direccion, "Datos del Cliente");
                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(false);
                }
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
            txtDNI.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtDireccion.Text = String.Empty;
        }

        private void HabilitarDeshabilitarTextBox(bool b)
        {
            txtDNI.IsEnabled = b;
            txtApellido.IsEnabled = b;
            txtNombre.IsEnabled = b;
            txtDireccion.IsEnabled = b;
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

        private bool ValidarTextBox()
        {
            bool bError = false;
            if (!txtDNI.Text.All(char.IsDigit))
            {
                lblErrorDNI.Content = "Este campo es numérico";
                lblErrorDNI.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else if (txtDNI.Text.Length != 8)
            {
                lblErrorDNI.Content = "Debe contener 8 números";
                lblErrorDNI.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorDNI.Visibility = System.Windows.Visibility.Hidden;

            if (txtApellido.Text == String.Empty)
            {
                lblErrorApellido.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorApellido.Visibility = System.Windows.Visibility.Hidden;

            if (txtNombre.Text == String.Empty)
            {
                lblErrorNombre.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorNombre.Visibility = System.Windows.Visibility.Hidden;

            if (txtDireccion.Text == String.Empty)
            {
                lblErrorDireccion.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorDireccion.Visibility = System.Windows.Visibility.Hidden;

            return bError;
        }

    }
}
