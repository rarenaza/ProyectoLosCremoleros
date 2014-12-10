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

namespace UTPPrototipo.Controllers
{
    public class AlumnoController : Controller
    {
        LNAlumno lnAlumno = new LNAlumno();
        LNAlumnoEstudio lnAlumnoEstudio = new LNAlumnoEstudio();
        LNAlumnoCV lnAlumnoCV = new LNAlumnoCV();
        LNAlumnoCVEstudio lnAlumnoCVEstudio = new LNAlumnoCVEstudio();

        LNPlantillaCV lnPlantillaCV = new LNPlantillaCV();

        LNOferta lnoferta = new LNOferta();

       

        public string codigoAlumno = "82727128";

        public ActionResult Index()
        {
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(codigoAlumno);


            return View(panel);
        }
        //public ActionResult Postulacion() 
        //{
        //    VistaPanelAlumnoPostulaciones panel = lnAlumno.ObtenerPanelPostulaciones(codigoAlumno);

        //    return View(panel);
        //}

        public ActionResult Postulacion()
        {
            List<VistaPostulacionAlumno> lista = new List<VistaPostulacionAlumno>();

            lista = lnoferta.ObtenerPostulantes();

            return View(lista);
        }

        public ActionResult PostulacionOferta()
        {
            return View();
        }
        public ActionResult PostulacionOferta2(int? id)
        {
            if (id != null)
            {
                VistaOfertaAlumno vistaofertalumno = new VistaOfertaAlumno();
                Alumno alumno = new Alumno();
                alumno = lnAlumno.ObtenerAlumnoPorCodigo(codigoAlumno);
                vistaofertalumno = lnoferta.OfertaAlumnoPostulacion((int)id,alumno.IdAlumno);
                //Periodo Publicacion
                List<SelectListItem> listItemsAlumnoCV = new List<SelectListItem>();
                foreach (AlumnoCV entidad in vistaofertalumno.ListaAlumnoCV)
                {
                    SelectListItem item = new SelectListItem();
                    item.Text = entidad.NombreCV.ToString();
                    item.Value = entidad.IdCV.ToString();
                    listItemsAlumnoCV.Add(item);
                }
                ViewBag.ListaAlumnoCV = listItemsAlumnoCV;
                return View(vistaofertalumno);
            }
            else
            {
                return RedirectToAction("BusquedaOferta");
            }
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

            entidad.ListaOfertas = lnoferta.MostrarUltimasOfertas(entidad.IdAlumno);
             return PartialView("_ResultadoBusquedaOfertas", entidad.ListaOfertas);
        }

        public ActionResult BusquedaSimpleOferta(VistaOfertaAlumno entidad)
        {
            entidad.ListaOfertas = lnoferta.MostrarUltimasOfertas(entidad.IdAlumno);
            return PartialView("_ResultadoBusquedaOfertas", entidad.ListaOfertas);
        }


