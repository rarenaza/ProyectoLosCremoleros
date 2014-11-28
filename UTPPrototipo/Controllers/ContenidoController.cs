using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.ContenidoCombo;
using UTPPrototipo.Models;
using UTPPrototipo.Models.ViewModels.Contenido;

namespace UTPPrototipo.Controllers
{
    public class ContenidoController : Controller
    {

    

        // GET: Contenido
        LNContenido ln = new LNContenido();

        // GET: Lista

       
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
        public ActionResult Contenido_insertar([Bind(Include = "")] ContenidoVista  contenidoHTML)
        {
                Contenido contenido = new Contenido ();

            if (contenidoHTML.ImagenHtml != null)
            {

                byte[] uploadedFile = new byte[contenidoHTML.ImagenHtml.InputStream.Length];
                contenidoHTML.ImagenHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(contenidoHTML.ImagenHtml.InputStream.Length));
                contenidoHTML.ArchivoNombreOriginal = contenidoHTML.ImagenHtml.FileName;
                contenidoHTML.ArchivoMimeType = contenidoHTML.ImagenHtml.ContentType;
                contenidoHTML.Imagen = uploadedFile; 
                                                

            }

            contenido.Titulo = contenidoHTML.Titulo;
            contenido.SubTitulo = contenidoHTML.SubTitulo;
            contenido.Descripcion = contenidoHTML.Descripcion;
            contenido.Imagen = contenidoHTML.Imagen;
            contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;
            contenido.EnPantallaPrincipal = contenidoHTML.EnPantallaPrincipal;
            contenido.Menu = contenidoHTML.Menu;
            contenido.CreadoPor = contenidoHTML.CreadoPor;         

          

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
            contenido = ln.Contenido_BuscarIndex(x);

            return View(contenido);

        }

        public ActionResult ContenidoEDitar_Buscar(int id)
        {


            DataTable dtprueba = ln.ContenidoMenu_Mostrar();

            ContenidoVistaCombo vista = new ContenidoVistaCombo();
     

            List<SelectListItem> listitem1 = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                vista.Titulo = dtprueba.Rows[i]["Titulo"].ToString();
                vista.IDMenu = dtprueba.Rows[i]["IDMenu"].ToString();


                listitem1.Add(new SelectListItem() { Value = vista.Titulo, Text = vista.IDMenu.ToString() });

            }

            ViewBag.DropDownValues1 = new SelectList(listitem1, "Text", "Value");


            return View(ln.ContenidoEDitar_Buscar(id)); 
            

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContenidoEDitar_Buscar([Bind(Include = "")] Contenido contenido)
        {
                      

            //if (ModelState.IsValid)
            //{
         
           
            if (ln.Contenido_Actualizar(contenido) == true)
            {
                ViewBag.Message = "Datos Actualizado";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Error al Actualizar";
                return View(contenido);
            }

            //}


        }
       
        public ActionResult RemoverArchivo(int id)
        {
 
            var contenido = ContenidoEDitar_Buscar(id);

            bool esExitoso = ln.Contenido_RemoverImagen(id);

            if (esExitoso)
            {         
               
                return RedirectToAction("ContenidoEDitar_Buscar", new { id = id });
            }            

            return View(contenido);


            //var req = db.Requerimientoes.Where(r => r.Id == id).FirstOrDefault();

            //if (req != null)
            //{
            //    //Se limpian los datos de la base de datos.
            //    req.ArchivoBD = null;
            //    req.ArchivoMimeType = "";
            //    req.ArchivoNombreOriginal = "";

            //    db.SaveChanges();

            //    return RedirectToAction("Edit", new { id = id });
            //}

            //return View(req);
            
            }

        //muestra un contenido nuevo el la pantalla contenido Index

        public ActionResult Index()
        {


            List<Contenido> contenido = new List<Contenido>();

            contenido = ln.Contenido_Mostrar();

            return View(contenido);


        }

        public ActionResult ObtenerListaContenidoMenu()
        {
                                  
            DataTable dtprueba = ln.ContenidoMenu_Mostrar();

            ContenidoVistaCombo vista = new ContenidoVistaCombo();

            List<SelectListItem> listitem1 = new List<SelectListItem>();

            for (int i = 0; i <= dtprueba.Rows.Count - 1; i++)
            {
                vista.Titulo = dtprueba.Rows[i]["DescripcionValor"].ToString();
                vista.IDMenu = dtprueba.Rows[i]["IDListaValor"].ToString();


                listitem1.Add(new SelectListItem() { Value = vista.Titulo, Text = vista.IDMenu.ToString() });

            }
            
            ViewBag.DropDownValues1 = new SelectList(listitem1, "Text", "Value");

            return View();
        }


        }

    }
