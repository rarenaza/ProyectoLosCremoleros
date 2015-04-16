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
        static bool RedirectionUrlValidationCallback(String redirectionUrl)
        {
            bool redirectionValidated = false;
            if (redirectionUrl.Equals(
                "https://autodiscover-s.outlook.com/autodiscover/autodiscover.xml"))
                redirectionValidated = true;

            return redirectionValidated;
        }

        public ActionResult OpenOWAFromWebClientReadFormQueryString()
        {
            
            ExchangeService service = (ExchangeService)Session["Office365"];
            
            // Create a view that will return the identifier for the first item in a folder.
            ItemView itemView = new ItemView(1);
            itemView.PropertySet = new PropertySet(BasePropertySet.IdOnly);

            // Make a call to EWS to get one item from the mailbox folder.
            FindItemsResults<Item> results = service.FindItems(WellKnownFolderName.Drafts, itemView);

            // Get the item identifier of the item to open in Outlook Web App.
            ItemId itemId = results.Items[0].Id;

            ExchangeServerInfo serverInfo = service.ServerInfo;
            var owaReadFormQueryString = string.Empty;
            var ewsIdentifer = itemId.UniqueId;

            try
            {
                // Make a call to EWS to bind to the item returned by the FindItems method.
                EmailMessage msg = EmailMessage.Bind(service, itemId);
                msg.Load(new PropertySet(BasePropertySet.IdOnly, EmailMessageSchema.WebClientReadFormQueryString));

                // Versions of Exchange starting with major version 15 and ending with Exchange Server 2013 build 15.0.775.09
                // returned a different query string fragment. This optional check is not required for applications that
                // target Exchange Online.
                if ((serverInfo.MajorVersion == 15) && (serverInfo.MajorBuildNumber < 775) && (serverInfo.MinorBuildNumber < 09))
                {
                    // If your client is connected to an Exchange 2013 server that has not been updated to CU3,
                    // this query string will be returned.
                    owaReadFormQueryString = string.Format("#viewmodel=_y.$Ep&ItemID={0}",
                      System.Web.HttpUtility.UrlEncode(ewsIdentifer, Encoding.UTF8));
                }
                else
                {
                    // If your client is connected to an Exchanger 2010, Exchange 2013 CU3, or Exchange Online server,
                    // the WebClientReadFormQueryString is used.
                    owaReadFormQueryString = msg.WebClientReadFormQueryString;
                }

                // Create the URL that Outlook Web App uses to open the email message.
                Uri url = service.Url;
                string owaReadAccessUrl = string.Format("{0}://{1}/owa/{2}",
                  url.Scheme, url.Host, owaReadFormQueryString);

                if (!string.IsNullOrEmpty(owaReadAccessUrl))
                {
                    System.Diagnostics.Process.Start("IEXPLORE.EXE", owaReadAccessUrl);
                }
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("ERRROR: Internet Explorer cannot be found.");
            }
            return View();
        }
	}
}