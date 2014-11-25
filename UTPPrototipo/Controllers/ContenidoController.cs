using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class ContenidoController : Controller
    {
        // GET: Contenido
        LNContenido ln = new LNContenido();

        // GET: Lista
        public ActionResult Index()
        {
            List<Contenido> contenido = new List<Contenido>();

            contenido = ln.Contenido_Mostrar();

            return View(contenido);
        }
        
        public ActionResult Contenido_insertar()
        {


            DataTable dtresultado = ln.ContenidoMenu_Mostrar();

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
            {
                string nombre = dtresultado.Rows[i]["Titulo"].ToString();
                string valor = dtresultado.Rows[i]["IdMenu"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }
            ViewData["ContenidoMenu"] = li;
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contenido_insertar([Bind(Include = "")] Contenido  contenido)
        {
               

            if (ln.Contenido_insertar(contenido) == true)
            {
                ViewBag.Message = "Registro Insertado Correctamente";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Error al Guardar la informacion";
                return View(contenido);
            }



        }

        public ActionResult Contenido_Buscar()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "2";
            contenido = ln.Contenido_Buscar(x);

            return View(contenido);

        }

    }
}