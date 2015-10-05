using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using UTPPrototipo.Models.ViewModels.Cuenta;
using System.Web.Routing;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using System.IO;

namespace UTPPrototipo.Common
{
    public class Util
    {
        public static string ObtenerSettings(string key)
        {
            string valor = string.Empty;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                valor = ConfigurationManager.AppSettings[key].ToString(); 
            }
            return valor;
        }

        public static string ObtenerRolEmpresa()
        {
            string valor = string.Empty;

            var currentSession = HttpContext.Current.Session;
            var myValue = currentSession["TicketEmpresa"];

            TicketEmpresa ticketUtp = (TicketEmpresa)myValue;
            return ticketUtp.Rol;
        }
    }

    public class VerificarSesion : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["TicketEmpresa"] == null && filterContext.HttpContext.Session["TicketUTP"] == null && filterContext.HttpContext.Session["TicketAlumno"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class RolEmpresa_Lectura : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TicketEmpresa ticket = (TicketEmpresa)filterContext.HttpContext.Session["TicketEmpresa"];
            if (ticket.Rol == "ROLADM") //Rol administrador.
            {
                if (filterContext.Controller.TempData.ContainsKey("keyDemo"))
                {
                    filterContext.Controller.TempData["keyDemo"] = "MensajeDemo";                    
                }
                else
                {
                    filterContext.Controller.TempData.Add("keyDemo", "MensajeDemo");
                }
                
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Empresa", action = "Index" }));
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
           
            base.OnActionExecuting(filterContext);
        }
    }

    public class AutorizarEmpresa : ActionFilterAttribute
    {
        public string Rol { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            TicketEmpresa ticket = (TicketEmpresa)filterContext.HttpContext.Session["TicketEmpresa"];
            string[] listaRoles = Rol.Split(',');
            if (!listaRoles.Contains(ticket.Rol))
            {                
                //Si el usuario autenticado no pertenece al Rol del parámetro => se redirecciona a la página principal.
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Empresa", action = "Index" }));
                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }

            //Caso contrario la ejecución continúa de manera normal.
            base.OnActionExecuting(filterContext);
        }
    }

    public class LogPortal : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //   

        //    base.OnActionExecuting(filterContext);
        //}

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                TicketEmpresa ticketEmpresa = (TicketEmpresa)filterContext.HttpContext.Session["TicketEmpresa"];
                TicketAlumno ticketAlumno = (TicketAlumno)filterContext.HttpContext.Session["TicketAlumno"];
                TicketUTP ticketUTP = (TicketUTP)filterContext.HttpContext.Session["TicketUtp"];
                
                string ip = HttpContext.Current.Request.UserHostAddress; 
                string accion = "";
                string controlador = "";

                //Se obtiene la acción y el controlador:
                var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                if (routeValues != null)
                {
                    if (routeValues.ContainsKey("action"))
                    {
                        accion = routeValues["action"].ToString();
                    }
                    if (routeValues.ContainsKey("controller"))
                    {
                        controlador = routeValues["controller"].ToString();
                    }
                }

                //Se obtiene el usuario autenticado:
                string usuario = ticketEmpresa == null ? (ticketAlumno == null ? ticketUTP.Usuario : ticketAlumno.Usuario) : ticketEmpresa.Usuario;
                Error error = new Error ();
                error.Usuario = usuario;
                error.IP = ip;
                error.Accion = accion;
                error.Controlador = controlador;
                error.ErrorMessage = filterContext.Exception.Message;
                error.ErrorInnerException = filterContext.Exception.InnerException == null ? "" : filterContext.Exception.InnerException.Message;
                error.ErrorSource = filterContext.Exception.Source;
                error.ErrorStackTrace = filterContext.Exception.StackTrace;

                LNLog.InsertarLog(error);
            }
                
            base.OnActionExecuted(filterContext);
        }
    }

    
}