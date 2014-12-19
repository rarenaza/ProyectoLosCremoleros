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
}