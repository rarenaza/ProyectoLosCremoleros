using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Common;
using UTPPrototipo.Models.ViewModels.Cuenta;
using CaptchaMvc.HtmlHelpers;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Text;
using Microsoft.Exchange.WebServices.Data;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using System.Security.Cryptography;
using LibCryptoUTP;

namespace UTPPrototipo.Controllers
{
    [LogPortal]
    public class CuentaController : Controller
    {
        //
        // GET: /Cuenta/
        LNAutenticarUsuario ln = new LNAutenticarUsuario();
        LNUsuario lnu = new LNUsuario();
        public ActionResult Ingresar()
        {
            return View();
        }

        public ActionResult Autenticar()
        {            
            return PartialView("_Login");
        }        
        public ActionResult Restablecer()
        {
            return PartialView("RecuperarClave");
        }

        private string Ip()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        public ActionResult CambiarClave(string contrasena, string usuario) 
        {
            DataSet dsResultado = ln.Autenticar_Usuario(usuario);
            string tipoUsuario = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoUsuario"]);
            // Encriptar clave y luego actualizar solo en caso de usuario empresa
            switch (tipoUsuario) {
                case "USEREM":
                    LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                    // encriptacion de la clave del usuario
                    byte[] bytes = Encoding.Default.GetBytes(contrasena);
                    SHA1 sha = new SHA1CryptoServiceProvider();
                    byte[] password = sha.ComputeHash(bytes);
                    String spassword = Encoding.Default.GetString(password);
                    // actualizar nueva clave con la clave encriptada
                    lnEmpresaUsuario.ActualizarContrasena(spassword, usuario);
                    break;
                default:
                    ViewBag.messageError = "Esta funcionalidad es solo para empresas";
                    break;
            }
            // redireccionar a la pagina de inicio
            return RedirectToAction("Index", "Home");
        }
        public ActionResult RecuperarClave()
        {
            return View();        
        }

        public ActionResult GenerarToken(string NombreUsuario, string submitButton, string token)
        {
            LNUsuario lnUsuario = new LNUsuario();
            DataSet dsResultado = ln.Autenticar_Usuario(NombreUsuario);

            string tipoUsuario = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoUsuario"]);

            if (tipoUsuario == "USEREM") {
                switch (submitButton) {
                    case "mail":
                        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                        var random = new Random();
                        var result = new string(
                            Enumerable.Repeat(chars, 8)
                                      .Select(s => s[random.Next(s.Length)])
                                      .ToArray());

                        string ip = Ip();
                        lnUsuario.InsertarToken(result, NombreUsuario, DateTime.Now.AddHours(1), DateTime.Now, ip);
                        
                        Mensaje mensaje = new Mensaje();
                        mensaje.DeUsuarioCorreoElectronico = "empleos@idat.edu.pe";
                        mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dsResultado.Tables[2].Rows[0]["CorreoElectronico"]); //Administrador UTP
                        mensaje.Asunto = "Cambio de Contraseña";
                        mensaje.MensajeTexto = "Estimado(a):" + NombreUsuario + "\r\n\r\n" +
                            "Es grato comunicarnos con usted para informarle que debido la confidencialidad de la información que contiene su cuenta, le hemos generado un token para que valide su información en nuestra intranet.\r\n\r\n" +
                            "-Token: " + result + "\r\n\r\n" +
                            Request.Url.GetLeftPart(UriPartial.Authority)+"/#Token"+
                            "Cordialmente \r\n\r\n" +
                            "Area de TI";
                        LNCorreo.EnviarCorreo(mensaje);
                        TempData["CorreoExitoso"] = "Se envio el TOKEN a las siguientes cuentas: "+mensaje.ParaUsuarioCorreoElectronico;
                        return RedirectToAction("Index", "Home");

                    case "Ingresar":
                        Session["Token"] = lnUsuario.ObtenerToken(NombreUsuario);
                        int id = Convert.ToInt32(dsResultado.Tables[2].Rows[0]["IdEmpresa"]);
                        LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                        List<VistaEmpresaUsuario> list = lnEmpresaUsuario.ObtenerUsuariosPorIdEmpresa(id);
                       
                        EmpresaUsuario empresaUsuario = lnEmpresaUsuario.ObtenerPorIdEmpresaUsuario(Convert.ToInt32(list[0].IdEmpresaUsuario));

                        if (Session["Token"] == null || Session["Token"].ToString() != token)
                        {
                            TempData["TokenNoExitoso"] = "El Token no es correcto.";
                            return RedirectToAction("Index", "Home");
                        }
                        return Json(empresaUsuario);

                    default:
                        return null;
                }
            }

