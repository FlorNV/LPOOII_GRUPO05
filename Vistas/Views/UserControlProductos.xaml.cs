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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.Win32;
using ClasesBase;

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlProductos.xaml
    /// </summary>
    public partial class UserControlProductos : UserControl {

        private bool editMode = false;
        private string codigo = "";
        private string imgProductoRuta = "";

        public UserControlProductos() {
            InitializeComponent();

            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        private void HabilitarBotonesInicio() {
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnCargarImg.IsEnabled = false;
            btnNuevo.IsEnabled = true;
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
            imgProducto.Source = null;
            imgProductoRuta = "";

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
            Content = new UserControlInicio();
        }

        // Cancelar la acción llevada hasta el momento
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        // Cargar una imagen
        private void btnCargarImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Archivos de imagen (.jpg)|*.jpg|All Files (*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(ofd.FileName);
                    img.EndInit();
                    img.Freeze();

                    imgProducto.Source = img;
                    imgProductoRuta = ofd.FileName;
                }
                catch(Exception err)
                {
                    MessageBox.Show("Error al cargar una imagen", "Error");
                }
            }
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

                    oProducto.Imagen = imgProductoRuta;

                    if (editMode)
                    {
                        // Guardar los cambios del producto
                        ClasesBase.TrabajarProductos.ModificarProducto(oProducto, codigo);
                        MessageBox.Show("Producto modificado", "Modificar");
                    }
                    else
                    {
                        // Insertar el nuevo producto
                        ClasesBase.TrabajarProductos.InsertarProducto(oProducto);
                        MessageBox.Show("Producto guardado", "Guardar");
                    }

                    MessageBox.Show(oProducto.Imagen);

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


                // Cargar imagen
                if (oProducto.Imagen == "" || oProducto.Imagen == null)
                {
                    imgProducto.Source = null;
                    imgProductoRuta = "";
                }
                else
                {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(oProducto.Imagen);
                    img.EndInit();
                    img.Freeze();

                    imgProducto.Source = img;
                    imgProductoRuta = oProducto.Imagen;
                }

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
            btnCargarImg.IsEnabled = state;
        }

        private void HabilitarBotonesABM(bool state) {
            btnNuevo.IsEnabled = state;
            btnModificar.IsEnabled = state;
            btnEliminar.IsEnabled = state;
        }
    }
}
