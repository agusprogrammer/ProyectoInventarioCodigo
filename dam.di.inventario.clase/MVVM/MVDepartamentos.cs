using dam.di.inventario.clase.Modelo;
using dam.di.inventario.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dam.di.inventario.clase.MVVM
{
    public class MVDepartamentos : MVBase
    {
        private diinventarioEntities invEnt;
        private DptoServicio dptoServ;

        public MVDepartamentos(diinventarioEntities ent)
        {
            invEnt = ent;
            dptoServ = new DptoServicio(invEnt);
        }

        public List<departamento> listaDepartamentos
        {
            get
            {
                return dptoServ.getAll().ToList();
            }
        }

    }
}