            TempData["TokenNoExitoso"] = "Esta funcionalidad es solo para empresas";
            return RedirectToAction("Index", "Home");
        }
        //-----

        public JsonResult verificarTokenUsuario(string NombreUsuario, string submitButton, string token)
        {
            String strMensaje = "";
            String tipoUsuario = "";
            LNUsuario lnUsuario = new LNUsuario();
            DataSet dsResultado = ln.Autenticar_Usuario(NombreUsuario);

            if ((dsResultado != null) && dsResultado.Tables.Count > 0 && dsResultado.Tables[0].Rows.Count > 0)
                tipoUsuario = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoUsuario"]);

            if (String.IsNullOrEmpty(tipoUsuario))
            {
                strMensaje = "El usuario ingresado no es válido";
            }
            if (tipoUsuario == "USEREM")
            {           
                Session["Token"] = lnUsuario.ObtenerToken(NombreUsuario);
                int id = Convert.ToInt32(dsResultado.Tables[2].Rows[0]["IdEmpresa"]);

                LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario();
                List<VistaEmpresaUsuario> list = lnEmpresaUsuario.ObtenerUsuariosPorIdEmpresa(id);

                EmpresaUsuario empresaUsuario = lnEmpresaUsuario.ObtenerPorIdEmpresaUsuario(Convert.ToInt32(list[0].IdEmpresaUsuario));

                if (Session["Token"] == null || Session["Token"].ToString() != token)
                {
                    strMensaje = "El Token no es correcto.";                
                }
            }
            if (tipoUsuario != "USEREM" && !String.IsNullOrEmpty(tipoUsuario))
            {
                strMensaje = "Esta funcionalidad es solo para empresas";
            }
            return Json(strMensaje);           
        }

        public JsonResult generarTokenUsuario(string NombreUsuario, string submitButton, string token)
        {
            String strMensaje = "";
            String tipoUsuario = "";
            LNUsuario lnUsuario = new LNUsuario();
            DataSet dsResultado = ln.Autenticar_Usuario(NombreUsuario);            

            if ((dsResultado != null) && dsResultado.Tables.Count>0 && dsResultado.Tables[0].Rows.Count>0)
                tipoUsuario = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoUsuario"]);

            if (String.IsNullOrEmpty(tipoUsuario))
            {
                strMensaje = "El usuario ingresado no es válido";
            }       
            if (tipoUsuario == "USEREM")
            {

                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var result = new string(
                    Enumerable.Repeat(chars, 8)
                              .Select(s => s[random.Next(s.Length)])
                              .ToArray());

                string ip = Ip();
                lnUsuario.InsertarToken(result, NombreUsuario, DateTime.Now.AddHours(1), DateTime.Now, ip);

                Mensaje mensaje = new Mensaje();
                mensaje.DeUsuarioCorreoElectronico = "empleos@idat.edu.pe";
                mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(dsResultado.Tables[2].Rows[0]["CorreoElectronico"]); //Administrador UTP
                mensaje.Asunto = "Cambio de Contraseña";
                mensaje.MensajeTexto = "Estimado(a):" + NombreUsuario + "\r\n\r\n" +
                    "Es grato comunicarnos con usted para informarle que debido la confidencialidad de la información que contiene su cuenta, le hemos generado un token para que valide su información en nuestra intranet.\r\n\r\n" +
                    "-Token: " + result + "\r\n\r\n" +
                    "Cordialmente \r\n\r\n" +
                    "Area de TI";
                LNCorreo.EnviarCorreo(mensaje);
                //strMensaje = "Se envio el TOKEN a las siguientes cuentas: " + mensaje.ParaUsuarioCorreoElectronico;                        
                strMensaje = "El token ha sido enviado a su correo electrónico.";               
            }
            if (tipoUsuario != "USEREM" && !String.IsNullOrEmpty(tipoUsuario))
            {
                strMensaje = "Esta funcionalidad es solo para empresas";
            }
          
            return Json(strMensaje);
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            StringBuilder captcha = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                captcha.Append(generarCaracterAzar());
                captcha.Append(" ");
            }
            //store answer 
            string sb = captcha.ToString().Replace(" ", "");
            Session["Captcha" + prefix] = sb.ToLower();

