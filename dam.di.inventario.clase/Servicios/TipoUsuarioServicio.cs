using dam.di.inventario.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dam.di.inventario.clase.Servicios
{
    /*
     * Clase que contiene las reglas de negocio de la clase tipo usuario
     */
    public class TipoUsuarioServicio : ServicioGenerico <TipoUsuarioServicio>
    {
        private DbContext contexto;

        /*
         * Constructor
         */
        public TipoUsuarioServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
        

    }
}
