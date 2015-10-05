using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Common;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Models.ViewModels.Empresa;
using UTPPrototipo.Utiles;
using System.Configuration;


namespace UTPPrototipo.Controllers
{
    [LogPortal]
    public class EventoController : Controller
    {
        LNEvento ad = new LNEvento();
        public PartialViewResult _Eventos(string Pantalla)
        {
            ViewBag.Pantalla = Pantalla;
            if (Pantalla == "Alumno")
            {
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                LNEvento lnEvento = new LNEvento();
                VistaEventosPorUsuario evento = lnEvento.EventosPorUsuario(ticket.Usuario);
                return PartialView(evento);
            }
            else
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                LNEvento lnEvento = new LNEvento();
                VistaEventosPorUsuario evento = lnEvento.EventosPorUsuario(ticket.Usuario);
                return PartialView(evento);
            }
        }


        //[ChildActionOnly]
        public PartialViewResult _Evento(string Pantalla, string idEvento)
        {
            ViewBag.Pantalla = Pantalla;
            if (Pantalla == "Alumno")
            {
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                LNEvento lnEvento = new LNEvento();
                Evento evento = lnEvento.EventoPorUsuario(Convert.ToInt32(Helper.Desencriptar(idEvento)), ticket.Usuario);
                return PartialView("_Evento", evento);
            }
            else
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                LNEvento lnEvento = new LNEvento();
                Evento evento = lnEvento.EventoPorUsuario(Convert.ToInt32(Helper.Desencriptar(idEvento)), ticket.Usuario);
                return PartialView("_Evento", evento);
            }


        }

        public ActionResult PaginaTicket(string idEvento, string idEventoAsistente, string nombres, string apellidos, string valorTipoDocumento, string numeroDocumento)
        {
            LNEvento evento = new LNEvento();
            evento.ActualizaEstadoTicket(Convert.ToInt32(Helper.Desencriptar(idEventoAsistente)), Constantes.TICKET_EMITIDO);
            ViewBag.idEvento = Convert.ToInt32(Helper.Desencriptar(idEvento));
            ViewBag.idEventoAsistente = Convert.ToInt32(Helper.Desencriptar(idEventoAsistente));
            ViewBag.nombre = Helper.Desencriptar(nombres) + " " + Helper.Desencriptar(apellidos);
            ViewBag.documentoIdentidad = Helper.Desencriptar(valorTipoDocumento) + " " + Helper.Desencriptar(numeroDocumento);
            return View();
        }

        public ActionResult InsertarEventoAsistente(string idEvento, string Pantalla)
        {
            string usuario;
            Evento evento = new Evento();


            ViewBag.Pantalla = Pantalla;

            if (Helper.Desencriptar(Pantalla) == "Alumno")
            {
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                usuario = ticket.Usuario;
                LNEvento lnEvento = new LNEvento();
                lnEvento.InsertarEventoAsistente(Convert.ToInt32(Helper.Desencriptar(idEvento)), usuario, usuario);
                evento = lnEvento.EventoPorUsuario(Convert.ToInt32(Helper.Desencriptar(idEvento)), ticket.Usuario);
                ExchangeService service = (ExchangeService)Session["Office365"];
                CreateAppointment(service, evento.FechaEvento, evento.FechaEventoFin, evento.LugarEvento, evento.NombreEvento , evento.DiasEvento);
                return RedirectToAction("Evento", "Alumno", new { idEvento = idEvento });

            }
            if (Helper.Desencriptar(Pantalla) == "Empresa")
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                usuario = ticket.Usuario;
                LNEvento lnEvento = new LNEvento();
                lnEvento.InsertarEventoAsistente(Convert.ToInt32(Helper.Desencriptar(idEvento)), usuario, usuario);
                return RedirectToAction("Evento", "Empresa", new { idEvento = idEvento });
            }

            return PartialView("_Evento", evento);
        }
        public static void CreateAppointment(ExchangeService service, string fechaInicio, string fechaFin, string lugar, string nombre, string dias)
        {
            if (ConfigurationManager.AppSettings["LogeoProduccion"] != "false")
            {
                Appointment app = new Appointment(service);
                app.Subject = nombre;
                app.Location = lugar;

                DateTime dateInicio = Convert.ToDateTime(fechaInicio);
                DateTime dateFin = Convert.ToDateTime(fechaFin);
                TimeSpan timeSpan = dateFin.Subtract(dateInicio);
                string timeFor = timeSpan.ToString(@"hh\:mm\:ss");
                var time = TimeSpan.Parse(timeFor);
                app.Start = dateInicio;
                app.End = dateInicio.Add(time);
                //-----
                if (dias != "")
                {
                    DayOfTheWeek[] days = stringToDays(dias);
                    app.Recurrence = new Recurrence.WeeklyPattern(app.Start.Date, 1, days);
                    app.Recurrence.StartDate = app.Start.Date;
                    app.Recurrence.EndDate = dateFin;
                }

                app.IsReminderSet = true;
                app.ReminderMinutesBeforeStart = 15;
                app.Save(SendInvitationsMode.SendToAllAndSaveCopy);
            }
        }
        private static DayOfTheWeek[] stringToDays(string dias)
        {
            List<DayOfTheWeek> days = new List<DayOfTheWeek>();
            string[] dia = dias.Split(',');

            for (int i = 0; i < dia.Count(); i++)
            {
                switch (dia[i])
                {
                    case "1":
                        days.Add(DayOfTheWeek.Monday);
                        break;
                    case "2":
                        days.Add(DayOfTheWeek.Tuesday);
                        break;
                    case "3":
                        days.Add(DayOfTheWeek.Wednesday);
                        break;
                    case "4":
                        days.Add(DayOfTheWeek.Thursday);
                        break;
                    case "5":
                        days.Add(DayOfTheWeek.Friday);
                        break;
                    case "6":
                        days.Add(DayOfTheWeek.Saturday);
                        break;
                    case "7":
                        days.Add(DayOfTheWeek.Sunday);
                        break;
                    default:
                        break;
                }
            }
            return days.ToArray();
        }
    }
}