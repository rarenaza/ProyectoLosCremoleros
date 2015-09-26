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
using System.Data;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Evento;

namespace UTPPrototipo.Controllers
{
    [VerificarSesion, LogPortal]
    public class MensajeController : Controller
    {
        LNMensaje lnMensaje = new LNMensaje();
        LNOferta lnOferta = new LNOferta();
        private int IdOferta = 0;
        private int IdEventoParametro = 0;
        private int IdEmpresaParametro = 0;
        private string UsuarioAlumno = "";
        private string ListaIdAlumnos = "";


        #region Métodos anteriores
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

        #endregion

        #region Mensajes formato nuevo

        /// <summary>
        /// MENSAJES_EMPRESA_INDEX: 
        /// </summary>
        /// <param name="pantalla"></param>
        /// <returns></returns>
        [HttpGet]
        public PartialViewResult _Mensajes(string pantalla, int idOferta = 0, string usuarioAlumno = "", int idEvento = 0, int idEmpresa = 0, int nroPaginaActual = 1)
        {
            ViewBag.Pantalla = pantalla;
            ViewBag.IdOferta = idOferta;
            ViewBag.IdEvento = idEvento;
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.UsuarioAlumno = usuarioAlumno;
            ViewBag.BotonRedactar = (pantalla == "EMPRESA_OFERTA") ? "disabled" : "";
            this.IdOferta = idOferta;
            this.IdEventoParametro = idEvento;
            this.UsuarioAlumno = usuarioAlumno;
            this.IdEmpresaParametro = idEmpresa;
            
            List<Mensaje> lista = new List<Mensaje>();

            lista = ObtenerListaMensajes(pantalla, nroPaginaActual); 
            
            return PartialView("_Mensajes", lista.OrderByDescending(m => m.FechaEnvio));
        }

        [HttpGet]
        public PartialViewResult _MensajesNuevo(string pantalla, int idOferta = 0, string usuarioAlumno = "", int idEvento = 0, int idEmpresa = 0, string listaIdAlumnos = "")
        {
            PartialViewResult vistaMensajeNuevo = new PartialViewResult();
            ViewBag.Pantalla = pantalla;
            ViewBag.UsuarioAlumno = usuarioAlumno;
            ViewBag.IdEmpresa = idEmpresa;
            ViewBag.IdEventoGeneral = idEvento;
            this.IdOferta = idOferta;
            this.IdEventoParametro = idEvento;
            this.IdEmpresaParametro = idEmpresa;
            this.ListaIdAlumnos = listaIdAlumnos;

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
                    vistaMensajeNuevo = mensajeAlumnoOfertaNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_UTP_INDEX:
                    vistaMensajeNuevo = mensajeUTPIndexNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_UTP_EMPRESA:
                    vistaMensajeNuevo = mensajeUTPEmpresaNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_UTP_ALUMNO:
                    vistaMensajeNuevo = mensajeUTPAlumnoNuevo(pantalla, usuarioAlumno);
                    break;

                case Constantes.MENSAJES_UTP_OFERTA:
                    vistaMensajeNuevo = mensajeUTPOfertaNuevo(pantalla);
                    break;

                case Constantes.MENSAJES_EMPRESA_EVENTO:
                    vistaMensajeNuevo = mensajeEmpresaEventoNuevo(pantalla, idEvento);
                    break;

                case Constantes.MENSAJES_ALUMNO_EVENTO:
                    vistaMensajeNuevo = mensajeAlumnoEventoNuevo(pantalla, idEvento);
                    break;

                case Constantes.MENSAJES_UTP_EVENTO:
                    vistaMensajeNuevo = mensajeUTPEventoNuevo(pantalla, idEvento);
                    break;

                case Constantes.MENSAJES_EMPRESA_HUNTING:
                    vistaMensajeNuevo = mensajeEmpresaHuntingNuevo(pantalla, listaIdAlumnos);
                    break;
            }

