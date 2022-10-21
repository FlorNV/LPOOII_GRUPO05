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

namespace Vistas {
    /// <summary>
    /// Interaction logic for Comprobante.xaml
    /// </summary>
    public partial class Comprobante : Window {
        public Comprobante(Venta venta, Cliente cliente, Producto prod, Vendedor vendedor) {
            InitializeComponent();

            tbNroVenta.Text += venta.NroFactura.ToString();
            tbFecha.Text += venta.FechaFactura;

            tbApellidoCli.Text += cliente.Apellido;
            tbNombreCli.Text += cliente.Nombre;
            tbDniCli.Text += cliente.DNI;

            tbApellidoVend.Text += vendedor.Apellido;
            tbNombreVend.Text += vendedor.Nombre;

            tbCantidad.Text = venta.Cantidad.ToString();
            tbDescripcion.Text = prod.Descripcion;
            tbPrecioUnit.Text = prod.Precio.ToString();
            tbImporte.Text = venta.Importe.ToString();

            tbTotal.Text = venta.Importe.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {

        }
    }
}
