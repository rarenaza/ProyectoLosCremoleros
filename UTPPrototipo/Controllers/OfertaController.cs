using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class OfertaController : Controller
    {
        // GET: Oferta
        LNOferta Ln = new LNOferta();

        public ActionResult Oferta_Mostrar()
        {
            List<Oferta> ListaOferta = new List<Oferta>();

            ListaOferta = Ln.Oferta_Mostrar();

            return View(ListaOferta);
           
        }
      

    }
}