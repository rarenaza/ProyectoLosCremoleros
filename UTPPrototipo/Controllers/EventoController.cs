using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Models.ViewModels.Empresa;

namespace UTPPrototipo.Controllers
{
    public class EventoController : Controller
    {
        LNEvento ad = new LNEvento();
        public PartialViewResult _Eventos(string Pantalla, int idEvento)
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            LNEvento lnEvento = new LNEvento();
            Evento evento = lnEvento.EventoPorUsuario(idEvento, ticket.Usuario);
            ViewBag.Pantalla = Pantalla;
            return PartialView("_Evento", evento);
        }


        //[ChildActionOnly]
        public PartialViewResult _Evento(string Pantalla, int idEvento)
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            LNEvento lnEvento = new LNEvento();
            Evento evento = lnEvento.EventoPorUsuario(idEvento, ticket.Usuario);
            ViewBag.Pantalla = Pantalla;
            return PartialView("_Evento", evento);
        }

        public ActionResult PaginaTicket(int idEvento, int idEventoAsistente, string nombres, string apellidos, string valorTipoDocumento, string numeroDocumento)
        {
            ViewBag.idEvento = idEvento;
            ViewBag.idEventoAsistente = idEventoAsistente;
            ViewBag.nombre = nombres + " " + apellidos;
            ViewBag.documentoIdentidad = valorTipoDocumento + " " + numeroDocumento;
            return View();
        }
<<<<<<< HEAD
=======
        public ActionResult InsertarEventoAsistente(int idEvento, string Pantalla)
        {
            string usuario;
            if (Pantalla == "Alumno")
            {
                TicketAlumno ticket = (TicketAlumno)Session["TicketAlumno"];
                usuario = ticket.Usuario;
                LNEvento lnEvento = new LNEvento();
                lnEvento.InsertarEventoAsistente(idEvento, usuario, usuario);
            }
            if (Pantalla == "Empresa")
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                usuario = ticket.Usuario;
                LNEvento lnEvento = new LNEvento();
                lnEvento.InsertarEventoAsistente(idEvento, usuario, usuario);
            }
            return View("Evento");
        }
        

>>>>>>> 310c018e18774702c98143e4a2441bb3f9c800eb
    }
}