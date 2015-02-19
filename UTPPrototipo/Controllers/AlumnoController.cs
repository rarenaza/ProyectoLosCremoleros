using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTPPrototipo.Models.ViewModels;
using UTPPrototipo.Common;
using System.IO;
using UTPPrototipo.Models.ViewModels.Cuenta;
using System.Web.Security;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
using UTPPrototipo.Utiles;

namespace UTPPrototipo.Controllers
{
    [VerificarSesion, LogPortal]
    public class AlumnoController : Controller
    {
        LNAlumno lnAlumno = new LNAlumno();
        LNAlumnoEstudio lnAlumnoEstudio = new LNAlumnoEstudio();
        LNAlumnoCV lnAlumnoCV = new LNAlumnoCV();
        LNAlumnoCVEstudio lnAlumnoCVEstudio = new LNAlumnoCVEstudio();
        LNPlantillaCV lnPlantillaCV = new LNPlantillaCV();
        LNOferta lnoferta = new LNOferta();
        LNOfertaPostulante lnofertapostulante = new LNOfertaPostulante();
        LNEmpresa lnempresa = new LNEmpresa();
        LNAlumnoExperienciaCargo lnalumnoexperienciacargo = new LNAlumnoExperienciaCargo();
        LNAlumnoExperiencia lnalumnoexperiencia = new LNAlumnoExperiencia();


        LNAlumnoCVExperienciaCargo lnalumnocvexperienciacargo = new LNAlumnoCVExperienciaCargo();
        LNAlumnoInformacionAdicional lnalumnoinformacionadicional = new LNAlumnoInformacionAdicional();
        LNAlumnoCVInformacionAdicional lnalumnocvinformacionadicional = new LNAlumnoCVInformacionAdicional();



        public ActionResult Index()
        {



            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(ticket.CodAlumnoUTP);


            return View(panel);
        }
        //public ActionResult Postulacion() 
        //{
        //    VistaPanelAlumnoPostulaciones panel = lnAlumno.ObtenerPanelPostulaciones(codigoAlumno);

        //    return View(panel);
        //}

        public ActionResult Postulacion()
        {
            //List<VistaPostulacionAlumno> lista = new List<VistaPostulacionAlumno>();

            //lista = lnoferta.ObtenerPostulantes();

            //return View(lista);
            return View();
        }

        public FileContentResult GetImageEmpresa(int IdEmpresa)
        {
            Empresa empresa = lnempresa.ObtenerDetalleEmpresaPorId(IdEmpresa);
            if (empresa.LogoEmpresa != null && string.IsNullOrEmpty(empresa.ArchivoMimeType) == false) return File(empresa.LogoEmpresa, empresa.ArchivoMimeType);
            //if (empresa.LogoEmpresa != null) return File(empresa.LogoEmpresa, empresa.ArchivoMimeType);

            else return null;
        }

        public FileContentResult GetImageAlumno(int IdAlumno)
        {
            Alumno alumno= lnAlumno.ObtenerAlumnoPorIdAlumno(IdAlumno);
            if (alumno.Foto != null && string.IsNullOrEmpty(alumno.ArchivoMimeType) == false) return File(alumno.Foto, alumno.ArchivoMimeType);
            //if (empresa.LogoEmpresa != null) return File(empresa.LogoEmpresa, empresa.ArchivoMimeType);

            else return null;
        }



        public ActionResult PostulacionOferta()
        {

            return View();
        }
        public ActionResult BusquedaSimplePostulacionOferta(VistaPostulacionAlumno entidad)
        {
            entidad.ListaPostulacionesOfertas = lnofertapostulante.ObtenerPostulantesPorIDAlumno(entidad.IdAlumno, entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.PaginaActual, Constantes.FILAS_POR_PAGINA);
            if (entidad.ListaPostulacionesOfertas.Count > 0)
            {
                entidad.MaxPagina = entidad.ListaPostulacionesOfertas[0].MaxPagina;
            }

            //Actualización para las paginaciones, se completa el objeto Paginación.

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.PaginaActual;
            //paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = entidad.MaxPagina; //cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            //int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            //if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            ViewBag.TipoBusqueda = "Simple";

            return PartialView("_ResultadoBusquedaPostulaciones", entidad);
        }
        public ActionResult DetalleEmpresa(int IdEmpresa)
        {
            Empresa empresa = new Empresa();
            empresa = lnempresa.ObtenerDetalleEmpresaPorId(IdEmpresa);
            return PartialView("_ModalDetalleEmpresa", empresa);
        }

        public ActionResult PostularOferta(OfertaPostulante entidad)
        {
            ViewBag.MensajePostulacion = "";
            if (entidad.IdCV > 0)
            {
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                entidad.CreadoPor = ticket.Usuario;

                //Se crea el CV en formato Word.
                string rutaPlantilla = Server.MapPath("~/Plantillas/PlantillaVER3.docx");
                PlantillaController plantilla = new PlantillaController();
                MemoryStream streamCurriculum = plantilla.CrearCurriculum(entidad.IdCV, rutaPlantilla);
                entidad.DocumentoCV = streamCurriculum.ToArray();
                lnofertapostulante.Insertar(entidad);
            }
            else
            {
                ViewBag.MensajePostulacion = "XXX";
            }
            return View();
        }
        public ActionResult PostulacionOferta2(string id)
        {
            if (id != null)
            {
                VistaOfertaAlumno vistaofertalumno = new VistaOfertaAlumno();
                Alumno alumno = new Alumno();
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                alumno = lnAlumno.ObtenerAlumnoPorCodigo(ticket.CodAlumnoUTP);
                vistaofertalumno = lnoferta.OfertaAlumnoPostulacion(Convert.ToInt32(Helper.Desencriptar(id)), alumno.IdAlumno);
                if (vistaofertalumno.Oferta != null && vistaofertalumno.Oferta.IdEmpresa > 0)
                {
                    //Periodo Publicacion
                    if (vistaofertalumno.Oferta.Postulacion == 0)
                    {
                        List<SelectListItem> listItemsAlumnoCV = new List<SelectListItem>();
                        foreach (AlumnoCV entidad in vistaofertalumno.ListaAlumnoCV)
                        {
                            SelectListItem item = new SelectListItem();
                            item.Text = entidad.NombreCV.ToString();
                            item.Value = entidad.IdCV.ToString();
                            listItemsAlumnoCV.Add(item);
                        }
                        ViewBag.ListaAlumnoCV = listItemsAlumnoCV;

                    }

                    return View(vistaofertalumno);
                }
                else
                {
                    return RedirectToAction("BusquedaOferta");
                }

            }
            else
            {
                return RedirectToAction("BusquedaOferta");
            }

        }
        public ActionResult EstadoPostulacionOferta(int? id)
        {
            if (id != null)
            {
                VistaOfertaAlumno vistaofertalumno = new VistaOfertaAlumno();
                Alumno alumno = new Alumno();
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                alumno = lnAlumno.ObtenerAlumnoPorCodigo(ticket.CodAlumnoUTP);
                vistaofertalumno = lnoferta.OfertaAlumnoPostulacion((int)id, alumno.IdAlumno);
                if (vistaofertalumno.Oferta != null && vistaofertalumno.Oferta.IdEmpresa > 0)
                {
                    //Periodo Publicacion
                    if (vistaofertalumno.Oferta.Postulacion == 0)
                    {
                        List<SelectListItem> listItemsAlumnoCV = new List<SelectListItem>();
                        foreach (AlumnoCV entidad in vistaofertalumno.ListaAlumnoCV)
                        {
                            SelectListItem item = new SelectListItem();
                            item.Text = entidad.NombreCV.ToString();
                            item.Value = entidad.IdCV.ToString();
                            listItemsAlumnoCV.Add(item);
                        }
                        ViewBag.ListaAlumnoCV = listItemsAlumnoCV;

                    }

                    return PartialView("_EstadoPostulacion", vistaofertalumno.Oferta);
                }
                else
                {
                    return RedirectToAction("BusquedaOferta");
                }

            }
            else
            {
                return RedirectToAction("BusquedaOferta");
            }

        }
        [HttpPost]
        public ActionResult DescargarCV(OfertaPostulante entidad)
        {

            byte[] cv = lnofertapostulante.OfertaPostulante_DescaragarCV(entidad.IdAlumno, entidad.IdOferta);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                stream.Write(cv, 0, cv.Length);
                Response.Buffer = false;
                Response.AppendHeader("Content-Type", "image/bmp");
                Response.AppendHeader("Content-Transfer-Encoding", "binary");
                Response.AppendHeader("Content-Disposition", "attachment; filename=imagen.bmp");
                Response.BinaryWrite(cv);
            }
            Response.End();
            //return new DescargaResult("Descarga", cv);

