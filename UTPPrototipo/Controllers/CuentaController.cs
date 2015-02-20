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

namespace UTPPrototipo.Controllers
{
    [LogPortal]
    public class CuentaController : Controller
    {
        //
        // GET: /Cuenta/
        LNAutenticarUsuario ln = new LNAutenticarUsuario();
        public ActionResult Ingresar()
        {
            return View();
        }

        public ActionResult Autenticar()
        {

            return PartialView("_Login");
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Autenticar(Usuario usuario)
        {
              
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
                    string contrasenaDecodificada = Convert.ToString(dsResultado.Tables[0].Rows[0]["Contrasena"]); //AGRAGAR PROCESO DE DESEMCRIPTAMIENTO
                    bool contrasenaValida = false;
                    if (tipoUsuario == "USERUT" || tipoUsuario == "USERAL")
                    {
                        //4. Si es Alumno o Usuario UTP Validar contra el AD
                        //bool ContrasenaValida = callWebServiceADUTP(usuario.Contrasena) 
                        contrasenaValida = true;
                    }
                    else
                    {
                        if (usuario.Contrasena == contrasenaDecodificada) contrasenaValida = true;
                    }
                    //Si la contraseña es válida, recupera los datos del Usuario de acuerdo al tipo, y contruye la session
                    if (contrasenaValida)
                    {
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
                            //ticketUtp.Rol = Convert.ToString(dsResultado.Tables[2].Rows[0]["Rol"]);

                            Session["TicketUtp"] = ticketUtp;


                            //TempData["ADMINISTRADORUTP"] = ticketUtp.Rol;

                            //ViewBag.mensaje = ticketUtp.Rol;


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

                            Session["TicketEmpresa"] = ticketEmpresa;


                            //TicketEmpresa tcke2 = (TicketEmpresa)Session["TicketEmpresa"];

                            //REdireccionas al indexl de la empresa

                            Ticket ticket = new Ticket();
                            ticket.UsuarioNombre = usuario.NombreUsuario;
                            ticket.IdEmpresa = 1;

                            Session["Ticket"] = ticket;

                            return RedirectToAction("Index", "Empresa");



                        }
                        if (tipoUsuario == "USERAL")
                        {
                            //Valida la contraseña en el AD.


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
                        if (dsDatosAlumno.Tables[0].Rows[0]["PrimerInicioDeSesion"] == DBNull.Value)
                        {
                            //Insertar en [Usuario]
                            lnUTPAlumnos.InsertarDatosDeAlumno(dsDatosAlumno);
                        }

                        TicketAlumno ticketAlumno = new TicketAlumno();
                        ticketAlumno.Usuario = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Codigo"]);
                        ticketAlumno.Nombre = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Nombres"]);
                        ticketAlumno.DNI = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["NumeroDocumento"]);
                        ticketAlumno.CorreoElectronico = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["CorreoInstitucional"]);
                        ticketAlumno.TelefonoCelular = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Celular"]);
                        ticketAlumno.TipoUsuario = "USERAL";
                        ticketAlumno.CodAlumnoUTP = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Codigo"]);
                        ticketAlumno.IdAlumno = Convert.ToInt32(dsDatosAlumno.Tables[0].Rows[0]["IdAlumno"]);

                        Session["TicketAlumno"] = ticketAlumno;

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
            return RedirectToAction("Index","Home");

            
        }        
	}
}
