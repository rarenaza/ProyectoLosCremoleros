using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOferta
    {
        ADOferta adOferta = new ADOferta();

        public DataTable Obtener()
        {
            return adOferta.Obtener();
        }
    }
}
