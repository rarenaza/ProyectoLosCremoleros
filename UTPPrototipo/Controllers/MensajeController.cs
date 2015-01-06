using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTPPrototipo.Controllers
{
    public class MensajeController : Controller
    {
        LNMensaje lnMensaje = new LNMensaje();

        public ActionResult Mensajes()
        {
            return View();
        }
        public PartialViewResult _ObtenerMensajes(int id)
        {
            int idOferta = id;

            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //Se guarda el Id de la oferta
            ViewBag.IdOferta = idOferta;

            List<Mensaje> lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticket.IdEmpresa, idOferta);

            return PartialView("_ObtenerMensajes", lista);
        }
        
        [ChildActionOnly]
        public PartialViewResult _MensajeFormulario(int id)
        {
            Mensaje mensaje = new Mensaje();
            mensaje.IdOferta = id;

            ViewBag.ParaUsuario = new SelectList(lnMensaje.ObtenePostulantesPorIdOferta(id), "AlumnoNombreUsuario", "AlumnoNombreCompleto");
           
            return PartialView("_MensajeFormulario", mensaje);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _MensajeFormularioGrabar(Mensaje mensaje)
        {          
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            mensaje.DeUsuario = ticket.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticket.CorreoElectronico;
            mensaje.ParaUsuarioCorreoElectronico = "demo@correo.com";
            mensaje.FechaEnvio = DateTime.Now;
            mensaje.IdEvento = 0;
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido
            mensaje.CreadoPor = ticket.Usuario;
               
            lnMensaje.Insertar(mensaje);

            List<Mensaje> lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticket.IdEmpresa, mensaje.IdOferta);

            ViewBag.IdOferta = mensaje.IdOferta;

            return PartialView("_ObtenerMensajes", lista);
        }

        #region Mensajes formato nuevo

        [HttpGet]
        public PartialViewResult _Mensajes(string pantalla)
        {
            ViewBag.Pantalla = pantalla;

            //int idOferta = id;

            //TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //Se guarda el Id de la oferta
            //ViewBag.IdOferta = idOferta;

            //List<Mensaje> lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticket.IdEmpresa, idOferta);

            return PartialView("_Mensajes");
        }

        [HttpGet]
        public PartialViewResult _MensajesRedactar(string pantalla)
        {
            PartialViewResult vistaParcialResultado = new PartialViewResult();
            vistaParcialResultado = PartialView("_MensajesRedactar");

            switch (pantalla)
            { 
                case "EMPRESA_MICUENTA":
                    vistaParcialResultado = mensajeEmpresaMiCuenta(pantalla);
                    break;
            }
            //OfertaEstudio ofertaEstudio = new OfertaEstudio();
            //ofertaEstudio.IdOferta = id;

            //ViewBag.TipoDeEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DE_ESTUDIO), "IdListaValor", "Valor");
            //ViewBag.EstadoDelEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_DEL_ESTUDIO), "IdListaValor", "Valor");

            return vistaParcialResultado;
        }

        

        [HttpGet]
        public PartialViewResult _MensajesVer()
        {
            //OfertaEstudio ofertaEstudio = new OfertaEstudio();
            //ofertaEstudio.IdOferta = id;

            //ViewBag.TipoDeEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DE_ESTUDIO), "IdListaValor", "Valor");
            //ViewBag.EstadoDelEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_DEL_ESTUDIO), "IdListaValor", "Valor");

            return PartialView("_MensajesVer");
        }

        /// <summary>
        /// Método interno para completar los datos de la pantalla Mi Cuenta en Empresa.
        /// </summary>
        private PartialViewResult mensajeEmpresaMiCuenta(string pantalla)
        {
            ViewBag.Pantalla = pantalla;
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

            //1. Obtener ofertas activas de la empresa.
            LNOferta lnOferta = new LNOferta ();
            //Se obtienen las ofertas activas
            List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList();
            //Se cargan en el ViewBag para ser consumidas desde el html.
            ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");
            

            return PartialView("_MensajesRedactar");
        }

        public JsonResult ObtenerPostulantesPorOferta(int idOferta)
        {
            LNOferta lnOferta = new LNOferta();

            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();
            postulantes = lnOferta.ObtenerPostulantesPorIdOferta(idOferta);

            return Json(postulantes, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}