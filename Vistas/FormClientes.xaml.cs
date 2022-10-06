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

        CollectionView Vista;

        private bool editMode = false;

        public FormClientes()
        {
            InitializeComponent();
        }
        
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(false);

            OcultarID(true);
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

                    if (editMode) {
                        // Guardar los cambios del producto
                        ClasesBase.TrabajarClientes.ModificarCliente(oCliente, Convert.ToInt32(txtID.Text));
                        MessageBox.Show("Cliente modificado", "Modificar");
                    } else {
                        // Insertar el nuevo producto
                        ClasesBase.TrabajarClientes.InsertarCliente(oCliente);
                        MessageBox.Show("Cliente guardado", "Guardar");
                    }

                    //grid_content.DataContext = TrabajarClientes.ObtenerClientesStatic();

                    ActualizarDatos();

                    OcultarID(false);
                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(true);
                }
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarBotones(true);
            HabilitarDeshabilitarTextBox(false);

            ActualizarDatos();
            OcultarID(false);
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
            txtID.Text = String.Empty;
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
            HabilitarBotonesABM(b);
            HabilitarBotonesGuardarCancelar(!b);
            HabilitarBotonesAnteriorSiguiente(b);
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

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ActualizarDatos();
            HabilitarDeshabilitarBotones(true);
            HabilitarDeshabilitarTextBox(false);
        }

        private void ActualizarDatos() {
            grid_content.DataContext = TrabajarClientes.ObtenerClientesStatic();
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e) {
            Vista.MoveCurrentToFirst();
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e) {
            Vista.MoveCurrentToLast();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e) {
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst) {
                Vista.MoveCurrentToLast();
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e) {
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast) {
                Vista.MoveCurrentToFirst();
            }
        }

        private void HabilitarBotonesGuardarCancelar(bool state) {
            btnCancelar.IsEnabled = state;
            btnGuardar.IsEnabled = state;
        }

        private void HabilitarBotonesABM(bool state) {
            btnNuevo.IsEnabled = state;
            btnModificar.IsEnabled = state;
            btnEliminar.IsEnabled = state;
        }

        private void HabilitarBotonesAnteriorSiguiente(bool state) {
            btnPrimero.IsEnabled = state;
            btnAnterior.IsEnabled = state;
            btnSiguiente.IsEnabled = state;
            btnUltimo.IsEnabled = state;
        }

        private void habilitarEdicion(bool mode) {
            txtDNI.IsEnabled = mode;
            txtNombre.IsEnabled = mode;
            txtApellido.IsEnabled = mode;
            txtDireccion.IsEnabled = mode;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e) {
            editMode = true;

            habilitarEdicion(editMode);
            HabilitarDeshabilitarBotones(false);
        }

        private void OcultarID(bool state) {
            if (state) {
                lblID.Visibility = Visibility.Hidden;
                txtID.Visibility = Visibility.Hidden;
            } else {
                lblID.Visibility = Visibility.Visible;
                txtID.Visibility = Visibility.Visible;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este cliente?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes) {
                TrabajarClientes.EliminarCliente(Convert.ToInt32(txtID.Text));
                LimpiarCampos();

                HabilitarDeshabilitarBotones(true);

                ActualizarDatos();
            }
        }

        private void btnVerClientes_Click(object sender, RoutedEventArgs e)
        {
            FormListadoClientes win = new FormListadoClientes();
            win.Show();
        }

    }
}
