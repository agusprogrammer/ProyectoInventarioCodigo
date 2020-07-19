using dam.di.inventario.clase.Modelo;
using dam.di.inventario.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dam.di.inventario.clase.Servicios
{
    public class RolServicio : ServicioGenerico <rol>
    {
        private DbContext contexto;

        public RolServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

    }
}
