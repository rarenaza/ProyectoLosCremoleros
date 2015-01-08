using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Mensaje;

namespace UTPPrototipo.Controllers
{
    public class MensajeController : Controller
    {
        LNMensaje lnMensaje = new LNMensaje();
        LNOferta lnOferta = new LNOferta();

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

        /// <summary>
        /// MENSAJES_EMPRESA_INDEX: 
        /// </summary>
        /// <param name="pantalla"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _Mensajes(string pantalla)
        {
            ViewBag.Pantalla = pantalla;

            //int idOferta = id;

            //TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //Se guarda el Id de la oferta
            //ViewBag.IdOferta = idOferta;

            List<Mensaje> lista = new List<Mensaje>();

            lista = ObtenerListaMensajes(pantalla); 

            //switch (pantalla)
            //{
            //    case Constantes.MENSAJES_EMPRESA_INDEX:
            //        lista = ObtenerListaMensajes(pantalla);
            //        break;
            //}

            return PartialView("_Mensajes", lista.OrderByDescending(m => m.FechaEnvio));
        }

        [HttpGet]
        public PartialViewResult _MensajesNuevo(string pantalla)
        {            
            return obtenerPantallaMensajeNuevo(pantalla);
        }

        private PartialViewResult obtenerPantallaMensajeNuevo(string pantalla)
        {
            PartialViewResult vistaMensajeNuevo = new PartialViewResult();
            ViewBag.Pantalla = pantalla;

            switch (pantalla)
            {
                case Constantes.MENSAJES_EMPRESA_INDEX:
                    vistaMensajeNuevo = mensajeEmpresaIndexNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_ALUMNO_INDEX:
                    vistaMensajeNuevo = mensajeAlumnoIndexNuevo(pantalla);
                    break;
            }

            return vistaMensajeNuevo;
        }


        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _MensajesNuevo(Mensaje mensaje)
        {
            if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_INDEX)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                mensaje.DeUsuario = ticket.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticket.CorreoElectronico;
                mensaje.CreadoPor = ticket.Usuario;
            }

            if (mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_INDEX)
            {
                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                mensaje.DeUsuario = ticketAlumno.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
                mensaje.CreadoPor = ticketAlumno.Usuario;
            }
            
            mensaje.FechaEnvio = DateTime.Now;
            mensaje.IdEvento = 0;
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido
            

            lnMensaje.Insertar(mensaje);
            ViewBag.Pantalla = mensaje.Pantalla;

            List<Mensaje> lista = ObtenerListaMensajes(mensaje.Pantalla);

            ViewBag.IdOferta = mensaje.IdOferta;

