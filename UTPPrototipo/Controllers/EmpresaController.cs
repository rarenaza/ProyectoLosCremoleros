using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTPPrototipo.Controllers
{
    public class EmpresaController : Controller
    {
        LNEmpresa lnEmpresa = new LNEmpresa();
        LNOferta lnOferta = new LNOferta();
        LNOfertaEmpresa lnOfertaEmpresa = new LNOfertaEmpresa();

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
            Oferta oferta = new Oferta();
            oferta.IdEmpresa = 1;
            oferta.UsuarioPropietarioEmpresa = "";
            oferta.EstadoOferta = "OFERPR"; //Estado pendiente de activación.
            oferta.FechaPublicacion = DateTime.Now;
            oferta.FechaFinProceso = DateTime.Now;
            oferta.IdEmpresaLocacion = 1; //TODO: Reemplazar por combo.
            oferta.DescripcionOferta = "descripci[on de la oferta";
            oferta.TipoTrabajo = "OFTTTC"; //Tipo de trabajo: Tiempo completo.
            //oferta.CargoOfrecido = "";  //Este dato viene del formulario.
            oferta.CreadoPor = "admin";

            return View(oferta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NuevaOferta(Oferta oferta)
        {
            if (ModelState.IsValid)
            {
                lnOfertaEmpresa.Insertar(oferta);
            }

            return View();
        }


        public ActionResult Reclutar()
        {
            return View();
        }

        public ActionResult VistaCabecera()
        {
            ////System.Threading.Thread.Sleep(4000);

            //VistaPanelCabecera panel = new VistaPanelCabecera();

            //panel = lnEmpresa.ObtenerPanelCabecera(usuarioEmpresa);

            return PartialView("_DatosUsuario");
        }

        public ActionResult Postulante()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            Empresa empresa = new Empresa();

            return View(empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                empresa.EstadoEmpresa = Constantes.ESTADO_EMPRESA_PENDIENTE_APROBACION;
                empresa.SectorEmpresarial = "SETECN"; //Data de prueba.
                empresa.DescripcionEmpresa = "";
                empresa.LinkVideo = "";
                empresa.AnoCreacion = 2014;
                empresa.NumeroEmpleados = "";
                empresa.SectorEmpresarial2 = "";
                empresa.SectorEmpresarial3 = "";

                empresa.CreadoPor = "admin";

                lnEmpresa.Insertar(empresa);
            }


            return RedirectToAction("Index", "Home");
        }
	}
}