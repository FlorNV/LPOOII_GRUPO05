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

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlAltaVenta.xaml
    /// </summary>
    public partial class UserControlAltaVenta : UserControl {

        private Producto productoSelected;
        private Cliente clienteSelected;
        private Vendedor vendedorSelected;

        public UserControlAltaVenta() {
            InitializeComponent();
            txtProductoCantidad.Text = "1";
            dtpFechaVenta.Text = DateTime.Now.ToString();
        }

        private void cmbClientes_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            clienteSelected = cmbClientes.SelectedValue as Cliente;
            txtClienteDNI.Text = clienteSelected.DNI;
            txtClienteNombreCompleto.Text = clienteSelected.Apellido + ", " + clienteSelected.Nombre;
        }

        private void cmbVendedores_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            vendedorSelected = cmbVendedores.SelectedValue as Vendedor;
            txtVendedorLegajo.Text = vendedorSelected.Legajo;
            txtVendedorNombreCompleto.Text = vendedorSelected.Apellido + ", " + vendedorSelected.Nombre;
        }

        private void Productos_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DataRowView dataRowView = Productos.SelectedItem as DataRowView;
            if (dataRowView != null) {

                try {
                    string codProducto = dataRowView[0].ToString();

                    productoSelected = TrabajarProductos.obtenerProductoPorCodigo(codProducto);

                    txtProductoCodigo.Text = productoSelected.CodProducto;
                    txtProductoPrecio.Text = productoSelected.Precio.ToString();
                    txtProductoTotal.Text = (Convert.ToInt32(txtProductoCantidad.Text) * productoSelected.Precio).ToString();
                } catch (Exception x) {
                    MessageBox.Show("Ingrese un número en cantidad");
                }

            }
        }

        private void txtProductoCantidad_TextChanged(object sender, TextChangedEventArgs e) {
            if (txtProductoPrecio != null) {
                if (!String.IsNullOrEmpty(txtProductoPrecio.Text)) {
                    int cant = Convert.ToInt32(txtProductoCantidad.Text);
                    decimal precio = Convert.ToDecimal(txtProductoPrecio.Text);
                    txtProductoTotal.Text = (cant * precio).ToString();
                }
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            //this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {

            if (esValido()) {
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Guardar Venta?", "Venta", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes) {
                    try {
                        Venta oVenta = new Venta();
                        oVenta.FechaFactura = Convert.ToDateTime(dtpFechaVenta.Text);
                        oVenta.Legajo = txtVendedorLegajo.Text;
                        oVenta.DNI = txtClienteDNI.Text;
                        oVenta.CodProducto = txtProductoCodigo.Text;
                        oVenta.Precio = Convert.ToDecimal(txtProductoPrecio.Text);
                        oVenta.Cantidad = Convert.ToInt32(txtProductoCantidad.Text);
                        oVenta.Importe = oVenta.Precio * oVenta.Cantidad;

                        // Asigno el resultado del nro de factura creado
                        oVenta.NroFactura = TrabajarVentas.insertarVenta(oVenta);

                        // Pasar datos al nuevo formulario
                        Comprobante com = new Comprobante(oVenta, clienteSelected, productoSelected, vendedorSelected);
                        com.Show();

                        MessageBox.Show("Venta Guardada", "Venta");
                        LimpiarCampos();
                    } catch (Exception x) {
                        MessageBox.Show("Error: " + x.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private bool esValido() {
            if (cmbClientes.SelectedValue != null) {
                if (cmbVendedores.SelectedValue != null) {
                    if (!String.IsNullOrEmpty(dtpFechaVenta.Text)) {
                        if (!String.IsNullOrEmpty(txtProductoCodigo.Text)) {
                            if (!String.IsNullOrEmpty(txtProductoCantidad.Text)) {
                                return true;
                            } else {
                                MessageBox.Show("Debe indicar la cantidad del producto", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        } else {
                            MessageBox.Show("Debe seleccionar un producto", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    } else {
                        lblErrorFechaVenta.Visibility = Visibility.Visible;
                    }
                } else {
                    MessageBox.Show("Debe seleccionar un Vendedor", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            } else {
                MessageBox.Show("Debe seleccionar un cliente", "¡Atención!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            return false;
        }

        private void LimpiarCampos() {
            dtpFechaVenta.Text = "";
            cmbClientes.SelectedValue = null;
            cmbVendedores.SelectedValue = null;
            txtClienteDNI.Text = txtClienteNombreCompleto.Text = txtProductoCodigo.Text = txtProductoPrecio.Text = "";
            txtProductoTotal.Text = txtVendedorLegajo.Text = txtVendedorNombreCompleto.Text = "";
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e) {
            Content = new UserControlInicio();
        }
    }
}
