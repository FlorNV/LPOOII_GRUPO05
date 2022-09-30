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
    /// Interaction logic for WinProductos.xaml
    /// </summary>
    public partial class WinProductos : Window
    {
        public Window Owner { get; set; }

        public WinProductos()
        {
            InitializeComponent();
        }

        // Obtiene el codigo del producto seleccionado
        private void getCodigo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = Producto.SelectedItem as DataRowView;
            string codProducto = dataRowView[0].ToString();

            Producto oProducto = TrabajarProductos.obtenerProductoPorCodigo(codProducto);

            ((FormProductos)this.Owner).txtCodigo.Text = oProducto.CodProducto;
            ((FormProductos)this.Owner).txtCategoria.Text = oProducto.Categoria;
            ((FormProductos)this.Owner).txtColor.Text = oProducto.Color;
            ((FormProductos)this.Owner).txtDescripcion.Text = oProducto.Descripcion;
            ((FormProductos)this.Owner).txtPrecio.Text = oProducto.Precio.ToString();

            // Mostrar Código
            ((FormProductos)this.Owner).lblCodigo.Visibility = Visibility.Visible;
            ((FormProductos)this.Owner).txtCodigo.Visibility = Visibility.Visible;

            // Inhabilitar los TextBox
            ((FormProductos)this.Owner).txtCodigo.IsEnabled = false;
            ((FormProductos)this.Owner).txtCategoria.IsEnabled = false;
            ((FormProductos)this.Owner).txtColor.IsEnabled = false;
            ((FormProductos)this.Owner).txtDescripcion.IsEnabled = false;
            ((FormProductos)this.Owner).txtPrecio.IsEnabled = false;
            
            this.Close();
        }

        //private void Editar_Click(object sender, RoutedEventArgs e)
        //{
        //    DataRowView dataRowView = (DataRowView)((Button)sender).DataContext;
        //    string categoria = dataRowView[0].ToString();
        //    MessageBox.Show(categoria);
        //}
    }
}
