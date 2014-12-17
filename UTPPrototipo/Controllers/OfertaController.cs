using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;


namespace UTPPrototipo.Controllers
{
    public class OfertaController : Controller
    {

        [HttpGet]
        public ActionResult FinalizarOferta(int id)
        {
            LNOferta lnOferta = new LNOferta();

            lnOferta.CambiarEstado(id, "OFERFI");  //Estado oferta finalizado.

            return Content("");
        }
    }
}