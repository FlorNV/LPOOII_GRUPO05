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

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlInicio.xaml
    /// </summary>
    public partial class UserControlInicio : UserControl {
        public UserControlInicio() {
            InitializeComponent();
        }

        private void btnAcercaDe_Click(object sender, RoutedEventArgs e)
        {
            AcercaDe win = new AcercaDe();
            win.Show();
        }
    }
}
