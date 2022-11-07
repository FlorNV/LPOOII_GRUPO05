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

using ClasesBase;
using System.Data;
using System.Collections.ObjectModel;

namespace Vistas.Views
{
    /// <summary>
    /// Lógica de interacción para UserControlProveedores.xaml
    /// </summary>
    public partial class UserControlProveedores : UserControl
    {
        CollectionView Vista;
        ObservableCollection<Proveedor> listaProveedores;
        private bool selectedItemList;

        private bool editMode = false;
        private CollectionViewSource vistaColeccionFiltrada;

        public UserControlProveedores()
        {
            InitializeComponent();

            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_proveedores"];
            listaProveedores = odp.Data as ObservableCollection<Proveedor>;

            selectedItemList = true;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(ProveedorItem.DataContext);
            vistaColeccionFiltrada = Resources["ListaProveedores"] as CollectionViewSource;

            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(false);
            OcultarID(true);
            ProveedorItem.DataContext = new Proveedor();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarTextBox())
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea agregar este proveedor?",
                   "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Proveedor oProveedor = new Proveedor();
                    oProveedor.CUIT = txtCUIT.Text;
                    oProveedor.RazonSocial = txtRazonSocial.Text;
                    oProveedor.Domicilio = txtDomicilio.Text;
                    oProveedor.Telefono = txtTelefono.Text;

                    if (editMode)
                    {
                        // Guardar los cambios del proveedor
                        TrabajarProveedores.ModificarProveedor(oProveedor, Convert.ToInt32(txtID.Text));
                        editMode = false;
                        MessageBox.Show("Proveedor modificado", "Modificar");
                    }
                    else
                    {
                        // Insertar el nuevo proveedor
                        TrabajarProveedores.InsertarProveedor(oProveedor);
                        MessageBox.Show("Proveedor guardado", "Guardar");
                    }

                    ActualizarDatos();
                    OcultarID(false);
                    HabilitarDeshabilitarTextBox(false);
                    HabilitarDeshabilitarBotones(true);
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
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este proveedor?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TrabajarProveedores.EliminarProveedor(Convert.ToInt32(txtID.Text));

                LimpiarCampos();
                HabilitarDeshabilitarBotones(true);
                ActualizarDatos();
            }
        }

        private void ActualizarDatos()
        {
            listaProveedores = TrabajarProveedores.ObtenerProveedores();
            ProveedorItem.DataContext = listaProveedores;
            CollectionViewSource cvs = Resources["ListaProveedores"] as CollectionViewSource;
            cvs.Source = listaProveedores;
            cvs.Filter += new FilterEventHandler(eventVistaProveedor_Filter);
            GridListaProveedores.DataContext = cvs;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(ProveedorItem.DataContext);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
            ActualizarDatos();
            OcultarID(false);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Content = new UserControlInicio();
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e)
        {
            ValidarSelectedItem();
            Vista.MoveCurrentToFirst();
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e)
        {
            ValidarSelectedItem();
            Vista.MoveCurrentToLast();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e)
        {
            ValidarSelectedItem();
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            ValidarSelectedItem();
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
            }
        }

        private void ValidarSelectedItem() {
            if (selectedItemList) {
                ProveedorItem.DataContext = listaProveedores;
                Vista = (CollectionView)CollectionViewSource.GetDefaultView(ProveedorItem.DataContext);
                selectedItemList = false;
            }
        }

        private void HabilitarBotonesInicio()
        {
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnNuevo.IsEnabled = true;
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
            HabilitarBotonesABM(b);
            HabilitarBotonesGuardarCancelar(!b);
            HabilitarBotonesAnteriorSiguiente(b);
        }

        private void HabilitarBotonesABM(bool state)
        {
            btnNuevo.IsEnabled = state;
            btnModificar.IsEnabled = state;
            btnEliminar.IsEnabled = state;
        }

        private void HabilitarBotonesGuardarCancelar(bool state)
        {
            btnCancelar.IsEnabled = state;
            btnGuardar.IsEnabled = state;
        }

        private void HabilitarBotonesAnteriorSiguiente(bool state)
        {
            btnPrimero.IsEnabled = state;
            btnAnterior.IsEnabled = state;
            btnSiguiente.IsEnabled = state;
            btnUltimo.IsEnabled = state;
        }

        private void OcultarID(bool state)
        {
            if (state)
            {
                lblID.Visibility = Visibility.Hidden;
                txtID.Visibility = Visibility.Hidden;
            }
            else
            {
                lblID.Visibility = Visibility.Visible;
                txtID.Visibility = Visibility.Visible;
            }
        }

        private void LimpiarCampos()
        {
            txtCUIT.Text = String.Empty;
            txtRazonSocial.Text = String.Empty;
            txtDomicilio.Text = String.Empty;
            txtTelefono.Text = String.Empty;
            txtID.Text = String.Empty;
        }

        private void habilitarEdicion(bool mode)
        {
            txtCUIT.IsEnabled = mode;
            txtRazonSocial.IsEnabled = mode;
            txtDomicilio.IsEnabled = mode;
            txtTelefono.IsEnabled = mode;
        }

        private Proveedor CargarProveedor() {
            Proveedor obj = new Proveedor();
            obj.CUIT = txtCUIT.Text;
            obj.Domicilio = txtDomicilio.Text;
            obj.RazonSocial = txtRazonSocial.Text;
            obj.Telefono = txtTelefono.Text;
            return obj;
        }

        
        private bool ValidarTextBox() {
            Proveedor pr = CargarProveedor();
            string err = pr.isValid();
            if (err != null) {
                MessageBox.Show("Revise los campos por favor.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            return false;
        }

        //Evento que toma el cambio de texto del filtro
        private void txtConsulta_TextChanged(object sender, TextChangedEventArgs e)
       {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaProveedor_Filter;
            }
        }
        //Evento del filtro
        //No se toca la grilla ni el data provider, solo se añade el filter
        //al CollectionViewSource.
        private void eventVistaProveedor_Filter(object sender, FilterEventArgs e)
        {
            Proveedor oProveedor = e.Item as Proveedor;
            if (txtConsulta == null)
            {
                return;
            }

            if (oProveedor.RazonSocial.StartsWith(txtConsulta.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void Proveedores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Proveedor selection = (Proveedor)Proveedores.SelectedItem;
            if (selection != null) 
            {
                selectedItemList = true;
                ProveedorItem.DataContext = new Proveedor();
                txtID.Text = selection.ID.ToString();
                txtCUIT.Text = selection.CUIT;
                txtRazonSocial.Text = selection.RazonSocial;
                txtDomicilio.Text = selection.Domicilio;
                txtTelefono.Text = selection.Telefono;

                // Inhabilitar los TextBox

                HabilitarDeshabilitarBotones(true);
            }
            /*
            DataRowView dataRowView = Proveedores.SelectedItem as DataRowView;
            if (dataRowView != null)
            {
                txtID.Text = dataRowView[0].ToString();
                txtCUIT.Text = dataRowView[1].ToString();
                txtRazonSocial.Text = dataRowView[2].ToString();
                txtDomicilio.Text = dataRowView[3].ToString();
                txtTelefono.Text = dataRowView[4].ToString();

                // Inhabilitar los TextBox

                HabilitarDeshabilitarBotones(true);
            }
             * */
        }

    }
}
