using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTPPrototipo.Controllers
{
    public class EmpresaController : Controller
    {
        //
        // GET: /Empresa/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Publicacion()
        {
            return View();
        }
        public ActionResult Oferta()
        {
            return View();
        }
	}
}