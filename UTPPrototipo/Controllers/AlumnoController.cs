using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTPPrototipo.Controllers
{
    public class AlumnoController : Controller
    {
        LNAlumno lnAlumno = new LNAlumno();

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
        public ActionResult PostulacionOferta2()
        {
            return View();
        }
        public ActionResult BusquedaOferta()
        {
            List<VistaOfertaAlumno> listaOferta = new List<VistaOfertaAlumno>();

            listaOferta = lnoferta.Oferta_Mostrar();
            
            return View(listaOferta);
                    
            
        }
        public ActionResult MiCV()
        {
            VistaPanelAlumnoMiCV panel = lnAlumno.ObtenerPanelMiCV(codigoAlumno);
            return View(panel);
        }

        //vista cabeceraa Oferta
        public ActionResult VistaCabecera()
        {
            string codigoAlumno = "82727128";

            VistaPanelAlumno panel = lnAlumno.ObtenerPanel(codigoAlumno);

            return PartialView("_DatosPersonales", panel.Alumno);

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
            
	}
}