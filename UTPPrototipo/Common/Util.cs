using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using UTPPrototipo.Models.ViewModels.Cuenta;
using System.Web.Routing;
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
}