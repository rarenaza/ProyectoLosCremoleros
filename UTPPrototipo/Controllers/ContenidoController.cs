using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        public ActionResult Contenido_insertar([Bind(Include = "")] ContenidoVista contenidoHTML)
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
           
            contenido.ArchivoMimeType = contenidoHTML.ArchivoMimeType;
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
        public ActionResult ContenidoEDitar_Buscar([Bind(Include = "")] ContenidoVista contenidoHTML)
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
            contenido.ModificadoPor  = contenidoHTML.ModificadoPor;
            contenido.IdContenido = contenidoHTML.IdContenido;  


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

        }

        //muestra un contenido nuevo el la pantalla contenido Index

            
        public ActionResult Index()
        {
            
            //var content = ln.Contenido_Mostrar();
            
            var content = ln.Contenido_Mostrar().Select(s => new
            {
                s.IdContenido,
                s.Titulo,
                s.Imagen,
                s.Descripcion,
                s.SubTitulo
            });

            List<ContenidoVista> contentModel = content.Select(item => new ContenidoVista()
            {
               IdContenido = item.IdContenido,
                Titulo  = item.Titulo,
                Imagen   = item.Imagen,
                Descripcion = item.Descripcion,
                SubTitulo  = item.SubTitulo
            }).ToList();
            return View(contentModel);



            //List<Contenido> contenido = new List<Contenido>();

            //contenido = ln.Contenido_Mostrar();

            //return View(contenido);

         
        }

        public ActionResult Portal()
        {


            List<Contenido> contenido = new List<Contenido>();

            contenido = ln.Contenido_Mostrar();

            return View(contenido);


        }


        public ActionResult RetrieveImage(int id) 
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpeg");
            }
            else
            {
                return null;
            }
        }
 
        public byte[] GetImageFromDataBase(int Id)
        {
            if (Id == 36)
            {
                int temporal = 0;
            }

            var q = from temp in ln.Contenido_Mostrar() where temp.IdContenido == Id select temp.Imagen;
            byte[] cover = q.First();
            return cover;

            //var q = ln.Contenido_mostrar_imagen(Id);
          
            //byte[] cover =q ;
            //return cover;
        }


        //public byte[] GetImageFromDataBase(int Id)
        //{
        //    var q = from temp in db.Contents where temp.ID == Id select temp.Image;
        //    byte[] cover = q.First();
        //    return cover;
        //}

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