            //image stream 
            FileContentResult img = null;

            Bitmap bmp = new Bitmap(238, 90);
            Graphics grafico = Graphics.FromImage(bmp);

            Brush brush = (Brush)new SolidBrush(ColorTranslator.FromHtml("#000000"));
            Brush brush2 = (Brush)new SolidBrush(ColorTranslator.FromHtml("#FFF"));

            grafico.Clear(Color.Gray);

            Pen p = new Pen(Color.White, 1);

            int[] x_ini = new int[10];
            x_ini[0] = new Random().Next(0, 100);
            x_ini[1] = new Random(x_ini[0]).Next(0, 100);
            x_ini[2] = new Random(x_ini[1]).Next(0, 100);
            x_ini[3] = new Random(x_ini[2]).Next(0, 100);
            x_ini[4] = new Random(x_ini[3]).Next(0, 100);
            x_ini[5] = new Random(x_ini[4]).Next(0, 100);
            x_ini[6] = new Random(x_ini[5]).Next(0, 100);
            x_ini[7] = new Random(x_ini[6]).Next(0, 100);
            x_ini[8] = new Random(x_ini[7]).Next(0, 100);
            x_ini[9] = new Random(x_ini[8]).Next(0, 100);

            int[] y_ini = new int[10];
            y_ini[0] = new Random(10).Next(0, 90);
            y_ini[1] = new Random(y_ini[0]).Next(0, 90);
            y_ini[2] = new Random(y_ini[1]).Next(0, 90);
            y_ini[3] = new Random(y_ini[2]).Next(0, 90);
            y_ini[4] = new Random(y_ini[3]).Next(0, 90);
            y_ini[5] = new Random(y_ini[4]).Next(0, 90);
            y_ini[6] = new Random(y_ini[5]).Next(0, 90);
            y_ini[7] = new Random(y_ini[6]).Next(0, 90);
            y_ini[8] = new Random(y_ini[7]).Next(0, 90);
            y_ini[9] = new Random(y_ini[8]).Next(0, 90);


            int[] x_fin = new int[10];
            x_fin[0] = new Random().Next(0, 100);
            x_fin[1] = new Random(x_fin[0]).Next(150, 238);
            x_fin[2] = new Random(x_fin[1]).Next(150, 238);
            x_fin[3] = new Random(x_fin[2]).Next(150, 238);
            x_fin[4] = new Random(x_fin[3]).Next(150, 238);
            x_fin[5] = new Random(x_fin[4]).Next(150, 238);
            x_fin[6] = new Random(x_fin[5]).Next(150, 238);
            x_fin[7] = new Random(x_fin[6]).Next(150, 238);
            x_fin[8] = new Random(x_fin[7]).Next(150, 238);
            x_fin[9] = new Random(x_fin[8]).Next(150, 238);

            int[] y_fin = new int[10];
            y_fin[0] = new Random(50).Next(0, 90);
            y_fin[1] = new Random(y_fin[0]).Next(0, 90);
            y_fin[2] = new Random(y_fin[1]).Next(0, 90);
            y_fin[3] = new Random(y_fin[2]).Next(0, 90);
            y_fin[4] = new Random(y_fin[3]).Next(0, 90);
            y_fin[5] = new Random(y_fin[4]).Next(0, 90);
            y_fin[6] = new Random(y_fin[5]).Next(0, 90);
            y_fin[7] = new Random(y_fin[6]).Next(0, 90);
            y_fin[8] = new Random(y_fin[7]).Next(0, 90);
            y_fin[9] = new Random(y_fin[8]).Next(0, 90);



