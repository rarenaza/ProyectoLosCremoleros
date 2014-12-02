using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Models.ViewModels.Contenido;

namespace UTPPrototipo.Controllers
{
    public class UTPController : Controller
    {
        LNContenido ln = new LNContenido();
        // GET: UTP
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Portal(string id)
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

            List<Contenido> lista = new List<Contenido>();
            if (id == null)
            {
               
            }

            else
            {
                              
                DataTable dtResultado = ln.Contenido_Mostrar2(id);

                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    Contenido contenido = new Contenido();
                    contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
                    contenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                    contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                    contenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                    contenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();

                    //contenido.Imagen = Encoding.UTF8.GetBytes(dtResultado.Rows[0]["Imagen"].ToString());
                    lista.Add(contenido);

                }

            }


            return View(lista);

            //return View();

            //List<Contenido> contenido = new List<Contenido>();

            //contenido = ln.Contenido_Mostrar();

            //return View(contenido);
          
          
        }
        public ActionResult BuscarGrilla()
        {
            List<Contenido> lista = new List<Contenido>(); 
          
            string x;
            x = "5";
            DataTable dtResultado = ln.Contenido_Mostrar2(x);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido contenido = new Contenido();
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
                contenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                contenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                contenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();
                
                //contenido.Imagen = Encoding.UTF8.GetBytes(dtResultado.Rows[0]["Imagen"].ToString());
                lista.Add(contenido);
      
            }
            return View(lista);
        }

        public ActionResult Portal_insertar()
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
        public ActionResult Portal_insertar([Bind(Include = "")] ContenidoVista contenidoHTML)
        {
            Contenido contenido = new Contenido();


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

            contenido.ArchivoMimeType = contenidoHTML.ArchivoMimeType;
            contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;
            contenido.EnPantallaPrincipal = contenidoHTML.EnPantallaPrincipal;
            contenido.Menu = contenidoHTML.Menu;
            contenido.CreadoPor = contenidoHTML.CreadoPor;


            if (ln.Contenido_insertar(contenido) == true)
            {
                ViewBag.Message = "Registro Insertado Correctamente";
                return RedirectToAction("Portal");
            }
            else
            {
                ViewBag.Message = "Error al Guardar la informacion";
                return View(contenido);
            }

        }

        public ActionResult Portal_Editar_Buscar(int id)
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


            ContenidoVista contenido = new ContenidoVista();

            DataTable dtResultado = ln.ContenidoEDitar_Buscar(id);

            if (dtResultado.Rows.Count > 0)
            {
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[0]["IdContenido"]);
                contenido.Menu = dtResultado.Rows[0]["CodMenu"].ToString();
                contenido.Titulo = dtResultado.Rows[0]["Titulo"].ToString();
                contenido.SubTitulo = dtResultado.Rows[0]["SubTitulo"].ToString();
                contenido.Descripcion = dtResultado.Rows[0]["Descripcion"].ToString();
                contenido.ModificadoPor = dtResultado.Rows[0]["ModificadoPor"] == null ? "" : dtResultado.Rows[0]["ModificadoPor"].ToString();
                contenido.EnPantallaPrincipal = Convert.ToBoolean(dtResultado.Rows[0]["EnPantallaPrincipal"].ToString());
                contenido.ArchivoNombreOriginal = dtResultado.Rows[0]["ArchivoNombreOriginal"].ToString();


            }


            return View(contenido);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Portal_Editar_Buscar([Bind(Include = "")] ContenidoVista contenidoHTML)
        {
      
            Contenido contenido = new Contenido();

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
            contenido.ModificadoPor = contenidoHTML.ModificadoPor;
            contenido.IdContenido = contenidoHTML.IdContenido;


            //if (ModelState.IsValid)
            //{


            if (ln.Contenido_Actualizar(contenido) == true)
            {
                ViewBag.Message = "Datos Actualizado";
                return RedirectToAction("Portal");
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

            var contenido = Portal_Editar_Buscar(id);

            bool esExitoso = ln.Contenido_RemoverImagen(id);

            if (esExitoso)
            {

                return RedirectToAction("Portal_Editar_Buscar", new { id = id });
            }

            return View(contenido);

        }

        public ActionResult Alumnos()
        {
            return View();
        }
        public ActionResult Empresas()
        {
            return View();
        }
        public ActionResult Sistema()
        {
            return View();
        }
        public ActionResult Reportes()
        {
            return View();
        }
        public ActionResult Eventos()
        {
            return View();
        }
    }
}