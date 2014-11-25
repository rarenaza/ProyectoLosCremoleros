using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class HomeController : Controller
    {
        LNContenido ln = new LNContenido();
        public ActionResult Index()
        {    
            ViewBag.ListaNoticias = ln.Contenido_Buscar("4");
            ViewBag.ListaEventos = ln.Contenido_Buscar("7");
            ViewBag.ListaTestimonios = ln.Contenido_Buscar("5");
            
            return View();
        }

        public ActionResult ParaEmpleadores()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "6";
            contenido = ln.Contenido_Buscar(x);

            return View(contenido);
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Testimonios()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "5";
            contenido = ln.Contenido_Buscar(x);

            return View(contenido);
        }
        public ActionResult DirEmp()
        {
            ////
            List<Contenido> contenido = new List<Contenido>();
            string x = "2";
            contenido = ln.Contenido_Buscar(x);

            return View(contenido);
                       
        }
        public ActionResult NoticiasEventos()
        {
            ViewBag.Noticias = ln.Contenido_Buscar("4");
            ViewBag.Eventos = ln.Contenido_Buscar("7");


            return View();


            //List<Contenido> contenido = new List<Contenido>();
            //string x = "4";
            //contenido = ln.Contenido_Buscar(x);

            //return View(contenido);
                    
        }
        public ActionResult Servicios()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "3";
            contenido = ln.Contenido_Buscar(x);

            return View(contenido);

       
        }
        public ActionResult TerminosDeUso()
        {
            return View();
        }
        public ActionResult PoliticasDePrivacidad()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
    }
}