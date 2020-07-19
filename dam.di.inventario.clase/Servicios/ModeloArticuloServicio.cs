using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using dam.di.inventario.clase.Modelo;

namespace dam.di.inventario.Servicios
{
    public class ModeloArticuloServicio : ServicioGenerico<modeloarticulo>
    {
        private DbContext contexto;

        public ModeloArticuloServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        public List<modeloarticulo> getModelosPorTipo (int tipo)
        {
            return contexto.Set<modeloarticulo>().Where(m => m.tipo == tipo).ToList();
        }
    }
}
