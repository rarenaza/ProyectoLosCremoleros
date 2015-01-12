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
using UTPPrototipo.Common;

namespace UTPPrototipo.Controllers
{
    [VerificarSesion]
    public class MensajeController : Controller
    {
        LNMensaje lnMensaje = new LNMensaje();
        LNOferta lnOferta = new LNOferta();
        private int IdOferta = 0;
        private string UsuarioAlumno = "";

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
        public PartialViewResult _Mensajes(string pantalla, int idOferta = 0, string usuarioAlumno = "")
        {
            ViewBag.Pantalla = pantalla;
            ViewBag.IdOferta = idOferta;
            ViewBag.UsuarioAlumno = usuarioAlumno;

            this.IdOferta = idOferta;
            this.UsuarioAlumno = usuarioAlumno;

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
        public PartialViewResult _MensajesNuevo(string pantalla, int idOferta = 0, string usuarioAlumno = "")
        {
            PartialViewResult vistaMensajeNuevo = new PartialViewResult();
            ViewBag.Pantalla = pantalla;
            ViewBag.UsuarioAlumno = usuarioAlumno;
            this.IdOferta = idOferta;

            switch (pantalla)
            {
                case Constantes.MENSAJES_EMPRESA_INDEX:
                    vistaMensajeNuevo = mensajeEmpresaIndexNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_EMPRESA_OFERTA:
                    vistaMensajeNuevo = mensajeEmpresaOfertaNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_ALUMNO_INDEX:
                    vistaMensajeNuevo = mensajeAlumnoIndexNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_ALUMNO_OFERTA:
                    vistaMensajeNuevo = mensajeAlumnoIndexNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_UTP_INDEX:
                    vistaMensajeNuevo = mensajeUTPIndexNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_UTP_ALUMNO:
                    vistaMensajeNuevo = mensajeUTPAlumnoNuevo(pantalla, usuarioAlumno);
                    break;

                //case Constantes.MENSAJES_UTP_OFERTA:
                //    vistaMensajeNuevo = mensajeUTPAlumnoNuevo(pantalla);
                //    break;
            }

            return vistaMensajeNuevo;
        }

        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _MensajesNuevo(Mensaje mensaje)
        {
            if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_INDEX || mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_OFERTA)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                mensaje.DeUsuario = ticket.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticket.CorreoElectronico;
                mensaje.CreadoPor = ticket.Usuario;
            }
            else
            if (mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_INDEX || mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_OFERTA)
            {
                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                mensaje.DeUsuario = ticketAlumno.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
                mensaje.CreadoPor = ticketAlumno.Usuario;
            }
            else
            if (mensaje.Pantalla == Constantes.MENSAJES_UTP_INDEX || mensaje.Pantalla == Constantes.MENSAJES_UTP_ALUMNO || 
                mensaje.Pantalla == Constantes.MENSAJES_UTP_OFERTA)
            {
                TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
                mensaje.DeUsuario = ticketUtp.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
                mensaje.CreadoPor = ticketUtp.Usuario;

            }

            mensaje.FechaEnvio = DateTime.Now;
            mensaje.IdEvento = 0;
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido
            
            lnMensaje.Insertar(mensaje);
            ViewBag.Pantalla = mensaje.Pantalla;
            this.UsuarioAlumno = mensaje.ParaUsuario;

            List<Mensaje> lista = ObtenerListaMensajes(mensaje.Pantalla);

            ViewBag.IdOfertaMensaje = mensaje.IdOfertaMensaje;
            ViewBag.UsuarioAlumno = mensaje.ParaUsuario; //Este valor contiene el dato del usuario alumno en las pantallas UTP - Alumno.
            

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
            
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
            TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
            TicketUTP ticketUTP = (TicketUTP)Session["TicketUtp"];

