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
using System.IO;

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlInicio.xaml
    /// </summary>
    public partial class UserControlInicio : UserControl {
        public UserControlInicio() {
            InitializeComponent();
        }

        private void imgInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AcercaDe acercaDe = new AcercaDe();
            acercaDe.Show();
            MessageBox.Show(Directory.GetCurrentDirectory().Remove(38) + "media\\Wildlife.wmv");
        }
    }
}
