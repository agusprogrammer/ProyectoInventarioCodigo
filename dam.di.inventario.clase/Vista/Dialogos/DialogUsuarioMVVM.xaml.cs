using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.MVVM;
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
    /// Lógica de interacción para DialogUsuarioMVVM.xaml
    /// </summary>
    public partial class DialogUsuarioMVVM : Window
    {
        private diinventarioEntities invEnt;
        private MVUsuario mVUsuario;
        //bool para activar la edicion
        private bool edicion;

        //uusario anterior para comprobar su username
        private usuario usuarioAnterior;

        public DialogUsuarioMVVM(diinventarioEntities ent, usuario usuEditarSelect, bool _edicion)
        {
            InitializeComponent();
            invEnt = ent;

            //cosas mv
            mVUsuario = new MVUsuario(invEnt);
            DataContext = mVUsuario; //si no se pone el datacontext el binding no va
            //deshabilitar boton de guardar en funcion de si hay errores o no
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mVUsuario.OnErrorEvent));
            mVUsuario.btnGuardar = btnGuardarUsu;

            //Bool para activacion de la edicion
            edicion = _edicion;

            //usuario anterior, antes de modificar
            usuarioAnterior = new usuario();
            usuarioAnterior = usuEditarSelect;

            //si la edicion es true
            if (edicion)
            {
                mVUsuario.usuarioNuevo = usuEditarSelect;
                passContrasenyaMV.Password = mVUsuario.usuarioNuevo.password;
            }

        }

        private void btnGuardarUsu_Click(object sender, RoutedEventArgs e)
        {
            //Edicion usuario
            if (edicion)
            {
                usuario usuEditar = mVUsuario.usuarioNuevo;
                usuEditar.password = passContrasenyaMV.Password;

                //Si la contrasenya esta vacia
                if (usuEditar.password != null || usuEditar.password == "")
                {
                    //Si el usuario es el mismo, realizar operaciones
                    if (usuEditar.username == usuarioAnterior.username)
                    {
                        //si el usuario tiene un departamento y un grupo no se puede poner
                        if (mVUsuario.usuarioNuevo.departamento1 != null && mVUsuario.usuarioNuevo.grupo1 != null)
                        {
                            MessageBox.Show("No se puede poner un usuario con un grupo y con un departamento", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            
                            if (mVUsuario.edita())
                            {
                                MessageBox.Show("Edicion ralizada correctamente", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                                DialogResult = true;
                            }
                            else
                            {
                                MessageBox.Show("Error al realizar la edicion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }

                    }//Sino el usuario no es el mismo, en caso de usuario nuevo modificado
                    else
                    {
                        //Comprobar que el usuario es repedido
                        bool usuBool = mVUsuario.usuarioUnico;

                        //si el usuario no existe
                        if(usuBool == true)
                        {
                            //si el usuario tiene un departamento y un grupo no se puede poner
                            if (mVUsuario.usuarioNuevo.departamento1 != null && mVUsuario.usuarioNuevo.grupo1 != null)
                            {
                                MessageBox.Show("No se puede poner un usuario con un grupo y con un departamento", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                if (mVUsuario.edita())
                                {
                                    MessageBox.Show("Edicion ralizada correctamente", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                                    DialogResult = true;
                                }
                                else
                                {
                                    MessageBox.Show("Error al realizar la edicion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show("Error al realizar la insercción, usuario repetido", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                    
                    
                    
                }
                else
                {
                    MessageBox.Show("no se puede dejar la contraseña vacia", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                
            } //Insertar usuario
            else
            {

                //Como el campo de la contrasenya passwordbox
                //no admite MVVM, se lo tenemos que poner por MVC 

                mVUsuario.usuarioNuevo.password = passContrasenyaMV.Password;
                //Ver como pasar la contrasenya al usuario nuevo

                //poner: if(mvUsuario.usuarioUnico) y poner foco en el txtUsername.Focus()
                bool usuBool = mVUsuario.usuarioUnico;

                if (usuBool == true)
                {
                    //si el usuario tiene un departamento y un grupo no se puede poner
                    if (mVUsuario.usuarioNuevo.departamento1 != null && mVUsuario.usuarioNuevo.grupo1 != null)
                    {
                        MessageBox.Show("No se puede poner un usuario con un grupo y con un departamento", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {

                        if (mVUsuario.guarda())
                        {
                            MessageBox.Show("insercción ralizada correctamente", "GESTION INVETARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                            DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Error al realizar la insercción", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Error al realizar la insercción, usuario repetido", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            

        }

        private void btnCancelarUsu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Eventos para seleccionar los combos
        //Habilita o deshabilita segun lo que 
        //seleccionamos en el combo del tipo
        private void comboTipoUsuMV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tipousuario tipoUsuComprobarMV = new tipousuario();
            tipoUsuComprobarMV = (tipousuario)comboTipoUsuMV.SelectedItem;

            if (tipoUsuComprobarMV != null)
            {
                if (tipoUsuComprobarMV.nombre.Equals("Alumno"))
                {
                    comboGrupoUsuMV.IsEnabled = true;
                }
                else
                {
                    comboGrupoUsuMV.IsEnabled = false;
                }

                if (tipoUsuComprobarMV.nombre.Equals("Profesor"))
                {
                    comboDepartamentoUsuMV.IsEnabled = true;
                }
                else
                {
                    comboDepartamentoUsuMV.IsEnabled = false;
                }
            }
        }
    }
}
