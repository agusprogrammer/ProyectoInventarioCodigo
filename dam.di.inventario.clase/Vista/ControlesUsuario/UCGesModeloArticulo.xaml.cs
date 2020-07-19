using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.MVVM;
using dam.di.inventario.clase.Vista.Dialogos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Lógica de interacción para UCGesModeloArticulo.xaml
    /// </summary>
    public partial class UCGesModeloArticulo : UserControl
    {

        private diinventarioEntities invEnt;
        private MVModelo mVMod;
        private PropertyGroupDescription grupoTipo; //Agurpacion checkbox
        private Predicate<object> predicadoNombre; //Filtro predicado de buscar por nombre

        public UCGesModeloArticulo(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            mVMod = new MVModelo(invEnt);
            DataContext = mVMod;

            //inicializa Property group descripcion 
            grupoTipo = new PropertyGroupDescription("tipo");
            predicadoNombre = new Predicate<object>(filtroNombre); 
            //Inicializar filtro predicado

        }

        //Agrupar list colletion view checkbox
        private void chkGroupTipo_Checked(object sender, RoutedEventArgs e)
        {
            //Agrupar por tipo, referencia MVVM con el tipo modeloArticulo modelo articulo
            //mVMod.listaModelosListColletion.GroupDescriptions.Add(new PropertyGroupDescription("tipo"));
            mVMod.listaModelosListColletion.GroupDescriptions.Add(grupoTipo);
        }

        //Desagrupar list colletion view checkbox
        private void chkGroupTipo_Unchecked(object sender, RoutedEventArgs e)
        {
            mVMod.listaModelosListColletion.GroupDescriptions.Clear(); //quita todas las agrupaciones
        }

        //combo filtro
        private void comboFiltroTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mVMod.listaModelosListColletion.Filter = new Predicate<object>(filtroTipo);
        }

        //Devuelve los modelos articulos que son iguales
        private bool filtroTipo(object obj)
        {
            bool correcto = true;
            //Primero hacemos la conversion
            modeloarticulo modeloArt;
            if (obj != null)
            {
                modeloArt = (modeloarticulo)obj;
                if (modeloArt.tipoarticulo == null || !modeloArt.tipoarticulo.nombre.Equals(mVMod.tipoArtSeleccionadoComboFiltro.nombre))
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

        //quitar filtros del combo
        private void quitarFiltrosModeloArt_Click(object sender, RoutedEventArgs e)
        {
            mVMod.listaModelosListColletion.Filter = null;
        }

        //filtro de buscar nombres
        private void txtFiltroNombre_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //mVMod.listaModelosListColletion.Filter = new Predicate<object>(filtroNombre);
            //Usamos el metodo de filtro nombre
            //Otra forma, se lo pasamos ya inicializado, forma recomendada:
            mVMod.listaModelosListColletion.Filter = predicadoNombre;
        }

        private bool filtroNombre(object obj)
        {
            bool correcto = true;
            modeloarticulo modeloArt;

            if (obj != null)
            {
                modeloArt = (modeloarticulo)obj; //Nota: en el if se pasa todo a mayusculas para que cuando busque, no distinga mayusculas de minusculas
                if (modeloArt.nombre == null || !modeloArt.nombre.ToUpper().StartsWith(txtFiltroNombre.Text.ToUpper()))
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

        //edicion de modelo articulo
        private void editarModeloArt_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            //comprobar si esta a null
            if (dgModeloArticulo.SelectedItem != null)
            {
                DialogModeloArticuloMVVM diag = new DialogModeloArticuloMVVM(invEnt, (modeloarticulo)(dgModeloArticulo.SelectedItem), true);
                diag.ShowDialog();

                //si el dialog result se ha cerrado correctamente
                if((bool)diag.DialogResult)
                {
                    dgModeloArticulo.Items.Refresh();
                }

            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un modelo de Articulo", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        //edicion mediante boton
        private void btnEditarModeloArticuloFiltro_Click(object sender, RoutedEventArgs e)
        {
            //comprobar si esta a null
            if (dgModeloArticulo.SelectedItem != null)
            {
                DialogModeloArticuloMVVM diag = new DialogModeloArticuloMVVM(invEnt, (modeloarticulo)(dgModeloArticulo.SelectedItem), true);
                diag.ShowDialog();

                //si el dialog result se ha cerrado correctamente
                if ((bool)diag.DialogResult)
                {
                    dgModeloArticulo.Items.Refresh();
                }

            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un modelo de Articulo", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Borrar modelo de articulo
        private void borrarModeloArticulo_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            //Ver si esta seleccionado el modelo de articulo
            if (dgModeloArticulo.SelectedItem != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Quieres borrar el elemento?", "GESTION INVENTARIO", MessageBoxButton.YesNo, MessageBoxImage.Question);

                //Ver si ha seleccionado que si quiere borrar el modelo de articulo 
                if (resultado == MessageBoxResult.Yes)
                {
                    modeloarticulo mod = (modeloarticulo)(dgModeloArticulo.SelectedItem);
                    if (mod.articulo.Count == 0)
                    {
                        //Borrar elemento
                        if (mVMod.borrar((modeloarticulo)(dgModeloArticulo.SelectedItem)))
                        {
                            //try { 
                            //mVMod.borrar((modeloarticulo)(dgModeloArticulo.SelectedItem));
                            //dgModeloArticulo.Items.Refresh(); //no se actualiza con refresh en windows7, pero si en w10
                            mVMod.listaModelosListColletion.Remove((modeloarticulo)(dgModeloArticulo.SelectedItem)); //el refresh es para el list collestion view, no las listas normales
                            MessageBox.Show("Elemento borrado", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                            dgModeloArticulo.Items.Refresh();
                            /*}
                                catch (Exception ex) //DbEntityValidationException
                            {
                                MessageBox.Show("El elemento tiene articulos y los articulos no pueden ir sin modelo, tienes que borrar sus articulos para realizar la operacion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                            }*/
                        }
                        else
                        {
                            MessageBox.Show("Error al borrar", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("El elemento tiene articulos y los articulos no pueden ir sin modelo, tienes que borrar sus articulos para realizar la operacion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                        
                    }
                    
                }
                else
                {
                    MessageBox.Show("Tienes que seleccionar un modelo de Articulo", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            
        }

        //Anyadir modelo de articulo
        private void anyadirModeloArt_ContextMenu_Click(object sender, RoutedEventArgs e)
        {
            DialogModeloArticuloMVVM diag = new DialogModeloArticuloMVVM(invEnt, null, false);
            diag.ShowDialog();

            if ((bool)diag.DialogResult)
            {
                dgModeloArticulo.Items.Refresh();
            }
        }

        private void btnAgregarModeloArticuloFiltro_Click(object sender, RoutedEventArgs e)
        {
            DialogModeloArticuloMVVM diag = new DialogModeloArticuloMVVM(invEnt, null, false);
            diag.ShowDialog();

            if ((bool)diag.DialogResult)
            {
                dgModeloArticulo.Items.Refresh();
            }
        }
    }
}
