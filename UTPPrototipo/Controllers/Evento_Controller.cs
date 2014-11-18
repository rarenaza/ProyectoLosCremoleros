using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class Evento_Controller : Controller
    {
        LNEvento ad = new LNEvento();
        // GET: Evento_
        public ActionResult Evento_Mostrar()
        {
            List<Evento> lista = new List<Evento>();

            lista = ad.Evento_Mostrar();

            return View(lista); 
        }

       
    }
}