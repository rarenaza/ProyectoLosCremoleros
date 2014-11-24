using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;


namespace UTPPrototipo.Controllers
{
    public class OfertaSectorEmpresarialController : Controller
    {
        LNOfertaSectorEmpresarial lnSector = new LNOfertaSectorEmpresarial();

        public ActionResult ObtenerSectoresEmpresariales(int idOferta)
        {
            List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(idOferta);

            return PartialView("_OfertaSectorEmpresarial", lista);
        }
    }
}