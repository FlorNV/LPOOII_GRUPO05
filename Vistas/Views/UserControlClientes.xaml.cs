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

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlClientes.xaml
    /// </summary>
    public partial class UserControlClientes : UserControl {

        CollectionView Vista;
        ObservableCollection<Cliente> listaClientes;

        private bool selectedItemList;

        private bool editMode = false;
        private CollectionViewSource vistaColeccionFiltrada;

        public UserControlClientes() {
            InitializeComponent();

            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["list_clientes"];
            listaClientes = odp.Data as ObservableCollection<Cliente>;

            selectedItemList = true;

            Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);
            vistaColeccionFiltrada = Resources["ListaClientes"] as CollectionViewSource;

            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e) {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(false);
            grid_content.DataContext = new Cliente();
            OcultarID(true);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (!ValidarTextBox()) {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea agregar este cliente?",
                   "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes) {

                    try {
                        Cliente oCliente = new Cliente();
                        oCliente.DNI = txtDNI.Text;
                        oCliente.Apellido = txtApellido.Text;
                        oCliente.Nombre = txtNombre.Text;
                        oCliente.Direccion = txtDireccion.Text;

                        if (editMode) {
                            // Guardar los cambios del cliente
                            ClasesBase.TrabajarClientes.ModificarCliente(oCliente, Convert.ToInt32(txtID.Text));
                            MessageBox.Show("Cliente modificado", "Modificar");
                            editMode = false;
                        } else {
                            // Insertar el nuevo cliente
                            ClasesBase.TrabajarClientes.InsertarCliente(oCliente);
                            MessageBox.Show("Cliente guardado", "Guardar");
                        }

                        ActualizarDatos();

                        OcultarID(false);
                        HabilitarDeshabilitarTextBox(false);
                        HabilitarDeshabilitarBotones(true);
                    } catch (Exception x) {
                        MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            LimpiarCampos();
            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
            ActualizarDatos();
            OcultarID(false);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e) {
            Content = new UserControlInicio();
        }

        private void HabilitarBotonesInicio()
        {
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            btnNuevo.IsEnabled = true;
        }

        private void HabilitarDeshabilitarTextBox(bool b) {
            txtDNI.IsEnabled = b;
            txtApellido.IsEnabled = b;
            txtNombre.IsEnabled = b;
            txtDireccion.IsEnabled = b;
        }

        private void HabilitarDeshabilitarBotones(bool b) {
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
            txtDNI.Text = String.Empty;
            txtApellido.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            txtID.Text = String.Empty;
        }

        private void habilitarEdicion(bool mode)
        {
            txtDNI.IsEnabled = mode;
            txtNombre.IsEnabled = mode;
            txtApellido.IsEnabled = mode;
            txtDireccion.IsEnabled = mode;
        }

        private Cliente CargarCliente() {
            Cliente cli = new Cliente();
            cli.DNI = txtDNI.Text;
            cli.Apellido = txtApellido.Text;
            cli.Nombre = txtNombre.Text;
            cli.Direccion = txtDireccion.Text;
            return cli;
        }
        
        private bool ValidarTextBox() {
            Cliente cli = CargarCliente();
            string err = cli.isValid();
            if (err != null) {
                MessageBox.Show("Revise los campos por favor.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }

            return false;
            /*
            if (!txtDNI.Text.All(char.IsDigit)) {
                lblErrorDNI.Content = "Este campo es numérico";
                lblErrorDNI.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            } else if (txtDNI.Text.Length != 8) {
                lblErrorDNI.Content = "Debe contener 8 números";
                lblErrorDNI.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            } else
                lblErrorDNI.Visibility = System.Windows.Visibility.Hidden;

            if (txtApellido.Text == String.Empty) {
                lblErrorApellido.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            } else
                lblErrorApellido.Visibility = System.Windows.Visibility.Hidden;

            if (txtNombre.Text == String.Empty) {
                lblErrorNombre.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            } else
                lblErrorNombre.Visibility = System.Windows.Visibility.Hidden;

            if (txtDireccion.Text == String.Empty) {
                lblErrorDireccion.Visibility = System.Windows.Visibility.Visible;
                bError = true;
            } else
                lblErrorDireccion.Visibility = System.Windows.Visibility.Hidden;

            return bError;
             * */
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            ActualizarDatos();
            HabilitarDeshabilitarBotones(true);
            HabilitarDeshabilitarTextBox(false);
        }

        private void ActualizarDatos() {
            try {
                listaClientes = TrabajarClientes.ObtenerClientes();
                grid_content.DataContext  = listaClientes;
                CollectionViewSource cvs = Resources["ListaClientes"] as CollectionViewSource;
                cvs.Source = listaClientes;
                cvs.Filter += new FilterEventHandler(eventVistaCliente_Filter);
                GridListaClientes.DataContext = cvs;
            
                Vista = (CollectionView)CollectionViewSource.GetDefaultView(GridListaClientes.DataContext);
            } catch (Exception x) {
                MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPrimero_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToFirst();
        }

        private void btnUltimo_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToLast();
        }

        private void btnAnterior_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst) {
                Vista.MoveCurrentToLast();
            }
        }

        private void btnSiguiente_Click(object sender, RoutedEventArgs e) {
            ValidarSelectedItem();
            if (Vista != null) {
                Vista.MoveCurrentToNext();
                if (Vista.IsCurrentAfterLast) {
                    Vista.MoveCurrentToFirst();
                }
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e) {
            editMode = true;

            habilitarEdicion(editMode);
            HabilitarDeshabilitarBotones(false);
           // ActualizarDatos();
        }

        private void ValidarSelectedItem() {
            if (selectedItemList) {
                grid_content.DataContext = listaClientes;
                Vista = (CollectionView)CollectionViewSource.GetDefaultView(grid_content.DataContext);
                selectedItemList = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este cliente?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes) {
                try {
                    TrabajarClientes.EliminarCliente(Convert.ToInt32(txtID.Text));
                    LimpiarCampos();
                    HabilitarDeshabilitarBotones(true);
                    ActualizarDatos();
                } catch (Exception x) {
                    MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Evento que toma el cambio de texto del filtro
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e) {
            if (vistaColeccionFiltrada != null) {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
        }
        //Evento del filtro
        //No se toca la grilla ni el data provider, solo se añade el filter
        //al CollectionViewSource.
        private void eventVistaCliente_Filter(object sender, FilterEventArgs e) {
            Cliente cliente = e.Item as Cliente;
            if (textBox1 == null) {
                return;
            }

            if (cliente.Apellido.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)) {
                e.Accepted = true;
            } else {
                e.Accepted = false;
            }
        }

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e) {
            VistaPreviadeImpresion win = new VistaPreviadeImpresion(vistaColeccionFiltrada);
            win.Show();
        }

        private void Clientes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            Cliente selection = (Cliente)Clientes.SelectedItem;
            if (selection != null) {
                selectedItemList = true;
                grid_content.DataContext = new Cliente();
                txtID.Text = selection.ID.ToString();
                txtApellido.Text = selection.Apellido;
                txtNombre.Text = selection.Nombre;
                txtDNI.Text = selection.DNI;
                txtDireccion.Text = selection.Direccion;

                // Inhabilitar los TextBox

                HabilitarDeshabilitarBotones(true);
            }
            /*
            DataRowView dataRowView = Clientes.SelectedItem as DataRowView;
            if (dataRowView != null) {

                string codProducto = dataRowView[0].ToString();

                //Cliente oCliente = TrabajarClientes.

                txtID.Text = dataRowView[0].ToString();
                txtDNI.Text = dataRowView[1].ToString();
                txtApellido.Text = dataRowView[2].ToString();
                txtNombre.Text = dataRowView[3].ToString();
                txtDireccion.Text = dataRowView[4].ToString();

                // Inhabilitar los TextBox

                HabilitarDeshabilitarBotones(true);
            }
             * */
        }

    }
}
