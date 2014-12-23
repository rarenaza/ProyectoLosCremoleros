using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
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
            LNGeneral lnGeneral = new LNGeneral();

            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor");
            ViewBag.NumeroEmpleadosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NRO_EMPLEADOS), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial2IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial3IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");

            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor");
            ViewBag.SexoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SEXO), "IdListaValor", "Valor");
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor");
            ViewBag.RolIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ROL_USUARIO), "IdListaValor", "Valor");
            ViewBag.EstadoUsuarioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_USUARIO), "IdListaValor", "Valor");


            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Registro(VistaRegistroEmpresa empresa)
        {
            LNUsuario lnUsuario = new LNUsuario();

            StringBuilder mensajeDeError = new StringBuilder();
            if (lnUsuario.ValidarNombreDeUsuario(empresa.CuentaUsuario))
            {
                mensajeDeError.Append( "El Nombre de Usuario ya está Registrado<br />");
                
            }
            if (lnUsuario.ValidarExistenciaEmpresa(empresa.PaisIdListaValor, empresa.IdentificadorTributario))
            {
                mensajeDeError.Append("La Empresa ya se encuentra registrada, por favor comuníquese con nosotros<br />");
                
            }
            if (ModelState.IsValid && mensajeDeError.ToString() == "")
            {                                                                                                                 
                LNEmpresa lnEmpresa = new LNEmpresa();

                //Empresa
                empresa.CreadoPor = empresa.CuentaUsuario;  //Usuario anónimo.
                empresa.EstadoIdListaValor = "EMPRRV";  //Estado de la empresa pendiente de aprobación.

                //Locación
                empresa.EstadoLocacionIdListaValor = "LOSTNO";  //Estado NO ACTIVA. Se debe activar al momento que UTP active la cuenta.      
                empresa.NombreLocacion = empresa.DireccionLocacion + ", " + empresa.DireccionDistritoLocacion + ", " + empresa.DireccionCiudadLocacion + ", " + empresa.DireccionDepartamentoLocacion;

                //Usuario
                empresa.RolIdListaValor = "ROLADE";  //La cuenta es creada como Rol: "Administrador de Empresa"
                empresa.EstadoUsuarioIdListaValor = "USEMPE";  //El usuario también se encuenta pendiente de activación. Se debe activar al momento que UTP active la cuenta.

                lnEmpresa.Insertar(empresa);

                //Si el registro fue exitoso redireccionar a página de resultado.
                TempData["GuardaRegistroExitoso"] = "Estimado(a) <strong>" + empresa.NombresUsuario + " " + empresa.ApellidosUsuario
                    + "</strong>, muchas gracias por enviarnos su información. En breve recibirá un correo de confirmación con sus datos.</br></br>Nuestro proceso de activación tomará un plazo no mayor a 24 horas, antes del cual estaremos comunicándole la activación de su Usuario. ";
                //Aquí debería enviarse un correo
                return RedirectToAction("Index");
            }

            LNGeneral lnGeneral = new LNGeneral();

            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor",empresa.PaisIdListaValor);
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor",empresa.SectorEmpresarial1IdListaValor);
            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor",empresa.TipoLocacionIdListaValor);
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor", empresa.TipoDocumentoIdListaValor);
            ViewBag.MensajeDeError = mensajeDeError;
            return View(empresa);
        }
    }
}