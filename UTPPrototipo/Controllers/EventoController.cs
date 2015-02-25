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

    }
}