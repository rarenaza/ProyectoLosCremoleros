using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Models.ViewModels.Cuenta;

namespace UTPPrototipo.Controllers
{
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

            //1. Validar si el login pertenece a la tabla Alumno.
            //2. Si se encuentra consultar la contraseña del alumno al AD.
            //3. Si el usuario y contraseña con correctas ir a la tabla UTPAlumno y 

            DataSet dsResultado = ln.Autenticar_Usuario(usuario.NombreUsuario, usuario.Contrasena);

            //DataSet dsResulatdo = ln.Autenticar_Usuario(usuario.NombreUsuario, usuario.Contrasena);

            if (dsResultado.Tables.Count > 0)
            {
                int existeUsuario = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["Existe"]);

                if (existeUsuario == 1)
                {
                    // La creacion del objeto
                    string tipoUsuario = Convert.ToString(dsResultado.Tables[1].Rows[0]["TipoUsuario"]);

                    if (tipoUsuario == "USERUT")
                    {
                        //Crear un onbjketo TikcetUTP

                        TicketUTP ticketUtp = new TicketUTP();
                        ticketUtp.Usuario = Convert.ToString(dsResultado.Tables[2].Rows[0]["Usuario"]);
                        ticketUtp.Nombre = Convert.ToString(dsResultado.Tables[2].Rows[0]["Nombre"]);
                        ticketUtp.CorreoElectronico = Convert.ToString(dsResultado.Tables[2].Rows[0]["CorreoElectronico"]);
                        ticketUtp.TelefonoCelular = Convert.ToString(dsResultado.Tables[2].Rows[0]["TelefonoCelular"]);
                        ticketUtp.TipoUsuario = Convert.ToString(dsResultado.Tables[2].Rows[0]["TipoUsuario"]);
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

                        Session["TicketEmpresa"] = ticketEmpresa;


                        //                        TicketEmpresa tcke2 = (TicketEmpresa)Session["TicketEmpresa"];

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
                        ticketAlumno.Usuario = Convert.ToString(dsResultado.Tables[2].Rows[0]["Usuario"]);
                        ticketAlumno.Nombre = Convert.ToString(dsResultado.Tables[2].Rows[0]["Nombre"]);
                        ticketAlumno.DNI = Convert.ToString(dsResultado.Tables[2].Rows[0]["Dni"]);
                        ticketAlumno.CorreoElectronico = Convert.ToString(dsResultado.Tables[2].Rows[0]["CorreoElectronico"]);
                        ticketAlumno.TelefonoCelular = Convert.ToString(dsResultado.Tables[2].Rows[0]["TelefonoCelular"]);
                        ticketAlumno.TipoUsuario = Convert.ToString(dsResultado.Tables[2].Rows[0]["TipoUsuario"]);
                        ticketAlumno.CodAlumnoUTP = Convert.ToString(dsResultado.Tables[2].Rows[0]["CodAlumnoUtp"]);

                        Session["TicketAlumno"] = ticketAlumno;


                        //REdireccionas al indexl del alumno
                        return RedirectToAction("Index", "Alumno");
                    }
                    //return JavaScript("OnSuccess();");

                    //Obtienes el tipo de usiuaor y la 3 teabla
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
                            Session["TicketAlumno"] = ticketAlumno;

                            //Si tiene acceso se redirecciona al portal principal del alumno.
                            return RedirectToAction("Index", "Alumno");
                        }
                      
                    }
                   
                    //ViewBag.Message = "Registrese";
                    //return RedirectToAction("Autenticar", "Cuenta");
                    //return JavaScript("OnFailure();");

                    
          
                    

                }
            }

               
            return PartialView("_Login", usuario);

            
        }        
	}
}
