using dam.di.inventario.clase.Modelo;
using dam.di.inventario.Servicios;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace dam.di.inventario.clase.MVVM
{
    public class MVArticulo : MVBase
    {
        private diinventarioEntities invEnt;
        private ArticuloServicio artServ;

        //Combos servicios
        //Nota: lo del combo estado va en la clase C# del formulario 
        private ServicioGenerico<usuario> usuServ; //Pasar usuario de admin
        private ServicioGenerico<modeloarticulo> modArtServ;
        private ServicioGenerico<espacio> espaServ;
        private ServicioGenerico<departamento> deptServ;

        //listCollectionview del datagrid de articulos
        private ListCollectionView listaCollecArticulosDataGrid;

        //Otra forma de lista para el combo de estados
        private List<String> listaEstadosMVArt;

        //Logger
        private static Logger log = LogManager.GetCurrentClassLogger();

        //Objetos auxiliares para guardar
        private articulo artNuevo; //insertar
        private departamento deptComboFiltroSel; //departamento del combo filtro

        //constructor
        public MVArticulo(diinventarioEntities ent)
        {
            invEnt = ent;
            inicializa();
        }

        //Metodo inicializar
        public void inicializa()
        {
            //inicializar servicios
            artServ = new ArticuloServicio(invEnt);

            //el usuario luego se lo pasamos por constructor
            usuServ = new ServicioGenerico<usuario>(invEnt);
            modArtServ = new ServicioGenerico<modeloarticulo>(invEnt);
            espaServ = new ServicioGenerico<espacio>(invEnt);
            deptServ = new ServicioGenerico<departamento>(invEnt);

            //Objetos
            artNuevo = new articulo();
            //Departamento del filtro
            deptComboFiltroSel = new departamento();

            //listCollectionview del datagrid modelos articulos
            listaCollecArticulosDataGrid = new ListCollectionView(artServ.getAll().ToList());

            //Otra forma de lista para el combo de estados
            listaEstadosMVArt = new List<String> { "Operativo", "Mantenimiento", "Baja" };

        }

        //------------------ DATAGRID -------------------
        //Lista de articulos del datagrid
        public List<articulo> listaArticulosDatagrid
        {
            get
            {
                return artServ.getAll().ToList();
            }

        }

        //ListCollectionView, permite uso de filtros
        public ListCollectionView listaArticulosListColletion
        {
            get
            {
                return listaCollecArticulosDataGrid;
            }
        }

        //------------------- Filtros ---------------------
        //Filtro combobox
        /* no hace falta porque ya tenemos una lista
        public List<departamento> listaDepartamentosComboFiltro
        {
            get
            {
                return deptServ.getAll().ToList();
            }

        }*/

        //Seleccion del departamento
        public departamento deptSeleccionadoComboFiltro
        {
            get
            {
                return deptComboFiltroSel;
            }
            set
            {
                deptComboFiltroSel = value;
                OnPropertyChanged("deptSeleccionadoComboFiltro");
            }
        }

        // -------------- FORMULARIOS -----------------
        //Listas de los combos

        //Otra forma de lista para el combo de estados
        public List<String> listaEstados
        {
            get
            {
                return listaEstadosMVArt;
            }
        }

        //Usuarios, coger el que le pasamos al constructor
        public List<usuario> listaUsuarios
        {
            get
            {
                return usuServ.getAll().ToList();
            }
        }

        //lista Modelos articulos
        public List<modeloarticulo> listaModeloArticulo
        {
            get
            {
                return modArtServ.getAll().ToList();
            }
        }

        //lista de dentro de (articulos)
        public List<articulo> listaArticulosDentroDe
        {
            get
            {
                return artServ.getAll().ToList();
            }
        }

        //lista espacios
        public List<espacio> listaEspacios
        {
            get
            {
                return espaServ.getAll().ToList();
            }
        }

        //lista departamentos
        public List<departamento> listaDepartamentos
        {
            get
            {
                return deptServ.getAll().ToList();
            }
        }

        //Objeto que recogen los datos de la interfaz
        public articulo articuloNuevo
        {
            get
            {
                return artNuevo;
            }
            set
            {
                artNuevo = value;
                OnPropertyChanged("articuloNuevo");
            }
        }

        //Metodo de guardar del MV
        public bool guarda()
        {
            bool correcto = true;

            //Poner id al articulo, no es auto incremental
            ponerId();

            artServ.add(artNuevo);

            try
            {
                artServ.save();
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\ninsertando articulo ... Error\n"
                    + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

        //Metodo para poner el id, no es auto incremental
        private void ponerId()
        {
            int articuloDelFinal = artServ.getLastId();
            artNuevo.idarticulo = articuloDelFinal + 1;
        }

        

    }
}
