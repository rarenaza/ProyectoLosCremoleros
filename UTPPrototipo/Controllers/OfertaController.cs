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

            lnOferta.CambiarEstado(Convert.ToInt32(idOferta), estado);  //Estado oferta finalizado.
            
            //No debe retornar vistas.
            return Content("");
        }

        [HttpGet]
        public ActionResult AsignarUsuario(string idOferta, string usuario)
        {
            LNOferta lnOferta = new LNOferta();

            lnOferta.AsignarUsuario(Convert.ToInt32(idOferta), usuario);

            //No debe retornar vistas.
            return Content("");
        }
    }
}