            return View();
        }

        public ActionResult BusquedaOferta()
        {
            VistaOfertaAlumno oferta = new VistaOfertaAlumno();
            LNGeneral lngeneral = new LNGeneral();
            LNOferta lnoferta = new LNOferta();
            Dictionary<int, string> listaperiodopublicacion = new Dictionary<int, string>();
            listaperiodopublicacion.Add(0, "Hoy");
            listaperiodopublicacion.Add(1, "Ayer");
            listaperiodopublicacion.Add(7, "Hace una semana");
            listaperiodopublicacion.Add(15, "Hace 15 dias");
            listaperiodopublicacion.Add(30, "Hace un mes");
            listaperiodopublicacion.Add(31, "Hace más de un mes");


            // oferta.ListaOfertas = lnoferta.Oferta_Mostrar();
            oferta.ListaEstudios = lngeneral.ObtenerListaValor(5);
            oferta.ListaEstadoEstudio = lngeneral.ObtenerListaValor(43);
            oferta.ListaSectorEmpresarial = lngeneral.ObtenerListaValor(8);
            oferta.ListaTipoTrabajo = lngeneral.ObtenerListaValor(29);
            oferta.ListaContrato = lngeneral.ObtenerListaValor(30);
            oferta.ListaTipoCargo = lngeneral.ObtenerListaValor(9);
            oferta.PeriodoPublicacion = listaperiodopublicacion;


            //Declara Lista
            //Periodo Publicacion
            List<SelectListItem> listItemsPublicacion = new List<SelectListItem>();
            foreach (KeyValuePair<int, string> entidad in oferta.PeriodoPublicacion)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Value.ToString();
                item.Value = entidad.Key.ToString();
                listItemsPublicacion.Add(item);
            }


