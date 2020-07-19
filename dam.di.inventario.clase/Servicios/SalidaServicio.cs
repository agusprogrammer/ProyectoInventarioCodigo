using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;
using dam.di.inventario.clase.Modelo;

namespace dam.di.inventario.Servicios
{
    public class SalidaServicio : ServicioGenerico<salida>
    {
        private DbContext contexto;

        public SalidaServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        public List<salida> getSalidasUsuario(int usu)
        {
            return contexto.Set<salida>().Where(s => s.usuario == usu).ToList();
        }
    }
}