        private VistaPanelAlumnoMiCV VistaMICV(int? IdCV)
        {
            //Declaracion de objetos
            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            AlumnoCV alumnocv = new AlumnoCV();
            //Datos del Alumno
            panel.alumno = lnAlumno.ObtenerAlumnoPorCodigo(codigoAlumno);

            //Lista de Curriculos del Alumno
            alumnocv.IdAlumno = panel.alumno.IdAlumno;
            alumnocv.IdPlantillaCV = int.Parse(Common.Util.ObtenerSettings("IdPlantillaCV"));
            alumnocv.NombreCV = Common.Util.ObtenerSettings("NameAlumnoCV");
            alumnocv.IncluirTelefonoFijo = false;
            alumnocv.IncluirCorreoElectronico2 = false;
            alumnocv.IncluirFoto = false;
            alumnocv.IncluirDireccion = false;
            alumnocv.Perfil = string.Empty;
            alumnocv.EstadoCV = "CVACT";
            alumnocv.CreadoPor = "administrador";
            panel.ListaAlumnoCV = lnAlumnoCV.ObtenerAlumnoCVPorIdAlumno(alumnocv);

            //Hallar el ID del Curriculo del alumno
            if (IdCV != null)
            {
                panel.IdCV = (int)IdCV;
                for (int i = 0; i <= panel.ListaAlumnoCV.Count - 1; i++) {
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
        public ActionResult MiCV()
        {
            
            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            panel = VistaMICV(null);

            return View(panel);
        }
        public ActionResult OpcionesCV(int? Id)
        {
            VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
            panel = VistaMICV(Id);
 

            List<SelectListItem> listItems = new List<SelectListItem>();
            foreach (AlumnoCV entidad in panel.ListaAlumnoCV)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.NombreCV;
                item.Value = entidad.IdCV.ToString();
                listItems.Add(item);
            }
            List<SelectListItem> listItemsPlantillaCV = new List<SelectListItem>();
            foreach (PlantillaCV entidad in panel.ListaPlantillaCV)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Plantilla;
                item.Value = entidad.IdPlantillaCV.ToString();
                listItemsPlantillaCV.Add(item);
            }
            ViewBag.ListaAlumnoCV = listItems;
            ViewBag.ListaPlantillaCV = listItemsPlantillaCV;

            return PartialView("_OpcionesAlumnoCV");
        }
        //public ActionResult OpcionesCV()
        //{
        //    VistaPanelAlumnoMiCV panel = new VistaPanelAlumnoMiCV();
        //    Alumno alumno = new Alumno();
        //    AlumnoCV alumnocv = new AlumnoCV();

        //    alumno = lnAlumno.ObtenerAlumnoPorCodigo(codigoAlumno);
        //    alumnocv.IdAlumno = alumno.IdAlumno;
        //    alumnocv.IdPlantillaCV = int.Parse(Common.Util.ObtenerSettings("IdPlantillaCV"));
        //    alumnocv.NombreCV = Common.Util.ObtenerSettings("NameAlumnoCV");
        //    alumnocv.IncluirTelefonoFijo = false;
        //    alumnocv.IncluirCorreoElectronico2 = false;
        //    alumnocv.IncluirFoto = false;
        //    alumnocv.Perfil = string.Empty;
        //    alumnocv.EstadoCV = "CVACT";

        //    alumnocv.CreadoPor = "administrador";
        //    var listaalumnocv = lnAlumnoCV.ObtenerAlumnoCVPorIdAlumno(alumnocv);
        //    var listaplantillacv = lnPlantillaCV.MostrarPlantillaCV();

        //    List<SelectListItem> listItems = new List<SelectListItem>();
        //    foreach (AlumnoCV entidad in listaalumnocv)
        //    {
        //        SelectListItem item = new SelectListItem();
        //        item.Text = entidad.NombreCV;
        //        item.Value = entidad.IdCV.ToString();
        //        listItems.Add(item);
        //    }
        //    List<SelectListItem> listItemsPlantillaCV = new List<SelectListItem>();
        //    foreach (PlantillaCV entidad in listaplantillacv)
        //    {
        //        SelectListItem item = new SelectListItem();
        //        item.Text = entidad.Plantilla;
        //        item.Value = entidad.IdPlantillaCV.ToString();
        //        listItemsPlantillaCV.Add(item);
        //    }
        //    panel.IdPlantillaCV = listaalumnocv[0].IdPlantillaCV;
        //    ViewBag.ListaAlumnoCV = listItems;
        //    ViewBag.ListaPlantillaCV = listItemsPlantillaCV;

        //    return PartialView("_OpcionesAlumnoCV", panel);
        //}
        public ActionResult AlumnoDatosGenerales(int Id)
        {

            Alumno alumno = new Alumno();
            AlumnoCV alumnocv = new AlumnoCV();

            alumno = lnAlumno.ObtenerAlumnoPorCodigo(codigoAlumno);
            if (alumno != null)
            {
                alumnocv = lnAlumnoCV.ObtenerAlumnoCVPorIdAlumnoYIdCV(alumno.IdAlumno, Id);
                alumno.IncluirCorreoElectronico1 = alumnocv.IncluirCorreoElectronico2;
                alumno.IncluirFoto = alumnocv.IncluirFoto;
                alumno.IncluirTelefonoFijo = alumnocv.IncluirTelefonoFijo;
                alumno.IncluirDireccion = alumnocv.IncluirDireccion;
                alumno.Perfil = alumnocv.Perfil;
            }
            return PartialView("_AlumnoDatosGenerales", alumno);
        }

        public ActionResult AlumnoEstudiosCV(int Id)
        {

            Alumno alumno = new Alumno();
            List<AlumnoEstudio> listaalumnoestudio = new List<AlumnoEstudio>();

            alumno = lnAlumno.ObtenerAlumnoPorCodigo(codigoAlumno);
            listaalumnoestudio = lnAlumnoEstudio.ObtenerAlumnoEstudioPorIdAlumno(alumno.IdAlumno);
            if (alumno != null && listaalumnoestudio.Count>0)
            {
                listaalumnoestudio = lnAlumnoCVEstudio.ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(Id, listaalumnoestudio);
            }
            return PartialView("_AlumnoEstudiosCV", listaalumnoestudio);
        }





        //vista cabeceraa Oferta
        public ActionResult VistaCabecera()
        {
            //string codigoAlumno = "82727128";
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(codigoAlumno);
            return PartialView("_DatosPersonales", panel.Alumno);
        }
        public ActionResult DatosAlumnoOferta()
        {
            //string codigoAlumno = "82727128";
            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(codigoAlumno);
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

      

            
	}
}