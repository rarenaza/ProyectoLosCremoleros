using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Models.ViewModels.Empresa;

namespace UTPPrototipo.Controllers
{
    public class EmpresaController : Controller
    {
        LNEmpresa lnEmpresa = new LNEmpresa();
        LNOferta lnOferta = new LNOferta();
        LNOfertaEmpresa lnOfertaEmpresa = new LNOfertaEmpresa();
        LNGeneral lnGeneral = new LNGeneral();
        public string usuarioEmpresa = "82727128";

        //
        // GET: /Empresa/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Publicacion(string filtroBusqueda)
        {
            //Se obtiene los datos de la sesion.
            Ticket ticket = (Ticket)Session["Ticket"];
            int idEmpresa = ticket.IdEmpresa;

            string filtro = filtroBusqueda == null ? "" : filtroBusqueda;

            List<VistaOfertaEmpresa> lista = lnOferta.Obtener_PanelEmpresa(idEmpresa, filtro);

            return View(lista);
        }
        public ActionResult Oferta(int idOferta)
        {
            Oferta oferta = lnOferta.ObtenerPorId(idOferta);

            return View(oferta);
        }

        
        public ActionResult EditarOferta(int idOferta)
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            Oferta oferta = lnOferta.ObtenerPorId(idOferta);

            LNGeneral lnGeneral = new LNGeneral();
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();

