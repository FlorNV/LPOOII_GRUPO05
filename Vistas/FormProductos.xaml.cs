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
    /// Interaction logic for FormProductos.xaml
    /// </summary>
    public partial class FormProductos : Window
    {
        public FormProductos()
        {
            InitializeComponent();
        }

        // Crear un nuevo producto
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(true);
        }

        // Limpia los campos de textos
        private void LimpiarCampos()
        {
            txtCodigo.Text = String.Empty;
            txtCategoria.Text = String.Empty;
            txtColor.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            txtPrecio.Text = String.Empty;
        }

        // Habilita o no los campos de texto según el booleano
        private void HabilitarDeshabilitarTextBox(bool b)
        {
            txtCodigo.IsEnabled = b;
            txtCategoria.IsEnabled = b;
            txtColor.IsEnabled = b;
            txtDescripcion.IsEnabled = b;
            txtPrecio.IsEnabled = b;
        }

        // Habilita o no los botones según el booleano
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

        // Cerrar el form actual
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Cancelar la acción llevada hasta el momento
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarBotones(false);
        }

        // Guardar un nuevo producto
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarTextBox())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea agregar este elemento?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                decimal precio = 0;
                try
                {
                    precio = Decimal.Parse(txtPrecio.Text);
                }
                catch
                {
                    MessageBox.Show("El campo precio debe ser un decimal!", "Verifique los campos");
                    lblErrorPrecio.Content = "Debe ser un decimal";
                    lblErrorPrecio.Visibility = System.Windows.Visibility.Visible;
                    return;
                }

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Producto oProducto = new Producto();
                    oProducto.CodProducto = txtCodigo.Text;
                    oProducto.Categoria = txtCategoria.Text;
                    oProducto.Color = txtColor.Text;
                    oProducto.Descripcion = txtDescripcion.Text;
                    oProducto.Precio = precio;

                    // TODO: Manejo de errores
                    // TODO: Validar que un producto no tenga el mismo codigo

                    // Insertar el nuevo producto
                    ClasesBase.TrabajarProductos.InsertarProducto(oProducto);

                    MessageBox.Show("Código del producto: " + oProducto.CodProducto +
                        "\nCategoría: " + oProducto.Categoria +
                        "\nColor: " + oProducto.Color +
                        "\nDescripción: " + oProducto.Descripcion +
                        "\nPrecio:" + oProducto.Precio, "Datos del nuevo producto");
                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(false);
                }
            }
        }

        private bool ValidarTextBox()
        {
            bool bError = false;
            if (txtCodigo.Text == String.Empty)
            {
                lblErrorCodigo.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorCodigo.Visibility = System.Windows.Visibility.Hidden;

            if (txtCategoria.Text == String.Empty)
            {
                lblErrorCategoria.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorCategoria.Visibility = System.Windows.Visibility.Hidden;

            if (txtColor.Text == String.Empty)
            {
                lblErrorColor.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorColor.Visibility = System.Windows.Visibility.Hidden;

            if (txtDescripcion.Text == String.Empty)
            {
                lblErrorDescripcion.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorDescripcion.Visibility = System.Windows.Visibility.Hidden;

            if (txtPrecio.Text == String.Empty)
            {
                lblErrorPrecio.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            }
            else
                lblErrorPrecio.Visibility = System.Windows.Visibility.Hidden;

            return bError;
        }
    }
}
