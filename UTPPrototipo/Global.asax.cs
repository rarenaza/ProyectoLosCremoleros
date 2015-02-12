using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace UTPPrototipo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Se establece la cultura para Perú            
            //Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-PE");

            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Mensajes";
            DefaultModelBinder.ResourceClassKey = "Mensajes";

            Thread.CurrentThread.CurrentCulture =  CultureInfo.CreateSpecificCulture("es-PE");
            Thread.CurrentThread.CurrentUICulture = new  CultureInfo("es-PE");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
