using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;

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
    }
}