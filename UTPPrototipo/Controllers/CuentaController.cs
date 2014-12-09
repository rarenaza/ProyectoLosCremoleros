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

        [HttpPost]
        public ActionResult Autenticar(Usuario usuario)
        {

            List<Usuario> lista = new List<Usuario>();

            DataSet dsResulatdo = ln.Autenticar_Usuario(usuario.NombreUsuario, usuario.Contrasena);

            //DataSet dsResulatdo = ln.Autenticar_Usuario(usuario.NombreUsuario, usuario.Contrasena);
             

            if (dsResulatdo.Tables.Count > 0)
            {
                int existeUsuario = Convert.ToInt32(dsResulatdo.Tables[0].Rows[0]["Existe"]);

                if (existeUsuario == 1)
                {
                    // La creacion del objeto
                    string tipoUsuario = Convert.ToString(dsResulatdo.Tables[1].Rows[0]["TipoUsuario"]);

                    if (tipoUsuario == "USERUT")
                    {
                        //Crear un onbjketo TikcetUTP

                        TicketUTP ticketUtp = new TicketUTP();
                        ticketUtp.Usuario = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Usuario"]);
                        ticketUtp.Nombre = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Nombre"]);
                        ticketUtp.CorreoElectronico = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["CorreoElectronico"]);
                        ticketUtp.TelefonoCelular = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["TelefonoCelular"]);
                        ticketUtp.TipoUsuario = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["TipoUsuario"]);
                        Session["TicketUtp"] = ticketUtp;

                        //REdireccionas al indexl de la uitp
                        return RedirectToAction("Index", "UTP");

                    }
                    if (tipoUsuario == "USEREM")
                    {
                        //Crear un onbjketo TikcetEmpresa

                        TicketEmpresa ticketEmpresa = new TicketEmpresa();
                        ticketEmpresa.Usuario = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Usuario"]);
                        ticketEmpresa.Nombre = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Nombre"]);
                        ticketEmpresa.DNI = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Dni"]);
                        ticketEmpresa.CorreoElectronico = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["CorreoElectronico"]);
                        ticketEmpresa.TelefonoCelular = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["TelefonoCelular"]);
                        ticketEmpresa.TipoUsuario = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["TipoUsuario"]);
                        ticketEmpresa.Idempresa = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["IdEmpresa"]);
                   
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
                        //Crear un onbjketo TikcetUAlumni


                        TicketAlumno ticketAlumno = new TicketAlumno();
                        ticketAlumno.Usuario = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Usuario"]);
                        ticketAlumno.Nombre = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Nombre"]);
                        ticketAlumno.DNI = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["Dni"]);
                        ticketAlumno.CorreoElectronico = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["CorreoElectronico"]);
                        ticketAlumno.TelefonoCelular = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["TelefonoCelular"]);
                        ticketAlumno.TipoUsuario = Convert.ToString(dsResulatdo.Tables[2].Rows[0]["TipoUsuario"]);
                        Session["TicketAlumno"] = ticketAlumno;


                        //REdireccionas al indexl del alumno
                        return RedirectToAction("Index", "Alumno");
                    }

                    //Obtienes el tipo de usiuaor y la 3 teabla
                }
                else
                {
                    ViewBag.Message = "Registrese";
                    //return RedirectToAction("Autenticar", "Cuenta");

                }
            }


            //return PartialView("_Login", usuario);
            return PartialView("_Login", usuario);




            //if (usuario.NombreUsuario == "alumno")
            //{
            //    return RedirectToAction("Index", "Alumno");
            //}
            //else
            //    if (usuario.NombreUsuario == "empresa")  
            //    {
           
            //        Ticket ticket = new Ticket();
            //        ticket.UsuarioNombre = usuario.NombreUsuario;
            //        ticket.IdEmpresa = 1;

            //        Session["Ticket"] = ticket;

            //        return RedirectToAction("Index", "Empresa");
            //    }
            //    else
            //        if (usuario.NombreUsuario == "utp")
            //        {

            //        }

    
            //return PartialView("_Login", usuario);
        }

        

	}
}