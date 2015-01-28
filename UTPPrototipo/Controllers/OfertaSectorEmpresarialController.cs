using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Common;


namespace UTPPrototipo.Controllers
{
    [LogPortal]
    public class OfertaSectorEmpresarialController : Controller
    {
        LNOfertaSectorEmpresarial lnSector = new LNOfertaSectorEmpresarial();
        LNGeneral lnGeneral = new LNGeneral();

        public ActionResult ObtenerSectoresEmpresariales(int idOferta)
        {
            //Se guarda el Id de la oferta para que sea pasado como parámetro al evento de crear sector.
            ViewBag.IdOferta = idOferta;

            List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(idOferta, 0);

            return PartialView("_OfertaSectorEmpresarial", lista);
        }


        public void Insertar(OfertaSectorEmpresarial ofertaSectorEmpresarial)
        {
            lnSector.Insertar(ofertaSectorEmpresarial);
        }


        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaSectorEmpresarialCrear(int id)
        {
            ViewBag.SectorEmpresarialIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            
            OfertaSectorEmpresarial ofertaSector = new OfertaSectorEmpresarial();
            ofertaSector.IdOferta = id;

            return PartialView("_OfertaSectorEmpresarialCrear", ofertaSector);
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaSectorEmpresarialCrear(OfertaSectorEmpresarial ofertaSector)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];                

                //ofertaSector.IdOferta = 13;
                //ofertaSector.SectorEmpresarial.IdListaValor = "ABE";
                //ofertaSector.ExperienciaExcluyente = false;
                //ofertaSector.AniosTrabajados = 3;
                ofertaSector.EstadoOfertaSectorEmpresarial.IdListaValor = "OFSEAC"; //Oferrta sector empresarial activa.
                ofertaSector.CreadoPor = ticket.Usuario;

                LNOfertaSectorEmpresarial lnOfertaSector = new LNOfertaSectorEmpresarial();
                lnOfertaSector.Insertar(ofertaSector);

                List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(ofertaSector.IdOferta, 0);

                ViewBag.IdOferta = ofertaSector.IdOferta;
                return PartialView("_OfertaSectorEmpresarial", lista);
            }
            else
            {
                //Código para ubicar los errores en el ModelState.
                var errors = ModelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .ToList();

                int a = 0;
            }
            return PartialView("_OfertaSectorEmpresarialCrear", ofertaSector);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaSectorEmpresarialEditar(int id)
        {           
            List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(0, id);

            OfertaSectorEmpresarial ofertaSector = lista[0];

            ViewBag.SectorEmpresarialIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", ofertaSector.SectorEmpresarialIdListaValor);

            return PartialView("_OfertaSectorEmpresarialEditar", ofertaSector);
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaSectorEmpresarialEditar(OfertaSectorEmpresarial ofertaSector)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                
                //ofertaSector.IdOferta = 13;
                //ofertaSector.SectorEmpresarial.IdListaValor = "ABE222";
                //ofertaSector.ExperienciaExcluyente = false;
                //ofertaSector.AniosTrabajados = 7;
                //ofertaSector.EstadoOfertaSectorEmpresarial.IdListaValor = "AWE222";
                ofertaSector.ModificadoPor = ticket.Usuario;

                LNOfertaSectorEmpresarial lnOfertaSector = new LNOfertaSectorEmpresarial();
                lnOfertaSector.Actualizar(ofertaSector);

                List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(ofertaSector.IdOferta, 0);

                ViewBag.IdOferta = ofertaSector.IdOferta;
                return PartialView("_OfertaSectorEmpresarial", lista);
            }

            //TODO: Validar cuando existe error al grabar.
            return PartialView("_OfertaSectorEmpresarialEditar", ofertaSector);
        }

        [HttpGet]
        public PartialViewResult _OfertaSectorEmpresarialEliminar(int id)
        {
            List<OfertaSectorEmpresarial> listaSectores = lnSector.ObtenerSectoresEmpresariales(0, id);

            OfertaSectorEmpresarial ofertaSector = listaSectores[0];

            this.lnSector.Eliminar(id);

            List<OfertaSectorEmpresarial> lista = lnSector.ObtenerSectoresEmpresariales(ofertaSector.IdOferta, 0); 

            ViewBag.IdOferta = ofertaSector.IdOferta;

            return PartialView("_OfertaSectorEmpresarial", lista);
        }
    }
}