using System.Data.Entity;
using dam.di.inventario.clase.Modelo;
using dam.di.inventario.Servicios;

namespace dam.di.inventario.Servicios
{
    public class DptoServicio : ServicioGenerico<departamento>
    {

        private DbContext contexto;

        public DptoServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
    }
}
