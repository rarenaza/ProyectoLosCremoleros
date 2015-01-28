using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTPPrototipo.Common;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Models.ViewModels.Empresa;

namespace UTPPrototipo.Controllers
{
    [VerificarSesion]

    public class EmpresaController : Controller
    {
        LNEmpresa lnEmpresa = new LNEmpresa();
        LNOferta lnOferta = new LNOferta();
        LNOfertaEmpresa lnOfertaEmpresa = new LNOfertaEmpresa();
        LNAlumnoCV lnAlumnocv = new LNAlumnoCV();

        LNGeneral lnGeneral = new LNGeneral();
        public string usuarioEmpresa = "82727128";

        //
        // GET: /Empresa/
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ListarCargo(string query)
        {

            var resultado = lnEmpresa.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarTipoEstudio(string query)
        {

            var resultado = lnEmpresa.ObtenerListaValor(Constantes.IDLISTA_TIPO_DE_ESTUDIO);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarEstudio(string query)
        {
            LNGeneral lngeneral = new LNGeneral();
            var resultado = lngeneral.ObtenerListaValor(Constantes.IDLISTA_DE_CARRERA);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarDistritos(string query)
        {
            LNGeneral lngeneral = new LNGeneral();
            var resultado = lngeneral.ObtenerListaValor(Constantes.IDLISTA_DISTRITO_PERU);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }







        [VerificarSesion]        
        public ActionResult Publicacion(string filtroBusqueda)
        {
            //Se obtiene los datos de la sesion.
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            int idEmpresa = ticket.IdEmpresa;
            string rolIdListaValor = ticket.Rol;

            string filtro = filtroBusqueda == null ? "" : filtroBusqueda;

            //List<VistaOfertaEmpresa> lista = lnOferta.Obtener_PanelEmpresa(idEmpresa, filtro, rolIdListaValor, ticket.Usuario);
            ViewBag.NroPaginaActual = 1;

            return View();
        }


        public ActionResult Oferta(int id)
        {
            int idOferta = id;            

            Oferta oferta = lnOferta.ObtenerPorId(idOferta);

            

            return View(oferta);
        }

        
        public ActionResult EditarOferta(int id)
        {
            int idOferta = id;

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
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            if (ModelState.IsValid)
            {
                //oferta.UsuarioPropietarioEmpresa = "";
                oferta.ModificadoPor = ticket.Usuario;

                lnOfertaEmpresa.Actualizar(oferta);

                //1. Mostrar mensaje de éxito.

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

            ViewBag.TipoCargoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO), "IdListaValor", "Valor", oferta.TipoCargoIdListaValor);
            ViewBag.TipoTrabajoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO), "IdListaValor", "Valor", oferta.TipoTrabajoIdListaValor);
            ViewBag.TipoContratoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO), "IdListaValor", "Valor", oferta.TipoContratoIdListaValor);
            ViewBag.IdEmpresaLocacion = new SelectList(lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa), "IdEmpresaLocacion", "NombreLocacion", oferta.IdEmpresaLocacion);
            ViewBag.RecibeCorreosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_OFERTA_RECIBECORREOS), "IdListaValor", "Valor", oferta.RecibeCorreosIdListaValor);

            return View(oferta);
        }

        [AutorizarEmpresa(Rol = "ROLEAD,ROLEUS")]
        public ActionResult NuevaOferta()
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //Se envían datos de prueba.
            Oferta oferta = new Oferta();
            oferta.IdEmpresa = ticket.IdEmpresa;
            oferta.UsuarioPropietarioEmpresa = "";            
            oferta.FechaPublicacion = DateTime.Now;                        
            oferta.CreadoPor = ticket.Usuario;
            oferta.FechaFinRecepcionCV = DateTime.Now; //Se establece la fecha actual para la nueva oferta.

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
                oferta.UsuarioPropietarioEmpresa = ticket.Usuario; //Se guarda el usuario asignado.
                oferta.EstadoOferta = "OFERCO"; //Estado oferta en construcción.
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

                int idOfertaGenerado = lnOfertaEmpresa.Insertar(oferta);

                if (idOfertaGenerado > 0)
                {
                    //1. Se completa el mensaje de éxito.
                    TempData["MsjExitoCrearOferta"] = "La oferta ha sido creada con éxito.";

                    //2. Se redirecciona a la nueva oferta.
                    return RedirectToAction("EditarOferta", new { id = idOfertaGenerado });
                }
                else
                {
                    //Mostrar un mensaje de error.
                }

                return RedirectToAction("Publicacion");
            }

            //Si existe error, se debe cargar nuevamente la información.
            LNGeneral lnGeneral = new LNGeneral();
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();

            //ViewBag.ListaTipoCargo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO);
            //ViewBag.ListaTipoTrabajo = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO);
            //ViewBag.ListaTipoContrato = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO);
            //ViewBag.ListaLocaciones = lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa);

            //Se completan las listas:

            ViewBag.TipoCargoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CARGO), "IdListaValor", "Valor", oferta.TipoCargoIdListaValor);
            ViewBag.TipoTrabajoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_TRABAJO), "IdListaValor", "Valor", oferta.TipoTrabajoIdListaValor);
            ViewBag.TipoContratoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_CONTRATO), "IdListaValor", "Valor", oferta.TipoContratoIdListaValor);
            ViewBag.IdEmpresaLocacion = new SelectList(lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa), "IdEmpresaLocacion", "NombreLocacion", oferta.IdEmpresaLocacion);
            ViewBag.RecibeCorreosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_OFERTA_RECIBECORREOS), "IdListaValor", "Valor", oferta.RecibeCorreosIdListaValor);


            return View(oferta);
        }


        public ActionResult Hunting()
        {
                       
            VistaAlumnoHunting utp = new VistaAlumnoHunting();

            LNGeneral lngeneral = new LNGeneral();


            //Tipo de Estudio

            //Busca Lista Tipo de Estudio
            utp.ListaTipoEstudio = lngeneral.ObtenerListaValor(7);
            List<SelectListItem> listItemsTipoEstudio = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.ListaTipoEstudio)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoEstudio.Add(item);
            }

            //Estado del estudio

            //Busca Lista Estado del estudio
            utp.ListaEstadoEstudio = lngeneral.ObtenerListaValor(43);
            List<SelectListItem> listItemEstadodelestudio = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.ListaEstadoEstudio)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemEstadodelestudio.Add(item);
            }

            //Sector empresarial

            //Busca Lista Sector Empresarial
            utp.ListaSectorEmpresarial = lngeneral.ObtenerListaValor(8);
            List<SelectListItem> listItemSector = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.ListaSectorEmpresarial)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemSector.Add(item);
            }

            //Tipo Cargo

            //Busca Lista Tipo Cargo
            utp.ListaTipoCargo = lngeneral.ObtenerListaValor(9);
            List<SelectListItem> listItemTipoCargo = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.ListaTipoCargo)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemTipoCargo.Add(item);
            }

            //Tipo Informacion Adicional

            //Busca Lista Tipo informacion Adicional
            utp.ListaInformacionAdicional = lngeneral.ObtenerListaValor(10);
            List<SelectListItem> listItemTipoInformacionAdicional = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.ListaInformacionAdicional)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemTipoInformacionAdicional.Add(item);
            }

            //Lista de Combos

            ViewBag.ListaTipoEstudio = listItemsTipoEstudio;
            ViewBag.ListaEstadodelestudio = listItemEstadodelestudio;
            ViewBag.ListaSectorEmpresarial = listItemSector;
            ViewBag.ListaTipoCargo = listItemTipoCargo;
            ViewBag.ListaTipoInformacionAdicional = listItemTipoInformacionAdicional;
            return View(utp);                 
           
        }


        public ActionResult BusquedaSimpleHunting(VistaAlumnoHunting entidad)
        {            
            LNGeneral lngeneral = new LNGeneral();
            //entidad.ListaBusqueda = lngeneral.EmpresaHuntingBuscarSimple(entidad.PalabraClave == null ? "" : entidad.PalabraClave);

            List<Hunting> lista = lngeneral.EmpresaHuntingBuscarSimple(entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.NroPagina, Constantes.FILAS_POR_PAGINA); //1 para demo.

            //Datos para la paginación.
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.NroPagina;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            ViewBag.TipoPaginacion = "Simple";

            return PartialView("_ResultadoBusquedaHunting", lista);

        }

        public ActionResult BusquedaAvanzadaHunting(VistaAlumnoHunting entidad)
        {
            LNGeneral lngeneral = new LNGeneral();
            //entidad.ListaBusqueda = lngeneral.EmpresaHuntingBuscarSimple(entidad.PalabraClave == null ? "" : entidad.PalabraClave);

            List<Hunting> lista = lngeneral.EmpresaHuntingBuscarAvanzada(entidad.IdTipoEstudio == null ? "" : entidad.IdTipoEstudio,
                entidad.Estudios == null ? "" : entidad.Estudios,
                entidad.IdEstadoEstudio == null ? "" : entidad.IdEstadoEstudio,
                entidad.IdSectorEmpresarial == null ? "" : entidad.IdSectorEmpresarial,
                entidad.AnosExperiencia,
                entidad.NombreCargo == null ? "" : entidad.NombreCargo,
                entidad.IdInformacionAdicional == null ? "" : entidad.IdInformacionAdicional,
                entidad.Conocimiento == null ? "" : entidad.Conocimiento,
                entidad.Distrito == null ? "" : entidad.Distrito,
                entidad.NroPagina, Constantes.FILAS_POR_PAGINA);

            //Datos para la paginación.
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.NroPagina;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            ViewBag.TipoPaginacion = "Avanzada";

            return PartialView("_ResultadoBusquedaHunting", lista);

        }


        public ActionResult Eventos()
        {
            return View();
        }
        public ActionResult Evento(int idEvento)
        {
            return View();
        }

        public ActionResult VistaCabecera(string estiloPanel)
        {            
            VistaPanelCabecera panel = new VistaPanelCabecera();
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            ViewBag.IdEmpresa = ticket.IdEmpresa;
            ViewBag.EstiloPanel = estiloPanel;

            //Se cargan los datos del empresaUsuario autenticado:
            panel = lnEmpresa.ObtenerPanelCabecera(ticket.Usuario);

            return PartialView("_DatosUsuario", panel);
        }

        public ActionResult Postulante(int? id)
        {
            if (id != null)
            {
            VistaOfertaPostulante vistaofertapostulante = lnAlumnocv.ObtenerPostulanteCV((int)id);
            
            //Se cargan las fases de la oferta.
            LNOferta lnOferta = new LNOferta();
            List<OfertaFase> listaFasesActivas = lnOferta.Obtener_OfertaFaseActivas(vistaofertapostulante.alumnocv.IdOferta);
            ViewBag.IdOfertaFase = new SelectList(listaFasesActivas, "IdListaValor", "FaseOferta", vistaofertapostulante.alumnocv.FaseOferta);
            
            ViewBag.IdOfertaPostulante = Convert.ToInt32(id);

            return View(vistaofertapostulante);
            }
            else
            {
                return RedirectToAction("Index", "Empresa");
            }
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

                //lnEmpresa.Insertar(empresa);
            }

            return RedirectToAction("Index", "Home");
        }

        [AutorizarEmpresa(Rol = "ROLEAD")] //Rol Administrador.        
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
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

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
                vista.IdOfertaPostulante = Convert.ToInt32(fila["IdOfertaPostulante"]);
                vista.IdOferta = Convert.ToInt32(fila["IdOferta"]);
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

        public ActionResult VistaOfertaPostulantes(int id, string estado, string columna, string orden)
        {
            LNOferta lnOferta = new LNOferta ();

            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();
            if (columna == "Fecha")
            {
                if (orden == "ASC")
                {                    
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderBy(m => m.FechaPostulacion).ToList();
                }
                else
                {
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderByDescending(m => m.FechaPostulacion).ToList();
                }
            }
            if (columna == "Nombre")
            {
                if (orden == "ASC")
                {                    
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderBy(m => m.Alumno.Apellidos).ToList();
                }
                else
                {
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderByDescending(m => m.Alumno.Apellidos).ToList();
                }
            }
            if (columna == "Fase")
            {
                if (orden == "ASC")
                {                  
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderBy(m => m.FaseOferta.Peso).ToList();
                }
                else
                {
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderByDescending(m => m.FaseOferta.Peso).ToList();
                }
            }
            if (columna == "Cumplimiento")
            {
                if (orden == "ASC")
                {                 
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderBy(m => m.NivelDeMatch).ToList();
                }
                else
                {
                    postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderByDescending(m => m.NivelDeMatch).ToList();
                }
            }

           if (postulantes.Count == 0)
            {
                postulantes = lnOferta.ObtenerPostulantesPorIdOferta(id).OrderBy(m => m.FechaPostulacion).ToList();
            }
                        
            //Llenar el combo de Fases:
            List<OfertaFase> listaFasesActivas = lnOferta.Obtener_OfertaFaseActivas(id);

            ViewBag.IdOfertaFase = new SelectList(listaFasesActivas, "IdListaValor", "FaseOferta");
            ViewBag.EstadoOfertaIdListaValor = estado;

            //Se envían datos para mostrar el orden de la grilla. Se valida si es NULL para la primera vez que cargue el bloque.
            ViewBag.Columna = columna == null ? "Fecha" : columna;
            ViewBag.Orden = orden == null ? "ASC" : orden;

            return PartialView("_VistaOfertaPostulantes", postulantes);
        }

        public ActionResult VistaOfertaCondiciones(Oferta oferta)
        {            
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //Se obtienen los usuarios de la empresa con roles Administrador, Supervisor y Usuario.
            LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
            List<VistaEmpresaUsuario> lista = lnEmpresaUsuario.ObtenerUsuariosActivosYPorRolesPorIdEmpresa(ticket.IdEmpresa);

            ViewBag.UsuarioPropietarioEmpresa = new SelectList(lista, "NombreUsuario", "NombreCompletoUsuario", oferta.UsuarioPropietarioEmpresa);

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

            //Se ponen valores a estos datos para que el IsValid pase. No son usados en la actualización de la empresa.
            empresa.EstadoIdListaValor = "..";
            empresa.NivelDeFacturacion = 0;
            empresa.UsuarioEC = "..";

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
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                int a = 0;
            }

            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor", empresa.PaisIdListaValor);
            ViewBag.NumeroEmpleadosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NRO_EMPLEADOS), "IdListaValor", "Valor", empresa.NumeroEmpleadosIdListaValor);
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial1IdListaValor);
            ViewBag.SectorEmpresarial2IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial2IdListaValor);
            ViewBag.SectorEmpresarial3IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial3IdListaValor);
            
            return PartialView("_EditarEmpresa", empresa);
        }

        public PartialViewResult _AdministrarDatosGenerales(int idEmpresa)
        {
            var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            return PartialView("_AdministrarDatosGenerales", empresa);
        }


        public PartialViewResult _AdministrarImagen()
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            EmpresaVista  empresa = new EmpresaVista();
            

            //DataTable dtResultado = lnEmpresa.Empresa_Elejir_Imagen(1);

            DataTable dtResultado = lnEmpresa.Empresa_Elegir_Imagen(ticket.IdEmpresa);

            if (dtResultado.Rows.Count > 0)
            {
                empresa.IdEmpresa               = Convert.ToInt32(dtResultado.Rows[0]["IdEmpresa"]);
                empresa.NombreComercial         = Convert.ToString(dtResultado.Rows[0]["NombreComercial"]);
                empresa.RazonSocial             = Convert.ToString(dtResultado.Rows[0]["RazonSocial"]);
                empresa.Pais.Valor              = Convert.ToString(dtResultado.Rows[0]["Pais"]);
                empresa.IdentificadorTributario = Convert.ToString(dtResultado.Rows[0]["IdentificadorTributario"]);
                empresa.DescripcionEmpresa      = Convert.ToString(dtResultado.Rows[0]["DescripcionEmpresa"]);
                empresa.AnoCreacion = Convert.ToInt32(dtResultado.Rows[0]["AnoCreacion"] == DBNull.Value ? null : dtResultado.Rows[0]["AnoCreacion"]);
                empresa.NumeroEmpleados.Valor   = Convert.ToString(dtResultado.Rows[0]["NumeroEmpleados"]);
                empresa.SectorEmpresarial.Valor = Convert.ToString(dtResultado.Rows[0]["SectorEmpresarial"]);
                empresa.ArchivoNombreOriginal   = Convert.ToString(dtResultado.Rows[0]["ArchivoNombreOriginal"]);            
            }

            return PartialView(empresa);           
        
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult _AdministrarActualizarImagen(EmpresaVista empresaHTML)
        {

            Empresa empresa = new Empresa();

            if (empresaHTML.LogoEmpresaHtml != null)
            {

                byte[] uploadedFile = new byte[empresaHTML.LogoEmpresaHtml.InputStream.Length];
                empresaHTML.LogoEmpresaHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(empresaHTML.LogoEmpresaHtml.InputStream.Length));
                empresaHTML.ArchivoNombreOriginal = empresaHTML.LogoEmpresaHtml.FileName;
                empresaHTML.ArchivoMimeType = empresaHTML.LogoEmpresaHtml.ContentType;
                empresaHTML.LogoEmpresa = uploadedFile;

                empresa.ArchivoNombreOriginal = empresaHTML.ArchivoNombreOriginal;

            }

            empresa.LogoEmpresa = empresaHTML.LogoEmpresa;
            empresa.ArchivoMimeType = empresaHTML.ArchivoMimeType;
            empresa.ArchivoNombreOriginal = empresaHTML.ArchivoNombreOriginal;

            //TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            //empresa.IdEmpresa = ticket.IdEmpresa;

            empresa.IdEmpresa = empresaHTML.IdEmpresa;

            //if (ModelState.IsValid)
            //{

            if (lnEmpresa.Empresa_Actualizar_imagen(empresa) == true)
            {
                ViewBag.Message = "Datos Actualizado";
                return RedirectToAction("Administrar","Empresa");

                
                //return PartialView("_AdministrarImagen", empresaHTML);
               
            }
            else
            {

                ViewBag.Message = "Error al Actualizar";
                return View(empresaHTML);
            }

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
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            
            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor");
            ViewBag.EstadoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_LOCACION), "IdListaValor", "Valor");

            return PartialView("_AdministrarNuevaUbicacion");
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _AdministrarUbicacionEditar(int id)
        {            
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();
            EmpresaLocacion empresaLocacion = lnEmpresaLocacion.ObtenerLocacionPorId(id);

            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor", empresaLocacion.TipoLocacionIdListaValor);
            ViewBag.EstadoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_LOCACION), "IdListaValor", "Valor", empresaLocacion.EstadoLocacionIdListaValor);

            return PartialView("_AdministrarUbicacionEditar", empresaLocacion);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _AdministrarNuevoUsuario()
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];                

            EmpresaUsuario empresaUsuario = new EmpresaUsuario();
            
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();

            ViewBag.IdEmpresaLocacion = new SelectList(lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa), "IdEmpresaLocacion", "NombreLocacion");
            ViewBag.SexoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SEXO), "IdListaValor", "Valor");
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor");
            
            //Obtiene todos registros que contengan la palabra "empresa".
            ViewBag.RolIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ROL_USUARIO, "ROLE"), "IdListaValor", "Valor");

            ViewBag.EstadoUsuarioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_USUARIO, "USEM"), "IdListaValor", "Valor");
            ViewBag.IdEmpresa = ticket.IdEmpresa;

            return PartialView("_AdministrarNuevoUsuario", empresaUsuario);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _AdministrarUsuarioEditar(int id)
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
            EmpresaUsuario empresaUsuario = lnEmpresaUsuario.ObtenerPorIdEmpresaUsuario(id);

            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();

            ViewBag.IdEmpresaLocacion = new SelectList(lnEmpresaLocacion.ObtenerLocaciones(ticket.IdEmpresa), "IdEmpresaLocacion", "NombreLocacion", empresaUsuario.IdEmpresaLocacion);
            ViewBag.SexoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SEXO), "IdListaValor", "Valor", empresaUsuario.SexoIdListaValor);
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor", empresaUsuario.TipoDocumentoIdListaValor);
            ViewBag.RolIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ROL_USUARIO, "ROLE"), "IdListaValor", "Valor", empresaUsuario.RolIdListaValor);
            ViewBag.EstadoUsuarioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_USUARIO, "USEM"), "IdListaValor", "Valor", empresaUsuario.EstadoUsuarioIdListaValor);

            ViewBag.IdEmpresa = ticket.IdEmpresa;

            //Se devuelve la lista parcial con el usuario.
            return PartialView("_AdministrarUsuarioEditar", empresaUsuario);
        }

        [ValidateAntiForgeryToken] 
        public PartialViewResult _AdministrarNuevaUbicacion(EmpresaLocacion empresaLocacion)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];                
                empresaLocacion.IdEmpresa = ticket.IdEmpresa;
                empresaLocacion.CreadoPor = ticket.Usuario;
                
                LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion ();
                lnEmpresaLocacion.Insertar(empresaLocacion);

                var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(ticket.IdEmpresa);

                return PartialView("_AdministrarUbicaciones", empresa.Locaciones);
                
            }

            return PartialView("_AdministrarNuevaUbicacion", empresaLocacion);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _AdministrarUbicacionEditar(EmpresaLocacion empresaLocacion)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                empresaLocacion.IdEmpresa = ticket.IdEmpresa;
                empresaLocacion.ModificadoPor = ticket.Usuario;

                LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();
                lnEmpresaLocacion.Actualizar(empresaLocacion);

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
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];                

                empresaUsuario.Empresa.IdEmpresa = ticket.IdEmpresa;
                empresaUsuario.CreadoPor = ticket.Usuario;         

                LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                lnEmpresaUsuario.Insertar(empresaUsuario);

                //Se obtienen los usuarios desde la BD.
                var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(ticket.IdEmpresa);

                return PartialView("_AdministrarUsuarios", empresa.Usuarios);
            }

            return PartialView("_AdministrarNuevoUsuario", empresaUsuario);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _AdministrarUsuarioEditar(EmpresaUsuario empresaUsuario)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                empresaUsuario.Empresa.IdEmpresa = ticket.IdEmpresa;
                empresaUsuario.ModificadoPor = ticket.Usuario;

                LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                lnEmpresaUsuario.Actualizar(empresaUsuario);

                //Se obtienen los usuarios desde la BD.
                var empresa = lnEmpresa.ObtenerDatosEmpresaPorId(ticket.IdEmpresa);

                return PartialView("_AdministrarUsuarios", empresa.Usuarios);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                int a = 0;
            }
            return PartialView("_AdministrarUsuarioEditar", empresaUsuario);
        }

        public PartialViewResult ObtenerOfertaFase(int idOferta)
        {
            //Se guarda el Id de la oferta
            ViewBag.IdOferta = idOferta;

            List<OfertaFase> lista = lnOferta.Obtener_OfertaFase(idOferta);

            return PartialView("_OfertaFase", lista);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken] // this action takes the viewModel from the modal
        public PartialViewResult _OfertaFaseEditar(List<OfertaFase> listaOfertaFase) //Este es como el submit
        {
            //OfertaFase nuevo = new OfertaFase();
            //listaOfertaFase.ListaFasesDeLaOferta.Add(nuevo);
            //List<OfertaFase> lista = (List<OfertaFase>)listaOfertaFase;

            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            foreach (var item in listaOfertaFase)
            {
                //Estos 3 registros siempre están activos.
                if (item.IdListaValor == "OFFAPR" || item.IdListaValor == "OFFACV" || item.IdListaValor == "OFFAFI")
                {
                    item.Incluir = true;
                }

                item.ModificadoPor = ticket.Usuario;
            }

            lnOferta.ActualizarOfertaFase(listaOfertaFase);
      
            //return PartialView("_OfertaFase", lista);
            return PartialView("_OfertaFase", listaOfertaFase);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken] // this action takes the viewModel from the modal
        public PartialViewResult _OfertaPostulanteMoverDeFase(List<OfertaPostulante> listaOfertaPostulante, string IdOfertaFase) //Este es como el submit
        {
            //Se establece el campo ModificadoPor
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            foreach (var item in listaOfertaPostulante)
            {
                item.ModificadoPor = ticket.Usuario;
            }

            //Se actualiza los datos del postulante.
            lnOferta.ActualizarFaseDePostulantes(listaOfertaPostulante, IdOfertaFase);

            //Se cargan los datos de la BD:
            int idOferta = listaOfertaPostulante[0].IdOferta; //Se obtiene el idOferta del primero de la lista

            List<OfertaPostulante> postulantes = lnOferta.ObtenerPostulantesPorIdOferta(idOferta);

            //Llenar el combo de Fases:
            List<OfertaFase> listaFasesActivas = lnOferta.Obtener_OfertaFaseActivas(idOferta);

            ViewBag.IdOfertaFase = new SelectList(listaFasesActivas, "IdListaValor", "FaseOferta");

            //return PartialView("_OfertaFase", lista);
            return PartialView("_VistaOfertaPostulantes", postulantes);
        }


        public FileResult ObtenerImagenEmpresa(int id)
        {
            const string alternativePicturePath = @"/img/sinimagen.jpg";
               

            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            EmpresaVista empresa = new EmpresaVista();


            //DataTable dtResultado = lnEmpresa.Empresa_Elejir_Imagen(1);


            DataTable dtResultado = lnEmpresa.Empresa_Elegir_Imagen(ticket.IdEmpresa);
           
            if (dtResultado.Rows.Count > 0)
            {
                empresa.LogoEmpresa = dtResultado.Rows[0]["LogoEmpresa"] == DBNull.Value ? null : (byte[])dtResultado.Rows[0]["LogoEmpresa"];
              

            }


            //Contenido producto = lista.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (empresa  != null && empresa.LogoEmpresa != null)
            {
                stream = new MemoryStream(empresa.LogoEmpresa);

            }
            else
            {
                
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }


        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
            Session["TicketEmpresa"] = null;
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _AdministrarUsuarioEditarTMP(EmpresaUsuario empresaUsuario)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

                empresaUsuario.Empresa.IdEmpresa = ticket.IdEmpresa;
                empresaUsuario.ModificadoPor = ticket.Usuario;

                LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                lnEmpresaUsuario.Actualizar(empresaUsuario);

                //Se crea una variable temporal para mostra el mensaje:
                TempData["_AdministrarUsuarioEditarTMP"] = "Los datos se modificaron con éxito.";

                VistaPanelCabecera panel = new VistaPanelCabecera();
               
                ViewBag.IdEmpresa = ticket.IdEmpresa;
                //Se cargan los datos del empresaUsuario autenticado:
                panel = lnEmpresa.ObtenerPanelCabecera(ticket.Usuario);

                return PartialView("_DatosUsuario", panel);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                int a = 0;
            }
            return PartialView("_AdministrarUsuarioEditar", empresaUsuario);
        }

        public ActionResult BuscarOfertas(string palabraClave, int nroPaginaActual = 1, int filasPorPagina = Constantes.FILAS_POR_PAGINA)
        {
            //Se obtiene los datos de la sesion.
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
            int idEmpresa = ticket.IdEmpresa;
            string rolIdListaValor = ticket.Rol;

            string filtro = palabraClave == null ? "" : palabraClave;

            //Al metodo normal se le añaden dos parámetros: nroPaginaActual y filasPorPagina
            List<VistaOfertaEmpresa> lista = lnOferta.Obtener_PanelEmpresa(idEmpresa, filtro, rolIdListaValor, ticket.Usuario, nroPaginaActual, filasPorPagina);

            //Datos para la paginación.
            //Una ves traido la info de la bd, se llenan estos campos del objeto Paginacion
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            //Se devuelve una vista parcial con los resultados.
            return PartialView("_ListaOfertas",lista);
        }

        [HttpGet]
        public ActionResult ValidarNroDocumento(string nroDocumento, int idEmpresa, int idEmpresaUsuario)
        {
            LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario ();

            int cantidad = lnEmpresaUsuario.ValidarNumeroDocumento(idEmpresa, nroDocumento, idEmpresaUsuario);

            return Json(cantidad, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ValidarUsuario(string usuario, int idEmpresa, int idEmpresaUsuario)
        {
            LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();

            int cantidad = lnEmpresaUsuario.ValidarUsuario(idEmpresa, usuario, idEmpresaUsuario);

            return Json(cantidad, JsonRequestBehavior.AllowGet);
        }
    }
}