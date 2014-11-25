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
            //Se obtiene los datos de la sesion.
            Ticket ticket = (Ticket)Session["Ticket"];
            int idEmpresa = ticket.IdEmpresa;

            List<VistaOfertaEmpresa> lista = lnOferta.Obtener_PanelEmpresa(idEmpresa);

            return View(lista);
        }
        public ActionResult Oferta()
        {
            return View();
        }

        
        public ActionResult NuevaOferta()
        {
            Ticket ticket = (Ticket)Session["Ticket"];

            //Se envían datos de prueba.
            Oferta oferta = new Oferta();
            oferta.IdEmpresa = ticket.IdEmpresa;
            oferta.UsuarioPropietarioEmpresa = "";
            oferta.EstadoOferta = "OFERPR"; //Estado pendiente de activación.
            oferta.FechaPublicacion = DateTime.Now;
            oferta.FechaFinProceso = DateTime.Now;
            oferta.IdEmpresaLocacion = 1; //TODO: Reemplazar por combo.
            oferta.DescripcionOferta = "descripción de la oferta";
            //oferta.TipoTrabajo = "OFTTTC"; //Tipo de trabajo: Tiempo completo.
            //oferta.CargoOfrecido = "";  //Este dato viene del formulario.
            oferta.CreadoPor = "admin";

            LNGeneral lnGeneral = new LNGeneral();
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion ();

            ViewBag.ListaTipoCargo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO); 
            ViewBag.ListaTipoTrabajo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO); 
            ViewBag.ListaTipoContrato = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO);
            ViewBag.ListaLocaciones = lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa);

            return View(oferta);
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]        
        //public ActionResult NuevaOferta([Bind(Include = "CreadorPor")] Oferta oferta)
        public ActionResult NuevaOferta(Oferta oferta)
        {
            Ticket ticket = (Ticket)Session["Ticket"];

            if (ModelState.IsValid)
            {
                oferta.UsuarioPropietarioEmpresa = "usuarioEmpresa";
                oferta.EstadoOferta = "OFERPR"; //Estado pendiente de activación.
                oferta.FechaPublicacion = DateTime.Now;
                //oferta.FechaFinProceso = DateTime.Now.AddDays(10);
                //oferta.IdEmpresaLocacion = 1; //TODO: Reemplazar por combo.
                oferta.DescripcionOferta = "descripcion de la oferta";
                //oferta.TipoTrabajo = "OFTTTC"; //Tipo de trabajo: Tiempo completo.    
                //oferta.TipoContrato = "";
                //oferta.TipoCargo = "";
                //oferta.Horario = "";
                //oferta.AreaEmpresa = "";
                oferta.CreadoPor = "admin";

                lnOfertaEmpresa.Insertar(oferta);

                return RedirectToAction("Publicacion");
            }

            //Si existe error, se debe cargar nuevamente la información.
            LNGeneral lnGeneral = new LNGeneral();
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();

            ViewBag.ListaTipoCargo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO);
            ViewBag.ListaTipoTrabajo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO);
            ViewBag.ListaTipoContrato = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO);
            ViewBag.ListaLocaciones = lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa);

            return View(oferta);
        }


        public ActionResult Reclutar()
        {
            return View();
        }

        public ActionResult VistaCabecera()
        {
            ////System.Threading.Thread.Sleep(4000);

            VistaPanelCabecera panel = new VistaPanelCabecera();

            panel = lnEmpresa.ObtenerPanelCabecera(usuarioEmpresa);

            return PartialView("_DatosUsuario", panel);
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