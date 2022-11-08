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
using ClasesBase;

namespace Vistas.Views {
    /// <summary>
    /// Lógica de interacción para UserControlUsuarios.xaml
    /// </summary>
    public partial class UserControlUsuarios : UserControl {

        private bool editMode = false;
        private Usuario user;

        public UserControlUsuarios(Usuario userLog) {
            InitializeComponent();

            user = userLog;
            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        private void HabilitarBotonesInicio() {
            btnCancelar.IsEnabled = false;
            btnGuardar.IsEnabled = false;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            btnNuevo.IsEnabled = true;
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

        private void HabilitarDeshabilitarTextBox(bool b) {
            //txtCodigo.IsEnabled = b;
            txtApellido.IsEnabled = b;
            txtNombre.IsEnabled = b;
            txtNombreUsuario.IsEnabled = b;
            txtPassword.IsEnabled = b;
        }

        private void LimpiarCampos() {
            //txtCodigo.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtNombreUsuario.Text = String.Empty;
            txtNombre.Text = String.Empty;
            txtLegajo.Text = String.Empty;
            txtApellido.Text = String.Empty;

            editMode = false;
        }

        private void Usuarios_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            DataRowView dataRowView = Usuarios.SelectedItem as DataRowView;
            if (dataRowView != null) {
                txtLegajo.Text = dataRowView[0].ToString();
                txtApellido.Text = dataRowView[1].ToString();
                txtNombre.Text = dataRowView[2].ToString();
                cmbRoles.SelectedValue = dataRowView[3].ToString();
                txtNombreUsuario.Text = dataRowView[4].ToString();
                txtPassword.Text = dataRowView[5].ToString();

                HabilitarDeshabilitarBotones(true);
            }
        }

        private void cmbRoles_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e) {
            LimpiarCampos();
            HabilitarBotonesInicio();
            HabilitarDeshabilitarTextBox(false);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e) {
            LimpiarCampos();
            HabilitarDeshabilitarTextBox(true);
            HabilitarDeshabilitarBotones(false);
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e) {
            editMode = true;

            habilitarEdicion(editMode);
            HabilitarDeshabilitarBotones(false);
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e) {
            if (cmbRoles.SelectedItem != null) {
                if (TrabajarUsuarios.ObtenerUsuarioPorUsername(txtNombreUsuario.Text) == null)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea guardar este elemento?",
                    "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        Usuario usr = new Usuario();
                        usr.Apellido = txtApellido.Text;
                        usr.Nombre = txtNombre.Text;
                        usr.Username = txtNombreUsuario.Text;
                        usr.Password = txtPassword.Text;
                        //usr.Rol = (cmbRoles.SelectedItem as String).Substring(38);
                        usr.Rol = cmbRoles.SelectedValue.ToString();
                        if (editMode)
                        {
                            usr.Legajo = Convert.ToInt32(txtLegajo.Text);
                            TrabajarUsuarios.ModificarUsuario(usr); ;
                            MessageBox.Show("Usuario modificado", "Modificar");
                        }
                        else
                        {
                            TrabajarUsuarios.InsertarUsuario(usr);
                            MessageBox.Show("Usuario guardado", "Guardar");
                        }

                        Usuarios.DataContext = TrabajarUsuarios.ObtenerUsuarios();

                        HabilitarDeshabilitarTextBox(false);
                        HabilitarBotonesInicio();

                        LimpiarCampos();
                    }
                } else {
                    MessageBox.Show("Este nombre de usuario ya esta registrado en el sistema.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else {
                MessageBox.Show("Seleccione un Rol", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e) {
            if(!String.IsNullOrEmpty(txtLegajo.Text)) {
                if (txtNombreUsuario.Text != "admin") {
                    if (txtNombreUsuario.Text != user.Username) {
                        MessageBoxResult messageBoxResult = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?",
                        "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            TrabajarUsuarios.EliminarUsuario(Convert.ToInt32(txtLegajo.Text));
                            LimpiarCampos();

                            HabilitarBotonesInicio();
                            HabilitarDeshabilitarTextBox(false);
                            Usuarios.DataContext = TrabajarUsuarios.ObtenerUsuarios();
                        }
                    } else {
                        MessageBox.Show("No puedes eliminar tu cuenta", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else {
                    MessageBox.Show("No puedes eliminar a este usuario", "Aviso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        

    }
}
