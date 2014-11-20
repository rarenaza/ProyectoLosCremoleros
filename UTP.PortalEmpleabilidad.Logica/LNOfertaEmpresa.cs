using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Datos;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOfertaEmpresa
    {
        ADOferta adOferta = new ADOferta();
                
        public void Insertar(Oferta oferta)
        {
            adOferta.Insertar(oferta);
        }



    }
}
