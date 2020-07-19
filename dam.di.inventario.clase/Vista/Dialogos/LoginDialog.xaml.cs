using dam.di.inventario.clase.Modelo;
using dam.di.inventario.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dam.di.inventario.clase.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        /// <summary>
        /// Variable para inicializar el contexto de la base de datos
        /// </summary>
        private diinventarioEntities invEnt;
        //El nombre de diinventarioEntities
        //se puso anteriormente en la configuracion de la conexion a la bd

        /// <summary>
        /// Variable que nos permite trabajar con la 
        /// tabla usuario de la base de datos
        /// </summary>
        private UsuarioServicio usuServ;

        public LoginDialog()
        {
            InitializeComponent();
            invEnt = new diinventarioEntities(); //inicializa el contexto de la base de datos
            usuServ = new UsuarioServicio(invEnt); //le pasamos la conexion a la bd
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            String usu = txtLogin.Text;
            String pass = passLogin.Password;

            if (usu.Equals("") || pass.Equals(""))
            {
                txtLogin.BorderBrush = Brushes.Red;
                passLogin.BorderBrush = Brushes.Red;
                MessageBox.Show("El usuario y/o contraseña estan vacios", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {

                if (usuServ.login(usu, pass))
                {
                    MainWindow ventana = new MainWindow(invEnt, usuServ.usuLogin);
                    ventana.Show();
                    this.Close();
                }
                else
                {
                    txtLogin.BorderBrush = Brushes.Red;
                    passLogin.BorderBrush = Brushes.Red;
                    MessageBox.Show("Error en el usuario y/o contraseña incorrectos", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
