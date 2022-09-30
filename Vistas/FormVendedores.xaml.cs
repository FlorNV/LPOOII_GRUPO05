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
        private bool editMode = false;

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
            if (!ValidarTextBox())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea guardar este vendedor?",
                  "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Vendedor oVendedor = new Vendedor();
                    oVendedor.Apellido = txtApellido.Text;
                    oVendedor.Nombre = txtNombre.Text;
                    oVendedor.Legajo = txtLegajo.Text;

                    // TODO: Manejo de errores
                    // TODO: Validar que un vendedor no tenga el mismo legajo

                    if (editMode) {
                        //Modificar vendedor
                        ClasesBase.TrabajarVendedores.modificarVendedor(oVendedor);
                    } else {
                        // Insertar el nuevo vendedor
                        ClasesBase.TrabajarVendedores.insertarVendedor(oVendedor);
                    }

                    LimpiarCampos();

                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(false);
                    habilitarEdicion(editMode);
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            editMode = true;
            habilitarEdicion(editMode);

            btnGuardar.IsEnabled = true;
            btnCancelar.IsEnabled = true;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este elemento?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TrabajarVendedores.eliminarVendedor(txtLegajo.Text);
                LimpiarCampos();
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

            editMode = false;
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

        private bool ValidarTextBox()
        {
            bool bError = false;
            if (txtLegajo.Text == String.Empty)
            {
                lblErrorLegajo.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorLegajo.Visibility = System.Windows.Visibility.Hidden;

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

            return bError;
        }

        private void btnVerVendedores_Click(object sender, RoutedEventArgs e)
        {
            WinVendedores win = new WinVendedores();
            win.Owner = this;
            win.Show();
        }

        private void habilitarEdicion(bool mode) {
            //txtCodigo.IsEnabled = mode;
            txtApellido.IsEnabled = mode;
            //txtLegajo.IsEnabled = mode;
            txtNombre.IsEnabled = mode;
        }
    }
}
