using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.Vista.ControlesUsuario;
using dam.di.inventario.clase.Vista.Dialogos;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dam.di.inventario.clase
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {

        private diinventarioEntities invEnt;
        private usuario usuLogin;

        public MainWindow(diinventarioEntities ent, usuario usu)
        {
            InitializeComponent();
            invEnt = ent;
            usuLogin = usu;
            //tbNombreUsu.Text = usuLogin.username; //esto es del dropdown button comentado
        }



        //Manejadores de eventos

        //Manejadores de eventos de estilo
        //Barra de herramientas
        private void botonOpciones_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            rectBotonOp.Fill = (Brush)bc.ConvertFrom("#0080FF");
        }

        private void botonOpciones_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            rectBotonOp.Fill = (Brush)bc.ConvertFrom("#2E9AFE");
        }

        private void botonSalir_MouseEnter(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            rectBotonSalir.Fill = (Brush)bc.ConvertFrom("#0080FF");
        }

        private void botonSalir_MouseLeave(object sender, MouseEventArgs e)
        {
            var bc = new BrushConverter();
            rectBotonSalir.Fill = (Brush)bc.ConvertFrom("#2E9AFE");
        }

        //Accordion botones -----------------------------------------------------
        //Usuarios:
        //Anyadir usuario
        private void botonAnyadirUsuario_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAnyadirUsu.TextDecorations = TextDecorations.Underline;
        }

        private void botonAnyadirUsuario_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAnyadirUsu.TextDecorations = null;
        }

        //editar usuario
        private void botonEditarUsuario_MouseEnter(object sender, MouseEventArgs e)
        {
            txtEditarUsu.TextDecorations = TextDecorations.Underline;
        }

        private void botonEditarUsuario_MouseLeave(object sender, MouseEventArgs e)
        {
            txtEditarUsu.TextDecorations = null;
        }

        //listar usuario
        private void botonListarUsuarios_MouseEnter(object sender, MouseEventArgs e)
        {
            txtListarUsu.TextDecorations = TextDecorations.Underline;
        }

        private void botonListarUsuarios_MouseLeave(object sender, MouseEventArgs e)
        {
            txtListarUsu.TextDecorations = null;
        }

        private void botonListarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            UCGesUsuario ucListaUsuarios = new UCGesUsuario(invEnt);
            if (gridCentral.Children != null)
            {
                gridCentral.Children.Clear();
            }

            gridCentral.Children.Add(ucListaUsuarios);
        }

        //Articulos:
        //Anyadir articulos
        private void botonAnyadirArticulo_Click(object sender, RoutedEventArgs e)
        {
            DialogArticuloMVC diag = new DialogArticuloMVC(invEnt, usuLogin);
            diag.ShowDialog();
        }

        private void botonAnyadirArticulo_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAnyadirArticulo.TextDecorations = TextDecorations.Underline;
        }

        private void botonAnyadirArticulo_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAnyadirArticulo.TextDecorations = null;
        }
        
        //Editar articulos
        private void botonEditarArticulo_MouseEnter(object sender, MouseEventArgs e)
        {
            txtEditarArticulo.TextDecorations = TextDecorations.Underline;
        }

        private void botonEditarArticulo_MouseLeave(object sender, MouseEventArgs e)
        {
            txtEditarArticulo.TextDecorations = null;
        }

        //Listar articulos
        private void botonListarArticulos_MouseEnter(object sender, MouseEventArgs e)
        {
            txtListarArticulos.TextDecorations = TextDecorations.Underline;
        }

        private void botonListarArticulos_MouseLeave(object sender, MouseEventArgs e)
        {
            txtListarArticulos.TextDecorations = null;
        }

        //Modelos de articulos: -----------------------------
        //Anyadir modelos articulos
        private void botonAnyadirModelosArticulos_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAnyadirModelosArticulos.TextDecorations = TextDecorations.Underline;
        }

        private void botonAnyadirModelosArticulos_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAnyadirModelosArticulos.TextDecorations = null;
        }

        //Inventario:
        //nuevo invnetario
        private void botonNuevoInventario_MouseEnter(object sender, MouseEventArgs e)
        {
            txtNuevoInventario.TextDecorations = TextDecorations.Underline;
        }

        private void botonNuevoInventario_MouseLeave(object sender, MouseEventArgs e)
        {
            txtNuevoInventario.TextDecorations = null;
        }
        
        //editar inventario
        private void botonEditarInventario_MouseEnter(object sender, MouseEventArgs e)
        {
            txtEditarInventario.TextDecorations = TextDecorations.Underline;
        }

        private void botonEditarInventario_MouseLeave(object sender, MouseEventArgs e)
        {
            txtEditarInventario.TextDecorations = null;
        }
        
        //Prestamos:
        //entrada prestamos
        private void botonEntradaPrestamos_MouseEnter(object sender, MouseEventArgs e)
        {
            txtEntradaPrestamos.TextDecorations = TextDecorations.Underline;
        }

        private void botonEntradaPrestamos_MouseLeave(object sender, MouseEventArgs e)
        {
            txtEntradaPrestamos.TextDecorations = null;
        }

        //salida prestamos
        private void botonSalidaPrestamos_MouseEnter(object sender, MouseEventArgs e)
        {
            txtSalidaPrestamos.TextDecorations = TextDecorations.Underline;
        }

        private void botonSalidaPrestamos_MouseLeave(object sender, MouseEventArgs e)
        {
            txtSalidaPrestamos.TextDecorations = null;
        }

        //Informacion:
        //Informes
        private void botonInformes_MouseEnter(object sender, MouseEventArgs e)
        {
            txtInformes.TextDecorations = TextDecorations.Underline;
        }

        private void botonInformes_MouseLeave(object sender, MouseEventArgs e)
        {
            txtInformes.TextDecorations = null;
        }

        //Charts
        private void botonCharts_MouseEnter(object sender, MouseEventArgs e)
        {
            txtCharts.TextDecorations = TextDecorations.Underline;
        }

        private void botonCharts_MouseLeave(object sender, MouseEventArgs e)
        {
            txtCharts.TextDecorations = null;
        }
        
        //Barra de botones ------------------------
        //Boton de salir 
        private void botonSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Modelos de articulo botones

        //Modelos de articulo:
        //Llamar al dialogo de modeloArticuloMVC
        private void botonAnyadirModelosArticulos_Click(object sender, RoutedEventArgs e)
        {
            DialogModeloArticuloMVC diag = new DialogModeloArticuloMVC(invEnt);
            diag.ShowDialog();
        }

        //Usuarios botones
        //Anyadir usuario
        private void botonAnyadirUsuario_Click(object sender, RoutedEventArgs e)
        {
            DialogUsuarioMVC diag = new DialogUsuarioMVC(invEnt);
            diag.ShowDialog();
        }

        //Menjadores de eventos MVVM -----------------------------------------------
        //Usuarios MVVM:
        //Anyadir usuario MVVM
        private void botonAnyadirUsuarioMVVM_Click(object sender, RoutedEventArgs e)
        {
            //Faltan cosas
            DialogUsuarioMVVM diag = new DialogUsuarioMVVM(invEnt, null, false);
            diag.ShowDialog();
        }

        //Estilo del boton
        private void botonAnyadirUsuarioMVVM_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAnyadirUsuMVVM.TextDecorations = TextDecorations.Underline;
        }
        
        private void botonAnyadirUsuarioMVVM_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAnyadirUsuMVVM.TextDecorations = null;
        }

        //Modelos articulo MVVM: 
        //Anyadir modelo de articulo MVVM
        private void botonAnyadirModelosArticulosMVVM_Click(object sender, RoutedEventArgs e)
        {
            DialogModeloArticuloMVVM diag = new DialogModeloArticuloMVVM(invEnt, null, false);
            diag.ShowDialog();
        }

        //Estilo boton
        private void botonAnyadirModelosArticulosMVVM_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAnyadirModelosArticulosMVVM.TextDecorations = TextDecorations.Underline;
        }

        private void botonAnyadirModelosArticulosMVVM_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAnyadirModelosArticulosMVVM.TextDecorations = null;
        }

        //Articulos MVVM
        private void botonAnyadirArticuloMVVM_Click(object sender, RoutedEventArgs e)
        {
            DialogArticuloMVVM diag = new DialogArticuloMVVM(invEnt, usuLogin);
            diag.ShowDialog();
        }

        private void botonAnyadirArticuloMVVM_MouseEnter(object sender, MouseEventArgs e)
        {
            txtAnyadirArticuloMVVM.TextDecorations = TextDecorations.Underline;
        }

        private void botonAnyadirArticuloMVVM_MouseLeave(object sender, MouseEventArgs e)
        {
            txtAnyadirArticuloMVVM.TextDecorations = null;
        }

        private void botonListarArticulos_Click(object sender, RoutedEventArgs e)
        {
            UCGesArticulo ucListaArticulos = new UCGesArticulo(invEnt);

            if (gridCentral.Children != null)
            {
                gridCentral.Children.Clear();
            }

            gridCentral.Children.Add(ucListaArticulos);
        }

        //Listar modelos de articulo MVVM
        private void botonListarModelosArticulosMVVM_MouseEnter(object sender, MouseEventArgs e)
        {
            txtListarModelosArticulosMVVM.TextDecorations = TextDecorations.Underline;
        }

        private void botonListarModelosArticulosMVVM_MouseLeave(object sender, MouseEventArgs e)
        {
            txtListarModelosArticulosMVVM.TextDecorations = null;
        }

        private void botonListarModelosArticulosMVVM_Click(object sender, RoutedEventArgs e)
        {
            UCGesModeloArticulo ucListaModArt = new UCGesModeloArticulo(invEnt);

            if (gridCentral.Children != null)
            {
                gridCentral.Children.Clear();
            }

            gridCentral.Children.Add(ucListaModArt);

        }

        //Boton de arboles
        private void botonArboles_MouseEnter(object sender, MouseEventArgs e)
        {
            txtArboles.TextDecorations = TextDecorations.Underline;
        }

        private void botonArboles_MouseLeave(object sender, MouseEventArgs e)
        {
            txtArboles.TextDecorations = null;
        }

        private void botonArboles_Click(object sender, RoutedEventArgs e)
        {
            UCAdministracionArboles ucAdminArboles = new UCAdministracionArboles(invEnt);

            if (gridCentral.Children != null)
            {
                gridCentral.Children.Clear();
            }

            gridCentral.Children.Add(ucAdminArboles);
        }
    }
}
