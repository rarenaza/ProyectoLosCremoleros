using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;

namespace UTPPrototipo.Controllers
{
    public class OfertaEstudioController : Controller
    {
        LNOfertaEstudio ofertaEstudio = new LNOfertaEstudio();

        public ActionResult ObtenerEstudios(int idOferta)
        {
            List<OfertaEstudio> lista = ofertaEstudio.ObtenerEstudios(idOferta);

            return PartialView("_OfertaEstudio", lista);
        }

    }
}