            return vistaMensajeNuevo;
        }

        

        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _MensajesNuevo(Mensaje mensaje)
        {
            if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_INDEX || mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_OFERTA 
                || mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_EVENTO)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                mensaje.DeUsuario = ticket.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticket.CorreoElectronico;
                mensaje.CreadoPor = ticket.Usuario;
                mensaje.DeUsuarioNombre = ticket.Nombre;
            }
            else
                if (mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_INDEX || mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_OFERTA 
                    || mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_EVENTO)
            {
                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                mensaje.DeUsuario = ticketAlumno.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
                mensaje.CreadoPor = ticketAlumno.Usuario;
                mensaje.DeUsuarioNombre = ticketAlumno.Nombre;
            }
            else
            if (mensaje.Pantalla == Constantes.MENSAJES_UTP_INDEX || mensaje.Pantalla == Constantes.MENSAJES_UTP_ALUMNO ||
                mensaje.Pantalla == Constantes.MENSAJES_UTP_OFERTA || mensaje.Pantalla == Constantes.MENSAJES_UTP_EVENTO ||
                mensaje.Pantalla == Constantes.MENSAJES_UTP_EMPRESA)
            {
                TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
                mensaje.DeUsuario = ticketUtp.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
                mensaje.CreadoPor = ticketUtp.Usuario;
                mensaje.DeUsuarioNombre = ticketUtp.Nombre;
            }
           
          

            mensaje.FechaEnvio = DateTime.Now;            
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido

            if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_HUNTING)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                mensaje.DeUsuario = ticket.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticket.CorreoElectronico;
                mensaje.CreadoPor = ticket.Usuario;
                mensaje.DeUsuarioNombre = ticket.Nombre;
                lnMensaje.InsertarHunting(mensaje);
            }
            else
            {
                lnMensaje.Insertar(mensaje);
            }
            
            //Se guardan las variables para utilizarlas al obtener la lista de mensajes.
            ViewBag.Pantalla = mensaje.Pantalla;
            this.UsuarioAlumno = mensaje.ParaUsuario;
            this.IdOferta = Convert.ToInt32(mensaje.IdOfertaMensaje);
            this.IdEventoParametro = Convert.ToInt32(mensaje.IdEvento);

            List<Mensaje> lista = ObtenerListaMensajes(mensaje.Pantalla);

            ViewBag.IdOfertaMensaje = mensaje.IdOfertaMensaje;
            ViewBag.IdOferta = mensaje.IdOfertaMensaje;
            ViewBag.IdEvento = mensaje.IdEvento;            
            ViewBag.UsuarioAlumno = mensaje.ParaUsuario; //Este valor contiene el dato del usuario alumno en las pantallas UTP - Alumno.

            TempData["MsjExitoCrearMensaje"] = "El mensaje se ha enviado con éxito.";

            return PartialView("_Mensajes", lista.OrderByDescending(m => m.FechaEnvio));
            
        }

        [HttpGet]
        public PartialViewResult _MensajesVer(int idMensaje, string pantalla)
        {                      
            Mensaje mensaje = lnMensaje.ObtenerPorIdMensaje(idMensaje);
            mensaje.Pantalla = pantalla;

            //Se obtiene al remitente del mensaje:
            string remitente = mensaje.DeUsuario;

            //Se obtiene al usuario que está autenticado:
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
            TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
            TicketUTP ticketUTP = (TicketUTP)Session["TicketUtp"];

            string usuarioAutenticado = "";
            if (ticketEmpresa != null) usuarioAutenticado = ticketEmpresa.Usuario; //usuario de empresa
            else if (ticketAlumno != null) usuarioAutenticado = ticketAlumno.Usuario; //usuario alumno
            else if (ticketUTP != null) usuarioAutenticado = ticketUTP.Usuario; //usuario utp

            ViewBag.BotonContestar = "";            
            if (remitente == usuarioAutenticado)
            {
                ViewBag.BotonContestar = "disabled";
            }
            else
            {
                //Se actualiza el estado del mensaje, sólo si el remitente es distinto al usuario autenticado.
                lnMensaje.ActualizarEstadoMensaje(idMensaje, "MSJLEI");  //Se actualiza a mensaje a leído.
            }

            return PartialView("_MensajesVer", mensaje);
        }
        
        /// <summary>
        /// Obtiene la lista de mensajes de acuerdo a la pantalla.
        /// </summary>
        /// <param name="pantalla"></param>
        /// <returns></returns>
        private List<Mensaje> ObtenerListaMensajes(string pantalla, int nroPaginaActual = 1)
        {
            List<Mensaje> lista = new List<Mensaje>();
            
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
            TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
            TicketUTP ticketUTP = (TicketUTP)Session["TicketUtp"];

            switch (pantalla)
            {
                case Constantes.MENSAJES_EMPRESA_INDEX:
                    lista = lnMensaje.ObtenerPorUsuario(ticketEmpresa.Usuario);
                    ViewBag.usuarioActual = ticketEmpresa.Usuario;
                    break;
                case Constantes.MENSAJES_EMPRESA_OFERTA:            
                    lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticketEmpresa.IdEmpresa, IdOferta);
                    ViewBag.usuarioActual = ticketEmpresa.Usuario;
                    break;
                case Constantes.MENSAJES_ALUMNO_INDEX:
                    lista = lnMensaje.ObtenerPorUsuario(ticketAlumno.Usuario);
                    ViewBag.usuarioActual = ticketAlumno.Usuario;
                    break;
                case Constantes.MENSAJES_ALUMNO_OFERTA:
                    lista = lnMensaje.ObtenerPorUsuario(ticketAlumno.Usuario).Where(m => m.Oferta.IdOferta == IdOferta).ToList();
                    ViewBag.usuarioActual = ticketAlumno.Usuario;
                    break;
                case Constantes.MENSAJES_UTP_INDEX:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario);
                    ViewBag.usuarioActual = ticketUTP.Usuario;
                    break;
                case Constantes.MENSAJES_UTP_EMPRESA:
                    //No se puede obtener mensajes solo de una empresa. La tabla Mensajes no tiene idEmpresa.
                    //19FEB: Se comenta esta línea y se obtienen todos los mensajes del usuario.
                    //lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(IdEmpresaParametro, 0);                    
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario);
                    ViewBag.usuarioActual = ticketUTP.Usuario;
                    break;
                case Constantes.MENSAJES_UTP_ALUMNO:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario).Where(m => m.DeUsuario == UsuarioAlumno || m.ParaUsuario == UsuarioAlumno).ToList();
                    ViewBag.usuarioActual = ticketUTP.Usuario;
                    break;
                case Constantes.MENSAJES_UTP_OFERTA:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario).Where(m => m.Oferta.IdOferta == IdOferta).ToList();
                    ViewBag.usuarioActual = ticketUTP.Usuario;
                    break;
                case Constantes.MENSAJES_EMPRESA_EVENTO:
                    lista = lnMensaje.ObtenerPorUsuario(ticketEmpresa.Usuario).Where(m => m.IdEvento == IdEventoParametro).ToList();
                    ViewBag.usuarioActual = ticketEmpresa.Usuario;
                    break;
                case Constantes.MENSAJES_ALUMNO_EVENTO:
                    lista = lnMensaje.ObtenerPorUsuario(ticketAlumno.Usuario).Where(m => m.IdEvento == IdEventoParametro).ToList();
                    ViewBag.usuarioActual = ticketAlumno.Usuario;
                    break;
                case Constantes.MENSAJES_UTP_EVENTO:
                    lista = lnMensaje.ObtenerPorUsuario(ticketUTP.Usuario).Where(m => m.IdEvento == IdEventoParametro).ToList();
                    ViewBag.usuarioActual = ticketUTP.Usuario;
                    break;
                case Constantes.MENSAJES_EMPRESA_HUNTING:
                    lista = lnMensaje.ObtenerPorUsuario(ticketEmpresa.Usuario);
                    ViewBag.usuarioActual = ticketEmpresa.Usuario;
                    break;

            }

            int cantidadTotal = lista.Count();

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA_UTP;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA_UTP;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA_UTP;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            ViewBag.TipoBusqueda = "Simple";

            return lista.OrderByDescending(m => m.FechaEnvio).Skip((nroPaginaActual - 1) * Constantes.FILAS_POR_PAGINA_UTP).Take(Constantes.FILAS_POR_PAGINA_UTP).ToList();
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
            mensajeRespuesta.IdEvento = mensajeBase.IdEvento;
            mensajeRespuesta.Pantalla = pantalla;
            mensajeRespuesta.IdEmpresa = mensajeBase.IdEmpresa;

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
            if (pantalla == Constantes.MENSAJES_UTP_INDEX || pantalla == Constantes.MENSAJES_UTP_ALUMNO || pantalla == Constantes.MENSAJES_UTP_OFERTA
                || pantalla == Constantes.MENSAJES_UTP_EMPRESA || pantalla == Constantes.MENSAJES_UTP_EVENTO)
            {
                TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
                mensajeRespuesta.DeUsuario = ticketUtp.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketUtp.Usuario;
            }
            else
             if (pantalla == Constantes.MENSAJES_EMPRESA_EVENTO)
            {                
                LNEvento lnEvento = new LNEvento();
                DataTable dtEvento = lnEvento.EVENTO_OBTENERPORID(mensajeBase.IdEvento);
                mensajeRespuesta.Evento.NombreEvento = Convert.ToString(dtEvento.Rows[0]["NombreEvento"]);

                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
                mensajeRespuesta.DeUsuario = ticketEmpresa.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketEmpresa.Usuario;
            }
            else
            if (pantalla == Constantes.MENSAJES_ALUMNO_EVENTO)
            {
                LNEvento lnEvento = new LNEvento();
                DataTable dtEvento = lnEvento.EVENTO_OBTENERPORID(mensajeBase.IdEvento);
                mensajeRespuesta.Evento.NombreEvento = Convert.ToString(dtEvento.Rows[0]["NombreEvento"]);

                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];
                mensajeRespuesta.DeUsuario = ticketAlumno.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketAlumno.Usuario;
            }
            else
            if (pantalla == Constantes.MENSAJES_EMPRESA_HUNTING)
            {
                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
                mensajeRespuesta.DeUsuario = ticketEmpresa.Usuario;
                mensajeRespuesta.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
                mensajeRespuesta.CreadoPor = ticketEmpresa.Usuario;
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
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido

            IdOferta = Convert.ToInt32(mensaje.IdOfertaMensaje);            
            IdEventoParametro = Convert.ToInt32(mensaje.IdEvento);
            IdEmpresaParametro = Convert.ToInt32(mensaje.IdEmpresa);
            int idMensajeInsertado = lnMensaje.Insertar(mensaje);
            //lnMensaje.ActualizarEstadoMensaje(idMensajeInsertado, "MSJCON");      //El responder siempre es Nuevo, el que cambia es el IdPadre.

            UsuarioAlumno = mensaje.ParaUsuario;

            List<Mensaje> lista = ObtenerListaMensajes(mensaje.Pantalla);

            ViewBag.IdEmpresa = IdEmpresaParametro;
            ViewBag.IdOfertaMensaje = mensaje.IdOferta;
            ViewBag.IdEvento = mensaje.IdEvento;
            ViewBag.Pantalla = mensaje.Pantalla;
            ViewBag.UsuarioAlumno = mensaje.ParaUsuario;

            TempData["MsjExitoCrearMensaje"] = "El mensaje se ha enviado con éxito.";

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

        private PartialViewResult mensajeAlumnoOfertaNuevo(string pantalla)
        {
            ViewBag.Pantalla = pantalla;
            
            //1. Obtener ofertas activas de la empresa.
            LNOferta lnOferta = new LNOferta();

            //Se obtiene el IdEmpresa de la oferta.
            int idEmpresa = lnMensaje.ObtenerIdEmpresaPorIdOferta(IdOferta);

            //Se obtiene las ofertas de la empresa y se selecciona sólo la oferta enviada como parámetro.
            List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(idEmpresa).Where(m => m.IdOferta == IdOferta).ToList();

            //Se cargan en el ViewBag para ser consumidas desde el html. Se establece el valor del IdOferta.
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", IdOferta);

            ViewBag.IdOfertaGeneral = IdOferta;

            Mensaje mensaje = new Mensaje();
            mensaje.Pantalla = pantalla;

            VistaEmpresaOferta ofertaSeleccionada = listaOfertas.Where(m => m.IdOferta == IdOferta).FirstOrDefault();
            mensaje.Asunto = ofertaSeleccionada == null ? "" : ofertaSeleccionada.CargoOfrecido;
            mensaje.ParaUsuario = ofertaSeleccionada.UsuarioPropietarioEmpresa;
            mensaje.ParaUsuarioNombre = ofertaSeleccionada.UsuarioPropietarioEmpresa;
            mensaje.ParaUsuarioCorreoElectronico = ofertaSeleccionada.UsuarioPropietarioEmpresaCorreo;

           
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

        private PartialViewResult mensajeUTPEmpresaNuevo(string pantalla)
        {
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];

            DataTable dt = lnMensaje.ObtenerUsuarioEmpresaPorIdEmpresa(IdEmpresaParametro);

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketUtp.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketUtp.CorreoElectronico;
            mensaje.ParaUsuario = Convert.ToString(dt.Rows[0]["Usuario"]);
            mensaje.ParaUsuarioNombre = Convert.ToString(dt.Rows[0]["Usuario"]);
            mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dt.Rows[0]["CorreoElectronico"]);
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
            mensaje.ParaUsuarioNombre = usuarioAlumno;
            mensaje.ParaUsuarioCorreoElectronico = alumno.CorreoElectronico1;
            mensaje.CreadoPor = ticketUtp.Usuario;
            mensaje.Pantalla = pantalla;
            
                        
            //Hallar las ofertas a las que el alumno a postulado.
            List<VistaAlumnoOferta> listaOfertas = lnMensaje.ObtenerOfertasPorIdAlumno(alumno.IdAlumno);

            //Se cargan en el ViewBag para ser consumidas desde el html.
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            return PartialView("_MensajesNuevo", mensaje);
        }


        private PartialViewResult mensajeUTPOfertaNuevo(string pantalla)
        {
            ViewBag.Pantalla = pantalla;
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

            //1. Obtener ofertas activas de la empresa.
            LNOferta lnOferta = new LNOferta();

            //Se obtiene el IdEmpresa de la oferta.
            int idEmpresa = lnMensaje.ObtenerIdEmpresaPorIdOferta(IdOferta);

            //Se obtiene las ofertas de la empresa y se selecciona solo una oferta.
            List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(idEmpresa).Where(m => m.IdOferta == IdOferta).ToList();
            
            //Se cargan en el ViewBag para ser consumidas desde el html. Se establece el valor del IdOferta.
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido", IdOferta);

            ViewBag.IdOfertaGeneral = IdOferta;

            Mensaje mensaje = new Mensaje();
            mensaje.Pantalla = pantalla;

            VistaEmpresaOferta ofertaSeleccionada = listaOfertas.Where(m => m.IdOferta == IdOferta).FirstOrDefault();
            mensaje.Asunto = ofertaSeleccionada == null ? "" : ofertaSeleccionada.CargoOfrecido;
            mensaje.ParaUsuario = ofertaSeleccionada.UsuarioPropietarioEmpresa;
            mensaje.ParaUsuarioNombre = ofertaSeleccionada.UsuarioPropietarioEmpresa;
            mensaje.ParaUsuarioCorreoElectronico = ofertaSeleccionada.UsuarioPropietarioEmpresaCorreo;
            mensaje.IdOfertaMensaje = IdOferta; //Se establece el IdOferta enviado como parámetro

            //Hay que llenar el combo de destinatarios con los postulantes y el usuario empresa de la oferta.


            return PartialView("_MensajesNuevo", mensaje);
        }

        private PartialViewResult mensajeEmpresaEventoNuevo(string pantalla, int idEvento)
        {
            ViewBag.Pantalla = pantalla;
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

            LNEvento lnEvento = new LNEvento();
            DataTable dtEvento = lnEvento.EVENTO_OBTENERPORID(idEvento);
            
            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketEmpresa.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
            mensaje.Pantalla = pantalla;
            mensaje.Asunto = Convert.ToString(dtEvento.Rows[0]["NombreEvento"]);
            mensaje.Evento.NombreEvento = Convert.ToString(dtEvento.Rows[0]["NombreEvento"]);
            mensaje.IdEvento = Convert.ToInt32(dtEvento.Rows[0]["IdEvento"]);

            //Se manda el correo al administrador de la UPT. No existe funcionalidad de asignar usuario UTP al evento.            
            //DataTable dtUsuarioUTPAdmin = lnMensaje.ObtenerUsuarioAdministradorUTP(); //--se obtiene, la información y se completan los campos.
            //mensaje.ParaUsuario = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["Usuario"]);
            //mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["CorreoElectronico"]);

            //04MAR15: Se coloca el usuario y correo del que ha creado el evento.
            mensaje.ParaUsuario = Convert.ToString(dtEvento.Rows[0]["CreadoPor"]);
            mensaje.ParaUsuarioNombre = Convert.ToString(dtEvento.Rows[0]["NombresUsuarioCreacion"]) + " " + Convert.ToString(dtEvento.Rows[0]["ApellidosUsuarioCreacion"]);
            mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtEvento.Rows[0]["CorreoUsuarioCreacion"]);
            
            return PartialView("_MensajesNuevo", mensaje);
        }

        private PartialViewResult mensajeAlumnoEventoNuevo(string pantalla, int idEvento)
        {
            ViewBag.Pantalla = pantalla;
            TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];

            LNEvento lnEvento = new LNEvento();
            DataTable dtEvento = lnEvento.EVENTO_OBTENERPORID(idEvento);

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketAlumno.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
            mensaje.Pantalla = pantalla;
            mensaje.Asunto = Convert.ToString(dtEvento.Rows[0]["NombreEvento"]);

            //Se manda el correo al administrador de la UPT. No existe funcionalidad de asignar usuario UTP al evento.
            //DataTable dtUsuarioUTPAdmin = lnMensaje.ObtenerUsuarioAdministradorUTP(); //--se obtiene, la información y se completan los campos.
            //mensaje.ParaUsuario = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["Usuario"]);
            //mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["CorreoElectronico"]);

            //04MAR15: Se coloca el usuario y correo del que ha creado el evento.
            mensaje.ParaUsuario = Convert.ToString(dtEvento.Rows[0]["CreadoPor"]);
            mensaje.ParaUsuarioNombre = Convert.ToString(dtEvento.Rows[0]["NombresUsuarioCreacion"]) + " " + Convert.ToString(dtEvento.Rows[0]["ApellidosUsuarioCreacion"]);
            mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtEvento.Rows[0]["CorreoUsuarioCreacion"]);

            return PartialView("_MensajesNuevo", mensaje);
        }


        private PartialViewResult mensajeUTPEventoNuevo(string pantalla, int idEvento)
        {
            ViewBag.Pantalla = pantalla;
            TicketUTP ticketUTP = (TicketUTP)Session["TicketUtp"];

            LNEvento lnEvento = new LNEvento();
            DataTable dtEvento = lnEvento.EVENTO_OBTENERPORID(idEvento);

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketUTP.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketUTP.CorreoElectronico;
            mensaje.Pantalla = pantalla;
            mensaje.Asunto = Convert.ToString(dtEvento.Rows[0]["NombreEvento"]);
            //mensaje.ParaUsuario = "faltaUsuarioUTPDelEvento";
            //mensaje.ParaUsuarioCorreoElectronico = "faltaUsuarioCorreoUTPDelEvento";

            //ViewBag.ParaUsuario = lnEvento.ObtenerAsistentes(idEvento, "EVTAAL"); //tipo alumno.
            ViewBag.IdEventoGeneral = idEvento;

            return PartialView("_MensajesNuevo", mensaje);
        }

        private PartialViewResult mensajeEmpresaHuntingNuevo(string pantalla, string listaIdAlumnos)
        {
            ViewBag.Pantalla = pantalla;
            TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

            LNOferta lnOferta = new LNOferta();            
            List<VistaEmpresaOferta> listaOfertas = lnOferta.ObtenerOfertasPorIdEmpresa(ticketEmpresa.IdEmpresa).Where(m => m.NombreEstado == "OFERAC").ToList(); //Activas.
            ViewBag.IdOfertaMensaje = new SelectList(listaOfertas, "IdOferta", "CargoOfrecido");

            Mensaje mensaje = new Mensaje();
            mensaje.DeUsuario = ticketEmpresa.Usuario;
            mensaje.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
            mensaje.ParaUsuario = lnMensaje.ObtenerListaAlumnosHunting(listaIdAlumnos); //Se guarda la lista de ids en esta propiedad.
            mensaje.ParaUsuarioNombre = lnMensaje.ObtenerListaAlumnosHunting(listaIdAlumnos); //Se guarda la lista de ids en esta propiedad.
            mensaje.ParaUsuarioCorreoElectronico = listaIdAlumnos;
            mensaje.Pantalla = pantalla;            
            mensaje.CreadoPor = ticketEmpresa.Usuario;

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

        public JsonResult ObtenerPostulantesPorOferta(int idOferta)
        {
            LNOferta lnOferta = new LNOferta();

            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();
            postulantes = lnOferta.ObtenerPostulantesPorIdOferta(idOferta);

            return Json(postulantes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerUsuarioEmpresaPorOferta(int idOferta)
        {            
            VistaMensajeUsuarioEmpresaOferta oferta = lnMensaje.ObtenerUsuarioEmpresaOfertaPorId(idOferta);
            
            return Json(oferta, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerDestinatariosUTPOferta(int idOferta)
        {
            LNOferta lnOferta = new LNOferta();

            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();
            postulantes = lnOferta.ObtenerPostulantesPorIdOferta(idOferta);
            
            VistaMensajeUsuarioEmpresaOferta oferta = lnMensaje.ObtenerUsuarioEmpresaOfertaPorId(idOferta);

            List<VistaMensajeDestinatario> destinatarios = new List<VistaMensajeDestinatario>();
            
            //Se agregan a los postulantes:
            foreach (var postulante in postulantes)
            {
                VistaMensajeDestinatario destinatario = new VistaMensajeDestinatario();
                destinatario.Usuario = postulante.Usuario;
                destinatario.Correo = postulante.CorreoElectronico;
                destinatario.TextoAMostrar = postulante.Usuario + " (" + postulante.CorreoElectronico + ")";

                destinatarios.Add(destinatario);
            }

            //Se agrega al usuario de la oferta:
            VistaMensajeDestinatario destinatarioOferta = new VistaMensajeDestinatario();
            destinatarioOferta.Usuario = oferta.UsuarioPropietarioEmpresa;
            destinatarioOferta.Correo = oferta.UsuarioPropietarioEmpresaCorreo;
            destinatarioOferta.TextoAMostrar = oferta.UsuarioPropietarioEmpresa + " (" + oferta.UsuarioPropietarioEmpresaCorreo + ")";

            destinatarios.Add(destinatarioOferta);

            return Json(postulantes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerAsistentesPorEvento(int idEvento)
        {
            LNEvento lnEvento = new LNEvento();

            List<VistaAsistente> asistentes = new List<VistaAsistente>();
            asistentes = lnEvento.ObtenerAsistentes(idEvento, "EVTAAL"); //tipo alumno.

            return Json(asistentes, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult _BaseMensajes()
        {
            return View();
        }

    }
}