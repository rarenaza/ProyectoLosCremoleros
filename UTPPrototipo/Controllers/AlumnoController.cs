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
        public string codigoAlumno = "82727128";

        public ActionResult Index()
        {
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(codigoAlumno);

            return View(panel);
        }
        public ActionResult Postulacion() 
        {
            VistaPanelAlumnoPostulaciones panel = lnAlumno.ObtenerPanelPostulaciones(codigoAlumno);

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
            VistaPanelAlumnoOfertas panel = lnAlumno.ObtenerPanelOfertas(codigoAlumno);
            return View(panel);
        }
        public ActionResult MiCV()
        {
            VistaPanelAlumnoMiCV panel = lnAlumno.ObtenerPanelMiCV(codigoAlumno);
            return View(panel);
        }

	}
}