using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.Validacion;
using dam.di.inventario.Servicios;
using NLog;
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
    /// Lógica de interacción para DialogArticuloMVC.xaml
    /// </summary>
    public partial class DialogArticuloMVC : Window
    {
        private diinventarioEntities invEnt;
        private ArticuloServicio artServ;
        private usuario usuarioInserta; //Recogemos los datos 
        //del usuario que inserta mediante el formulario

        private List<string> listaEstados;

        private ServicioGenerico<usuario> usuServ;
        private ServicioGenerico<modeloarticulo> modServ;
        private ServicioGenerico<espacio> espacioServ;
        private ServicioGenerico<departamento> deptServ;

        private static Logger log = LogManager.GetCurrentClassLogger();
        private ValidaMVC valMVC; //Clase de validacion 

        //constructor, pasarle el usuario
        public DialogArticuloMVC(diinventarioEntities ent, usuario usu)
        {
            InitializeComponent();
            invEnt = ent;

            //Servicios
            artServ = new ArticuloServicio(invEnt);
            usuServ = new ServicioGenerico<usuario>(invEnt);
            modServ = new ServicioGenerico<modeloarticulo>(invEnt);
            espacioServ = new ServicioGenerico<espacio>(invEnt);
            deptServ = new ServicioGenerico<departamento>(invEnt);

            //Inicializar validacion
            valMVC = new ValidaMVC(); 

            usuarioInserta = usu;
            comboUsu.Text = usuarioInserta.username; //Pasar el usuario al combo usuario
            
            inicializar();
        }

        //Inicializa los componentes
        private void inicializar()
        {
            listaEstados = new List<string> {"Operativo","Mantenimiento","Baja"};
            comboEstado.ItemsSource = listaEstados; //Pasar al combo
            
            //Poner componentes en combobox
            comboUsu.ItemsSource = usuServ.getAll().ToList();
            comboModelo.ItemsSource = modServ.getAll().ToList();
            comboEspacio.ItemsSource = espacioServ.getAll().ToList();
            comboDepartamento.ItemsSource = deptServ.getAll().ToList();
            comboDentroDe.ItemsSource = artServ.getAll().ToList();
        }

        private void recogeArticulo(articulo articulo)
        {
            //recoger id de articulo
            int articuloDelFinal = artServ.getLastId();
            articulo.idarticulo = articuloDelFinal + 1;

            //Ver si son los objetos usuario, en caso de fallo
            //articulo.usuarioalta = ((usuario)(comboUsu.SelectedItem)).idusuario;
            usuario usuArt = new usuario();
            if(comboUsu.SelectedItem != null)
            {
                usuArt = (usuario)comboUsu.SelectedItem;
                articulo.usuario = usuArt;
            }
            
            articulo.numserie = txtNumSerie.Text;
            //Fecha
            articulo.fechaalta = calFechaAlta.SelectedDate;
            //Elemento de la lista de estados
            articulo.estado = comboEstado.SelectedItem.ToString();
            
            //articulo.modelo = ((modeloarticulo)(comboModelo.SelectedItem)).idmodeloarticulo;
            modeloarticulo modArt = new modeloarticulo();
            if(comboModelo.SelectedItem != null)
            {
                modArt = (modeloarticulo)comboModelo.SelectedItem;
                articulo.modeloarticulo = modArt;
            }

            //articulo.espacio = ((espacio)(comboEspacio.SelectedItem)).idespacio;
            espacio espArt = new espacio();
            if (comboEspacio.SelectedItem != null)
            {
                espArt = (espacio)comboEspacio.SelectedItem;
                articulo.espacio1 = espArt;
            }

            //articulo.dentrode = ((articulo)(comboDentroDe.SelectedItem)).idarticulo;
            articulo artDentroDe = new articulo();
            if(comboDentroDe.SelectedItem != null)
            {
                artDentroDe = (articulo)comboDentroDe.SelectedItem;
                articulo.dentrode = artDentroDe.idarticulo;
            }

            //articulo.departamento = ((departamento)(comboDepartamento.SelectedItem)).iddepartamento;
            departamento deptArt = new departamento();
            if(comboDepartamento.SelectedItem != null)
            {
                deptArt = (departamento)comboDepartamento.SelectedItem;
                articulo.departamento1 = deptArt;
            }

            articulo.observaciones = txtObservaciones.Text;
        }

        //Eventos botones
        //Evento de guardar
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (valida())
            {
                
                articulo art = new articulo();
                recogeArticulo(art);

                try
                {
                    artServ.add(art);
                    artServ.save();
                    MessageBox.Show("Articulo guardado correctamente", "GESTIÓN ARTICULOS", MessageBoxButton.OK, MessageBoxImage.Information);
                    log.Info("\nInsertando un articulo ... Todo correcto");
                    //Devuelve true y cierra el dialogo
                    DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar en la base de datos", "GESTÓN ARTICULOS", MessageBoxButton.OK, MessageBoxImage.Error);
                    log.Error("insertando un usuario: " + ex.StackTrace);
                }

            }//if de validacion

        }

        //Cerrar formulario
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //metodo de validacion para 
        //los campos obligatorios
        private bool valida()
        {
            bool correcto = true;

            //Validacion combo usuario
            if (valMVC.objetoVacio(comboUsu.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboUsu);
            }
            else
            {
                ValidaMVC.quitarError(comboUsu);
            }

            //-- fecha --
            //Validacion campo de fecha, ver si es asi
            if (valMVC.objetoVacio(calFechaAlta.SelectedDate))
            {
                correcto = false;
                ValidaMVC.muestraError(calFechaAlta);
            }
            else
            {
                ValidaMVC.quitarError(calFechaAlta);
            }
            
            //Validacion combo de estado
            if (valMVC.objetoVacio(comboEstado.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboEstado);
            }
            else
            {
                ValidaMVC.quitarError(comboEstado);
            }

            //Validacion combo de modelo
            if (valMVC.objetoVacio(comboModelo.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboModelo);
            }
            else
            {
                ValidaMVC.quitarError(comboModelo);
            }

            //Validacion combo de espacio
            if (valMVC.objetoVacio(comboEspacio.SelectedItem))
            {
                correcto = false;
                ValidaMVC.muestraError(comboEspacio);
            }
            else
            {
                ValidaMVC.quitarError(comboEspacio);
            }

            return correcto;
        }

        //si se selecciona algun elemento de este, le quita el error 
        private void comboUsu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(comboUsu);
        }

        private void comboEstado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(comboEstado);
        }

        private void comboModelo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(comboModelo);
        }

        private void comboEspacio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(comboEspacio);
        }

        //-- Fecha --
        //Validacion de fecha
        private void calFechaAlta_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidaMVC.quitarError(calFechaAlta);
        }
    }
}
