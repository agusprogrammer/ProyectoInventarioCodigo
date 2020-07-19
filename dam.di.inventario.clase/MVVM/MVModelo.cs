using dam.di.inventario.clase.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dam.di.inventario.clase.Servicios;
using dam.di.inventario.Servicios;
using System.Data.Entity.Infrastructure;
using NLog;
using System.Windows.Data;

namespace dam.di.inventario.clase.MVVM
{
    public class MVModelo : MVBase
    {
        //------------ VARIABLES ------------------------

        private diinventarioEntities invEnt;
        private ModeloArticuloServicio modServ;
        private ServicioGenerico<tipoarticulo> tipoServ;
        private modeloarticulo modNuevo; //Objeto auxiliar para guardar
        //los datos recogidos en la interfaz

        //Objeto tipo articulo auxiliar para la seleccion del filtro de combobox
        private tipoarticulo tipoArtCombofiltro; 

        //Logger de errores, pone los errores en un fichero
        private static Logger log = LogManager.GetCurrentClassLogger();

        //listCollectionview del datagrid modelos articulos
        private ListCollectionView listaCollecModeloArtDataGrid;
        //Esta lista es un tipo de lista que nos permite usar filtros,
        //Realiza la misma funcion de recoger datos que listaModelosDataGrid
        
        //Modelo de articulo de los detalles
        private modeloarticulo modSelectDetaDatagrid;
        

        //---------------- CONSTRUCTOR E INICIO -----------------------------------

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="ent"></param>
        public MVModelo(diinventarioEntities ent)
        {
            invEnt = ent;
            inicializa();
        }

        //Metodo inicializar
        private void inicializa()
        {
            modServ = new ModeloArticuloServicio(invEnt);
            tipoServ = new ServicioGenerico<tipoarticulo>(invEnt);
            modNuevo = new modeloarticulo();

            //listCollectionview del datagrid modelos articulos
            listaCollecModeloArtDataGrid = new ListCollectionView(modServ.getAll().ToList());

            //filtro combo
            tipoArtCombofiltro = new tipoarticulo();

            //Modelo detalle
            modSelectDetaDatagrid = new modeloarticulo();

            
        }

        //----------------- Listas del datagrid --------------------------------

        //Lista de modelos NORMAL para el data grid
        //Nota: con la lista normal, los filtros no funcionan, 
        //si se usan filtros, estos van con ListCollectionView
        
        //Lista normal
        public List<modeloarticulo> listaModelosDataGrid
        {
            get
            {
                return modServ.getAll().ToList();
            }
        }

        //Lista usada para el datagrid con filtros, el ListCollectionView
        //Forma correcta del list collection view

        //ListCollectionView, permite uso de filtros
        public ListCollectionView listaModelosListColletion
        {
            get
            {
                return listaCollecModeloArtDataGrid;
            }
        }
        //NOTA: no hace falta poner la lista de datalles 
        //en el nuevo datagrid

        //Si se pone asi el list colletion view,
        //el datagrid lo llama continuamente de la bd, puede causar problemas
        /*
        public ListCollectionView listaModelosListColletion
        {
            get
            {
                return new ListCollectionView(modServ.getAll().ToList());
            }
        }
        */

        //detalle seleccionado datagrid
        public modeloarticulo modeloSeleccionadoDeta
        {
            get
            {
                return modSelectDetaDatagrid;
            }
            set
            {
                modSelectDetaDatagrid = value;
                OnPropertyChanged("modeloSeleccionadoDeta");
            }
        }
        
        //------------------ FILTROS ------------------------------

        //Cosas del combobox filtro de tipo articulos -------------------------
        //filtro combobox de tipo articulos, seleccion
        public tipoarticulo tipoArtSeleccionadoComboFiltro
        {
            get
            {
                return tipoArtCombofiltro;
            }
            set
            {
                tipoArtCombofiltro = value;
                OnPropertyChanged("tipoArtSeleccionadoComboFiltro");
            }
        }

        //lista de tipos del combo
        //Nota: este se puede quitar y usar lista tipos
        public List<tipoarticulo> listaTiposArtComboFiltro
        {
            get
            {
                return tipoServ.getAll().ToList();
            }
        }
        //---------------------------------------------------------------

        //Formularios ---------------------------------------------------

        //lista de tipos de articulos para el 
        //formulario de modelos de articulo 
        public List<tipoarticulo> listaTipos
        {
            get
            {
                return tipoServ.getAll().ToList();
            }
        }
        

        /// <summary>
        /// Objeto que recoge los datos de la interfaz
        /// </summary>
        public modeloarticulo modeloNuevo
        {
            get
            {
                return modNuevo;
            }
            set
            {
                modNuevo = value;
                OnPropertyChanged("modeloNuevo");
            }
        }

        //metodo de guardar
        public bool guarda()
        {
            bool correcto = true;
            modServ.add(modNuevo);
            try
            {
                modServ.save();
                log.Error("\nInsertando modelo articulo ... Todo correcto\n");

            }catch(DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nInsertando modelo articulo ... Error\n" 
                    + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

        //metodo de editar
        public bool edita()
        {
            bool correcto = true;
            modServ.edit(modNuevo);
            try
            {
                modServ.save();
                log.Error("\nEditando modelo articulo ... Todo correcto\n");
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nEditando modelo articulo ... Error\n"
                    + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

        //Borrar modelo
        public bool borrar(modeloarticulo modelo)
        {
            bool correcto = true;
            modServ.delete(modelo);
            try
            {
                try
                {
                    modServ.save();
                    log.Error("\nBorrar modelo articulo ... Todo correcto\n");
                }
                catch (Exception ex)
                {
                    correcto = false;
                    log.Error("\nBorrar modelo articulo ... Error\n" + "\n" + ex.StackTrace);
                }
                

            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nBorrar modelo articulo ... Error\n"
                    + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

    }
}
