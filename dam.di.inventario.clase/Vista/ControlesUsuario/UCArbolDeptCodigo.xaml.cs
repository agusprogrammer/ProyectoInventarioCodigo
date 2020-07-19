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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dam.di.inventario.clase.Vista.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCArbolDeptCodigo.xaml
    /// </summary>
    public partial class UCArbolDeptCodigo : UserControl
    {
        private diinventarioEntities invEnt;
        private DptoServicio dptoServ;
        private List<departamento> listaDepts;

        public UCArbolDeptCodigo(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            dptoServ = new DptoServicio(invEnt);
            listaDepts = dptoServ.getAll().ToList();
            gridCentralArbolCod.Children.Add(creaArbol()); //Poner el arbol creado
        }

        //Metodo que genera los nodos
        private TreeViewItem nodoNuevo(string texto, string path)
        {
            //Generar objetos necesarios
            TreeViewItem nodo = new TreeViewItem();
            StackPanel stNodo = new StackPanel();
            //Label lblNodo = new Label();
            TextBlock txtNodo = new TextBlock();
            //Nota: para que tenga el mismo aspecto se ha cambiado el label por textblock
            Image imgNodo = new Image();

            //Configurar objetos
            stNodo.Orientation = Orientation.Horizontal;
            stNodo.Margin = new Thickness(1); //Tambien Puede ser (1,1,1,1)
            //lblNodo.Content = texto;
            txtNodo.Text = texto;
            imgNodo.Source = new BitmapImage(new Uri("pack://application:,,/Recursos/Iconos/" + path));
            imgNodo.Height = 16;

            //asignar los objetos a nuestro panel
            stNodo.Children.Add(imgNodo);
            //stNodo.Children.Add(lblNodo);
            stNodo.Children.Add(txtNodo);
            nodo.Header = stNodo;

            return nodo;
        }


        //Creacion del arbol, metodo que genera el arbol
        private TreeView creaArbol()
        {
            TreeView arbolDepartamentos = new TreeView()
            {
                BorderBrush = Brushes.White
            };
            
            //Crear nodo
            //coger lista de departamentos
            TreeViewItem nodoDept;
            foreach(departamento dept in listaDepts)
            {
                nodoDept = nodoNuevo(dept.nombre, "dept.png");
                arbolDepartamentos.Items.Add(nodoDept); //Agregar al arbol, elemento principal

                //Crear nodo
                //coger lista de usuarios
                TreeViewItem nodoUsu;
                foreach(usuario usu in dept.usuario)
                {
                    nodoUsu = nodoNuevo(usu.nombre, "usuarioArbol.png");
                    nodoDept.Items.Add(nodoUsu); //Agregar al nodo de departamentos

                    //Crear nodo
                    //coger lista de salidas

                    TreeViewItem nodoSalida;
                    foreach(salida sal in usu.salida)
                    {
                        nodoSalida = nodoNuevo(sal.fechasalida + " - " + sal.fechasalida, "salidaUser.png");
                        nodoUsu.Items.Add(nodoSalida);
                    }

                }

            }

            return arbolDepartamentos;

        }



    }
}
