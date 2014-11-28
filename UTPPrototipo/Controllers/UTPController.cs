using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UTPPrototipo.Controllers
{
    public class UTPController : Controller
    {
        // GET: UTP
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registros()
        {
            return View();
        }
        public ActionResult Portal()
        {
            return View();
        }
    }
}