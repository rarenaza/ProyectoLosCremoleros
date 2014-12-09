using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;

namespace UTPPrototipo.Controllers
{
    public class OfertaEstudioController : Controller
    {
        LNOfertaEstudio lnOfertaEstudio = new LNOfertaEstudio();
        private int idOfertaEstudiosTodos = 0; //Al enviar 0 se obtiene todas los estudios de la oferta.

        public ActionResult ObtenerEstudios(int idOferta)
        {
            
            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(idOferta, idOfertaEstudiosTodos);

            return PartialView("_OfertaEstudio", lista);
        }


        public void Insertar(OfertaEstudio ofertaEstudio)
        {
            lnOfertaEstudio.Insertar(ofertaEstudio);
        }


        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaEstudioCrear()
        {
            return PartialView("_OfertaEstudioCrear");
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaEstudioCrear(OfertaEstudio ofertaEstudio)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                ofertaEstudio.IdOferta = 13;
                ofertaEstudio.CicloEstudio = "AWE";
                ofertaEstudio.Estudio = "Estudios";
                ofertaEstudio.TipoDeEstudio.IdListaValor = "ABC";
                ofertaEstudio.EstadoDelEstudio.IdListaValor = "AWE";
                ofertaEstudio.EstadoOfertaEstudio.IdListaValor = "ABC";
                ofertaEstudio.CreadoPor = "admin";

                LNOfertaEstudio lnOfertaEstudio = new LNOfertaEstudio();
                lnOfertaEstudio.Insertar(ofertaEstudio);

                List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(13, idOfertaEstudiosTodos);

                return PartialView("_OfertaEstudio", lista);
            }

            return PartialView("_OfertaEstudioCrear", ofertaEstudio);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaEstudioEditar(int id)
        {
            //Obtener los datos del modelo de editar y pasarlo como parámetro.
            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(0, id);

            return PartialView("_OfertaEstudioEditar", lista[0]);
        }

        [ValidateAntiForgeryToken] // this action takes the viewModel from the modal
        public PartialViewResult _OfertaEstudioEditar(OfertaEstudio ofertaEstudio) //Este es como el submit
        {
            if (ModelState.IsValid)
            {
                //Datos DEMO
                ofertaEstudio.IdOferta = 13;
                ofertaEstudio.CicloEstudio = "AWE2";
                ofertaEstudio.Estudio = "Estudios2";
                ofertaEstudio.TipoDeEstudio.IdListaValor = "ABC2";
                ofertaEstudio.EstadoDelEstudio.IdListaValor = "AWE2";
                ofertaEstudio.EstadoOfertaEstudio.IdListaValor = "ABC2";
                ofertaEstudio.ModificadoPor = "admin2";

                lnOfertaEstudio.Actualizar(ofertaEstudio);

                List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(13, idOfertaEstudiosTodos);  //TODO: Obtener el Id de la oferta.

                return PartialView("_OfertaEstudio", lista);
              
            }

            return PartialView("_OfertaEstudioEditar", ofertaEstudio);
        }
    }
}