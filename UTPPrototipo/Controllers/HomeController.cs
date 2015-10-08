using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTPPrototipo.Common;
using UTPPrototipo.Models.ViewModels.Contenido;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Utiles;

namespace UTPPrototipo.Controllers
{
    [LogPortal]
    public class HomeController : Controller
    {
        LNContenido ln = new LNContenido();

        public ActionResult Index()
        {            
            ViewBag.ListaIndex = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_NOTICIAS_EVENTOS);
            ViewBag.ListaNoticias = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_NOTICIAS);
            ViewBag.ListaEventos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_SE_VIENE);
            ViewBag.ListaTestimonios = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_NOS_CUENTAN);

            return View();
        }

        public FileResult Imagen_Index(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";


            List<Contenido> contenido = new List<Contenido>();
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_NOTICIAS_EVENTOS);

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
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_EMPLEADORES);


            List<ContenidoVista> contentModel = contenido.Select(item => new ContenidoVista()
            {
                Menu = item.Menu,
                Titulo = item.Titulo,
                SubTitulo = item.SubTitulo,
                Descripcion = item.Descripcion,
                Imagen = item.Imagen,
                IdContenido = item.IdContenido,
                Pestana = item.Pestana,

            }).ToList();

            return View(contentModel);
        }

        public FileResult Imagen_ParaEmpleadores(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> contenido = new List<Contenido>();
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_EMPLEADORES);

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

        public ActionResult Contacto(string pantalla = "")
        {
            Mensaje mensaje = new Mensaje();
            mensaje.Pantalla = pantalla;
            mensaje.DeUsuario = Constantes.NOMBRE_UTP;
            mensaje.DeUsuarioCorreoElectronico = Constantes.CORREO_PRINCIPAL;

            if (pantalla == Constantes.MENSAJES_EMPRESA_CONTACTO)
            {
                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];

                mensaje.DeUsuario = ticketEmpresa.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketEmpresa.CorreoElectronico;
                mensaje.DeUsuarioNombre = ticketEmpresa.Nombre;
            }

            if (pantalla == Constantes.MENSAJES_ALUMNO_CONTACTO)
            {
                TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];

                mensaje.DeUsuario = ticketAlumno.Usuario;
                mensaje.DeUsuarioCorreoElectronico = ticketAlumno.CorreoElectronico;
                mensaje.DeUsuarioNombre = ticketAlumno.Nombre;
            }

            ViewBag.Pantalla = pantalla;

            return View(mensaje);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Contacto(Mensaje mensaje)
        {
            LNMensaje lnMensaje = new LNMensaje();
            if (mensaje.Pantalla == Constantes.MENSAJES_EMPRESA_CONTACTO)
            {
                
                TicketEmpresa ticketEmpresa = (TicketEmpresa)Session["TicketEmpresa"];
                DataTable dtUsuarioAsignado = lnMensaje.ObtenerUsuarioUTPAsignadoAEmpresa(ticketEmpresa.IdEmpresa);

                if (dtUsuarioAsignado.Rows.Count > 0)
                {
                    mensaje.ParaUsuario = Convert.ToString(dtUsuarioAsignado.Rows[0]["Usuario"]);
                    mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtUsuarioAsignado.Rows[0]["CorreoElectronico"]);
                    mensaje.CreadoPor = ticketEmpresa.Usuario;
                }
            }
            else
                if (mensaje.Pantalla == Constantes.MENSAJES_ALUMNO_CONTACTO)
                {
                    TicketAlumno ticketAlumno = (TicketAlumno)Session["TicketAlumno"];

                    //Obtener usuario administrador UTP.                    
                    DataTable dtUsuarioUTPAdmin = lnMensaje.ObtenerUsuarioAdministradorUTP();

                    mensaje.ParaUsuario = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["Usuario"]);
                    mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["CorreoElectronico"]);
                    mensaje.CreadoPor = ticketAlumno.Usuario;
                }
                else
                    if (mensaje.Pantalla == Constantes.MENSAJES_INICIO)
                    {                        
                        //Obtener usuario administrador UTP.                    
                        DataTable dtUsuarioUTPAdmin = lnMensaje.ObtenerUsuarioAdministradorUTP();

                        mensaje.ParaUsuario = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["Usuario"]);
                        mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["CorreoElectronico"]);
                        mensaje.DeUsuario = mensaje.DeUsuarioCorreoElectronico;
                        mensaje.CreadoPor = mensaje.DeUsuarioCorreoElectronico; //Se coloca el correo de la persona.
                    }

            mensaje.FechaEnvio = DateTime.Now;
            mensaje.IdEvento = 0;
            mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido

            
            lnMensaje.Insertar(mensaje);

            TempData["MsjExitoCrearMensaje"] = "El mensaje se envió con éxito";

            return RedirectToAction("Contacto", new { pantalla = mensaje.Pantalla });
        }

        public ActionResult Testimonios()
        {
            ViewBag.Testimonios = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_TESTIMONIOS);
            ViewBag.Opiniones = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_EMPRESAS_OPINAN);
            ViewBag.Casos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_EXITO_IMPARABLE);

            return View();
        }

        public ActionResult Convenios()
        {
            return View();
        
        }
        public FileResult Imagen_Testimonios(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> contenido = new List<Contenido>();
            contenido = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_TESTIMONIOS);

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
            List<Contenido> contenido = new List<Contenido>();
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_AREA_EMPLEABILIDAD);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();


            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
            }
            else
            {
                stream = new MemoryStream();

                var path = System.Web.HttpContext.Current.Server.MapPath(@"~/img/sinimagen.jpg");
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        } 
         

        public ActionResult NoticiasEventos()
        {
            ViewBag.Noticias = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_NOTICIAS);
            ViewBag.Eventos = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_EVENTOS);

            return View();
        }
        
        public FileResult Imagen_Noticia(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> Noticia = new List<Contenido>();
            Noticia = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_NOTICIAS);

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
            Evento = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_EVENTOS);

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
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_SERVICIOS);

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
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(Constantes.CONTENIDO_SERVICIOS);

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

        public ActionResult Conocenos()
        {
            ViewBag.Contenido = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_AREA_EMPLEABILIDAD)[0];
            ViewBag.Staff = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_STAFF);

            return View();
        }

        public ActionResult Alumno()
        {
            ViewBag.Slides = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_ALUMNOS);

            return View();
        }

        public ActionResult NotiEmpleos()
        {
            ViewBag.Contenidos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_NOTICIAS);

            return View();
        }

        public ActionResult LoQueSeViene()
        {
            ViewBag.Contenidos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_SE_VIENE);

            return View();
        }

        public ActionResult LasEmpresasOpinan()
        {
            ViewBag.Contenidos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_EMPRESAS_OPINAN);

            return View();
        }

        public ActionResult CasosDeExitoImparable()
        {
            ViewBag.Contenidos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_EXITO_IMPARABLE);

            return View();
        }

        public ActionResult QueNosCuentan()
        {
            ViewBag.Contenidos = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_NOS_CUENTAN);

            return View();
        }

        public FileResult Imagen(int id, string source)
        {
            List<Contenido> contenido = new List<Contenido>();
            contenido = ln.Contenido_BuscarNoticiasEventosOtros(source);

            Contenido producto = contenido.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
            }
            else
            {
                stream = new MemoryStream();

                var path = System.Web.HttpContext.Current.Server.MapPath(@"~/img/sinimagen.jpg");
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }

        public FileResult Imagen_Staff(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> Noticia = new List<Contenido>();
            Noticia = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_STAFF);

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

        public FileResult Imagen_Imparable(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> Noticia = new List<Contenido>();
            Noticia = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_ALUMNOS);

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

        public ActionResult Empresa()
        {
            ViewBag.Tabs = ln.Contenido_BuscarIndex(Constantes.CONTENIDO_EMPRESAS);

            var tabs = (List<Contenido>)(ViewBag.Tabs);

            return View();
        }

        public JsonResult GetStateProvincia(string IDListaValorPadre)
        {
            LNGeneral lnGeneral = new LNGeneral();
            DataTable dtProvincia = lnGeneral.Home_ListarDistritos(IDListaValorPadre);
            List<SelectListItem> li = new List<SelectListItem>();
            for (int i = 0; i <= dtProvincia.Rows.Count - 1; i++)
            {
                string nombre = dtProvincia.Rows[i]["Valor"].ToString();
                string valor = dtProvincia.Rows[i]["IdListaValor"].ToString();
                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };
                li.Add(item);
            }

            return Json(new SelectList(li, "Value", "Text"));
        }

        public ActionResult Registro()
        {
            LNGeneral lnGeneral = new LNGeneral();
            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor", "PAIPER");
            ViewBag.NumeroEmpleadosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NRO_EMPLEADOS), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial2IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial3IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor");
            ViewBag.SexoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SEXO), "IdListaValor", "Valor");
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor");
            ViewBag.RolIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ROL_USUARIO), "IdListaValor", "Valor");
            ViewBag.EstadoUsuarioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_USUARIO), "IdListaValor", "Valor");
            ViewBag.DireccionDepartamentoLocacion = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_Departamento), "IdListaValor", "Valor");
            ViewBag.DireccionCiudadLocacion = new SelectList(String.Empty, "IdListaValor", "Valor");
            ViewBag.DireccionDistritoLocacion = new SelectList(String.Empty, "IdListaValor", "Valor");

            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Registro(VistaRegistroEmpresa empresa)
        {
            LNUsuario lnUsuario = new LNUsuario();
            StringBuilder mensajeDeError = new StringBuilder();
            if (lnUsuario.ValidarNombreDeUsuario(empresa.CuentaUsuario))
            {
                mensajeDeError.Append("El Nombre de Usuario ya está Registrado<br />");
            }
            if (lnUsuario.ValidarExistenciaEmpresa(empresa.PaisIdListaValor, empresa.IdentificadorTributario))
            {
                mensajeDeError.Append("La Empresa ya se encuentra registrada, por favor comuníquese con nosotros<br />");
            }
            Regex rgx = new Regex(@"^\d{8}$");
            if (empresa.TipoDocumentoIdListaValor == "TDDNI")
	        {
                if (!rgx.IsMatch(empresa.NumeroDocumento))
                {
                    mensajeDeError.Append("El Numero de Documento es inválido<br />");
                }
	        }
            if (ModelState.IsValid && mensajeDeError.ToString() == "")
            {
                LNEmpresa lnEmpresa = new LNEmpresa();
                //Empresa
                empresa.CreadoPor = empresa.CuentaUsuario; //Usuario anónimo.
                empresa.EstadoIdListaValor = "EMPRRV"; //Estado de la empresa pendiente de aprobación.
                //Ubicación
                empresa.EstadoLocacionIdListaValor = "LOSTNO"; //Estado NO ACTIVA. Se debe activar al momento que UTP active la cuenta. 


                if (empresa.PaisIdListaValor == "PAIPER")
                {
                    empresa.NombreLocacion = empresa.DireccionLocacion + ", " + empresa.TextDistrito + ", " + empresa.TextoCiudad + ", " + empresa.TextoDepartamento;
                
                }
                else
                {
                    empresa.NombreLocacion = empresa.DireccionLocacion + ", " + empresa.DireccionDistritoLocacion + ", " + empresa.DireccionCiudadLocacion + ", " + empresa.DireccionDepartamentoLocacion;
                }

                //validar si el pais es peru, si es asi entonces:
                
                //Usuario
                empresa.RolIdListaValor = "ROLEAD"; //La cuenta es creada como Rol: "Administrador de Empresa"
                empresa.EstadoUsuarioIdListaValor = "USEUTP"; //El usuario también se encuenta pendiente de activación. Se debe activar al momento que UTP active la cuenta.
                byte[] bytes = Encoding.Default.GetBytes(empresa.Contrasena);
                SHA1 sha = new SHA1CryptoServiceProvider();
                byte[] password = sha.ComputeHash(bytes);
                String spassword = Encoding.Default.GetString(password);
                empresa.Contrasena = spassword;
                lnEmpresa.Insertar(empresa);

                //Enviar mensaje de correo:
                LNMensaje lnMensaje = new LNMensaje();
                DataTable dtUsuarioUTPAdmin = lnMensaje.ObtenerUsuarioAdministradorUTP();

                if (dtUsuarioUTPAdmin.Rows.Count > 0)
                { 
                    Mensaje mensaje = new Mensaje();
                    mensaje.DeUsuarioCorreoElectronico = empresa.EmailUsuario;
                    mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dtUsuarioUTPAdmin.Rows[0]["CorreoElectronico"]); //Administrador UTP
                    mensaje.Asunto = empresa.NombreComercial + " Empresa registrada en el Portal:";
                    mensaje.MensajeTexto = "La empresa '" + empresa.NombreComercial + "' se ha registrado en el portal y está a la espera de activación";
                    LNCorreo.EnviarCorreo(mensaje);
                }

                //Si el registro fue exitoso redireccionar a página de resultado.
                TempData["GuardaRegistroExitoso"] = "Estimado(a) <strong>" + empresa.NombresUsuario + " " + empresa.ApellidosUsuario
                + "</strong>, muchas gracias por enviarnos su información. En breve recibirá un correo de confirmación con sus datos.</br></br>Nuestro proceso de activación tomará un plazo no mayor a 1 día útil, antes del cual estaremos comunicándole la activación de su Usuario. ";
                //Aquí debería enviarse un correo
                return RedirectToAction("Index");
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                //Variable temporal para poner el break
                int a = 0;
            }

            LNGeneral lnGeneral = new LNGeneral();

            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor", empresa.PaisIdListaValor);
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial1IdListaValor);            
            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor", empresa.TipoLocacionIdListaValor);
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor", empresa.TipoDocumentoIdListaValor);            
            ViewBag.DireccionDepartamentoLocacion = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_Departamento), "IdListaValor", "Valor", empresa.DireccionDepartamentoLocacion);
            ViewBag.DireccionCiudadLocacion = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_Provincia).Where(p => p.IdListaValorPadre == empresa.DireccionDepartamentoLocacion), "IdListaValor", "Valor", empresa.DireccionCiudadLocacion);
            ViewBag.DireccionDistritoLocacion = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_DISTRITO_PERU).Where(d => d.IdListaValorPadre == empresa.DireccionCiudadLocacion), "IdListaValor", "Valor", empresa.DireccionDistritoLocacion);

            ViewBag.MensajeDeError = mensajeDeError;

            return View(empresa);
        }

        /// <summary>
        /// Acción que devuelve la vista de la oferta desde la perspectiva del alumno
        /// En esta llamada no existe alumno.
        /// </summary>
        /// <param name="id">idOferta</param>
        /// <returns></returns>
        public ActionResult VerOferta(string crypt)
        {
            int id = Convert.ToInt32(Helper.Desencriptar(crypt));

            LNAlumno lnAlumno = new LNAlumno();
            LNOferta lnOferta = new LNOferta();
            VistaOfertaAlumno vistaofertalumno = new VistaOfertaAlumno();
                
            vistaofertalumno = lnOferta.OfertaAlumnoPostulacion((int)id, -1); //Se manda -1 porque no existe alumno en esta vista.
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

            return Content("");
        }
    }
}