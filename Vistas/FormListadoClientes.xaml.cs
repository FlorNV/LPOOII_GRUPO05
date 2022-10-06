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
    /// Interaction logic for FormListadoClientes.xaml
    /// </summary>
    public partial class FormListadoClientes : Window
    {
        private CollectionViewSource vistaColeccionFiltrada;
        public FormListadoClientes()
        {
            InitializeComponent();
            //traigo la coleccion
            vistaColeccionFiltrada = Resources["ListaClientes"] as CollectionViewSource;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Evento que toma el cambio de texto del filtro
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (vistaColeccionFiltrada != null)
            {
                vistaColeccionFiltrada.Filter += eventVistaCliente_Filter;
            }
        }
        //Evento del filtro
        //No se toca la grilla ni el data provider, solo se añade el filter
        //al CollectionViewSource.
        private void eventVistaCliente_Filter(object sender, FilterEventArgs e)
        {
            Cliente cliente = e.Item as Cliente;
            if (textBox1 == null) {
                return;
            }

            if (cliente.Apellido.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else {
                e.Accepted = false;
            }
        }
        

    }
}
