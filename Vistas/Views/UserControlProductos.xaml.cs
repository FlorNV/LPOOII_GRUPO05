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
        private string imgProductoRuta = "";

        CollectionView Vista;
        ObservableCollection<Producto> listaProductos;

        private bool selectedItemList;

        private CollectionViewSource vistaColeccionFiltrada;

        public UserControlProductos() {
            InitializeComponent();

            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_productos"];
            listaProductos = odp.Data as ObservableCollection<Producto>;

            selectedItemList = true;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(ProductoItem.DataContext);
            vistaColeccionFiltrada = Resources["ListaProductos"] as CollectionViewSource;

            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        private void HabilitarBotonesInicio() {
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnCargarImg.IsEnabled = false;
            btnNuevo.IsEnabled = true;
        }

         // Crear un nuevo producto
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(false);
            ProductoItem.DataContext = new Producto();
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
            HabilitarBotonesAnteriorSiguiente(b);
        }

        private void HabilitarBotonesAnteriorSiguiente(bool state) {
            btnPrimero.IsEnabled = state;
            btnAnterior.IsEnabled = state;
            btnSiguiente.IsEnabled = state;
            btnUltimo.IsEnabled = state;
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
            ActualizarDatos();
        }

        private void ActualizarDatos() {
            try {
                listaProductos = TrabajarProductos.ObtenerProductos();
                ProductoItem.DataContext = listaProductos;
                CollectionViewSource cvs = Resources["ListaProductos"] as CollectionViewSource;
                cvs.Source = listaProductos;
                cvs.Filter += new FilterEventHandler(eventVistaCliente_Filter);
                GridListaProductos.DataContext = cvs;

                Vista = (CollectionView)CollectionViewSource.GetDefaultView(ProductoItem.DataContext);
            } catch (Exception x) {
                MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

                if (messageBoxResult == MessageBoxResult.Yes) {
                    try {
                        Producto oProducto = new Producto();
                        oProducto.Categoria = txtCategoria.Text;
                        oProducto.Color = txtColor.Text;
                        oProducto.Descripcion = txtDescripcion.Text;
                        oProducto.Precio = Convert.ToDecimal(txtPrecio.Text);
                        oProducto.Imagen = imgProductoRuta;

                        if (editMode)
                        {
                            // Guardar los cambios del producto
                            ClasesBase.TrabajarProductos.ModificarProducto(oProducto, txtCodProducto.Text);
                            MessageBox.Show("Producto modificado", "Modificar");
                            editMode = false;
                        }
                        else
                        {
                            // Insertar el nuevo producto
                            ClasesBase.TrabajarProductos.InsertarProducto(oProducto);
                            MessageBox.Show("Producto guardado", "Guardar");
                        }
                        //MessageBox.Show(oProducto.Imagen);
                        ActualizarDatos();
                        HabilitarDeshabilitarTextBox(false);
                        HabilitarDeshabilitarBotones(true);
                    } catch (Exception x) {
                        MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
                try {
                    TrabajarProductos.eliminarProducto(txtCodProducto.Text);
                    LimpiarCampos();
                    HabilitarDeshabilitarBotones(true);
                    ActualizarDatos();
                } catch (Exception x) {
                    MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool ValidarTextBox()
        {
            Producto prd = CargarProducto();
            string err = prd.isValid();
            if (err != null) {
                MessageBox.Show("Revise los campos por favor.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            return false;
        }

        private Producto CargarProducto() {
            Producto prd = new Producto();
            prd.Categoria = txtCategoria.Text;
            prd.Color = txtColor.Text;
            prd.Descripcion = txtDescripcion.Text;
            prd.Precio = Convert.ToDecimal(txtPrecio.Text);
            return prd;
        }

        private void habilitarEdicion(bool mode) {
            txtCategoria.IsEnabled = mode;
            txtColor.IsEnabled = mode;
            txtDescripcion.IsEnabled = mode;
            txtPrecio.IsEnabled = mode;
        }

        private void Productos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Producto selection = (Producto)Productos.SelectedItem;
            if (selection != null) {
                selectedItemList = true;
                ProductoItem.DataContext = new Producto();
                txtCategoria.Text = selection.Categoria;
                txtColor.Text = selection.Color;
                txtDescripcion.Text = selection.Descripcion;
                txtPrecio.Text = selection.Precio.ToString();
                txtCodProducto.Text = selection.CodProducto.ToString();

                // Cargar imagen
                if (selection.Imagen == "" || selection.Imagen == null) {
                    imgProducto.Source = null;
                    imgProductoRuta = "";
                } else {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(selection.Imagen);
                    img.EndInit();
                    img.Freeze();

                    imgProducto.Source = img;
                    imgProductoRuta = selection.Imagen;
                }

                // Inhabilitar los TextBox
                HabilitarDeshabilitarBotones(true);
            }
            /*
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
            }*/
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            //productosList = convertToObservable( TrabajarProductos.obtenerProductos());
            ActualizarDatos();
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

        //Evento que toma el cambio de texto del filtro
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                if (vistaColeccionFiltrada != null) {
                    vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
                }
            } catch(Exception ex) {
                MessageBox.Show("Error inserperado:\n" + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Evento del filtro
        //No se toca la grilla ni el data provider, solo se añade el filter
        //al CollectionViewSource.
        private void eventVistaCliente_Filter(object sender, FilterEventArgs e) {
            Producto prod = e.Item as Producto;
            if (textBox1 == null) {
                return;
            }

            if (prod.Descripcion.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)) {
                e.Accepted = true;
            } else {
                e.Accepted = false;
            }
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToFirst();
            // Cargar imagen
            CargarImagenCollectionViewSource();
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToLast();
            CargarImagenCollectionViewSource();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst) {
                Vista.MoveCurrentToLast();
            }
            CargarImagenCollectionViewSource();
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast) {
                Vista.MoveCurrentToFirst();
            }
            CargarImagenCollectionViewSource();
        }
        private void CargarImagenCollectionViewSource() {
            Producto selection = (Producto)Vista.CurrentItem;
            try {
                if (selection.Imagen == "" || selection.Imagen == null) {
                    imgProducto.Source = null;
                    imgProductoRuta = "";
                } else {
                    BitmapImage img = new BitmapImage();
                    img.BeginInit();
                    img.UriSource = new Uri(selection.Imagen);
                    img.EndInit();
                    img.Freeze();

                    imgProducto.Source = img;
                    imgProductoRuta = selection.Imagen;
                }
            } catch (Exception x) {
                MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ValidarSelectedItem() {
            if (selectedItemList) {
                ProductoItem.DataContext = listaProductos;
                Vista = (CollectionView)CollectionViewSource.GetDefaultView(ProductoItem.DataContext);
                selectedItemList = false;
            }
        }
    }
}
