using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Logica;

namespace UTP.PortalEmpleabilidad.Servicio
{
    public class Program
    {
        static void Main(string[] args)
        {
            Procesar();
        }


        private static void Procesar()
        {
            UTP.PortalEmpleabilidad.Logica.LNGeneral lnGeneral = new UTP.PortalEmpleabilidad.Logica.LNGeneral();
            
            //Se llaman a los funciones en la librería Logica.
            lnGeneral.EnviarOfertaCorreosPendientes();
            lnGeneral.FinalizarOfertasPorFechaDeRecepcion();          

        }
    }
}
