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
    public class MVUsuario : MVBase
    {
        private diinventarioEntities invEnt;
        private UsuarioServicio usuServ;

        //Combos servicios
        private ServicioGenerico<tipousuario> tipoUsuServ;
        private ServicioGenerico<rol> rolServ;
        private ServicioGenerico<grupo> grupoServ;
        private ServicioGenerico<departamento> departamentoServ;

        //Logger
        private static Logger log = LogManager.GetCurrentClassLogger();

        //Objetos auxiliares para guardar
        private usuario usuNuevo;

        //usuario seleccionado del datagrid para 
        //mostrar los detalles de direccion  
        private usuario usuSelect;

        //Departanemto de usuario para el filtro de depts
        private grupo usuGrupoComboFiltroSel;

        //listCollectionview del datagrid de articulos
        private ListCollectionView listaCollecUsuariosDataGrid;
        

        //---------------- Constructor ----------------

        //constructor
        public MVUsuario(diinventarioEntities ent)
        {
            invEnt = ent;
            iniciliza();
        }

        //Metodo inicializar
        private void iniciliza()
        {
            //Servicios
            //servicio de usuario para el formulario
            usuServ = new UsuarioServicio(invEnt);
            //Listas
            tipoUsuServ = new ServicioGenerico<tipousuario>(invEnt);
            rolServ = new ServicioGenerico<rol>(invEnt);
            grupoServ = new ServicioGenerico<grupo>(invEnt);
            departamentoServ = new ServicioGenerico<departamento>(invEnt);

            //Objetos
            usuNuevo = new usuario();
            usuSelect = new usuario();

            //Departanemto de usuario para el filtro de depts
            usuGrupoComboFiltroSel = new grupo();

            //ListColletionView usuarios
            listaCollecUsuariosDataGrid = new ListCollectionView(usuServ.getAll().ToList());

        }

        //------------- Seleccion en el datagrid para los detalles de direccion ------------

        public usuario usuSelectDatagrid
        {
            get
            {
                return usuSelect;
            }
            set
            {
                usuSelect = value;
                OnPropertyChanged("usuSelectDatagrid");
            }
        }

        //------------- listas datagrid ---------------

        //Lista de usuarios del datagrid
        public List<usuario> listaUsuariosDatagrid
        {
            get
            {
                return usuServ.getAll().ToList();
            }
        }

        //ListCollectionView datagrid usuarios
        public ListCollectionView listaUsuariosListColletion
        {
            get
            {
                return listaCollecUsuariosDataGrid;
            }
        }

        //Item seleccionado del filtro de departamento
        //Seleccion del departamento
        public grupo usuGrupoSeleccionadoComboFiltro
        {
            get
            {
                return usuGrupoComboFiltroSel;
            }
            set
            {
                usuGrupoComboFiltroSel = value;
                OnPropertyChanged("usuGrupoSeleccionadoComboFiltro");
            }
        }

        //listas combobox ----------------------------
        //tipo usuario
        public List<tipousuario> listaTipoUsuarios
        {
            get
            {
                return tipoUsuServ.getAll().ToList();
            }
        }

        //roles
        public List<rol> listaRoles
        {
            get
            {
                return rolServ.getAll().ToList();
            }
        }

        //grupos
        public List<grupo> listaGrupos
        {
            get
            {
                return grupoServ.getAll().ToList();
            }
        }

        //departamentos
        public List<departamento> listaDepartamentos
        {
            get
            {
                return departamentoServ.getAll().ToList();
            }
        }

        //Objetos que recogen los datos de la interfaz
        public usuario usuarioNuevo
        {
            get
            {
                return usuNuevo;
            }
            set
            {
                usuNuevo = value;
                OnPropertyChanged("usuarioNuevo");
            }
        }
            
        /*
        public tipousuario tipousuarioNuevoInterfaz
        {
            get
            {
                return tipoUsuNuevo;
            }
            set
            {
                tipoUsuNuevo = value;
                OnPropertyChanged("tipousuarioNuevoInterfaz");
            }
        }

        public rol rolNuevoInterfaz
        {
            get
            {
                return rolNuevo;
            }
            set
            {
                rolNuevo = value;
                OnPropertyChanged("rolNuevoInterfaz");
            }
        }

        public grupo grupoNuevoInterfaz
        {
            get
            {
                return grupoNuevo;
            }
            set
            {
                grupoNuevo = value;
                OnPropertyChanged("grupoNuevoInterfaz");
            }
        }

        public departamento departamentoNuevoInterfaz
        {
            get
            {
                return departamentoNuevo;
            }
            set
            {
                departamentoNuevo = value;
                OnPropertyChanged("departamentoNuevoInterfaz");
            }
        }
        */

        //Metodo de guardar
        public bool guarda()
        {
            bool correcto = true;
            usuServ.add(usuNuevo);

            try
            {
                usuServ.save();
            }
            catch(DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\ninsertando usuario ... Error\n"
                    + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
            
        } 

        //comprobar usuario unico
        public bool usuarioUnico
        {
            get
            {
                return usuServ.usuarioUnico(usuNuevo.username);
            }
        }

        //Metodo de editar
        public bool edita()
        {
            bool correcto = true;
            usuServ.edit(usuNuevo);
            try
            {
                usuServ.save();
                log.Error("\nEditando usuario ... Todo correcto\n");
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nEditando usuario ... Error\n"
                    + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;

        }
        
        //Borrar usuario
        public bool borrar(usuario usuBorrar)
        {
            bool correcto = true;
            usuServ.delete(usuBorrar);
            try
            {
                try
                {
                    usuServ.save();
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
