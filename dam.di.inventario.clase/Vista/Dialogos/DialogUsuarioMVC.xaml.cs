using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.Servicios;
using dam.di.inventario.clase.Validacion;
using dam.di.inventario.Servicios;
using NLog;
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
    /// Lógica de interacción para DialogUsuarioMVC.xaml
    /// </summary>
    public partial class DialogUsuarioMVC : Window
    {
        //segunda forma de realizar un formulario MVC de insertar

        private diinventarioEntities invEnt;
        private UsuarioServicio usuServ;
        private ServicioGenerico<tipousuario> tipoUsuServ;
        private ServicioGenerico<rol> rolServ;
        private ServicioGenerico<grupo> grupoServ;
        private ServicioGenerico<departamento> departamentoServ;
        //Nota: los objetos son en minuscula, usar los objetos de los pojos
        //estos objetos se ponen en el ServicioGenerico<> en la anotacion de
        //diamante, cada uno es un objeto pojo (en realidad se llaman los pocos)

        /*
        Nota: si la primera forma da error de que no detecta los
        servicios y los objetos para los comboboxes, 
        usar el servicio generico
        */

        private static Logger log = LogManager.GetCurrentClassLogger();
        private ValidaMVC valMVC; //Clase de validacion

        public DialogUsuarioMVC(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            usuServ = new UsuarioServicio(invEnt);

            //validacion
            valMVC = new ValidaMVC();

            //inicializar mediante servicio generico
            tipoUsuServ = new ServicioGenerico<tipousuario>(invEnt);
            rolServ = new ServicioGenerico<rol>(invEnt);
            grupoServ = new ServicioGenerico<grupo>(invEnt);
            departamentoServ = new ServicioGenerico<departamento>(invEnt);

            /*
            tipoUsuServ = new TipoUsuarioServicio(invEnt);
            rolServ = new RolServicio(invEnt);
            grupoServ = new GrupoServicio(invEnt);
            dptoServ = new DptoServicio(invEnt);
            */

            //poner las listas en el combo
            comboTipoUsu.ItemsSource = tipoUsuServ.getAll().ToList();
            comboRolUsu.ItemsSource = rolServ.getAll().ToList();
            comboGrupoUsu.ItemsSource = grupoServ.getAll().ToList();
            comboDepartamentoUsu.ItemsSource = departamentoServ.getAll().ToList();

        }

        //Metodo de recoger usuario
        private void recogeUsuario(usuario usuario)
        {
            usuario.username = textboxUsername.Text;
            usuario.password = passContrasenya.Password;

            //Comboboxes, si hay alguno que se deja en blanco, este dara un error
            //combo Rol
            //usuario.rol1 = (rol)comboRolUsu.SelectedItem;
            rol rolUsu = new rol();
            if (comboRolUsu != null)
            {
                rolUsu = (rol)comboRolUsu.SelectedItem;
                usuario.rol1 = rolUsu;
            }

            //combo tipo
            tipousuario tipoUsu = new tipousuario();
            if (comboTipoUsu.SelectedItem != null)
            {
                tipoUsu = (tipousuario)comboTipoUsu.SelectedItem;
                usuario.tipousuario = tipoUsu;
            }

            //combo grupo
            grupo grupoUsu = new grupo();
            if (comboGrupoUsu.SelectedItem != null)
            {
                grupoUsu = (grupo)comboGrupoUsu.SelectedItem;
                usuario.grupo1 = grupoUsu;
            }

            //combo departamento
            departamento deptUsu = new departamento();
            if(comboDepartamentoUsu.SelectedItem != null)
            {
                deptUsu = (departamento)comboDepartamentoUsu.SelectedItem;
                usuario.departamento1 = deptUsu;
            }
            
            usuario.nombre = textboxNombre.Text;
            usuario.apellido1 = textboxApellido1.Text;
            usuario.apellido2 = textboxApellido2.Text;
            usuario.domicilio = textboxDomicilio.Text;
            usuario.poblacion = textboxPoblacion.Text;
            usuario.codpostal = textboxCodigoPostal.Text;
            usuario.email = textboxEmail.Text;
            usuario.telefono = textboxTelefono.Text;
        }

        //evento de guardar
        private void btnGuardarUsu_Click(object sender, RoutedEventArgs e)
        {

            if (valida())
            {

                usuario usu = new usuario();
                recogeUsuario(usu);

                try
                {
                    if (usuServ.usuarioUnico(usu.username))
                    {

                        usuServ.add(usu);
                        usuServ.save();

                        MessageBox.Show("Usuario guardado correctamente", "GESTIÓN USUARIOS", MessageBoxButton.OK, MessageBoxImage.Information);
                        log.Info("\nInsertando un usuario ... Todo correcto");
                        //Devuelve true y cierra el dialogo
                        DialogResult = true;

                    }
                    else
                    {
                            //No se pueden repetir nombres de usuario
                            MessageBox.Show("Username de usuario incorrecto", "GESTÓN USUARIOS", MessageBoxButton.OK, MessageBoxImage.Error);
                            log.Error("\nInsertando un usuario: ...Usuario repetido");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar en la base de datos", "GESTÓN USUARIOS", MessageBoxButton.OK, MessageBoxImage.Error);
                    log.Error("insertando un usuario: " + ex.StackTrace);
                }
                
            }

        }

        //Metodo de validar
        private bool valida()
        {
            bool correcto = true;

            //Textbox Username
            if (valMVC.validaCadena(textboxUsername.Text))
            {
                correcto = false;
                ValidaMVC.muestraError(textboxUsername);
            }
            else
            {
                ValidaMVC.quitarError(textboxUsername);
            }

            //Pass Password (Mirar si es correcto)
            if (valMVC.validaCadena(passContrasenya.Password))
            {
                correcto = false;
                ValidaMVC.muestraError(passContrasenya);
            }
            else
            {
                ValidaMVC.quitarError(passContrasenya);
            }

            //combo Tipo usuario
            if (valMVC.objetoVacio(comboTipoUsu.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboTipoUsu);
            }
            else
            {
                ValidaMVC.quitarError(comboTipoUsu);
            }

            //combo Rol
            if (valMVC.objetoVacio(comboRolUsu.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboRolUsu);
            }
            else
            {
                ValidaMVC.quitarError(comboRolUsu);
            }

            //Textbox Nombre
            if (valMVC.validaCadena(textboxNombre.Text))
            {
                correcto = false;
                ValidaMVC.muestraError(textboxNombre);
            }
            else
            {
                ValidaMVC.quitarError(textboxNombre);
            }

            //Textbox Apellido 1
            if (valMVC.validaCadena(textboxApellido1.Text))
            {
                correcto = false;
                ValidaMVC.muestraError(textboxApellido1);
            }
            else
            {
                ValidaMVC.quitarError(textboxApellido1);
            }

            return correcto;
        }

        //Eventos de seleccion de componentes
        private void textboxUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textboxUsername.Text))
            {
                ValidaMVC.muestraError(textboxUsername);
            }
            else
            {
                ValidaMVC.quitarError(textboxUsername);
            }
        }

        private void passContrasenya_PasswordChanged(object sender, RoutedEventArgs e)
        {
            //Ver si es asi la validacion de contrasenya
            if (string.IsNullOrEmpty(passContrasenya.Password))
            {
                ValidaMVC.muestraError(passContrasenya);
            }
            else
            {
                ValidaMVC.quitarError(passContrasenya);
            }
        }

        private void comboTipoUsu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //depende del tipo de usuario seleccionar una cosa u otra
            ValidaMVC.quitarError(comboTipoUsu); //Cambiar a imagen del combo

            //ver si es asi lo de cambiar el tipo
            tipousuario tipoUsuComprobar = new tipousuario();
            tipoUsuComprobar = (tipousuario)comboTipoUsu.SelectedItem;

            if (tipoUsuComprobar != null)
            {
                if (tipoUsuComprobar.nombre.Equals("Alumno"))
                {
                    comboGrupoUsu.IsEnabled = true;
                }
                else
                {
                    comboGrupoUsu.IsEnabled = false;
                }

                if (tipoUsuComprobar.nombre.Equals("Profesor"))
                {
                    comboDepartamentoUsu.IsEnabled = true;
                }
                else
                {
                    comboDepartamentoUsu.IsEnabled = false;
                }
            }

            ValidaMVC.quitarError(comboTipoUsu);

        }

        private void comboRolUsu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(comboRolUsu);
        }

        private void textboxNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textboxNombre.Text))
            {
                ValidaMVC.muestraError(textboxNombre);
            }
            else
            {
                ValidaMVC.quitarError(textboxNombre);
            }
        }

        private void textboxApellido1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(textboxApellido1.Text))
            {
                ValidaMVC.muestraError(textboxApellido1);
            }
            else
            {
                ValidaMVC.quitarError(textboxApellido1);
            }
        }

        private void btnCancelarUsu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