            //Carreras y Estudios
            List<SelectListItem> listItemsCarrera = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaEstudios)
            {
                SelectListItem item = new SelectListItem();
                //item.Text = entidad.Valor.ToUpper();
                item.Text = entidad.Valor.ToString();
                item.Value = entidad.IdListaValor.ToString();
                listItemsCarrera.Add(item);
            }

            //Estado Estudio
            List<SelectListItem> listItemsEstadoEstudio = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaEstadoEstudio)
            {
                SelectListItem item = new SelectListItem();
                //item.Text = entidad.Valor.ToUpper();
                item.Text = entidad.Valor.ToString();
                item.Value = entidad.IdListaValor.ToString();
                listItemsEstadoEstudio.Add(item);
            }
            //Sector Empresarial
            List<SelectListItem> listItemsSectorEmpresarial = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaSectorEmpresarial)
            {
                SelectListItem item = new SelectListItem();
                //item.Text = entidad.Valor.ToUpper();
                item.Text = entidad.Valor.ToString();
                item.Value = entidad.IdListaValor.ToString();
                listItemsSectorEmpresarial.Add(item);
            }
            //Tipo Trabajo
            List<SelectListItem> listItemsTipoTrabajo = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaTipoTrabajo)
            {
                SelectListItem item = new SelectListItem();
                //item.Text = entidad.Valor.ToUpper();
                item.Text = entidad.Valor.ToString();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoTrabajo.Add(item);
            }
            //Contrato
            List<SelectListItem> listItemsContrato = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaContrato)
            {
                SelectListItem item = new SelectListItem();
                //item.Text = entidad.Valor.ToUpper();
                item.Text = entidad.Valor.ToString();
                item.Value = entidad.IdListaValor.ToString();
                listItemsContrato.Add(item);
            }
            //Tipo Cargo
            List<SelectListItem> listItemsTipoCargo = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaTipoCargo)
            {
                SelectListItem item = new SelectListItem();
                //item.Text = entidad.Valor.ToUpper();
                item.Text = entidad.Valor.ToString();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoCargo.Add(item);
            }


            //Lista de Combos
            ViewBag.PeriodoPublicacion = listItemsPublicacion;
            ViewBag.ListaEstudios = listItemsCarrera;
            ViewBag.ListaEstadoEstudio = listItemsEstadoEstudio;
            ViewBag.ListaSectorEmpresarial = listItemsSectorEmpresarial;
            ViewBag.ListaTipoTrabajo = listItemsTipoTrabajo;
            ViewBag.ListaContrato = listItemsContrato;
            ViewBag.ListaTipoCargo = listItemsTipoCargo;

            return View(oferta);


        }



        public ActionResult BusquedaAvanzadaOferta(VistaOfertaAlumno entidad)
        {
            entidad.ListaOfertas = lnoferta.BuscarAvanzadoOfertasAlumno(entidad);
            if (entidad.ListaOfertas.Count > 0)
            {
                entidad.MaxPagina = entidad.ListaOfertas[0].MaxPagina;
            }

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.PaginaActual;
            //paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = entidad.MaxPagina; // cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            //int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            //if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            ViewBag.TipoBusqueda = "Avanzada";

            return PartialView("_ResultadoBusquedaOfertas", entidad);
        }

        public ActionResult BusquedaSimpleOferta(VistaOfertaAlumno entidad)
        {
            entidad.ListaOfertas = lnoferta.BuscarFiltroOfertasAlumno(entidad.IdAlumno, entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.PaginaActual, entidad.NumeroRegistros);
            if (entidad.ListaOfertas.Count > 0)
            {
                entidad.MaxPagina = entidad.ListaOfertas[0].MaxPagina;
            }

            //Actualización para las paginaciones, se completa el objeto Paginación.
            
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.PaginaActual;
            //paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = entidad.MaxPagina; // cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            //int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            //if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            ViewBag.TipoBusqueda = "Simple";

            return PartialView("_ResultadoBusquedaOfertas", entidad);
        }






        #region "Mi Portal"

        #endregion
        #region "Postulaciones"

        #endregion
        #region "Ofertas"

        #endregion

        #region MiCV
        private VistaPanelAlumnoMiCV VistaMICV(int IdAlumno, int IdCV)
        {
            //Declaracion de objetos
            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            AlumnoCV alumnocv = new AlumnoCV();
            //Lista de Curriculos del Alumno
            alumnocv.IdAlumno = IdAlumno;
            alumnocv.IdPlantillaCV = int.Parse(Common.Util.ObtenerSettings("IdPlantillaCV"));
            alumnocv.NombreCV = Common.Util.ObtenerSettings("NameAlumnoCV");
            alumnocv.IncluirTelefonoFijo = false;
            alumnocv.IncluirCorreoElectronico2 = false;
            alumnocv.IncluirFoto = false;
            alumnocv.IncluirDireccion = false;
            alumnocv.Perfil = string.Empty;
            alumnocv.EstadoCV = "CVACT";
            alumnocv.CreadoPor = ticket.Usuario;
            panel.ListaAlumnoCV = lnAlumnoCV.ObtenerAlumnoCVPorIdAlumno(alumnocv);

            //Hallar el ID del Curriculo del alumno
            if (IdCV != 0)
            {
                panel.IdCV = IdCV;
                for (int i = 0; i <= panel.ListaAlumnoCV.Count - 1; i++)
                {
                    if (panel.ListaAlumnoCV[i].IdCV == IdCV)
                    {
                        panel.IdPlantillaCV = panel.ListaAlumnoCV[i].IdPlantillaCV;
                        panel.PorcentajeCV = panel.ListaAlumnoCV[i].PorcentajeCV;
                        break;
                    }
                }
            }
            else
            {
                panel.IdCV = panel.ListaAlumnoCV[0].IdCV;
                panel.IdPlantillaCV = panel.ListaAlumnoCV[0].IdPlantillaCV;
                panel.PorcentajeCV = panel.ListaAlumnoCV[0].PorcentajeCV;
            }


            //Lista las plantilla de curriculo
            panel.ListaPlantillaCV = lnPlantillaCV.MostrarPlantillaCV();


            return panel;
        }
        public ActionResult MiCV()
        {

            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            panel.alumno = lnAlumno.ObtenerAlumnoPorCodigo(ticket.CodAlumnoUTP);

            return View(panel);
        }
        public ActionResult OpcionesCV(VistaPanelAlumnoMiCV entidad)
        {
            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            panel = VistaMICV(entidad.IdAlumno, entidad.IdCV);


            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (AlumnoCV modelo in panel.ListaAlumnoCV)
            {
                SelectListItem item = new SelectListItem();
                item.Text = modelo.NombreCV;
                item.Value = modelo.IdCV.ToString();
                listItems.Add(item);
            }
            List<SelectListItem> listItemsPlantillaCV = new List<SelectListItem>();
            foreach (PlantillaCV modelo in panel.ListaPlantillaCV)
            {
                SelectListItem item = new SelectListItem();
                item.Text = modelo.Plantilla;
                item.Value = modelo.IdPlantillaCV.ToString();
                listItemsPlantillaCV.Add(item);
            }
            ViewBag.ListaAlumnoCV = listItems;
            ViewBag.ListaPlantillaCV = listItemsPlantillaCV;

            return PartialView("_OpcionesAlumnoCV", panel);
        }

        public ActionResult AlumnoDatosGenerales(VistaPanelAlumnoMiCV entidad)
        {

            Alumno alumno = new Alumno();
            AlumnoCV alumnocv = new AlumnoCV();
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumno = lnAlumno.ObtenerAlumnoPorCodigo(ticket.CodAlumnoUTP);
            if (alumno != null)
            {
                alumnocv = lnAlumnoCV.ObtenerAlumnoCVPorIdAlumnoYIdCV(alumno.IdAlumno, entidad.IdCV);
                alumno.IncluirCorreoElectronico2 = alumnocv.IncluirCorreoElectronico2;
                alumno.IncluirFoto = alumnocv.IncluirFoto;
                alumno.IncluirTelefonoFijo = alumnocv.IncluirTelefonoFijo;
                alumno.IncluirDireccion = alumnocv.IncluirDireccion;
                alumno.IncluirNombre1 = alumnocv.IncluirNombre1;
                alumno.IncluirNombre2 = alumnocv.IncluirNombre2;
                alumno.IncluirNombre3 = alumnocv.IncluirNombre3;
                alumno.IncluirNombre4 = alumnocv.IncluirNombre4;

                alumno.Perfil = alumnocv.Perfil;
                alumno.ListaNombres = alumno.Nombres.Split(new Char[] { ' ' });
            }
            return PartialView("_AlumnoDatosGenerales", alumno);
        }


        public ActionResult BuscarDatosEmpresas(int idempresa)
        {
            //string descripocn = "";
            

            AlumnoExperiencia vista = new AlumnoExperiencia();

            DataTable dtResultado = lnAlumno.Utp_BuscarDatosListaEmpresas(idempresa);

            if (dtResultado.Rows.Count> 0)
            {
                vista.IdEmpresa = Convert.ToInt32(dtResultado.Rows[0]["IdEmpresa"]);
                vista.Empresa = dtResultado.Rows[0]["NombreComercial"].ToString();
                vista.RazonSocial = dtResultado.Rows[0]["RazonSocial"].ToString();
                vista.DescripcionEmpresa = dtResultado.Rows[0]["DescripcionEmpresa"].ToString();
                vista.SectorEmpresarial = dtResultado.Rows[0]["SectorEmpresarial"].ToString();
                vista.SectorEmpresarial2 = dtResultado.Rows[0]["SectorEmpresarial2"].ToString();
                vista.SectorEmpresarial3 = dtResultado.Rows[0]["SectorEmpresarial3"].ToString();
                vista.Pais = dtResultado.Rows[0]["Pais"].ToString();
                vista.ValorSectorEmpresarial = dtResultado.Rows[0]["ValorSectorEmpresarial"].ToString();
            }

            return Json(vista, JsonRequestBehavior.AllowGet);

        }


        //public JsonResult Utp_BuscarDatosListaEmpresas(string query)
        //{
        //    LNGeneral lngeneral = new LNGeneral();
        //    var resultado = lngeneral.ObtenerListaValor(Constantes.IDLISTA_DISTRITO_PERU);
        //    var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}







        public ActionResult AlumnoEstudiosCV(VistaPanelAlumnoMiCV entidad)
        {

            Alumno alumno = new Alumno();
            List<AlumnoEstudio> listaalumnoestudio = new List<AlumnoEstudio>();
            listaalumnoestudio = lnAlumnoEstudio.ObtenerAlumnoEstudioPorIdAlumno(entidad.IdAlumno);
            if (alumno != null && listaalumnoestudio.Count > 0)
            {
                listaalumnoestudio = lnAlumnoCVEstudio.ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(entidad.IdCV, listaalumnoestudio);
            }
            return PartialView("_AlumnoEstudiosCV", listaalumnoestudio);
        }

        public ActionResult AlumnoExperienciaCV(VistaPanelAlumnoMiCV entidad)
        {

            Alumno alumno = new Alumno();
            List<AlumnoExperienciaCargo> listaalumnoexperienciacargo = new List<AlumnoExperienciaCargo>();


            listaalumnoexperienciacargo = lnalumnoexperienciacargo.ObtenerAlumnoExperienciaCargoPorIdAlumno(entidad.IdAlumno);
            if (alumno != null && listaalumnoexperienciacargo.Count > 0)
            {
                listaalumnoexperienciacargo = lnalumnocvexperienciacargo.ObtenerAlumnoCVExperienciaCargoPorIdCV(entidad.IdCV, listaalumnoexperienciacargo);
            }
            return PartialView("_AlumnoExperienciaCV", listaalumnoexperienciacargo);
        }

        public ActionResult AlumnoInformacionAdicionalCV(VistaPanelAlumnoMiCV entidad)
        {


            List<AlumnoInformacionAdicional> listaalumnoinformacionadicional = new List<AlumnoInformacionAdicional>();


            listaalumnoinformacionadicional = lnalumnoinformacionadicional.ObtenerAlumnoInformacionAdicionalPorIdAlumno(entidad.IdAlumno);
            if (listaalumnoinformacionadicional.Count > 0)
            {
                listaalumnoinformacionadicional = lnalumnocvinformacionadicional.ObtenerAlumnoCVInformacionAdicionalPorIdCV(entidad.IdCV, listaalumnoinformacionadicional);
            }
            return PartialView("_AlumnoInformacionAdicionalCV", listaalumnoinformacionadicional);
        }

        public PartialViewResult ModalRegistroEstudio(int IdAlumno, int IdCV)
        {

            LNGeneral lngeneral = new LNGeneral();
            AlumnoEstudio alumnoestudio = new AlumnoEstudio();

            alumnoestudio.IdAlumno = IdAlumno;
            alumnoestudio.IdCV = IdCV;
            Dictionary<int, string> meses = new Dictionary<int, string>();
            meses.Add(1, "Enero");
            meses.Add(2, "Febrero");
            meses.Add(3, "Marzo");
            meses.Add(4, "Abril");
            meses.Add(5, "Mayo");
            meses.Add(6, "Junio");
            meses.Add(7, "Julio");
            meses.Add(8, "Agosto");
            meses.Add(9, "Setiembre");
            meses.Add(10, "Octubre");
            meses.Add(11, "Noviembre");
            meses.Add(12, "Diciembre");
            ViewBag.meses = meses;
            ViewBag.TipoDeEstudio = new SelectList(lngeneral.ObtenerListaValor(7), "IdListaValor", "Valor");
            ViewBag.EstadoDelEstudio = new SelectList(lngeneral.ObtenerListaValor(43), "IdListaValor", "Valor");
            ViewBag.Observacion = new SelectList(lngeneral.ObtenerListaValor(44), "IdListaValor", "Valor");
            alumnoestudio.Movimiento = 1;
            return PartialView("_ModalRegistroEstudio", alumnoestudio);
        }

        public PartialViewResult ModalModificarEstudio(int IdEstudio, int IdAlumno, int IdCV)
        {

            LNGeneral lngeneral = new LNGeneral();
            AlumnoEstudio alumnoestudio = new AlumnoEstudio();
            alumnoestudio = lnAlumnoEstudio.ObtenerAlumnoEstudioPorId(IdEstudio);
            if (alumnoestudio != null && alumnoestudio.IdEstudio > 0)
            {
                alumnoestudio.IdAlumno = IdAlumno;
                alumnoestudio.IdCV = IdCV;
                alumnoestudio.IdEstudio = IdEstudio;
                Dictionary<int, string> meses = new Dictionary<int, string>();
                meses.Add(1, "Enero");
                meses.Add(2, "Febrero");
                meses.Add(3, "Marzo");
                meses.Add(4, "Abril");
                meses.Add(5, "Mayo");
                meses.Add(6, "Junio");
                meses.Add(7, "Julio");
                meses.Add(8, "Agosto");
                meses.Add(9, "Setiembre");
                meses.Add(10, "Octubre");
                meses.Add(11, "Noviembre");
                meses.Add(12, "Diciembre");
                ViewBag.TipoDeEstudio = new SelectList(lngeneral.ObtenerListaValor(7), "IdListaValor", "Valor", alumnoestudio.TipoDeEstudio);
                ViewBag.meses = meses;
                ViewBag.EstadoDelEstudio = new SelectList(lngeneral.ObtenerListaValor(43), "IdListaValor", "Valor", alumnoestudio.EstadoDelEstudio);
                ViewBag.Observacion = new SelectList(lngeneral.ObtenerListaValor(44), "IdListaValor", "Valor", alumnoestudio.Observacion);
                alumnoestudio.Movimiento = 2;
                return PartialView("_ModalRegistroEstudio", alumnoestudio);
            }
            else
            {
                return PartialView();
            }

        }

        public PartialViewResult ModalRegistroExperiencia(int IdAlumno, int IdCV)
        {

            LNGeneral lngeneral = new LNGeneral();
            AlumnoExperiencia alumnoexperiencia = new AlumnoExperiencia();

            alumnoexperiencia.IdAlumno = IdAlumno;
            alumnoexperiencia.IdCV = IdCV;
            Dictionary<int, string> meses = new Dictionary<int, string>();
            meses.Add(1, "Enero");
            meses.Add(2, "Febrero");
            meses.Add(3, "Marzo");
            meses.Add(4, "Abril");
            meses.Add(5, "Mayo");
            meses.Add(6, "Junio");
            meses.Add(7, "Julio");
            meses.Add(8, "Agosto");
            meses.Add(9, "Setiembre");
            meses.Add(10, "Octubre");
            meses.Add(11, "Noviembre");
            meses.Add(12, "Diciembre");
            ViewBag.meses = meses;


            ViewBag.SectorEmpresarial = new SelectList(lngeneral.ObtenerListaValor(8), "IdListaValor", "Valor");
            ViewBag.Pais = new SelectList(lngeneral.ObtenerListaValor(17), "IdListaValor", "Valor");
            ViewBag.TipoCargo = new SelectList(lngeneral.ObtenerListaValor(9), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial2 = ViewBag.SectorEmpresarial;
            ViewBag.SectorEmpresarial3 = ViewBag.SectorEmpresarial;
            alumnoexperiencia.Movimiento = 1;


            return PartialView("_ModalRegistrarExperiencia", alumnoexperiencia);
        }

        public PartialViewResult ModalModificarExperiencia(int IdExperienciaCargo, int IdAlumno, int IdCV)
        {

            LNGeneral lngeneral = new LNGeneral();
            AlumnoExperiencia alumnoexperiencia = new AlumnoExperiencia();
            alumnoexperiencia = lnalumnoexperienciacargo.ObtenerAlumnoExperienciaCargoPorId(IdExperienciaCargo);
            if (alumnoexperiencia != null && alumnoexperiencia.IdExperienciaCargo > 0)
            {
                alumnoexperiencia.IdAlumno = IdAlumno;
                alumnoexperiencia.IdCV = IdCV;
                alumnoexperiencia.IdExperienciaCargo = IdExperienciaCargo;

                Dictionary<int, string> meses = new Dictionary<int, string>();
                meses.Add(1, "Enero");
                meses.Add(2, "Febrero");
                meses.Add(3, "Marzo");
                meses.Add(4, "Abril");
                meses.Add(5, "Mayo");
                meses.Add(6, "Junio");
                meses.Add(7, "Julio");
                meses.Add(8, "Agosto");
                meses.Add(9, "Setiembre");
                meses.Add(10, "Octubre");
                meses.Add(11, "Noviembre");
                meses.Add(12, "Diciembre");
                ViewBag.meses = meses;


                ViewBag.SectorEmpresarial = new SelectList(lngeneral.ObtenerListaValor(8), "IdListaValor", "Valor", alumnoexperiencia.SectorEmpresarial);
                ViewBag.SectorEmpresarial2 = new SelectList(lngeneral.ObtenerListaValor(8), "IdListaValor", "Valor", alumnoexperiencia.SectorEmpresarial2);
                ViewBag.SectorEmpresarial3 = new SelectList(lngeneral.ObtenerListaValor(8), "IdListaValor", "Valor", alumnoexperiencia.SectorEmpresarial3);
                ViewBag.Pais = new SelectList(lngeneral.ObtenerListaValor(17), "IdListaValor", "Valor", alumnoexperiencia.Pais);
                ViewBag.TipoCargo = new SelectList(lngeneral.ObtenerListaValor(9), "IdListaValor", "Valor", alumnoexperiencia.TipoCargo);
                alumnoexperiencia.Movimiento = 2;


                return PartialView("_ModalRegistrarExperiencia", alumnoexperiencia);
            }
            else
            {
                return PartialView();
            }

        }



        public PartialViewResult ModalRegistroInformacionAdicional(int IdAlumno, int IdCV)
        {

            AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();
            LNGeneral lngeneral = new LNGeneral();

            Dictionary<int, string> meses = new Dictionary<int, string>();
            meses.Add(1, "Enero");
            meses.Add(2, "Febrero");
            meses.Add(3, "Marzo");
            meses.Add(4, "Abril");
            meses.Add(5, "Mayo");
            meses.Add(6, "Junio");
            meses.Add(7, "Julio");
            meses.Add(8, "Agosto");
            meses.Add(9, "Setiembre");
            meses.Add(10, "Octubre");
            meses.Add(11, "Noviembre");
            meses.Add(12, "Diciembre");
            ViewBag.meses = meses;
            ViewBag.TipoConocimientoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(10), "IdListaValor", "Valor");
            ViewBag.PaisIdListaValor = new SelectList(lngeneral.ObtenerListaValor(17), "IdListaValor", "Valor");
            ViewBag.NivelConocimientoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(16), "IdListaValor", "Valor");
            alumnoinformacionadicional.IdAlumno = IdAlumno;
            alumnoinformacionadicional.IdCV = IdCV;
            alumnoinformacionadicional.Movimiento = 1;

            return PartialView("_ModalRegistrarInformacionAdicional", alumnoinformacionadicional);
        }

        public PartialViewResult ModalModificarInformacionAdicional(int IdInformacionAdicional, int IdAlumno, int IdCV)
        {

            AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();
            alumnoinformacionadicional = lnalumnoinformacionadicional.ObtenerAlumnoEstudioPorId(IdInformacionAdicional);
            if (alumnoinformacionadicional != null && alumnoinformacionadicional.IdInformacionAdicional > 0)
            {
                LNGeneral lngeneral = new LNGeneral();

                Dictionary<int, string> meses = new Dictionary<int, string>();
                meses.Add(1, "Enero");
                meses.Add(2, "Febrero");
                meses.Add(3, "Marzo");
                meses.Add(4, "Abril");
                meses.Add(5, "Mayo");
                meses.Add(6, "Junio");
                meses.Add(7, "Julio");
                meses.Add(8, "Agosto");
                meses.Add(9, "Setiembre");
                meses.Add(10, "Octubre");
                meses.Add(11, "Noviembre");
                meses.Add(12, "Diciembre");
                ViewBag.meses = meses;
                ViewBag.TipoConocimientoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(10), "IdListaValor", "Valor", alumnoinformacionadicional.TipoConocimientoIdListaValor);
                ViewBag.PaisIdListaValor = new SelectList(lngeneral.ObtenerListaValor(17), "IdListaValor", "Valor", alumnoinformacionadicional.PaisIdListaValor);
                ViewBag.NivelConocimientoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(16), "IdListaValor", "Valor", alumnoinformacionadicional.NivelConocimientoIdListaValor);
                alumnoinformacionadicional.IdAlumno = IdAlumno;
                alumnoinformacionadicional.IdCV = IdCV;
                alumnoinformacionadicional.IdInformacionAdicional = IdInformacionAdicional;
                alumnoinformacionadicional.Movimiento = 2;

                return PartialView("_ModalRegistrarInformacionAdicional", alumnoinformacionadicional);
            }
            else
            {
                return PartialView();
            }

        }

        public PartialViewResult ModalRegistroCV(int IdAlumno)
        {

            AlumnoCV alumnocv = new AlumnoCV();
            alumnocv.IdAlumno = IdAlumno;
            return PartialView("_ModalRegistroCV", alumnocv);

        }

        public PartialViewResult _RegistrarAlumnoEstudio(AlumnoEstudio alumnoestudio)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumnoestudio.CreadoPor = ticket.Usuario;
            if (alumnoestudio.Movimiento == 1)
            {
                lnAlumnoEstudio.Insertar(alumnoestudio);
            }
            else if (alumnoestudio.Movimiento == 2)
            {
                lnAlumnoEstudio.Update(alumnoestudio);
            }


            Alumno alumno = new Alumno();
            List<AlumnoEstudio> listaalumnoestudio = new List<AlumnoEstudio>();


            listaalumnoestudio = lnAlumnoEstudio.ObtenerAlumnoEstudioPorIdAlumno(alumnoestudio.IdAlumno);
            if (alumno != null && listaalumnoestudio.Count > 0)
            {
                listaalumnoestudio = lnAlumnoCVEstudio.ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(alumnoestudio.IdCV, listaalumnoestudio);
            }
            return PartialView("_AlumnoEstudiosCV", listaalumnoestudio);
        }

        public PartialViewResult _RegistrarAlumnoExperiencia(AlumnoExperiencia alumnoexperiencia)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumnoexperiencia.CreadoPor = ticket.Usuario;
            alumnoexperiencia.Usuario = ticket.Usuario;

            if (alumnoexperiencia.Movimiento == 1)
            {
                lnalumnoexperiencia.Registrar(alumnoexperiencia);
            }
            else if (alumnoexperiencia.Movimiento == 2)
            {
                lnalumnoexperienciacargo.Update(alumnoexperiencia);
            }


            List<AlumnoExperienciaCargo> listaalumnoexperienciacargo = new List<AlumnoExperienciaCargo>();


            listaalumnoexperienciacargo = lnalumnoexperienciacargo.ObtenerAlumnoExperienciaCargoPorIdAlumno(alumnoexperiencia.IdAlumno);
            if (listaalumnoexperienciacargo.Count > 0)
            {
                listaalumnoexperienciacargo = lnalumnocvexperienciacargo.ObtenerAlumnoCVExperienciaCargoPorIdCV(alumnoexperiencia.IdCV, listaalumnoexperienciacargo);
            }
            return PartialView("_AlumnoExperienciaCV", listaalumnoexperienciacargo);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _RegistrarAlumnoInformacionAdicional(AlumnoInformacionAdicional alumnoinformacionadicional)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumnoinformacionadicional.Usuario = ticket.Usuario;
            if (alumnoinformacionadicional.Movimiento == 1)
            {
                lnalumnoinformacionadicional.Registrar(alumnoinformacionadicional);
            }
            else if (alumnoinformacionadicional.Movimiento == 2)
            {
                lnalumnoinformacionadicional.Update(alumnoinformacionadicional);
            }

            List<AlumnoInformacionAdicional> listaalumnoinformacionadicional = new List<AlumnoInformacionAdicional>();


            listaalumnoinformacionadicional = lnalumnoinformacionadicional.ObtenerAlumnoInformacionAdicionalPorIdAlumno(alumnoinformacionadicional.IdAlumno);
            if (listaalumnoinformacionadicional.Count > 0)
            {
                listaalumnoinformacionadicional = lnalumnocvinformacionadicional.ObtenerAlumnoCVInformacionAdicionalPorIdCV(alumnoinformacionadicional.IdCV, listaalumnoinformacionadicional);
            }
            return PartialView("_AlumnoInformacionAdicionalCV", listaalumnoinformacionadicional);
        }
        public PartialViewResult _RegistrarCV(AlumnoCV entidad)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            entidad.Usuario = ticket.Usuario;
            entidad.IdPlantillaCV = int.Parse(Util.ObtenerSettings("IdPlantillaCV"));
            ViewBag.Mensaje = "";
            if (lnAlumnoCV.RegistrarCV(ref entidad))
            {
                ViewBag.Mensaje = "Se registro el CV.";
            }
            else
            {
                ViewBag.Mensaje = "No se registro el CV.";
            }
            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            panel = VistaMICV(entidad.IdAlumno, entidad.IdCV);


            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (AlumnoCV modelo in panel.ListaAlumnoCV)
            {
                SelectListItem item = new SelectListItem();
                item.Text = modelo.NombreCV;
                item.Value = modelo.IdCV.ToString();
                listItems.Add(item);
            }
            List<SelectListItem> listItemsPlantillaCV = new List<SelectListItem>();
            foreach (PlantillaCV modelo in panel.ListaPlantillaCV)
            {
                SelectListItem item = new SelectListItem();
                item.Text = modelo.Plantilla;
                item.Value = modelo.IdPlantillaCV.ToString();
                listItemsPlantillaCV.Add(item);
            }
            ViewBag.ListaAlumnoCV = listItems;
            ViewBag.ListaPlantillaCV = listItemsPlantillaCV;

            return PartialView("_OpcionesAlumnoCV", panel);



        }
        public PartialViewResult RegistrarInfoCV(AlumnoCV entidad)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            entidad.Usuario = ticket.Usuario;
            lnAlumnoCV.UpdateInfo(entidad);
            ViewBag.Mensaje = "Se registro la información del CV.";
            return PartialView("_AlertModal");
        }

        public PartialViewResult DesactivarEstudioAlumno(int IdAlumno, int IdCV, int IdEstudio)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            //entidad.Usuario = ticket.CodAlumnoUTP;
            lnAlumnoEstudio.Desactivar(IdEstudio, ticket.Usuario);
            Alumno alumno = new Alumno();
            List<AlumnoEstudio> listaalumnoestudio = new List<AlumnoEstudio>();


            listaalumnoestudio = lnAlumnoEstudio.ObtenerAlumnoEstudioPorIdAlumno(IdAlumno);
            if (alumno != null && listaalumnoestudio.Count > 0)
            {
                listaalumnoestudio = lnAlumnoCVEstudio.ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(IdCV, listaalumnoestudio);
            }
            return PartialView("_AlumnoEstudiosCV", listaalumnoestudio);
        }
        public PartialViewResult DesactivarExperienciaAlumno(int IdAlumno, int IdCV, int IdExperienciaCargo)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            //entidad.Usuario = ticket.CodAlumnoUTP;
            lnalumnoexperienciacargo.Desactivar(IdExperienciaCargo, ticket.Usuario);
            List<AlumnoExperienciaCargo> listaalumnoexperienciacargo = new List<AlumnoExperienciaCargo>();


            listaalumnoexperienciacargo = lnalumnoexperienciacargo.ObtenerAlumnoExperienciaCargoPorIdAlumno(IdAlumno);
            if (listaalumnoexperienciacargo.Count > 0)
            {
                listaalumnoexperienciacargo = lnalumnocvexperienciacargo.ObtenerAlumnoCVExperienciaCargoPorIdCV(IdCV, listaalumnoexperienciacargo);
            }
            return PartialView("_AlumnoExperienciaCV", listaalumnoexperienciacargo);
        }
        public PartialViewResult DesactivarInformacionAdicionalAlumno(int IdAlumno, int IdCV, int IdInformacionAdicional)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            //entidad.Usuario = ticket.CodAla en utumnoUTP;
            lnalumnoinformacionadicional.Desactivar(IdInformacionAdicional, ticket.Usuario);
            List<AlumnoInformacionAdicional> listaalumnoinformacionadicional = new List<AlumnoInformacionAdicional>();


            listaalumnoinformacionadicional = lnalumnoinformacionadicional.ObtenerAlumnoInformacionAdicionalPorIdAlumno(IdAlumno);
            if (listaalumnoinformacionadicional.Count > 0)
            {
                listaalumnoinformacionadicional = lnalumnocvinformacionadicional.ObtenerAlumnoCVInformacionAdicionalPorIdCV(IdCV, listaalumnoinformacionadicional);
            }
            return PartialView("_AlumnoInformacionAdicionalCV", listaalumnoinformacionadicional);
        }





        #endregion

        #region "Generales"
        public ActionResult DatosAlumno(string Id)
        {
            if (Id != null)
            {
                Alumno alumno = lnAlumno.ObtenerAlumnoPorIdAlumno(Convert.ToInt32(Helper.Desencriptar(Id)));
                if (alumno != null && string.IsNullOrEmpty(alumno.Usuario) == false)
                {
                    LNGeneral lngeneral = new LNGeneral();
                    ViewBag.TipoDocumentoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(1), "IdListaValor", "Valor", alumno.TipoDocumentoIdListaValor);
                    ViewBag.SexoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(2), "IdListaValor", "Valor", alumno.SexoIdListaValor);
                    ViewBag.DireccionRegionId = new SelectList(lngeneral.ObtenerListaValor(47), "IdListaValor", "Valor", alumno.DireccionRegionId);
                    ViewBag.EstadoAlumno = new SelectList(lngeneral.ObtenerListaValor(3), "IdListaValor", "Valor", alumno.EstadoAlumno);
                    return View(alumno);
                }
                else
                {
                    return RedirectToAction("Index", "Alumno");
                }
            }
            else
            {
                return RedirectToAction("Index", "Alumno");
            }

        }
        [HttpPost]
        public ActionResult DatosAlumno(Alumno entidad, HttpPostedFileBase foto2)
        {
            LNGeneral lngeneral = new LNGeneral();
            if (ModelState.IsValid)
            {
                if (foto2 != null)
                {
                    entidad.ArchivoMimeType = foto2.ContentType;
                    entidad.Foto = new byte[foto2.ContentLength];
                    foto2.InputStream.Read(entidad.Foto, 0, foto2.ContentLength);

                }

                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                entidad.Usuario = ticket.Usuario;
                lnAlumno.ModifcarDatos(entidad);
                Alumno alumno = lnAlumno.ObtenerAlumnoPorIdAlumno(entidad.IdAlumno);
                if (alumno != null && string.IsNullOrEmpty(alumno.Usuario) == false)
                {
                    ViewBag.TipoDocumentoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(1), "IdListaValor", "Valor", alumno.TipoDocumentoIdListaValor);
                    ViewBag.SexoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(2), "IdListaValor", "Valor", alumno.SexoIdListaValor);
                    ViewBag.DireccionRegionId = new SelectList(lngeneral.ObtenerListaValor(47), "IdListaValor", "Valor", alumno.DireccionRegionId);
                    ViewBag.EstadoAlumno = new SelectList(lngeneral.ObtenerListaValor(3), "IdListaValor", "Valor", alumno.EstadoAlumno);
                }
                else
                {
                    ViewBag.TipoDocumentoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(1), "IdListaValor", "Valor", entidad.TipoDocumentoIdListaValor);
                    ViewBag.SexoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(2), "IdListaValor", "Valor", entidad.SexoIdListaValor);
                    ViewBag.DireccionRegionId = new SelectList(lngeneral.ObtenerListaValor(47), "IdListaValor", "Valor", entidad.DireccionRegionId);
                    ViewBag.EstadoAlumno = new SelectList(lngeneral.ObtenerListaValor(3), "IdListaValor", "Valor", entidad.EstadoAlumno);
                }

            }
            else
            {
                //     var errors = ModelState.Select(x => x.Value.Errors)
                //.Where(y => y.Count > 0)
                //.ToList();
                ViewBag.TipoDocumentoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(1), "IdListaValor", "Valor", entidad.TipoDocumentoIdListaValor);
                ViewBag.SexoIdListaValor = new SelectList(lngeneral.ObtenerListaValor(2), "IdListaValor", "Valor", entidad.SexoIdListaValor);
                ViewBag.DireccionRegionId = new SelectList(lngeneral.ObtenerListaValor(47), "IdListaValor", "Valor", entidad.DireccionRegionId);
                ViewBag.EstadoAlumno = new SelectList(lngeneral.ObtenerListaValor(3), "IdListaValor", "Valor", entidad.EstadoAlumno);
            }



            return View(entidad);
        }
        public ActionResult ListarListaValor(string Id)
        {
            LNGeneral lngeneral = new LNGeneral();
            var Data = lngeneral.ObtenerListaValorPorIdPadre(Id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarEstudio(string query)
        {
            LNGeneral lngeneral = new LNGeneral();
            var resultado = lngeneral.ObtenerListaValorPorIdPadre(Constantes.TIPO_ESTUDIO_UNIVERSITARIO);
            var result = resultado.Where(s => s.Valor.ToLower().Contains(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarDistritos(string query)
        {
            LNGeneral lngeneral = new LNGeneral();
            var resultado = lngeneral.ObtenerListaValor(Constantes.IDLISTA_DISTRITO_PERU);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarAreasEmpresa(string query)
        {
            LNGeneral lngeneral = new LNGeneral();
            var resultado = lngeneral.ObtenerListaValor(Constantes.IDLISTA_AREA_EMPRESA);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public FileResult ReturnImagen(byte[] imagen)
        {

            if (imagen != null) return File(imagen, "image/jpg");
            else return null;
        }
        //public FileContentResult ReturnImagen(byte[] imagen, string MimeType)
        //{

        //    if (imagen != null && string.IsNullOrEmpty(MimeType) == false) return File(imagen, MimeType);

        //    else return null;
        //}

        #endregion











        //vista cabeceraa Oferta
        public ActionResult VistaCabecera(string colapsaDatos)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(ticket.CodAlumnoUTP);
            ViewBag.ColapsaDatos = colapsaDatos;
            return PartialView("_DatosPersonales", panel.Alumno);
        }
        public ActionResult DatosAlumnoOferta()
        {

            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(ticket.CodAlumnoUTP);
            return PartialView("_CabeceraOfertaAlumno", panel.Alumno);
        }


        //Obtiene las Listas de opciones Cargo
        public ActionResult ObtenerLista_Cargo()
        {
            const int id = 9;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                string nombre = dtprueba.Rows[i]["DescripcionValor"].ToString();
                string valor = dtprueba.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }

            ViewData["ListaValor"] = li;
            return View();

        }

        //Obtiene las Listas de opciones Tipo de Trabajo
        public ActionResult ObtenerLista_TipodeTrabajo()
        {
            const int id = 29;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                string nombre = dtprueba.Rows[i]["DescripcionValor"].ToString();
                string valor = dtprueba.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }

            ViewData["ListaValor"] = li;
            return View();

        }

        //Obtiene las Listas de opciones Contrato
        public ActionResult ObtenerLista_Contrato()
        {
            const int id = 30;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                string nombre = dtprueba.Rows[i]["DescripcionValor"].ToString();
                string valor = dtprueba.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }

            ViewData["ListaValor"] = li;
            return View();

        }

        //Obtiene las Listas de opciones Sector Empresarial
        public ActionResult ObtenerLista_SectorEmpresarial()
        {
            const int id = 8;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                string nombre = dtprueba.Rows[i]["DescripcionValor"].ToString();
                string valor = dtprueba.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }

            ViewData["ListaValor"] = li;
            return View();

        }

        //Obtiene las Listas de opciones Grado
        public ActionResult ObtenerLista_Grado()
        {
            //Busca  en la tabla lista a  Tipo de Estudio
            const int id = 7;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                string nombre = dtprueba.Rows[i]["DescripcionValor"].ToString();
                string valor = dtprueba.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }

            ViewData["ListaValor"] = li;
            return View();

        }

        //Obtiene las Listas de opciones Carrera
        public ActionResult ObtenerLista_Carrera()
        {
            //Busca  en la tabla lista a carrera profesional
            const int id = 5;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                string nombre = dtprueba.Rows[i]["DescripcionValor"].ToString();
                string valor = dtprueba.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }

            ViewData["ListaValor"] = li;


            return View();

        }

        public ActionResult ObtenerListaOpciones()
        {

            //Busca  en la tabla lista a carrera profesional
            const int id = 5;

            DataTable dtprueba = lnoferta.ObtenerLista_ListaValor(id);
            VistaComboAlumno vista = new VistaComboAlumno();

            List<SelectListItem> listitem2 = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                vista.Cargo = dtprueba.Rows[i]["DescripcionValor"].ToString();
                vista.IDListaValor = dtprueba.Rows[i]["IDListaValor"].ToString();


                listitem2.Add(new SelectListItem() { Value = vista.Cargo, Text = vista.IDListaValor.ToString() });


            }


            ViewBag.DropDownValues2 = new SelectList(listitem2, "Text", "Value");

            //---------------------

            //Busca  en la tabla lista Grado
            const int id7 = 7;

            DataTable dtprueba7 = lnoferta.ObtenerLista_ListaValor(id7);
            VistaComboAlumno vista7 = new VistaComboAlumno();

            List<SelectListItem> listitem7 = new List<SelectListItem>();

            for (int i7 = 0; i7 <= dtprueba7.Rows.Count - 1; i7++)
            {
                vista7.Cargo = dtprueba7.Rows[i7]["DescripcionValor"].ToString();
                vista7.IDListaValor = dtprueba7.Rows[i7]["IDListaValor"].ToString();


                listitem7.Add(new SelectListItem() { Value = vista7.Cargo, Text = vista7.IDListaValor.ToString() });


            }


            ViewBag.DropDownValues7 = new SelectList(listitem7, "Text", "Value");

            return View();
        }

        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
            Session["TicketAlumno"] = null;
            return RedirectToAction("Index", "Home");
        }
        //public ActionResult DatosAlumno()
        //{
        //    return View();
        //}

        public ActionResult AlertaCvAlumno()
        {
            List<AlertasCvAlumno> lista = new List<AlertasCvAlumno>();
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            DataTable dtResultado = lnoferta.AlertaCvAlumno(ticket.Usuario);
            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlertasCvAlumno alumno = new AlertasCvAlumno();
                alumno.NombreCV = Convert.ToString(dtResultado.Rows[i]["NombreCV"]);
                alumno.PorcentajeCV = Convert.ToInt32(dtResultado.Rows[i]["PorcentajeCV"]);
                alumno.IdCV = Convert.ToInt32(dtResultado.Rows[i]["IdCV"]);
                lista.Add(alumno);
            }




            //return PartialView(alumno);
            return PartialView("AlertaCvAlumno", lista);
        }

        public ActionResult AlertaCvAlumnoDia()
        {

            List<AlertasCvAlumno> lista = new List<AlertasCvAlumno>();


            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];

            DataTable dtResultado = lnoferta.AlertaCvAlumnoDia(ticket.Usuario);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlertasCvAlumno alumno = new AlertasCvAlumno();
                    alumno.Dia = Convert.ToInt32(dtResultado.Rows[i]["Dia"] == DBNull.Value ? null : dtResultado.Rows[i]["Dia"]);

                    lista.Add(alumno);
                }
            }


            //return PartialView(alumno);
            return PartialView("AlertaCvAlumnoDia", lista);
        }

        public ActionResult AlertaCvAlumnoMes()
        {

            List<AlertasCvAlumno> lista = new List<AlertasCvAlumno>();


            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];

            DataTable dtResultado = lnoferta.AlertaCvAlumnoMes(ticket.Usuario);


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlertasCvAlumno alumno = new AlertasCvAlumno();

                //31DIC14 - Aldo Chocos: Se valida que no sea nulo
                alumno.MesesSinTrabajo = Convert.ToInt32(dtResultado.Rows[i]["MesesSinTrabajo"] == DBNull.Value ? 0 : dtResultado.Rows[i]["MesesSinTrabajo"]);

                lista.Add(alumno);
            }

            //return PartialView(alumno);
            return PartialView("AlertaCvAlumnoMes", lista);
        }
        public ActionResult Eventos()
        {
            return View();
        }
        public ActionResult Evento(string idEvento)
        {
            return View();
        }


        //[ValidateInput(false)]
        //public ActionResult Portal_Editar_Buscar(int id)
        //{
        //    DataTable dtresultado = ln.ContenidoMenu_Mostrar();

        //    List<SelectListItem> li = new List<SelectListItem>();

        //    for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
        //    {
        //        string nombre = dtresultado.Rows[i]["Titulo"].ToString();
        //        string valor = dtresultado.Rows[i]["IdMenu"].ToString();



        //        SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

        //        li.Add(item);

        //    }
        //    ViewData["ContenidoMenu"] = li;


        //    ContenidoVista contenido = new ContenidoVista();


        //    DataTable dtResultado = ln.ContenidoEDitar_Buscar(id);

        //    if (dtResultado.Rows.Count > 0)
        //    {
        //        contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[0]["IdContenido"]);
        //        contenido.Menu = dtResultado.Rows[0]["CodMenu"].ToString();
        //        contenido.Titulo = dtResultado.Rows[0]["Titulo"].ToString();
        //        contenido.SubTitulo = dtResultado.Rows[0]["SubTitulo"].ToString();
        //        contenido.Descripcion = dtResultado.Rows[0]["Descripcion"].ToString();
        //        contenido.ModificadoPor = dtResultado.Rows[0]["ModificadoPor"] == null ? "" : dtResultado.Rows[0]["ModificadoPor"].ToString();
        //        contenido.EnPantallaPrincipal = Convert.ToBoolean(dtResultado.Rows[0]["EnPantallaPrincipal"].ToString());
        //        contenido.Activo = Convert.ToBoolean(dtResultado.Rows[0]["Activo"] == DBNull.Value ? 0 : dtResultado.Rows[0]["Activo"]);
        //        contenido.ArchivoNombreOriginal = dtResultado.Rows[0]["ArchivoNombreOriginal"].ToString();
        //        contenido.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaCreacion"].ToString());
        //        contenido.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaModificacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaModificacion"].ToString());
        //        contenido.CreadoPor = dtResultado.Rows[0]["CreadoPor"] == null ? "" : dtResultado.Rows[0]["CreadoPor"].ToString();

        //    }


        //    return View(contenido);


        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult Portal_Editar_Buscar([Bind(Include = "")] ContenidoVista contenidoHTML)
        //{

        //    Contenido contenido = new Contenido();

        //    if (contenidoHTML.ImagenHtml != null)
        //    {

        //        byte[] uploadedFile = new byte[contenidoHTML.ImagenHtml.InputStream.Length];
        //        contenidoHTML.ImagenHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(contenidoHTML.ImagenHtml.InputStream.Length));
        //        contenidoHTML.ArchivoNombreOriginal = contenidoHTML.ImagenHtml.FileName;
        //        contenidoHTML.ArchivoMimeType = contenidoHTML.ImagenHtml.ContentType;
        //        contenidoHTML.Imagen = uploadedFile;

        //        contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;

        //    }

        //    contenido.Titulo = contenidoHTML.Titulo;
        //    contenido.SubTitulo = contenidoHTML.SubTitulo;
        //    contenido.Descripcion = contenidoHTML.Descripcion;
        //    contenido.Imagen = contenidoHTML.Imagen;
        //    contenido.ArchivoMimeType = contenidoHTML.ArchivoMimeType;
        //    contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;

        //    contenido.EnPantallaPrincipal = contenidoHTML.EnPantallaPrincipal;
        //    contenido.Activo = contenidoHTML.Activo;

        //    contenido.Menu = contenidoHTML.Menu;
        //    contenido.CreadoPor = contenidoHTML.CreadoPor;
        //    contenido.FechaCreacion = contenidoHTML.FechaCreacion;

        //    TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];

        //    contenido.ModificadoPor = ticketUtp.Usuario;
        //    //contenido.ModificadoPor = contenidoHTML.ModificadoPor;
        //    contenido.IdContenido = contenidoHTML.IdContenido;

        //    //if (ModelState.IsValid)
        //    //{

        //    if (ln.Contenido_Actualizar(contenido) == true)
        //    {
        //        ViewBag.Message = "Datos Actualizado";
        //        return RedirectToAction("Portal");
        //    }
        //    else
        //    {

        //        DataTable dtresultado = ln.ContenidoMenu_Mostrar();

        //        List<SelectListItem> li = new List<SelectListItem>();

        //        for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
        //        {
        //            string nombre = dtresultado.Rows[i]["Titulo"].ToString();
        //            string valor = dtresultado.Rows[i]["IdMenu"].ToString();



        //            SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

        //            li.Add(item);

        //        }
        //        ViewData["ContenidoMenu"] = li;

        //        ViewBag.Message = "Error al Actualizar";
        //        return View(contenidoHTML);
        //    }

        //    //}

        //}




    }
}