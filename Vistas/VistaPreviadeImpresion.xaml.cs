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
    /// Interaction logic for VistaPreviadeImpresion.xaml
    /// </summary>
    public partial class VistaPreviadeImpresion : Window
    {

        public VistaPreviadeImpresion(CollectionViewSource vistaColeccionFiltrada)
        {
            InitializeComponent();
            Binding binding = new Binding();
            binding.Source = vistaColeccionFiltrada;
            BindingOperations.SetBinding(Clientes, ListView.ItemsSourceProperty, binding);
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
            }
        }
    }
}
