using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTPPrototipo.Controllers
{
    public class EmpresaController : Controller
    {
        LNEmpresa lnEmpresa = new LNEmpresa();
        public string usuarioEmpresa = "82727128";

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


        public ActionResult VistaCabecera()
        {
            //System.Threading.Thread.Sleep(4000);

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