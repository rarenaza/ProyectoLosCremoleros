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

namespace UTPPrototipo.Controllers
{
    [VerificarSesion]
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

        public ActionResult PostulacionOferta()
        {

            return View();
        }
        public ActionResult BusquedaSimplePostulacionOferta(VistaPostulacionAlumno entidad)
        {
            entidad.ListaPostulacionesOfertas = lnofertapostulante.ObtenerPostulantesPorIDAlumno(entidad.IdAlumno, entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.PaginaActual, entidad.NumeroRegistros);
            if (entidad.ListaPostulacionesOfertas.Count > 0)
            {
                entidad.MaxPagina = entidad.ListaPostulacionesOfertas[0].MaxPagina;
            }
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
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            entidad.CreadoPor = ticket.Usuario;
            lnofertapostulante.Insertar(entidad);
            return View();
        }
        public ActionResult PostulacionOferta2(int? id)
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
            listaperiodopublicacion.Add(7, "Hace uan semana");
            listaperiodopublicacion.Add(15, "Hace 15 dias");
            listaperiodopublicacion.Add(30, "Hace un mes");
            listaperiodopublicacion.Add(31, "Hace mas de un mes");


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
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsCarrera.Add(item);
            }

            //Estado Estudio
            List<SelectListItem> listItemsEstadoEstudio = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaEstadoEstudio)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsEstadoEstudio.Add(item);
            }
            //Sector Empresarial
            List<SelectListItem> listItemsSectorEmpresarial = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaSectorEmpresarial)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsSectorEmpresarial.Add(item);
            }
            //Tipo Trabajo
            List<SelectListItem> listItemsTipoTrabajo = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaTipoTrabajo)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoTrabajo.Add(item);
            }
            //Contrato
            List<SelectListItem> listItemsContrato = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaContrato)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsContrato.Add(item);
            }
            //Tipo Cargo
            List<SelectListItem> listItemsTipoCargo = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaTipoCargo)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
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
            return PartialView("_ResultadoBusquedaOfertas", entidad);
        }

        public ActionResult BusquedaSimpleOferta(VistaOfertaAlumno entidad)
        {
            entidad.ListaOfertas = lnoferta.BuscarFiltroOfertasAlumno(entidad.IdAlumno, entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.PaginaActual, entidad.NumeroRegistros);
            if (entidad.ListaOfertas.Count > 0)
            {
                entidad.MaxPagina = entidad.ListaOfertas[0].MaxPagina;
            }
            return PartialView("_ResultadoBusquedaOfertas", entidad);
        }


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
                        break;
                    }
                }
            }
            else
            {
                panel.IdCV = panel.ListaAlumnoCV[0].IdCV;
                panel.IdPlantillaCV = panel.ListaAlumnoCV[0].IdPlantillaCV;
            }


            //Lista las plantilla de curriculo
            panel.ListaPlantillaCV = lnPlantillaCV.MostrarPlantillaCV();


            return panel;
        }



        #region "Mi Portal"

        #endregion
        #region "Postulaciones"

        #endregion
        #region "Ofertas"

        #endregion

        #region "Mi CV"
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
                alumno.IncluirCorreoElectronico1 = alumnocv.IncluirCorreoElectronico2;
                alumno.IncluirFoto = alumnocv.IncluirFoto;
                alumno.IncluirTelefonoFijo = alumnocv.IncluirTelefonoFijo;
                alumno.IncluirDireccion = alumnocv.IncluirDireccion;
                alumno.Perfil = alumnocv.Perfil;
            }
            return PartialView("_AlumnoDatosGenerales", alumno);
        }

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
                listaalumnoexperienciacargo = lnalumnocvexperienciacargo.ObtenerAlumnoCVExperienciaCargoPorIdAlumno(entidad.IdCV, listaalumnoexperienciacargo);
            }
            return PartialView("_AlumnoExperienciaCV", listaalumnoexperienciacargo);
        }

        public ActionResult AlumnoInformacionAdicionalCV(VistaPanelAlumnoMiCV entidad)
        {


            List<AlumnoInformacionAdicional> listaalumnoinformacionadicional = new List<AlumnoInformacionAdicional>();


            listaalumnoinformacionadicional = lnalumnoinformacionadicional.ObtenerAlumnoInformacionAdicionalPorIdAlumno(entidad.IdAlumno);
            if (listaalumnoinformacionadicional.Count > 0)
            {
                listaalumnoinformacionadicional = lnalumnocvinformacionadicional.ObtenerAlumnoCVInformacionAdicionalPorIdAlumno(entidad.IdCV, listaalumnoinformacionadicional);
            }
            return PartialView("_AlumnoInformacionAdicionalCV", listaalumnoinformacionadicional);
        }



        public ActionResult ModalRegistroEstudio()
        {

            AlumnoEstudio alumnoestudio = new AlumnoEstudio();
            LNGeneral lngeneral = new LNGeneral();

            alumnoestudio.ListaEstudios = lngeneral.ObtenerListaValor(5);
            alumnoestudio.ListaTipoEstudios = lngeneral.ObtenerListaValor(7);
            alumnoestudio.ListaEstadoEstudio = lngeneral.ObtenerListaValor(43);
            alumnoestudio.ListaObservacionEstudios = lngeneral.ObtenerListaValor(44);
            //Declara Lista
            //Carreras y Estudios
            List<SelectListItem> listItemsCarrera = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoestudio.ListaEstudios)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsCarrera.Add(item);
            }

            //Tipo Estudios
            List<SelectListItem> listItemsTipoEstudios = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoestudio.ListaTipoEstudios)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoEstudios.Add(item);
            }

            //Estado Estudio
            List<SelectListItem> listItemsEstadoEstudio = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoestudio.ListaEstadoEstudio)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsEstadoEstudio.Add(item);
            }

            //Observaciones
            List<SelectListItem> listItemsObservaciones = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoestudio.ListaObservacionEstudios)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsObservaciones.Add(item);
            }



            //Lista de Combos
            ViewBag.ListaEstudios = listItemsCarrera;
            ViewBag.ListaTipoEstudios = listItemsTipoEstudios;
            ViewBag.ListaEstadoEstudio = listItemsEstadoEstudio;
            ViewBag.ListaObservacionEstudios = listItemsObservaciones;

            return PartialView("_ModalRegistroEstudio");
        }

        public ActionResult ModalRegistroExperiencia()
        {

            AlumnoExperienciaCargo alumnoexperienciacargo = new AlumnoExperienciaCargo();
            LNGeneral lngeneral = new LNGeneral();

            alumnoexperienciacargo.ListaSectorEmpresarial = lngeneral.ObtenerListaValor(8);
            alumnoexperienciacargo.ListaPais = lngeneral.ObtenerListaValor(17);
            alumnoexperienciacargo.ListaTipoCargo = lngeneral.ObtenerListaValor(9);
            //Declara Lista
            //Sector Empresarial
            List<SelectListItem> listItemsSectorEmpresarial = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoexperienciacargo.ListaSectorEmpresarial)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsSectorEmpresarial.Add(item);
            }


            //Paises
            List<SelectListItem> listItemsPaises = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoexperienciacargo.ListaPais)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsPaises.Add(item);
            }

            //Tipo Cargo
            List<SelectListItem> listItemsTipoCargo = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoexperienciacargo.ListaTipoCargo)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoCargo.Add(item);
            }


            //Lista de Combos
            ViewBag.ListaSectorEmpresarial = listItemsSectorEmpresarial;
            ViewBag.ListaPais = listItemsPaises;
            ViewBag.ListaTipoCargo = listItemsTipoCargo;

            return PartialView("_ModalRegistrarExperiencia");
        }

        public ActionResult ModalRegistroInformacionAdicional()
        {

            AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();
            LNGeneral lngeneral = new LNGeneral();

            alumnoinformacionadicional.ListaTipoConocimiento = lngeneral.ObtenerListaValor(10);
            alumnoinformacionadicional.ListaPais = lngeneral.ObtenerListaValor(17);
            alumnoinformacionadicional.ListaNivelConocimiento = lngeneral.ObtenerListaValor(16);
            //Declara Lista
            //Tipo de Conocimiento
            List<SelectListItem> listItemsTipoConocimiento = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoinformacionadicional.ListaTipoConocimiento)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoConocimiento.Add(item);
            }


            //Paises
            List<SelectListItem> listItemsPaises = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoinformacionadicional.ListaPais)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsPaises.Add(item);
            }

            //Nivel de Conocimiento
            List<SelectListItem> listItemsNivelConocimiento = new List<SelectListItem>();
            foreach (ListaValor entidad in alumnoinformacionadicional.ListaNivelConocimiento)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsNivelConocimiento.Add(item);
            }


            //Lista de Combos
            ViewBag.ListaTipoConocimiento = listItemsTipoConocimiento;
            ViewBag.ListaPais = listItemsPaises;
            ViewBag.ListaNivelConocimiento = listItemsNivelConocimiento;

            return PartialView("_ModalRegistrarInformacionAdicional");
        }



        public ActionResult RegistrarAlumnoEstudio(AlumnoEstudio alumnoestudio)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumnoestudio.CreadoPor = ticket.Usuario;
            lnAlumnoEstudio.Insertar(alumnoestudio);
            return View();
        }

        public ActionResult RegistrarAlumnoExperiencia(AlumnoExperiencia alumnoexperiencia)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumnoexperiencia.CreadoPor = ticket.Usuario;
            lnalumnoexperiencia.Registrar(alumnoexperiencia);
            return View();
        }

        public ActionResult RegistrarAlumnoInformacionAdicional(AlumnoInformacionAdicional alumnoinformacionadicional)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            alumnoinformacionadicional.CreadoPor = ticket.Usuario;
            lnalumnoinformacionadicional.Registrar(alumnoinformacionadicional);
            return View();
        }

        public ActionResult RegistrarCV(VistaAlumnoCV entidad)
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            entidad.Usuario = ticket.CodAlumnoUTP;
            lnAlumnoCV.UpdateInfo(entidad);
            return View();
        }

        

        #endregion











        //vista cabeceraa Oferta
        public ActionResult VistaCabecera()
        {
            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(ticket.CodAlumnoUTP);
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
        public ActionResult DatosAlumno()
        {
            return View();
        }

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
                lista.Add(alumno );
            }

            //return PartialView(alumno);
            return PartialView("AlertaCvAlumno", lista ); 
        }

        public ActionResult AlertaCvAlumnoDia()
        {

            List<AlertasCvAlumno> lista = new List<AlertasCvAlumno>();


            TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];

            DataTable dtResultado = lnoferta.AlertaCvAlumnoDia(ticket.Usuario);


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlertasCvAlumno alumno = new AlertasCvAlumno();
                alumno.Dia = Convert.ToInt32(dtResultado.Rows[i]["Dia"]);
          
                lista.Add(alumno);
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
                alumno.MesesSinTrabajo = Convert.ToInt32(dtResultado.Rows[i]["MesesSinTrabajo"]);

                lista.Add(alumno);
            }

            //return PartialView(alumno);
            return PartialView("AlertaCvAlumnoMes", lista);
        }

    }
}