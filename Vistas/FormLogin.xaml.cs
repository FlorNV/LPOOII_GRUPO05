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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class FormLogin : Window
    {
        public FormLogin()
        {
            InitializeComponent();
            //lblValidacionUsuario.Visibility = System.Windows.Visibility.Hidden;
            //lblValidacionContrasena.Visibility = System.Windows.Visibility.Hidden;
        }

        //private void btnIngresar_Click(object sender, RoutedEventArgs e)
        //{
        //    ValidarInputs();
        //    string szUsuario = txtUsuario.Text;
        //    string szPassword = txtContrasena.Password;
        //    if (szUsuario != "" && szPassword != "")
        //    {
        //        if ((szUsuario == "admin" && szPassword == "admin") ||
        //            (szUsuario == "vendedor" && szPassword == "vendedor"))
        //        {
        //            FormPrincipal frmPrincipal = new FormPrincipal();
        //            frmPrincipal.lblUsername.Content = txtUsuario.Text;
        //            frmPrincipal.Show();
        //            this.Close();
        //        }
        //        else
        //            MessageBox.Show("Credenciales incorrectas");
        //        clearTextBoxs();
        //    }
        //}

        //private void ValidarInputs()
        //{
        //    if (txtUsuario.Text == "")
        //        lblValidacionUsuario.Visibility = System.Windows.Visibility.Visible;
        //    else
        //        lblValidacionUsuario.Visibility = System.Windows.Visibility.Hidden;

        //    if (txtContrasena.Password == "")
        //        lblValidacionContrasena.Visibility = System.Windows.Visibility.Visible;
        //    else
        //        lblValidacionContrasena.Visibility = System.Windows.Visibility.Hidden;

        //}

        //private void clearTextBoxs()
        //{
        //    txtUsuario.Clear();
        //    txtContrasena.Clear();
        //}

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
