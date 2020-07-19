using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using dam.di.inventario.clase.Modelo;

namespace dam.di.inventario.Servicios
{
    public class TipoArticuloServicio : ServicioGenerico<tipoarticulo>
    {
        public TipoArticuloServicio(DbContext context) : base(context)
        {
        }
    }
}
