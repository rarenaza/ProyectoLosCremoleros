using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;

namespace UTPPrototipo.Controllers
{
    public class OfertaInformacionAdicionalController : Controller
    {
        LNOfertaInformacionAdicional infoAdicional = new LNOfertaInformacionAdicional();

        public ActionResult ObtenerInformacionAdicional(int idOferta)
        {
            List<OfertaInformacionAdicional> lista = infoAdicional.ObtenerInformacionAdicional(idOferta);

            return PartialView("_OfertaInformacionAdicional",lista);
        }

        public void InsertarInformacionAdicional(OfertaInformacionAdicional ofertaInformacionAdicional)
        {
           infoAdicional.InsertarInformacionAdicional(ofertaInformacionAdicional);
        }
    }
}