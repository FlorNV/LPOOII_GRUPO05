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
    /// Interaction logic for FormProductos.xaml
    /// </summary>
    public partial class FormProductos : Window
    {
        private bool editMode = false;
        private string codigo = "";

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
            //txtCodigo.Text = String.Empty;
            txtCategoria.Text = String.Empty;
            txtColor.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            txtPrecio.Text = "0";

            editMode = false;
        }

        // Habilita o no los campos de texto según el booleano
        private void HabilitarDeshabilitarTextBox(bool b)
        {
            //txtCodigo.IsEnabled = b;
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
            //ocultarTextBoxCodigo();
        }

        //private void ocultarTextBoxCodigo() {
        //    lblCodigo.Visibility = Visibility.Hidden;
        //    txtCodigo.Visibility = Visibility.Hidden;
        //}

        // Guardar producto
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
                    //lblErrorPrecio.Content = "Debe ser un decimal";
                    //lblErrorPrecio.Visibility = System.Windows.Visibility.Visible;
                    return;
                }

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Producto oProducto = new Producto();
                    oProducto.Categoria = txtCategoria.Text;
                    oProducto.Color = txtColor.Text;
                    oProducto.Descripcion = txtDescripcion.Text;
                    oProducto.Precio = precio;

                    // TODO: Manejo de errores
                    // TODO: Validar que un producto no tenga el mismo codigo
                    if (editMode) {
                        // Guardar los cambios del producto
                        ClasesBase.TrabajarProductos.ModificarProducto(oProducto, codigo);
                        MessageBox.Show("Producto modificado", "Modificar");
                    } else {
                        // Insertar el nuevo producto
                        ClasesBase.TrabajarProductos.InsertarProducto(oProducto);
                        MessageBox.Show("Producto guardado", "Guardar");
                    }

                    Producto.DataContext = TrabajarProductos.obtenerProductos();

                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(false);

                    LimpiarCampos();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            editMode = true;

            habilitarEdicion(editMode);

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este elemento?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TrabajarProductos.eliminarProducto(codigo);
                LimpiarCampos();
                HabilitarDeshabilitarBotones(false);
                Producto.DataContext = TrabajarProductos.obtenerProductos();
            }
        }

        private bool ValidarTextBox()
        {
            if (txtCategoria.Text == String.Empty || 
                txtColor.Text == String.Empty || 
                txtDescripcion.Text == String.Empty || 
                txtPrecio.Text == String.Empty)
            {
                return true;
            }

            return false;
        }

        private void habilitarEdicion(bool mode) {
            txtCategoria.IsEnabled = mode;
            txtColor.IsEnabled = mode;
            txtDescripcion.IsEnabled = mode;
            txtPrecio.IsEnabled = mode;
        }

        private void getCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = Producto.SelectedItem as DataRowView;
            string codProducto = dataRowView[0].ToString();

            Producto oProducto = TrabajarProductos.obtenerProductoPorCodigo(codProducto);

            codigo = oProducto.CodProducto;
            txtCategoria.Text = oProducto.Categoria;
            txtColor.Text = oProducto.Color;
            txtDescripcion.Text = oProducto.Descripcion;
            txtPrecio.Text = oProducto.Precio.ToString();

            // Inhabilitar los TextBox
            txtCategoria.IsEnabled = false;
            txtColor.IsEnabled = false;
            txtDescripcion.IsEnabled = false;
            txtPrecio.IsEnabled = false;
        }

    }
}
