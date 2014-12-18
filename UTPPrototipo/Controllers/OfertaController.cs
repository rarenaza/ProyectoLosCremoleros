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
        public ActionResult FinalizarOferta(string idOferta, string estado)
        {
            LNOferta lnOferta = new LNOferta();

            //lnOferta.CambiarEstado(id, "OFERFI");  //Estado oferta finalizado.
            lnOferta.CambiarEstado(Convert.ToInt32(idOferta), estado);  //Estado oferta finalizado.

            return Content("");
        }
    }
}