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
    /// Lógica de interacción para DialogArticuloMVVM.xaml
    /// </summary>
    public partial class DialogArticuloMVVM : Window
    {
        private diinventarioEntities invEnt;
        private MVArticulo mVArticulo;
        private usuario usuUserName;
        private List<string> listaEstadosMV;

        //constructor
        public DialogArticuloMVVM(diinventarioEntities ent, usuario usu)
        {
            InitializeComponent();
            invEnt = ent;

            //Seleccion y creacion MV
            mVArticulo = new MVArticulo(invEnt);
            DataContext = mVArticulo;
            
            //crear lista combo estados
            listaEstadosMV = new List<string> {"Operativo", "Mantenimiento", "Baja"};
            comboEstadoMV.ItemsSource = listaEstadosMV;

            //Preseleccionar el nombre de 
            //usuario en el combo de usuario
            usuUserName = usu;
            comboUsuMV.Text = usuUserName.username;
            comboUsuMV.SelectedItem = usuUserName.username;
            //Esta ultima linea es la preseleccion en el combo

            //deshabilitar boton de guardar en funcion de si hay errores o no
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mVArticulo.OnErrorEvent));
            mVArticulo.btnGuardar = btnGuardarMV; //Ver si es asi

        }
        
        private void btnGuardarMV_Click(object sender, RoutedEventArgs e)
        {
            
            if (mVArticulo.guarda())
            {
                MessageBox.Show("insercción ralizada correctamente", "GESTION INVETARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Error al realizar la insercción", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnCancelarMV_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
