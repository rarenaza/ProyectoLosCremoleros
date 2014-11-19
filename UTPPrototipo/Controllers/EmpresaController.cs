using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTPPrototipo.Controllers
{
    public class EmpresaController : Controller
    {
        LNEmpresa lnEmpresa = new LNEmpresa();
        LNOferta lnOferta = new LNOferta();

        public string usuarioEmpresa = "82727128";

        //
        // GET: /Empresa/
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Publicacion()
        {
            List<VistaOfertaEmpresa> lista = lnOferta.Obtener_PanelEmpresa(0);

            return View(lista);
        }
        public ActionResult Oferta()
        {
            return View();
        }
        public ActionResult NuevaOferta()
        {
            return View();
        }
        
        public ActionResult VistaCabecera()
        {
            VistaPanelCabecera panel = new VistaPanelCabecera();

            panel = lnEmpresa.ObtenerPanelCabecera(usuarioEmpresa);

            return PartialView("_DatosUsuario", panel);
        }

        public ActionResult Postulante()
        {
            return View();
        }
	}
}