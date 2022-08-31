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

                MessageBox.Show("Código del producto: " + oProducto.CodProducto +
                    "\Categoría: " + oProducto.Categoria +
                    "\nColor: " + oProducto.Color +
                    "\nDescripción: " +  oProducto.Descripcion + 
                    "\nPrecio:" + oProducto.Precio, "Datos del nuevo producto");
                HabilitarDeshabilitarTextBox(false);
                HabilitarDeshabilitarBotones(false);
            }
        }
    }
}