            return PartialView("_Mensajes", lista.OrderByDescending(m => m.FechaEnvio));
            
        }

        [HttpGet]
        public PartialViewResult _MensajesVer(int idMensaje, string pantalla)
        {                      
            Mensaje mensaje = lnMensaje.ObtenerPorIdMensaje(idMensaje);
            mensaje.Pantalla = pantalla;

            return PartialView("_MensajesVer", mensaje);
        }
        
        /// <summary>
        /// Obtiene la lista de mensajes de acuerdo a la pantalla.
        /// </summary>
        /// <param name="pantalla"></param>
        /// <returns></returns>
        private List<Mensaje> ObtenerListaMensajes(string pantalla)
        {
            List<Mensaje> lista = new List<Mensaje>();
                        
            switch (pantalla)
            {
                case Constantes.MENSAJES_EMPRESA_INDEX:
                    TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];                    
                    lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticketEmpresa.IdEmpresa, 0);
                    break;
                case Constantes.MENSAJES_ALUMNO_INDEX:
                    TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                    lista = lnMensaje.ObtenerPorAlumno(ticketAlumno.Usuario);
                    break;
            }            
            return lista;
        }

        public JsonResult ObtenerPostulantesPorOferta(int idOferta)
        {
            LNOferta lnOferta = new LNOferta();

            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();
            postulantes = lnOferta.ObtenerPostulantesPorIdOferta(idOferta);

            return Json(postulantes, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public PartialViewResult _MensajesResponder(string pantalla, int idMensaje)
        {
            Mensaje mensajeBase = lnMensaje.ObtenerPorIdMensaje(idMensaje); //Este es el mensaje original.
            Mensaje mensajeRespuesta = new Mensaje();
            mensajeRespuesta.Asunto = "Re: " + mensajeBase.Asunto;
            mensajeRespuesta.IdMensajePadre = mensajeBase.IdMensaje;
            mensajeRespuesta.ParaUsuario = mensajeBase.DeUsuario;
            mensajeRespuesta.ParaUsuarioCorreoElectronico = mensajeBase.DeUsuarioCorreoElectronico;
            mensajeRespuesta.IdOferta = mensajeBase.IdOferta;
            mensajeRespuesta.Pantalla = pantalla;

            if (pantalla == Constantes.MENSAJES_EMPRESA_INDEX)
            {
                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
                mensajeRespuesta.DeUsuario = ticketEmpresa.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;

                //1. Obtener ofertas activas de la empresa.
                LNOferta lnOferta = new LNOferta();

                //Se obtienen las ofertas activas
                List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList();

                //Se cargan en el ViewBag para ser consumidas desde el html y se establece el IdOferta.
                ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", mensajeBase.IdOferta);

            }
            else
                if (pantalla == Constantes.MENSAJES_ALUMNO_INDEX)
                {
                    TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                    mensajeRespuesta.DeUsuario = ticketAlumno.Usuario;
                    mensajeRespuesta.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;

                    //Hallar las ofertas a las que el alumno a postulado.
                    List<VistaAlumnoOferta> listaOfertas = lnMensaje.ObtenerOfertasPorIdAlumno(ticketAlumno.IdAlumno);

                    //Se cargan en el ViewBag para ser consumidas desde el html.
                    ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", mensajeBase.IdOferta);
                }

            ViewBag.Pantalla = pantalla;

            return PartialView("_MensajesResponder", mensajeRespuesta);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _MensajesResponder(Mensaje mensaje)
        {
            if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_INDEX)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                mensaje.CreadoPor = ticket.Usuario;
            }
            else
                if (mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_INDEX)
                {
                    TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                    mensaje.CreadoPor = ticketAlumno.Usuario;
                }           
            mensaje.FechaEnvio = DateTime.Now;
            mensaje.IdEvento = 0; //Por desarrollar.
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido
           
            lnMensaje.Insertar(mensaje);

            List<Mensaje> lista = ObtenerListaMensajes(mensaje.Pantalla); //Falta definir qué mensajes se mostrarán.

            ViewBag.IdOferta = mensaje.IdOferta;
            ViewBag.Pantalla = mensaje.Pantalla;

            return PartialView("_Mensajes", lista.OrderByDescending(m => m.FechaEnvio));
        }


        /// <summary>
        /// Método interno para completar los datos de la pantalla Mi Cuenta en Empresa.
        /// </summary>
        private PartialViewResult mensajeEmpresaIndexNuevo(string pantalla)
        {
            ViewBag.Pantalla = pantalla;
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

            //1. Obtener ofertas activas de la empresa.
            LNOferta lnOferta = new LNOferta();

            //Se obtienen las ofertas activas
            List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList();
            
            //Se cargan en el ViewBag para ser consumidas desde el html.
            ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            Mensaje mensaje = new Mensaje();
            mensaje.Pantalla = pantalla;

            return PartialView("_MensajesNuevo", mensaje);
        }

        private PartialViewResult mensajeAlumnoIndexNuevo(string pantalla)
        {
            TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketAlumno.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
            mensaje.Pantalla = pantalla;
            
            //Hallar las ofertas a las que el alumno a postulado.
            List<VistaAlumnoOferta> listaOfertas = lnMensaje.ObtenerOfertasPorIdAlumno(ticketAlumno.IdAlumno);
            
            //Se cargan en el ViewBag para ser consumidas desde el html.
            ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            return PartialView("_MensajesNuevo", mensaje);
        }

        [Obsolete]
        private PartialViewResult obtenerVistaMensajeResponder(string pantalla, int idMensaje)
        {
            Mensaje mensajeBase = lnMensaje.ObtenerPorIdMensaje(idMensaje); //Este es el mensaje original.
            Mensaje mensajeRespuesta = new Mensaje();
            mensajeRespuesta.Asunto = "Re: " + mensajeBase.Asunto;
            mensajeRespuesta.IdMensajePadre = mensajeBase.IdMensaje;
            mensajeRespuesta.ParaUsuario = mensajeBase.DeUsuario;
            mensajeRespuesta.ParaUsuarioCorreoElectronico = mensajeBase.DeUsuarioCorreoElectronico;
            mensajeRespuesta.IdOferta = mensajeBase.IdOferta;
            mensajeRespuesta.Pantalla = pantalla;

            if (pantalla == Constantes.MENSAJES_EMPRESA_INDEX)
            {
                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
                mensajeRespuesta.DeUsuario = ticketEmpresa.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
                
                //1. Obtener ofertas activas de la empresa.
                LNOferta lnOferta = new LNOferta();
                
                //Se obtienen las ofertas activas
                List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList();

                //Se cargan en el ViewBag para ser consumidas desde el html y se establece el IdOferta.
                ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", mensajeBase.IdOferta);

            }
            else
            if (pantalla == Constantes.MENSAJES_ALUMNO_INDEX)
            {
                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                mensajeRespuesta.DeUsuario = ticketAlumno.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;

                //Hallar las ofertas a las que el alumno a postulado.
                List<VistaAlumnoOferta> listaOfertas = lnMensaje.ObtenerOfertasPorIdAlumno(ticketAlumno.IdAlumno);

                //Se cargan en el ViewBag para ser consumidas desde el html.
                ViewBag.IdOferta = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", mensajeBase.IdOferta);
            }
                                                         
            ViewBag.Pantalla = pantalla;

            return PartialView("_MensajesResponder", mensajeRespuesta);
        }

        public JsonResult ObtenerUsuarioEmpresaPorOferta(int idOferta)
        {            
            VistaMensajeUsuarioEmpresaOferta oferta = lnMensaje.ObtenerUsuarioEmpresaOfertaPorId(idOferta);
            
            return Json(oferta, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}