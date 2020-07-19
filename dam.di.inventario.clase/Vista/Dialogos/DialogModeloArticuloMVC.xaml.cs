using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.Servicios;
using dam.di.inventario.clase.Validacion;
using dam.di.inventario.Servicios;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Lógica de interacción para DialogModeloArticuloMVC.xaml
    /// </summary>
    public partial class DialogModeloArticuloMVC : Window
    {
        //Primera forma de realizar un formulario MVC de insertar

        private diinventarioEntities invEnt;
        private TipoArticuloServicio tipoServ;
        private ModeloArticuloServicio modServ;
        
        private static Logger log = LogManager.GetCurrentClassLogger();
        private ValidaMVC valMVC; //Clase de validacion


        public DialogModeloArticuloMVC(diinventarioEntities ent)
        {
            InitializeComponent();
            invEnt = ent;
            tipoServ = new TipoArticuloServicio(invEnt);
            modServ = new ModeloArticuloServicio(invEnt);
            valMVC = new ValidaMVC();
            comboTipo.ItemsSource = tipoServ.getAll().ToList();
            
        }

        private void recogeModelo(modeloarticulo modelo)
        {
            modelo.nombre = txtNombre.Text;
            modelo.marca = txtMarca.Text;
            modelo.modelo = txtModelo.Text;
            modelo.descripcion = txtDescripcion.Text;
            //modelo.tipo = ((tipoarticulo)(comboTipo.SelectedItem)).idtipoarticulo;
            tipoarticulo tipoart = new tipoarticulo();
            if (comboTipo.SelectedItem != null)
            {
                tipoart = (tipoarticulo)comboTipo.SelectedItem;
                modelo.tipoarticulo = tipoart;
            }

            /*
            if(comboTipo.SelectedItem != null)
            {
                tipoart.idtipoarticulo = ((tipoarticulo)(comboTipo.SelectedItem)).idtipoarticulo;
                modelo.tipo = tipoart.idtipoarticulo;
            }
            */

        }

        //Evento del boton de guardar
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //Comprobar validacion de los campos
            if (valida())
            {
                
                modeloarticulo modelo = new modeloarticulo();
                recogeModelo(modelo);

                try
                {   
                    //Inserta MVC
                    modServ.add(modelo);
                    modServ.save();
                    MessageBox.Show("Modelo de articulo guardado correctamente","GESTIÓN MODELO ARTICULO",MessageBoxButton.OK,MessageBoxImage.Information);
                    log.Info("\nInsertando un modelo de articulo ... Todo correcto");
                    //Devuelve true y cierra el dialogo
                    DialogResult = true;

                }catch (Exception ex)
                {
                    //No inserta
                    MessageBox.Show("Error al insertar en la base de datos","GESTÓN MODELO ARTÍCULO",MessageBoxButton.OK,MessageBoxImage.Error);
                    log.Error("insertando un modelo de articulo: " + ex.StackTrace);
                }

            } //if de validacion
            
        }

        //Metodo de validacion de los
        //campos obligatorios
        private bool valida()
        {
            bool correcto = true;
            if (valMVC.validaCadena(txtNombre.Text))
            {
                //Nota: el constructor no recibe imgTxtNombre, 
                //esta comentado esta parte esta en el proyecto
                //de validacion con imagen
                correcto = false;
                ValidaMVC.muestraError(txtNombre);
            }
            else
            {
                ValidaMVC.quitarError(txtNombre); 
                
            }

            if (valMVC.objetoVacio(comboTipo.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboTipo);
            }
            else
            {
                ValidaMVC.quitarError(comboTipo);
            }

            return correcto;
        }
        
        //Evento de cerrar el formulario
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Evento del textbox de nombre que es obligatorio
        //que hace que cambie de estado mientras 
        //se este escribiendo
        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                ValidaMVC.muestraError(txtNombre);
            }
            else
            {
                ValidaMVC.quitarError(txtNombre);
            }
        }

        //Evento del combobox de tipo, que si se selecciona
        //algun elemento de este, le quita el error 
        private void comboTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(comboTipo); //Cambiar a imagen del combo
        }
    }
}