            ViewBag.TipoCargoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO), "IdListaValor", "Valor", oferta.TipoCargoIdListaValor);
            ViewBag.TipoTrabajoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO), "IdListaValor", "Valor", oferta.TipoTrabajoIdListaValor);
            ViewBag.TipoContratoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO), "IdListaValor", "Valor", oferta.TipoContratoIdListaValor);
            ViewBag.IdEmpresaLocacion = new SelectList(lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa), "IdEmpresaLocacion", "NombreLocacion", oferta.IdEmpresaLocacion);
            ViewBag.RecibeCorreosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_OFERTA_RECIBECORREOS), "IdListaValor", "Valor", oferta.RecibeCorreosIdListaValor);

            //ViewBag.ListaTipoCargo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO);
            //ViewBag.ListaTipoTrabajo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO);
            //ViewBag.ListaTipoContrato = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO);
            //ViewBag.ListaLocaciones = lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa);

            return View(oferta);
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]        
        public ActionResult EditarOferta(Oferta oferta)
        {
            Ticket ticket = (Ticket)Session["Ticket"];

            if (ModelState.IsValid)
            {
                oferta.UsuarioPropietarioEmpresa = "";
                oferta.ModificadoPor = "adminEmpresa";

                lnOferta.Actualizar(oferta);

                //1. Mostrar mensaje de exito.

                //2. Redireccionar a la lista.
                return RedirectToAction("Publicacion");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                int a = 0;
            }
            LNGeneral lnGeneral = new LNGeneral();
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();

            ViewBag.ListaFaseOferta = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_FASE_OFERTA);
            ViewBag.ListaTipoCargo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO);
            ViewBag.ListaTipoTrabajo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO);
            ViewBag.ListaTipoContrato = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO);
            ViewBag.ListaLocaciones = lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa);

            return View(oferta);
        }

        public ActionResult NuevaOferta()
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //Se envían datos de prueba.
            Oferta oferta = new Oferta();
            oferta.IdEmpresa = ticket.IdEmpresa;
            oferta.UsuarioPropietarioEmpresa = "";            
            oferta.FechaPublicacion = DateTime.Now;                        
            oferta.CreadoPor = ticket.Usuario;

            LNGeneral lnGeneral = new LNGeneral();
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion ();

            //Se completan las listas:

            ViewBag.TipoCargoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO), "IdListaValor", "Valor");
            ViewBag.TipoTrabajoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO), "IdListaValor", "Valor");
            ViewBag.TipoContratoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO), "IdListaValor", "Valor");
            ViewBag.IdEmpresaLocacion = new SelectList(lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa), "IdEmpresaLocacion", "NombreLocacion"); 
            ViewBag.RecibeCorreosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_OFERTA_RECIBECORREOS), "IdListaValor", "Valor");

            return View(oferta);
        }

        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]        
        //public ActionResult NuevaOferta([Bind(Include = "CreadorPor")] Oferta oferta)
        public ActionResult NuevaOferta(Oferta oferta)
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            if (ModelState.IsValid)
            {
                oferta.UsuarioPropietarioEmpresa = ticket.Usuario;
                oferta.EstadoOferta = "OFERPR"; //Estado pendiente de activación.
                //oferta.FechaPublicacion = DateTime.Now;
                //oferta.FechaFinProceso = DateTime.Now.AddDays(10);
                //oferta.IdEmpresaLocacion = 1; //TODO: Reemplazar por combo.
                //oferta.DescripcionOferta = "descripcion de la oferta";
                //oferta.TipoTrabajo = "OFTTTC"; //Tipo de trabajo: Tiempo completo.    
                //oferta.TipoContrato = "";
                //oferta.TipoCargo = "";
                //oferta.Horario = "";
                //oferta.AreaEmpresa = "";
                oferta.CreadoPor = ticket.Usuario;

                lnOfertaEmpresa.Insertar(oferta);

                //Mostrar alerta de éxito.

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
                empresa.EstadoEmpresa.Valor = Constantes.ESTADO_EMPRESA_PENDIENTE_APROBACION;
                empresa.SectorEmpresarial.Valor = "SETECN"; //Data de prueba.
                empresa.DescripcionEmpresa = "";
                empresa.LinkVideo = "";
                empresa.AnoCreacion = 2014;
                empresa.NumeroEmpleados.Valor = "";
                empresa.SectorEmpresarial2.Valor = "";
                empresa.SectorEmpresarial3.Valor = "";

                empresa.CreadoPor = "admin";

                lnEmpresa.Insertar(empresa);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Administrar()
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //LNEmpresa lnEmpresa = new LNEmpresa();
            //var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(ticket.IdEmpresa);

            ViewBag.IdEmpresa = ticket.IdEmpresa;

            return View();
        }

        public ActionResult ObtenerNuevasPostulaciones()
        {
            Ticket ticket = (Ticket)Session["Ticket"];

            LNOferta lnOferta = new LNOferta();
            List<VistaNuevasPostulaciones> lista = new List<VistaNuevasPostulaciones>();

            DataTable dtResultado = lnOferta.ObtenerPostulacionesPorEmpresa(ticket.IdEmpresa);
            
            //Se realiza el FOR en esta porque se está utilizando una vista modelo de presentación.
            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaNuevasPostulaciones vista = new VistaNuevasPostulaciones();
                vista.CargaOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                vista.FechaPostulacion = Convert.ToDateTime(fila["FechaPostulacion"]);
                vista.AlumnoNombres = Convert.ToString(fila["AlumnoNombres"]);
                vista.AlumnoApellidos = Convert.ToString(fila["AlumnoApellidos"]);
                vista.Cumplimiento = Convert.ToInt32(fila["Cumplimiento"]);

                lista.Add(vista);
            }

            return PartialView("_VistaNuevasPostulaciones", lista);
        }

        public ActionResult ObtenerNuevosMensajes()
        {
            Ticket ticket = (Ticket)Session["Ticket"];

            LNMensaje lnMensaje = new LNMensaje ();
            //Se envía 0 para que obtener los mensajes de todas las ofertas.
            List<Mensaje> lista = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticket.IdEmpresa, 0);

            return PartialView("_VistaNuevosMensajes", lista);
        }

        #region Vistas parciales de la Oferta

        public ActionResult VistaOfertaAnuncio(Oferta oferta)
        {

            return PartialView("_VistaOfertaAnuncio", oferta);
        }

        public ActionResult VistaOfertaPostulantes(Oferta oferta)
        {
            LNOferta lnOferta = new LNOferta ();
            List<OfertaPostulante> postulantes = lnOferta.ObtenerPostulantesPorIdOferta(oferta.IdOferta);
            
            return PartialView("_VistaOfertaPostulantes", postulantes);
        }

        public ActionResult VistaOfertaCondiciones(Oferta oferta)
        {

            return PartialView("_VistaOfertaCondiciones", oferta);
        }

        public ActionResult VistaOfertaRequisitos(Oferta oferta)
        {

            return PartialView("_VistaOfertaRequisitos", oferta);
        }

        public ActionResult VistaOfertaMensajes(Oferta oferta)
        {
            Ticket ticket = (Ticket)Session["Ticket"];

            LNMensaje lnMensaje = new LNMensaje();
            //Se envía 0 para que obtener los mensajes de todas las ofertas.
            List<Mensaje> mensajes = lnMensaje.ObtenerPorIdEmpresaIdOferta(ticket.IdEmpresa, oferta.IdOferta);

            return PartialView("_VistaOfertaMensajes", mensajes);
        }
        #endregion

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult EditarEmpresa(int id)
        {
            //Se obtiene la información de la BD
            var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(id);

            //Se envían los valores al HTML.
            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor", empresa.PaisIdListaValor);
            ViewBag.NumeroEmpleadosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NRO_EMPLEADOS), "IdListaValor", "Valor", empresa.NumeroEmpleadosIdListaValor);
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial1IdListaValor);
            ViewBag.SectorEmpresarial2IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial2IdListaValor);
            ViewBag.SectorEmpresarial3IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial3IdListaValor);
            
            return PartialView("_EditarEmpresa", empresa);
        }

        [ValidateAntiForgeryToken] // this action takes the viewModel from the modal
        public PartialViewResult EditarEmpresa(Empresa empresa) //Este es como el submit
        {  
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                empresa.ModificadoPor = ticket.Usuario;
                lnEmpresa.Actualizar(empresa);

                empresa = lnEmpresa.ObtenerDatosEmpresaPorId(empresa.IdEmpresa);

                return PartialView("_AdministrarDatosGenerales", empresa);

                //return RedirectToAction("Administrar");
            }

            return PartialView("_EditarEmpresa", empresa);
        }

        public PartialViewResult _AdministrarDatosGenerales(int idEmpresa)
        {
            var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            return PartialView("_AdministrarDatosGenerales", empresa);
        }

        public PartialViewResult _AdministrarUbicaciones(int idEmpresa)
        {
            var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            return PartialView("_AdministrarUbicaciones", empresa.Locaciones);
        }

        public PartialViewResult _AdministrarUsuarios(int idEmpresa)
        {
            var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            return PartialView("_AdministrarUsuarios", empresa.Usuarios);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _AdministrarNuevaUbicacion()
        {            
            return PartialView("_AdministrarNuevaUbicacion");
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _AdministrarNuevoUsuario()
        {
            return PartialView("_AdministrarNuevoUsuario");
        }

        [ValidateAntiForgeryToken] 
        public PartialViewResult _AdministrarNuevaUbicacion(EmpresaLocacion empresaLocacion) //Acá llega el submit
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                empresaLocacion.IdEmpresa = ticket.IdEmpresa;
                empresaLocacion.TipoLocacion.IdListaValor = "TIPO123";
                empresaLocacion.NombreLocacion = "Nueva " + DateTime.Now.ToString();
                empresaLocacion.CorreoElectronico = "correo " + DateTime.Now.ToString();
                empresaLocacion.Direccion = "" + DateTime.Now.ToString();
                empresaLocacion.TelefonoFijo = "" + DateTime.Now.ToString();
                empresaLocacion.DireccionDistrito = "" + DateTime.Now.ToString(); ;
                empresaLocacion.DireccionCiudad = "" + DateTime.Now.ToString(); ;
                empresaLocacion.DireccionRegion = "" + DateTime.Now.ToString(); ;
                empresaLocacion.EstadoLocacion.IdListaValor = "EST123";

                LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion ();
                lnEmpresaLocacion.Insertar(empresaLocacion, usuarioCreacion);

                var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(ticket.IdEmpresa);

                return PartialView("_AdministrarUbicaciones", empresa.Locaciones);
                
            }

            return PartialView("_AdministrarNuevaUbicacion", empresaLocacion);
        }

        [ValidateAntiForgeryToken] 
        public PartialViewResult _AdministrarNuevoUsuario(EmpresaUsuario empresaUsuario) 
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                empresaUsuario.Empresa.IdEmpresa = ticket.IdEmpresa;
                empresaUsuario.Usuario.NombreUsuario = "nombreusuario";
                empresaUsuario.Nombres = "Aldo";
                empresaUsuario.Apellidos = "Chocos";
                empresaUsuario.TipoDocumento.IdListaValor = "123";
                empresaUsuario.NumeroDocumento = "";
                empresaUsuario.Sexo.IdListaValor = "M";
                empresaUsuario.EmpresaLocacion.IdEmpresaLocacion = 1;
                empresaUsuario.CorreoElectronico = "";
                empresaUsuario.TelefonoFijo = "";
                empresaUsuario.TelefonoCelular = "";
                empresaUsuario.TelefonoAnexo = "";

                LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                lnEmpresaUsuario.Insertar(empresaUsuario, usuarioCreacion);

                var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(ticket.IdEmpresa);

                return PartialView("_AdministrarUsuarios", empresa.Usuarios);
            }

            return PartialView("_AdministrarNuevoUsuario", empresaUsuario);
        }
    }
}