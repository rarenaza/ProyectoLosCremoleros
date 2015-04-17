using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class CalendarioController : Controller
    {
        //
        // GET: /Calendario/
        public ActionResult VistaCalendario()
        {
            //ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            //service.Credentials = new WebCredentials("criteriaitdev01@criteriait.onmicrosoft.com", "Cr1ter14_2015");
            //service.AutodiscoverUrl("criteriaitdev01@criteriait.onmicrosoft.com", RedirectionUrlValidationCallback);


            ExchangeService service = (ExchangeService)Session["Office365"];
            FindItemsResults<Appointment> foundAppointments =
        service.FindAppointments(WellKnownFolderName.Calendar,
            new CalendarView(DateTime.Now, DateTime.Now.AddYears(1),5));
            
           Calendario calendario = new Calendario();
           List<ItemCalendario> icalendario = new List<ItemCalendario>();
            foreach (Appointment app in foundAppointments)
            {
                ItemCalendario i = new ItemCalendario();
                
                i.Asunto = app.Subject;                
                i.FechaHoraDesde = app.Start;
                i.FechaHoraHasta = app.End;
                i.Ubicacion = app.Location;
                
                icalendario.Add(i);
                
            }
            calendario.ListaItemCalendario = icalendario;
            return PartialView("_VistaCalendario", calendario);
        }  
	}
}