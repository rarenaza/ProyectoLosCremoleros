using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;

namespace UTPPrototipo.Controllers
{
    public class OfertaInformacionAdicionalController : Controller
    {
        LNOfertaInformacionAdicional lnInfoAdicional = new LNOfertaInformacionAdicional();
        LNGeneral lnGeneral = new LNGeneral();

        public ActionResult ObtenerInformacionAdicional(int idOferta)
        {
            //Se guarda el Id de la oferta
            ViewBag.IdOferta = idOferta;

            List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(idOferta, 0);
            
            ViewBag.ListaTipoConocimiento = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_OTRO_CONOCIMIENTO), "IdListaValor", "Valor");
            
            //lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_OTRO_CONOCIMIENTO);

            return PartialView("_OfertaInformacionAdicional",lista);
        }

        public void InsertarInformacionAdicional(OfertaInformacionAdicional ofertaInformacionAdicional)
        {
           lnInfoAdicional.Insertar(ofertaInformacionAdicional);
        }


        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaInformacionAdicionalCrear(int id)
        {
            //Se cargan los combos del tipo.
            ViewBag.TipoConocimientoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_OTRO_CONOCIMIENTO), "IdListaValor", "Valor");
            ViewBag.NivelConocimientoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NIVEL_CONOCIMIENTOS).OrderBy(m => m.Peso), "IdListaValor", "Valor");

            OfertaInformacionAdicional ofertaInfoAdicional = new OfertaInformacionAdicional();
            ofertaInfoAdicional.IdOferta = id;

            return PartialView("_OfertaInformacionAdicionalCrear", ofertaInfoAdicional);
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaInformacionAdicionalCrear(OfertaInformacionAdicional ofertaInfoAdicional)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                
                //ofertaInfo.IdOferta = ofertaInfo.IdOferta;
                //ofertaInfo.TipoConocimiento.IdListaValor = "ABE";  //Datos de prueba
                //ofertaInfo.Conocimiento = "conocimiento"; //Datos de prueba
                //ofertaInfo.NivelConocimiento.IdListaValor = "ABC"; //Datos de prueba
                //ofertaInfo.AniosExperiencia = 5; //Datos de prueba
                ofertaInfoAdicional.EstadoOfertaInformacionAdicional.IdListaValor = "OFOCAC"; //Estado oferta Informaciónn adicional Activo
                ofertaInfoAdicional.CreadoPor = ticket.Usuario;

                LNOfertaInformacionAdicional lnOfertaInfo = new LNOfertaInformacionAdicional();
                lnOfertaInfo.Insertar(ofertaInfoAdicional);

                List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(ofertaInfoAdicional.IdOferta, 0);

                ViewBag.IdOferta = ofertaInfoAdicional.IdOferta;
                return PartialView("_OfertaInformacionAdicional", lista);
            }

            return PartialView("_OfertaInformacionAdicionalCrear", ofertaInfoAdicional);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaInformacionAdicionalEditar(int id)
        {
            List<ListaValor> listaTipoInfoAdicional = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_OTRO_CONOCIMIENTO);
            List<ListaValor> listaNivelConocimiento = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NIVEL_CONOCIMIENTOS);
          
            List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(0, id);

            OfertaInformacionAdicional ofertaInfoAdicionalEdicion = lista[0];
            ViewBag.TipoConocimientoIdListaValor = new SelectList(listaTipoInfoAdicional, "IdListaValor", "Valor", ofertaInfoAdicionalEdicion.TipoConocimiento.IdListaValor);
            ViewBag.NivelConocimientoIdListaValor = new SelectList(listaNivelConocimiento.OrderBy(m => m.Peso), "IdListaValor", "Valor", ofertaInfoAdicionalEdicion.NivelConocimiento.IdListaValor);

            return PartialView("_OfertaInformacionAdicionalEditar", ofertaInfoAdicionalEdicion);
        }

        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaInformacionAdicionalEditar(OfertaInformacionAdicional ofertaInfoAdicional)
        {
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];                

                //ofertaInfo.IdOferta = 13;
                //ofertaInfo.TipoConocimiento.IdListaValor = "ABE22";
                //ofertaInfo.Conocimiento = "conocimiento22";
                //ofertaInfo.NivelConocimiento.IdListaValor = "ABC22";
                //ofertaInfo.AniosExperiencia = 5;
                //ofertaInfo.EstadoOfertaInformacionAdicional.IdListaValor = "as22";
                ofertaInfoAdicional.ModificadoPor = ticket.Usuario;

                LNOfertaInformacionAdicional lnOfertaInfo = new LNOfertaInformacionAdicional();
                lnOfertaInfo.Actualizar(ofertaInfoAdicional);

                List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(ofertaInfoAdicional.IdOferta, 0);

                ViewBag.IdOferta = ofertaInfoAdicional.IdOferta;
                return PartialView("_OfertaInformacionAdicional", lista);
            }
            else
            {
                //Código para ubicar los errores en el ModelState.
                var errors = ModelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .ToList();
                
                int a = 0;            
            }

            return PartialView("_OfertaInformacionAdicionalEditar", ofertaInfoAdicional);
        }

        [HttpGet]
        public PartialViewResult _OfertaInformacionAdicionalEliminar(int id)
        {
            List<OfertaInformacionAdicional> listaInfoAdicional = this.lnInfoAdicional.ObtenerInformacionAdicional(0, id);

            OfertaInformacionAdicional ofertaInfoAdicional = listaInfoAdicional[0];

            this.lnInfoAdicional.Eliminar(id);

            List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(ofertaInfoAdicional.IdOferta, 0);

            ViewBag.IdOferta = ofertaInfoAdicional.IdOferta;

            return PartialView("_OfertaInformacionAdicional", lista);
        }
    }
}