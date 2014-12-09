using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;


namespace UTPPrototipo.Controllers
{
    public class OfertaSectorEmpresarialController : Controller
    {
        LNOfertaSectorEmpresarial lnSector = new LNOfertaSectorEmpresarial();

        public ActionResult ObtenerSectoresEmpresariales(int idOferta)
        {
            List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(idOferta, 0);

            return PartialView("_OfertaSectorEmpresarial", lista);
        }


        public void Insertar(OfertaSectorEmpresarial ofertaSectorEmpresarial)
        {
            lnSector.Insertar(ofertaSectorEmpresarial);
        }


        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaSectorEmpresarialCrear()
        {
            return PartialView("_OfertaSectorEmpresarialCrear");
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaSectorEmpresarialCrear(OfertaSectorEmpresarial ofertaSector)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                ofertaSector.IdOferta = 13;
                ofertaSector.SectorEmpresarial.IdListaValor = "ABE";
                ofertaSector.ExperienciaExcluyente = false;
                ofertaSector.AniosTrabajados = 3;
                ofertaSector.EstadoOfertaSectorEmpresarial.IdListaValor = "AWE";
                ofertaSector.CreadoPor = "admin";

                LNOfertaSectorEmpresarial lnOfertaSector = new LNOfertaSectorEmpresarial();
                lnOfertaSector.Insertar(ofertaSector);

                List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(13, 0);

                return PartialView("_OfertaSectorEmpresarial", lista);
            }

            return PartialView("_OfertaSectorEmpresarialCrear", ofertaSector);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaSectorEmpresarialEditar(int id)
        {
            List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(0, id);

            return PartialView("_OfertaSectorEmpresarialEditar", lista[0]);
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaSectorEmpresarialEditar(OfertaSectorEmpresarial ofertaSector)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                ofertaSector.IdOferta = 13;
                ofertaSector.SectorEmpresarial.IdListaValor = "ABE222";
                ofertaSector.ExperienciaExcluyente = false;
                ofertaSector.AniosTrabajados = 7;
                ofertaSector.EstadoOfertaSectorEmpresarial.IdListaValor = "AWE222";
                ofertaSector.ModificadoPor = "admin22";

                LNOfertaSectorEmpresarial lnOfertaSector = new LNOfertaSectorEmpresarial();
                lnOfertaSector.Actualizar(ofertaSector);

                List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(13, 0);

                return PartialView("_OfertaSectorEmpresarial", lista);
            }

            return PartialView("_OfertaSectorEmpresarialEditar", ofertaSector);
        }
    }
}