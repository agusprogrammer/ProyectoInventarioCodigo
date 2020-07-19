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
    /// Lógica de interacción para UCArbolDeptartamentos.xaml
    /// </summary>
    public partial class UCArbolDeptartamentos : UserControl
    {
        private diinventarioEntities invEnt;
        private MVDepartamentos mvDept;

        public UCArbolDeptartamentos(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            mvDept = new MVDepartamentos(invEnt);
            DataContext = mvDept;
        }

        private void editarUsu_contextMenu_ArbolDept_Click(object sender, RoutedEventArgs e)
        {
            if (treeViewDepartamentos.SelectedItem != null)
            {
                if(treeViewDepartamentos.SelectedItem is usuario)
                {
                    //MessageBox.Show("Usuario seleccionado", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                    //edicion de usuario
                    DialogUsuarioMVVM diag = new DialogUsuarioMVVM(invEnt, (usuario)(treeViewDepartamentos.SelectedItem), true);
                    diag.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Esto que has seleccionado no es un Usuario", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un usuario", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Ejemplo de evento del arbol que selecciona directamente
        //sirve para mostrar mensajes o seleccionar en un datagrid nada mas se selecciona un elemento
        /*
        private void treeViewDepartamentos_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is usuario)
            {
                usuario usu = (usuario)e.NewValue;
                //MessageBox()
                dtEmpleados.ItemsSource = usu.salida;

            }
        }
        */
    }
}
