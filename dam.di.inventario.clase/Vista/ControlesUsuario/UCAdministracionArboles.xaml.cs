using dam.di.inventario.clase.Modelo;
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
    /// Lógica de interacción para UCAdministracionArboles.xaml
    /// </summary>
    public partial class UCAdministracionArboles : UserControl
    {
        private diinventarioEntities invEnt;

        public UCAdministracionArboles(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
        }

        private void btnArbolDpt_Click(object sender, RoutedEventArgs e)
        {
            UCArbolDeptartamentos uCArbolDept = new UCArbolDeptartamentos(invEnt);

            if (gridCentralArboles.Children != null)
            {
                gridCentralArboles.Children.Clear();
            }

            gridCentralArboles.Children.Add(uCArbolDept);
        }

        private void btnArbolDptCod_Click(object sender, RoutedEventArgs e)
        {
            UCArbolDeptCodigo uCArbolDeptCod = new UCArbolDeptCodigo(invEnt);

            if (gridCentralArboles.Children != null)
            {
                gridCentralArboles.Children.Clear();
            }

            gridCentralArboles.Children.Add(uCArbolDeptCod);

        }
    }
}
