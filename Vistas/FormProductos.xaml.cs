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

using System.Collections.ObjectModel;
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
            HabilitarDeshabilitarBotones(false);
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
            HabilitarBotonesABM(b);
            HabilitarBotonesGuardarCancelar(!b);
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
            HabilitarDeshabilitarBotones(true);
            HabilitarDeshabilitarTextBox(false);
        }

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

                    Productos.DataContext = TrabajarProductos.obtenerProductos();

                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(true);

                    LimpiarCampos();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            editMode = true;

            habilitarEdicion(editMode);
            HabilitarDeshabilitarBotones(false);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este elemento?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TrabajarProductos.eliminarProducto(codigo);
                LimpiarCampos();

                HabilitarDeshabilitarBotones(true);

                Productos.DataContext = TrabajarProductos.obtenerProductos();
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
            DataRowView dataRowView = Productos.SelectedItem as DataRowView;
            if (dataRowView != null) {
                
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

                HabilitarDeshabilitarBotones(true);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //productosList = convertToObservable( TrabajarProductos.obtenerProductos());
            HabilitarDeshabilitarBotones(true);
            HabilitarDeshabilitarTextBox(false);
        }

        private ObservableCollection<Producto> convertToObservable(DataTable dt) {
            ObservableCollection<Producto> prods = new ObservableCollection<Producto>();
            foreach (DataRow row in dt.Rows) {
                prods.Add(new Producto() {
                    CodProducto = Convert.ToString(row["Prod_Codigo"]),
                    Categoria = (string)row["Prod_Categoria"],
                    Color = (string)row["Prod_Color"],
                    Descripcion = (string)row["Prod_Descripcion"],
                    Precio = (decimal)row["Prod_Precio"]
                });
            }

            return prods;
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
    }
}
