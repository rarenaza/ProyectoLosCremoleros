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
        public ActionResult Index()
        {
            LNAlumno ln = new LNAlumno();
            Alumno alumno = ln.ObtenerAlumnoPorCodigo("82727128");

            return View(alumno);
        }
        public ActionResult Postulacion() 
        {
            return View();
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
            return View();
        }
        public ActionResult MiCV()
        {
            return View();
        }

	}
}