            switch (pantalla)
            {
                case Constantes.MENSAJES_EMPRESA_INDEX:
                    lista = lnMensaje.ObtenerPorUsuario(ticketEmpresa.Usuario);
                    break;
                case Constantes.MENSAJES_EMPRESA_OFERTA:            
                    lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticketEmpresa.IdEmpresa, IdOferta);
                    break;
                case Constantes.MENSAJES_ALUMNO_INDEX:
                    lista = lnMensaje.ObtenerPorUsuario(ticketAlumno.Usuario);
                    break;
                case Constantes.MENSAJES_ALUMNO_OFERTA:
                    lista = lnMensaje.ObtenerPorUsuario(ticketAlumno.Usuario).Where(m => m.Oferta.IdOferta == IdOferta).ToList();
                    break;
                case Constantes.MENSAJES_UTP_INDEX:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario);
                    break;
                case Constantes.MENSAJES_UTP_ALUMNO:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario).Where(m => m.DeUsuario == UsuarioAlumno || m.ParaUsuario == UsuarioAlumno).ToList();
                    break;
                case Constantes.MENSAJES_UTP_OFERTA:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario).Where(m => m.Oferta.IdOferta == IdOferta).ToList();
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
            mensajeRespuesta.IdOfertaMensaje = mensajeBase.IdOferta;
            mensajeRespuesta.Pantalla = pantalla;

            if (pantalla == Constantes.MENSAJES_EMPRESA_INDEX || pantalla == Constantes.MENSAJES_EMPRESA_OFERTA)
            {
                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
                mensajeRespuesta.DeUsuario = ticketEmpresa.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketEmpresa.Usuario;

                //1. Obtener ofertas activas de la empresa.
                LNOferta lnOferta = new LNOferta();

                //Se obtienen las ofertas activas
                List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList();

                //Se cargan en el ViewBag para ser consumidas desde el html y se establece el IdOferta.
                ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", mensajeBase.IdOferta);

            }
            else
            if (pantalla == Constantes.MENSAJES_ALUMNO_INDEX || pantalla == Constantes.MENSAJES_ALUMNO_OFERTA)
            {
                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                mensajeRespuesta.DeUsuario = ticketAlumno.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketAlumno.Usuario;

                //Hallar las ofertas a las que el alumno a postulado.
                List<VistaAlumnoOferta> listaOfertas = lnMensaje.ObtenerOfertasPorIdAlumno(ticketAlumno.IdAlumno);

                //Se cargan en el ViewBag para ser consumidas desde el html.
                ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", mensajeBase.IdOferta);
            }
            else
            if (pantalla == Constantes.MENSAJES_UTP_INDEX || pantalla == Constantes.MENSAJES_UTP_ALUMNO)
            {
                TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
                mensajeRespuesta.DeUsuario = ticketUtp.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketUtp.Usuario;
            }

            ViewBag.Pantalla = pantalla;

            return PartialView("_MensajesResponder", mensajeRespuesta);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _MensajesResponder(Mensaje mensaje)
        {
            //if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_INDEX || mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_OFERTA)
            //{
            //    TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            //    mensaje.CreadoPor = ticket.Usuario;
            //}
            //else
            //if (mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_INDEX || mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_OFERTA)
            //{
            //    TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
            //    mensaje.CreadoPor = ticketAlumno.Usuario;
            //}
            //else
            //if (mensaje.Pantalla == Constantes.MENSAJES_UTP_INDEX)
            //{
            //    TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
            //    mensaje.CreadoPor = ticketUtp.Usuario;
            //}

            mensaje.FechaEnvio = DateTime.Now;
            mensaje.IdEvento = 0; //Por desarrollar.
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido

            IdOferta = Convert.ToInt32(mensaje.IdOfertaMensaje);
            lnMensaje.Insertar(mensaje);
            UsuarioAlumno = mensaje.ParaUsuario;

            List<Mensaje> lista = ObtenerListaMensajes(mensaje.Pantalla);

            ViewBag.IdOfertaMensaje = mensaje.IdOferta;
            ViewBag.Pantalla = mensaje.Pantalla;
            ViewBag.UsuarioAlumno = mensaje.ParaUsuario;

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
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            Mensaje mensaje = new Mensaje();
            mensaje.Pantalla = pantalla;

            return PartialView("_MensajesNuevo", mensaje);
        }

        /// <summary>
        /// Método interno para completar los datos de la pantalla Oferta en Empresa.
        /// </summary>
        private PartialViewResult mensajeEmpresaOfertaNuevo(string pantalla)
        {
            ViewBag.Pantalla = pantalla;
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

            //1. Obtener ofertas activas de la empresa.
            LNOferta lnOferta = new LNOferta();

            //Se obtienen las ofertas activas
            List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList();

            VistaEmpresaOferta ofertaSeleccionada = listaOfertas.Where(m => m.IdOferta == IdOferta).FirstOrDefault();

            //Se cargan en el ViewBag para ser consumidas desde el html. Se establece el valor del IdOferta.
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            ViewBag.IdOfertaGeneral = IdOferta;

            Mensaje mensaje = new Mensaje();
            mensaje.Pantalla = pantalla;
            mensaje.Asunto = ofertaSeleccionada.CargoOfrecido;
            //mensaje.ParaUsuario = ofertaSeleccionada.UsuarioPropietarioEmpresa;
            //mensaje.ParaUsuarioCorreoElectronico = ofertaSeleccionada.UsuarioPropietarioEmpresaCorreo;
            //mensaje.Oferta.CargoOfrecido = ofertaSeleccionada.CargoOfrecido;
            

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
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            return PartialView("_MensajesNuevo", mensaje);
        }

        private PartialViewResult mensajeUTPIndexNuevo(string pantalla)
        {
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketUtp.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
            mensaje.CreadoPor = ticketUtp.Usuario;            
            mensaje.Pantalla = pantalla;
                        
            return PartialView("_MensajesNuevo", mensaje);
        }


        private PartialViewResult mensajeUTPAlumnoNuevo(string pantalla, string usuarioAlumno)
        {
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
            LNAlumno lnAlumno = new LNAlumno();
            Alumno alumno = lnAlumno.ObtenerAlumnoPorCodigo(usuarioAlumno);

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketUtp.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
            mensaje.ParaUsuario = usuarioAlumno;
            mensaje.ParaUsuarioCorreoElectronico = alumno.CorreoElectronico1;
            mensaje.CreadoPor = ticketUtp.Usuario;
            mensaje.Pantalla = pantalla;
            
                        
            //Hallar las ofertas a las que el alumno a postulado.
            List<VistaAlumnoOferta> listaOfertas = lnMensaje.ObtenerOfertasPorIdAlumno(alumno.IdAlumno);

            //Se cargan en el ViewBag para ser consumidas desde el html.
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

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