            grafico.DrawLine(p, new Point(x_ini[0], y_ini[0]), new Point(x_fin[0], y_fin[0]));
            grafico.DrawLine(p, new Point(x_ini[1], y_ini[1]), new Point(x_fin[1], y_fin[1]));
            grafico.DrawLine(p, new Point(x_ini[2], y_ini[2]), new Point(x_fin[2], y_fin[2]));
            grafico.DrawLine(p, new Point(x_ini[3], y_ini[3]), new Point(x_fin[3], y_fin[3]));
            grafico.DrawLine(p, new Point(x_ini[4], y_ini[4]), new Point(x_fin[4], y_fin[4]));

            grafico.DrawEllipse(p, x_ini[5], y_ini[5], 20, 20);
            grafico.DrawEllipse(p, x_fin[6], y_fin[6], 10, 10);
            grafico.DrawEllipse(p, x_ini[7], y_ini[7], 30, 30);

            StringFormat drawFormat = new StringFormat();
            drawFormat.LineAlignment = StringAlignment.Center;
            drawFormat.Alignment = StringAlignment.Center;
            grafico.DrawString(captcha.ToString(), new Font("Arial", 20, FontStyle.Bold), Brushes.Black, 100, 12 * (new Random().Next(2, 6)), drawFormat);
            //captcha = sb.ToString().Replace(" ", "");
            using (MemoryStream ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(ms.GetBuffer(), "image/Jpeg");
            }

