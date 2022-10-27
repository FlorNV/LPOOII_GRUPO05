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
using System.Data;

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlUsuarios.xaml
    /// </summary>
    public partial class UserControlUsuarios : UserControl {
        public UserControlUsuarios() {
            InitializeComponent();
        }
            
        private void habilitarEdicion(bool mode) {
            txtApellido.IsEnabled = mode;
            txtNombre.IsEnabled = mode;
            txtNombreUsuario.IsEnabled = mode;
            txtPassword.IsEnabled = mode;
        }

        private void HabilitarBotonesGuardarCancelar(bool state) {
            btnCancelar.IsEnabled = state;
            btnGuardar.IsEnabled = state;
        }

        private void HabilitarBotonesABM(bool state) {
            btnNuevo.IsEnabled = state;
            btnModificar.IsEnabled = state;
            btnEliminar.IsEnabled = state;
        }

        private void HabilitarDeshabilitarBotones(bool b) {
            HabilitarBotonesABM(b);
            HabilitarBotonesGuardarCancelar(!b);
        }

        private void Usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DataRowView dataRowView = Usuarios.SelectedItem as DataRowView;
            //MessageBox.Show(dataRowView.ToString());
            if (dataRowView != null) {
                txtLegajo.Text = dataRowView[0].ToString();
                txtApellido.Text = dataRowView[1].ToString();
                txtNombre.Text = dataRowView[2].ToString();
                cmbRoles.SelectedItem = dataRowView[3].ToString();
                txtNombreUsuario.Text = dataRowView[4].ToString();
                txtPassword.Text = dataRowView[5].ToString();

                // Inhabilitar los TextBox
                /*
                txtCategoria.IsEnabled = false;
                txtColor.IsEnabled = false;
                txtDescripcion.IsEnabled = false;
                txtPrecio.IsEnabled = false;
                 * */

                HabilitarDeshabilitarBotones(true);
            }
        }

        private void cmbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            txtNombre.Text = cmbRoles.SelectedItem.ToString();
        }
        

    }
}
