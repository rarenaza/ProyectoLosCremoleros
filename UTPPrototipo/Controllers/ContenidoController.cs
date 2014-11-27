using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Models;
using UTPPrototipo.Models.ViewModels.Contenido;

namespace UTPPrototipo.Controllers
{
    public class ContenidoController : Controller
    {
      


        // GET: Contenido
        LNContenido ln = new LNContenido();

        // GET: Lista

        //muestra un contenido nuevo el la pantalla contenido Index
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


        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }

             


        public byte[] GetImageFromDataBase(int Id)
        {


            //List<Contenido> contenido = ln.Contenido_Mostrar();

            //List<ContenidoVista> listaVista = new List<ContenidoVista>();

            //foreach (var itemBD in contenido)
            //{
            //    ContenidoVista vista = new ContenidoVista();

            //    vista.Titulo = itemBD.Titulo;
            //    vista.SubTitulo = itemBD.SubTitulo;
            //    vista.Descripcion = itemBD.Descripcion;
            //    vista.Imagen = itemBD.Imagen;
            //    vista.EnPantallaPrincipal = itemBD.EnPantallaPrincipal;
            //    vista.Menu = itemBD.Menu;
            //    vista.CreadoPor = itemBD.CreadoPor;


            //    listaVista.Add(vista);



            //}



            //return View(listaVista);



            var q = from temp in ln.Contenido_Mostrar() where temp.IdContenido == Id select temp.Imagen;
            byte[] cover = q.First();
            return cover;
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
    
            return View(ln.ContenidoEDitar_Buscar(id)); 
            

        }
          
       

    }
}