            return img;
        }

        private object generarCaracterAzar()
        {
            Random oAzar = new Random();
            int n = oAzar.Next(26) + 65;
            System.Threading.Thread.Sleep(15);
            return ((char)n);
        }

        static bool RedirectionUrlValidationCallback(String redirectionUrl)
        {
            bool redirectionValidated = false;
            if (redirectionUrl.Equals(
                "https://autodiscover-s.outlook.com/autodiscover/autodiscover.xml"))
                redirectionValidated = true;

            return redirectionValidated;
        }

        private void autenticarExchange(Usuario usuario)
        {
            bool enableExchange = Convert.ToBoolean(ConfigurationManager.AppSettings["LogeoProduccion"]);

            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);

            Session["ResultadoLogeo"] = "";
            if (enableExchange == false)
            {
                try
                {
                    service.Credentials = new WebCredentials("criteriaitdev02@criteriait1.onmicrosoft.com", "Cr1ter14_2015");
                    service.AutodiscoverUrl("criteriaitdev02@criteriait1.onmicrosoft.com", RedirectionUrlValidationCallback);
                }
                catch (Exception)
                {
                    Session["ResultadoLogeo"] = "No se pudo conectar al Office 365";
                }

            }
            else
            {
                try
                {
                    service.Credentials = new WebCredentials(usuario.NombreUsuario + "@idat.edu.pe", usuario.Contrasena);
                    service.AutodiscoverUrl(usuario.NombreUsuario + "@idat.edu.pe", RedirectionUrlValidationCallback);
                }
                catch (Exception)
                {
                    Session["ResultadoLogeo"] = "No se pudo conectar al Office 365";
                }
            }

            service.UseDefaultCredentials = false;

            Session["Office365"] = service;
        }

        /// <summary>
        /// Valida si es empleado UTP.
        /// </summary>
        /// <param name="usuario">Usuario a validar contra el AD</param>
        /// <param name="contrasenia">Contraseña AD</param>
        /// <returns></returns>
        public bool ValidadADEmpleado(string usuario, string contrasenia)
        {
            int intResult;
            string contraseniaEnc;

            EncriptaUTP oEncriptaUTP = new EncriptaUTP();

            contraseniaEnc = oEncriptaUTP.EncriptaAES128(contrasenia.ToString(), System.Configuration.ConfigurationManager.AppSettings["KEY_ENCRYPT"]);

            //ServiceReference.ServiceADSoapClient ws = new ServiceReference.ServiceADSoapClient();

            intResult = 1;//ws.validaUsuarioAD(usuario, contraseniaEnc, System.Configuration.ConfigurationManager.AppSettings["strParam"]);

            if (intResult == 1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Valida al alumno contra el AD. Adicionalmente, valida si es egresado o matriculado
        /// </summary>
        /// <param name="usuario">Usuario del AD</param>
        /// <param name="contrasenia">Contraseña del AD</param>
        /// <returns></returns>
        public bool ValidadADAlumno(string usuario, string contrasenia)
        {

            int intResult;
            string contraseniaEnc;

            EncriptaUTP oEncriptaUTP = new EncriptaUTP();

            contraseniaEnc = oEncriptaUTP.EncriptaAES128(contrasenia.ToString(), System.Configuration.ConfigurationManager.AppSettings["KEY_ENCRYPT"]);

            //ServiceReference.ServiceADSoapClient ws = new ServiceReference.ServiceADSoapClient();

            intResult = 1;//ws.validaUsuarioAD(usuario, contraseniaEnc, System.Configuration.ConfigurationManager.AppSettings["strParam"]);

            if (intResult == 1)
            {
                //DataTable dtResultado = ln.ValidarAlumno(usuario);

                //if (dtResultado.Rows[0]["CANTIDAD"].ToString() == "1")
                    return true;
                //else
                //    return false;
            }
            else
                return false;
        }

        //------
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Autenticar(Usuario usuario)
        {
            Session["TerminosCondiciones"] = false;

            if (Session["Captcha"] == null || usuario.Captcha ==null || Convert.ToString(Session["Captcha"]) != Convert.ToString(usuario.Captcha).ToLower())
            {
                TempData["UsuarioNoExitoso"] = "El texto no coincide con la imagen";
                //ModelState.AddModelError("Captcha", "Wrong value of sum, please try again.");
                return RedirectToAction("Index", "Home");
            }
            
            List<Usuario> lista = new List<Usuario>();

            //1. Recuperar datos de Usuario
            DataSet dsResultado = ln.Autenticar_Usuario(usuario.NombreUsuario);
            //2. Si el Usuario existe en el Portal validar que está activo
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                string estadoUsuario = Convert.ToString(dsResultado.Tables[0].Rows[0]["EstadoUsuario"]);

                if (estadoUsuario == "USEMAC")
                {

                    // 3. Si está activo Validar su Contraseña
                    string tipoUsuario = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoUsuario"]);
                    string contrasenaDecodificada = Convert.ToString(dsResultado.Tables[0].Rows[0]["Contrasena"]); //AGREGAR PROCESO DE DESEMCRIPTAMIENTO
                    bool contrasenaValida = false;
                    //4. Si es Alumno o Usuario UTP Validar contra el AD
                    if (tipoUsuario == "USERUT" || tipoUsuario == "USERAL")
                    {
                        if (tipoUsuario == "USERUT")
                            if (ValidadADEmpleado(usuario.NombreUsuario, usuario.Contrasena))
                                contrasenaValida = true;
                        if (tipoUsuario == "USERAL")
                            if (ValidadADAlumno(usuario.NombreUsuario, usuario.Contrasena))
                            {
                                contrasenaValida = true;
                                autenticarExchange(usuario);
                            }
                    }
                    else
                    {
                        byte[] bytes = Encoding.Default.GetBytes(usuario.Contrasena);
                        SHA1 sha = new SHA1CryptoServiceProvider();
                        byte[] password = sha.ComputeHash(bytes);
                        String spassword = Encoding.Default.GetString(password);
                        // This is one implementation of the abstract class SHA1.
                        if (contrasenaDecodificada == spassword) contrasenaValida = true;
                    }
                    //Si la contraseña es válida, recupera los datos del Usuario de acuerdo al tipo, y contruye la session
                    if (contrasenaValida)
                    {
                        Session["Rol"] = tipoUsuario;
                        if (tipoUsuario == "USERUT")
                        {
                            //Crear un onbjketo TikcetUTP

                            TicketUTP ticketUtp = new TicketUTP();
                            ticketUtp.Usuario = Convert.ToString(dsResultado.Tables[3].Rows[0]["Usuario"]);
                            ticketUtp.Nombre = Convert.ToString(dsResultado.Tables[3].Rows[0]["Nombre"]);
                            ticketUtp.CorreoElectronico = Convert.ToString(dsResultado.Tables[3].Rows[0]["CorreoElectronico"]);
                            ticketUtp.TelefonoCelular = Convert.ToString(dsResultado.Tables[3].Rows[0]["TelefonoCelular"]);
                            ticketUtp.TipoUsuario = Convert.ToString(dsResultado.Tables[3].Rows[0]["TipoUsuario"]);

                            ////agrege este campo 
                            ticketUtp.Rol = Convert.ToString(dsResultado.Tables[0].Rows[0]["Rol"]);

                            Session["TicketUtp"] = ticketUtp;
 
                            //REdireccionas al indexl de la uitp
                            return RedirectToAction("Index", "UTP");

                        }
                        if (tipoUsuario == "USEREM")
                        {
                            //Crear un onbjketo TikcetEmpresa

                            TicketEmpresa ticketEmpresa = new TicketEmpresa();
                            ticketEmpresa.Usuario = Convert.ToString(dsResultado.Tables[2].Rows[0]["Usuario"]);
                            ticketEmpresa.Nombre = Convert.ToString(dsResultado.Tables[2].Rows[0]["Nombre"]);
                            ticketEmpresa.DNI = Convert.ToString(dsResultado.Tables[2].Rows[0]["Dni"]);
                            ticketEmpresa.CorreoElectronico = Convert.ToString(dsResultado.Tables[2].Rows[0]["CorreoElectronico"]);
                            ticketEmpresa.TelefonoCelular = Convert.ToString(dsResultado.Tables[2].Rows[0]["TelefonoCelular"]);
                            ticketEmpresa.TipoUsuario = Convert.ToString(dsResultado.Tables[2].Rows[0]["TipoUsuario"]);
                            ticketEmpresa.IdEmpresa = Convert.ToInt32(dsResultado.Tables[2].Rows[0]["IdEmpresa"]);
                            ticketEmpresa.Rol = Convert.ToString(dsResultado.Tables[2].Rows[0]["Rol"]);
                            ticketEmpresa.EstadoEmpresa = Convert.ToString(dsResultado.Tables[2].Rows[0]["EstadoEmpresa"]);
                            if (ticketEmpresa.EstadoEmpresa == "EMPRAC")
                            {
                                Session["TicketEmpresa"] = ticketEmpresa;
                                Session["TerminosCondiciones"] = dsResultado.Tables[0].Rows[0]["TerminosCondiciones"] == DBNull.Value ? false : Convert.ToBoolean(
                                   Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TerminosCondiciones"])
                               );

                                Ticket ticket = new Ticket();
                                ticket.UsuarioNombre = usuario.NombreUsuario;
                                ticket.IdEmpresa = 1;

                                Session["Ticket"] = ticket;
                               

                                return RedirectToAction("Index", "Empresa");
                            }
                            else
                            {
                                if (ticketEmpresa.EstadoEmpresa == "EMPRNO") TempData["UsuarioNoExitoso"] = "Empresa Desactivada, comuníquese con su Administrador";
                                if (ticketEmpresa.EstadoEmpresa == "EMPRSU") TempData["UsuarioNoExitoso"] = "Empresa Suspendida, comuníquese con su Administrador";
                            }

                        }
                        if (tipoUsuario == "USERAL")
                        {
                            TicketAlumno ticketAlumno = new TicketAlumno();
                            ticketAlumno.Usuario = Convert.ToString(dsResultado.Tables[1].Rows[0]["Usuario"]);
                            ticketAlumno.Nombre = Convert.ToString(dsResultado.Tables[1].Rows[0]["Nombre"]);
                            ticketAlumno.DNI = Convert.ToString(dsResultado.Tables[1].Rows[0]["Dni"]);
                            ticketAlumno.CorreoElectronico = Convert.ToString(dsResultado.Tables[1].Rows[0]["CorreoElectronico"]);
                            ticketAlumno.TelefonoCelular = Convert.ToString(dsResultado.Tables[1].Rows[0]["TelefonoCelular"]);
                            ticketAlumno.TipoUsuario = Convert.ToString(dsResultado.Tables[1].Rows[0]["TipoUsuario"]);
                            ticketAlumno.CodAlumnoUTP = Convert.ToString(dsResultado.Tables[1].Rows[0]["CodAlumnoUtp"]);
                            ticketAlumno.IdAlumno = Convert.ToInt32(dsResultado.Tables[1].Rows[0]["IdAlumno"]);

                            Session["TicketAlumno"] = ticketAlumno;
                            Session["TerminosCondiciones"] = dsResultado.Tables[0].Rows[0]["TerminosCondiciones"] == DBNull.Value ? false : Convert.ToBoolean(
                                Convert.ToInt32(dsResultado.Tables[0].Rows[0]["TerminosCondiciones"])
                            );

                            //REdireccionas al indexl del alumno
                            return RedirectToAction("Index", "Alumno");
                        }
                    }
                    else
                    {
                        //Mensaje de error de contraseña
                        TempData["UsuarioNoExitoso"] = "Contraseña inválida";
                    }

                }
                else
                {
                    //Mensaje de error de Usuario no activo
                    if (estadoUsuario == "USEMFI") TempData["UsuarioNoExitoso"] = "Usuario Deshabilitado, comuníquese con su Administrador";
                    if (estadoUsuario == "USEMNO") TempData["UsuarioNoExitoso"] = "Usuario Desactivado, comuníquese con su Administrador";
                    if (estadoUsuario == "USEUTP") TempData["UsuarioNoExitoso"] = "Usuario pendiente de Validación por UTP";
                }
            }
            else
            {
                //Si no existe se busca en la tabla UTPAlumno

                //Buscar al usuario en UTPAlumno
                LNUTPAlumnos lnUTPAlumnos = new LNUTPAlumnos();
                DataSet dsDatosAlumno = lnUTPAlumnos.ObtenerDatosPorCodigo(usuario.NombreUsuario);

                if (dsDatosAlumno.Tables[0].Rows.Count > 0)
                {
                    //Si la tabla contiene filas => sí existe el alumno en la tabla UTPAlumnos.

                    //1. Validar la contraseña con el AD
                    bool tieneAcceso = false;
                    //bool tieneAcceso = callWebServiceADUTP(usuario.Contrasena)                   
                    tieneAcceso = true;

                    if (tieneAcceso)
                    {
                        //2. Leer el campo PrimerInicioDeSesion. Si es nulo entonces insertar la información en Usuario, Alumno y AlumnoEstudio.
                        //3. Actualizar el campo PrimerInicioDeSesion a 1.
                        int idAlumno = 0;
                        if (dsDatosAlumno.Tables[0].Rows[0]["PrimerInicioDeSesion"] == DBNull.Value)
                        {
                            //Insertar en [Usuario]
                            idAlumno = lnUTPAlumnos.InsertarDatosDeAlumno(dsDatosAlumno);

                        }

                        TicketAlumno ticketAlumno = new TicketAlumno();
                        ticketAlumno.Usuario = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Codigo"]);
                        ticketAlumno.Nombre = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Nombres"]) + " " + Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Apellidos"]);
                        ticketAlumno.DNI = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["NumeroDocumento"]);
                        ticketAlumno.CorreoElectronico = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["CorreoInstitucional"]);
                        ticketAlumno.TelefonoCelular = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Celular"]);
                        ticketAlumno.TipoUsuario = "USERAL";
                        ticketAlumno.CodAlumnoUTP = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Codigo"]);
                        ticketAlumno.IdAlumno = idAlumno;

                        Session["TicketAlumno"] = ticketAlumno;

                        autenticarExchange(usuario);
                        //Si tiene acceso se redirecciona al portal principal del alumno.
                        return RedirectToAction("Index", "Alumno");
                    }
                    else
                    {
                        //Mensaje de error de contraseña
                        TempData["UsuarioNoExitoso"] = "Contraseña inválida";
                    }


                }
                else
                {
                    //Mensaje de error de contraseña
                    TempData["UsuarioNoExitoso"] = "Usuario o Contraseña inválida";
                }

                //ViewBag.Message = "Registrese";
                //return RedirectToAction("Autenticar", "Cuenta");
                //return JavaScript("OnFailure();");

            }



            //return PartialView("_Login", usuario);
            //TempData["UsuarioNoExitoso"] = usuario.NombreUsuario;
            //Aquí debería enviarse un correo
            return RedirectToAction("Index", "Home");


        }

        public JsonResult ActualizarTerminosCondiciones(string usuario)
        {
            Session["TerminosCondiciones"] = true;

            return Json(lnu.ActualizarTerminosCondiciones(usuario), JsonRequestBehavior.AllowGet);
        }
    }
}
