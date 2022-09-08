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

namespace Vistas
{
    /// <summary>
    /// Interaction logic for ControlUsuarioLogin.xaml
    /// </summary>
    public partial class ControlUsuarioLogin : UserControl
    {
        public ControlUsuarioLogin()
        {
            InitializeComponent();
            lblValidacionUsuario.Visibility = System.Windows.Visibility.Hidden;
            lblValidacionContrasena.Visibility = System.Windows.Visibility.Hidden;
        }
        private void btnIngresar_Click(object sender, RoutedEventArgs e)
        {
            ValidarInputs();
            string szUsuario = txtUsuario.Text;
            string szPassword = txtContrasena.Password;
            if (szUsuario != "" && szPassword != "")
            {
                if ((szUsuario == "admin" && szPassword == "admin") ||
                    (szUsuario == "vendedor" && szPassword == "vendedor"))
                {
                    FormPrincipal frmPrincipal = new FormPrincipal();
                    frmPrincipal.lblUsername.Content = txtUsuario.Text;
                    frmPrincipal.Show();
                    Window window = Window.GetWindow(this);
                    window.Close();
                }
                else
                    MessageBox.Show("Credenciales incorrectas");
                clearTextBoxs();
            }
        }
        private void ValidarInputs()
        {
            if (txtUsuario.Text == "")
                lblValidacionUsuario.Visibility = System.Windows.Visibility.Visible;
            else
                lblValidacionUsuario.Visibility = System.Windows.Visibility.Hidden;

            if (txtContrasena.Password == "")
                lblValidacionContrasena.Visibility = System.Windows.Visibility.Visible;
            else
                lblValidacionContrasena.Visibility = System.Windows.Visibility.Hidden;

        }

        private void clearTextBoxs()
        {
            txtUsuario.Clear();
            txtContrasena.Clear();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window window = Window.GetWindow(this);
                window.DragMove();
            }
        }

    }
}
