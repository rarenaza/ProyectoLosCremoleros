using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Models.ViewModels.Contenido;

namespace UTPPrototipo.Controllers
{
    public class HomeController : Controller
    {
        LNContenido ln = new LNContenido();
        public ActionResult Index()
        {

            ViewBag.ListaIndex = ln.Contenido_BuscarNoticiasEventosOtros("1");

            ViewBag.ListaNoticias = ln.Contenido_BuscarIndex("4");
            ViewBag.ListaEventos = ln.Contenido_BuscarIndex("7");
            ViewBag.ListaTestimonios = ln.Contenido_BuscarIndex("5");

            return View();

            //List<Contenido> contenido = new List<Contenido>();
            //string x = "1";
            //contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);
            //return View(contenido);
        }

        public FileResult Imagen_Index(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";


            List<Contenido> contenido = new List<Contenido>();
            string x = "1";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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

        public ActionResult ParaEmpleadores()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "6";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            //return View(contenido);

            List<ContenidoVista> contentModel = contenido.Select(item => new ContenidoVista()
            {
                Menu = item.Menu,
                Titulo = item.Titulo,
                SubTitulo = item.SubTitulo,
                Descripcion = item.Descripcion,
                Imagen = item.Imagen,
                IdContenido = item.IdContenido

            }).ToList();
            return View(contentModel);
        }

        public FileResult Imagen_ParaEmpleadores(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> contenido = new List<Contenido>();
            string x = "6";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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

        public ActionResult Contacto()
        {
            return View();
        }
        public ActionResult Testimonios()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "5";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);


            List<ContenidoVista> contentModel = contenido.Select(item => new ContenidoVista()
            {
                Menu = item.Menu,
                Titulo = item.Titulo,
                SubTitulo = item.SubTitulo,
                Descripcion = item.Descripcion,
                Imagen = item.Imagen,
                IdContenido = item.IdContenido

            }).ToList();
            return View(contentModel);

       

        }

        public FileResult Imagen_Testimonios(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> contenido = new List<Contenido>();
            string x = "5";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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


        public ActionResult DirEmp()
        {
   //////
            List<Contenido> contenido = new List<Contenido>();
            string x = "2";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);
            
            List<ContenidoVista> contentModel = contenido.Select(item => new ContenidoVista()
            {
                Menu = item.Menu,
                Titulo = item.Titulo,
                SubTitulo =item.SubTitulo,
                Descripcion =item .Descripcion,
                Imagen = item.Imagen,
                IdContenido =item .IdContenido 

            }).ToList();
            return View(contentModel);
                       
        }

        public FileResult Imagen_Diremp(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> contenido = new List<Contenido>();
            string x = "2";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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
         

        public ActionResult NoticiasEventos()
        {
            ViewBag.Noticias = ln.Contenido_BuscarNoticiasEventosOtros("4");
            ViewBag.Eventos = ln.Contenido_BuscarNoticiasEventosOtros("7");


            return View();


            //List<Contenido> contenido = new List<Contenido>();
            //string x = "4";
            //contenido = ln.Contenido_Buscar(x);

            //return View(contenido);
                    
        }
        public FileResult Imagen_Noticia(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> Noticia = new List<Contenido>();
            string x = "4";
            Noticia = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = Noticia.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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

        public FileResult Imagen_Eventos(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> Evento = new List<Contenido>();
            string x = "7";
            Evento = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = Evento .Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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
        public ActionResult Servicios()
        {
            List<Contenido> contenido = new List<Contenido>();
            string x = "3";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            //return View(contenido);

            List<ContenidoVista> contentModel = contenido.Select(item => new ContenidoVista()
            {
                Menu = item.Menu,
                Titulo = item.Titulo,
                SubTitulo = item.SubTitulo,
                Descripcion = item.Descripcion,
                Imagen = item.Imagen,
                IdContenido = item.IdContenido

            }).ToList();
            return View(contentModel);
       
        }

        public FileResult Imagen_Servicios(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> contenido = new List<Contenido>();
            string x = "3";
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(x);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
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
        public ActionResult TerminosDeUso()
        {
            return View();
        }
        public ActionResult PoliticasDePrivacidad()
        {
            return View();
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Registro()
        {
            return View();
        }
    }
}