using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.MVVM;
using dam.di.inventario.clase.Vista.Dialogos;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dam.di.inventario.clase.Vista.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCGesUsuario.xaml
    /// </summary>
    public partial class UCGesUsuario : UserControl
    {
        private diinventarioEntities invEnt;
        private MVUsuario mVUsu;
        private PropertyGroupDescription deptUsu; //Agurpacion checkbox
        private Predicate<object> predicadoNombreAp; //Filtro Predicado de busqueda por nombre o apellido

        public UCGesUsuario(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            mVUsu = new MVUsuario(invEnt);
            DataContext = mVUsu;

            //inicializa Property group descripcion 
            deptUsu = new PropertyGroupDescription("departamento1");

            //Inicializa filtro predicado busqueda nombre apellido
            predicadoNombreAp = new Predicate<object>(filtroNombreAp);
        }

        //Agrupar por departamento usuario
        private void chkGroupDept_Checked(object sender, RoutedEventArgs e)
        {
            mVUsu.listaUsuariosListColletion.GroupDescriptions.Add(deptUsu);
        }

        //Des agrupar por departamento de usuario
        private void chkGroupDept_Unchecked(object sender, RoutedEventArgs e)
        {
            mVUsu.listaUsuariosListColletion.GroupDescriptions.Clear();
        }

        //filtro del combo
        private void comboGrupoUsu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mVUsu.listaUsuariosListColletion.Filter = new Predicate<object>(filtroGrupoUsu);
        }

        //Metodo de filtrar usuarios por grupo
        //Metodos filtros
        //Devuelve los articulos que son iguales al seleccionado en el combo
        private bool filtroGrupoUsu(object obj)
        {
            bool correcto = true;
            //Primero hacemos la conversion
            usuario usu;
            if (obj != null)
            {
                usu = (usuario)obj;
                if (usu.grupo1 == null || !usu.grupo1.nombre.Equals(mVUsu.usuGrupoSeleccionadoComboFiltro.nombre))
                {
                    correcto = false;
                }
            }
            else
            {
                correcto = false;
            }
            return correcto;
        }

        //Quitar filtros
        private void quitarFiltrosUsuario_Click(object sender, RoutedEventArgs e)
        {
            mVUsu.listaUsuariosListColletion.Filter = null;
        }

        //Evento del Filtro de busqueda por nombre o apellido
        private void txtFiltroNombreUsu_SelectionChanged(object sender, RoutedEventArgs e)
        {
            mVUsu.listaUsuariosListColletion.Filter = predicadoNombreAp;
        }

        //Metodo del filtro de busqueda por nombre o apellido
        private bool filtroNombreAp(object obj)
        {
            bool correcto = true;
            usuario usu;

            if (obj != null)
            {
                usu = (usuario)obj; //Nota: en el if se pasa todo a mayusculas para que cuando busque, no distinga mayusculas de minusculas
                if ((usu.nombre == null || !usu.nombre.ToUpper().StartsWith(txtFiltroNombreUsu.Text.ToUpper()))
                    && (usu.apellido1 == null || !usu.apellido1.ToUpper().StartsWith(txtFiltroNombreUsu.Text.ToUpper())))
                {
                    correcto = false;
                }

                /*
                if (usu.apellido1 == null || !usu.apellido1.ToUpper().StartsWith(txtFiltroNombreUsu.Text.ToUpper()))
                {
                    correcto = false;
                }
                */

            }
            else
            {
                correcto = false;
            }

            return correcto;

        }

        //metodos y eventos de gestion de usuarios -----------------------------------
        //Anyadir usuario
        private void anyadirUsuario_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            metodoInsertar();
        }

        //Edicion datagrid
        private void editarUsuario_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            metodoEdicion();
        }

        private void borrarUsuario_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            metodoBorrado();
        }

        //Agregar usuario
        private void btnAgregarUsuarioFiltro_Click(object sender, RoutedEventArgs e)
        {
            metodoInsertar();
        }

        //editar usuario
        private void btnEditarUsuarioFiltro_Click(object sender, RoutedEventArgs e)
        {
            metodoEdicion();
        }

        private void btnEliminarUsuarioFiltro_Click(object sender, RoutedEventArgs e)
        {
            metodoBorrado();
        }

        //Metodo de inserccion
        private void metodoInsertar()
        {
            DialogUsuarioMVVM diag = new DialogUsuarioMVVM(invEnt, null, false);
            diag.ShowDialog();

            if ((bool)diag.DialogResult)
            {
                dgUsuario.Items.Refresh();
            }
        }

        //Metodo de edicion
        private void metodoEdicion()
        {
            if (dgUsuario.SelectedItem != null)
            {
                DialogUsuarioMVVM diag = new DialogUsuarioMVVM(invEnt, (usuario)(dgUsuario.SelectedItem), true);
                diag.ShowDialog();

                if ((bool)diag.DialogResult)
                {
                    dgUsuario.Items.Refresh();
                }

            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un usuario", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Metodo de borrado
        private void metodoBorrado()
        {
            //Ver si esta seleccionado el usuario
            if (dgUsuario.SelectedItem != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Quieres borrar el elemento?", "GESTION INVENTARIO", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //Ver si ha seleccionado que si quiere borrar el modelo de articulo 
                if (resultado == MessageBoxResult.Yes)
                {
                    usuario usu = (usuario)(dgUsuario.SelectedItem);

                    //comprobacion de elementos que no pueden ir sin usuario
                    if (usu.salida.Count == 0 && usu.ficherousuario.Count == 0)
                    {
                        //Borrar elemento
                        if (mVUsu.borrar((usuario)(dgUsuario.SelectedItem)))
                        {
                            mVUsu.listaUsuariosListColletion.Remove((usuario)(dgUsuario.SelectedItem));
                            MessageBox.Show("Elemento borrado", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                            dgUsuario.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Error al borrar", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        //si sus salidas son mayor que 0
                        if (usu.salida.Count > 0)
                        {
                            MessageBox.Show("El usuario tiene salidas y las salidas no pueden ir sin usuario, tienes que borrar sus salidas para realizar la operacion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                        //si sus ficheros de usuario son mayor que 0
                        if (usu.ficherousuario.Count > 0)
                        {
                            MessageBox.Show("El usuario tiene ficheros de usuario y los ficheros de usuario no pueden ir sin usuario, tienes que borrar sus ficheros de usuario para realizar la operacion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    
                }
                //else
                //{
                //    MessageBox.Show("Operacion cancelada", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
                //}
                
            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un usuario", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        
    }
}
