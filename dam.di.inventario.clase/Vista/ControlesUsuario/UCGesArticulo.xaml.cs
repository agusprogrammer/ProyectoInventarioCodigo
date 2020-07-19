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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dam.di.inventario.clase.Vista.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCGesArticulo.xaml
    /// </summary>
    public partial class UCGesArticulo : UserControl
    {
        private diinventarioEntities invEnt;
        private MVArticulo mvArt;
        private PropertyGroupDescription grupoEspacio; //Agurpacion checkbox

        public UCGesArticulo(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            mvArt = new MVArticulo(invEnt);
            DataContext = mvArt;

            //inicializa Property group descripcion 
            grupoEspacio = new PropertyGroupDescription("espacio");
        }

        //Agrupar por espacio
        private void chkGroupArtEspacio_Checked(object sender, RoutedEventArgs e)
        {
            mvArt.listaArticulosListColletion.GroupDescriptions.Add(grupoEspacio);
        }

        //desagrupar por espacio
        private void chkGroupArtEspacio_Unchecked(object sender, RoutedEventArgs e)
        {
            mvArt.listaArticulosListColletion.GroupDescriptions.Clear();
        }

        //Filtro combobox departamento articulo
        private void comboFiltroDept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mvArt.listaArticulosListColletion.Filter = new Predicate<object>(filtroDepartamento);
        }

        //Quitar filtros
        private void quitarFiltrosArt_Click(object sender, RoutedEventArgs e)
        {
            mvArt.listaArticulosListColletion.Filter = null;
        }
        
        //Metodos filtros
        //Devuelve los articulos que son iguales al seleccionado en el combo
        private bool filtroDepartamento(object obj)
        {
            bool correcto = true;
            //Primero hacemos la conversion
            articulo art;
            if (obj != null)
            {
                art = (articulo)obj;
                if (art.departamento1 == null || !art.departamento1.nombre.Equals(mvArt.deptSeleccionadoComboFiltro.nombre))
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



    }
}
