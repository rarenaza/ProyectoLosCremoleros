using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class AlumnoController : Controller
    {
        LNAlumno lnAlumno = new LNAlumno();

        public ActionResult Index()
        {
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel("82727128");

            return View(panel);
        }
        public ActionResult Postulacion() 
        {
            VistaPanelAlumnoPostulaciones panel = lnAlumno.ObtenerPanelPostulaciones("82727128");

            return View(panel);
        }
        public ActionResult PostulacionOferta()
        {
            return View();
        }
        public ActionResult PostulacionOferta2()
        {
            return View();
        }
        public ActionResult BusquedaOferta()
        {
            VistaPanelAlumnoPostulaciones panel = lnAlumno.ObtenerPanelPostulaciones("82727128");
            return View(panel);
        }
        public ActionResult MiCV()
        {
            return View();
        }

	}
}