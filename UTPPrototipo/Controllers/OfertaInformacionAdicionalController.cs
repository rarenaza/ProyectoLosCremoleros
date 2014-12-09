using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;

namespace UTPPrototipo.Controllers
{
    public class OfertaInformacionAdicionalController : Controller
    {
        LNOfertaInformacionAdicional lnInfoAdicional = new LNOfertaInformacionAdicional();
        LNGeneral lnGeneral = new LNGeneral();

        public ActionResult ObtenerInformacionAdicional(int idOferta)
        {
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
        public PartialViewResult _OfertaInformacionAdicionalCrear()
        {
            //Se cargan los combos del tipo.
            ViewBag.TipoConocimientoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_OTRO_CONOCIMIENTO), "IdListaValor", "Valor");
            ViewBag.NivelConocimientoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NIVEL_CONOCIMIENTOS).OrderBy(m => m.Peso), "IdListaValor", "Valor");

            return PartialView("_OfertaInformacionAdicionalCrear");
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaInformacionAdicionalCrear(OfertaInformacionAdicional ofertaInfo)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                ofertaInfo.IdOferta = 13;
                //ofertaInfo.TipoConocimiento.IdListaValor = "ABE";  //Datos de prueba
                //ofertaInfo.Conocimiento = "conocimiento"; //Datos de prueba
                //ofertaInfo.NivelConocimiento.IdListaValor = "ABC"; //Datos de prueba
                //ofertaInfo.AniosExperiencia = 5; //Datos de prueba
                ofertaInfo.EstadoOfertaInformacionAdicional.IdListaValor = "OFOCAC"; //Estado Activo
                ofertaInfo.CreadoPor = usuarioCreacion;

                LNOfertaInformacionAdicional lnOfertaInfo = new LNOfertaInformacionAdicional();
                lnOfertaInfo.Insertar(ofertaInfo);

                List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(13, 0);

                return PartialView("_OfertaInformacionAdicional", lista);
            }

            return PartialView("_OfertaInformacionAdicionalCrear", ofertaInfo);
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
        public PartialViewResult _OfertaInformacionAdicionalEditar(OfertaInformacionAdicional ofertaInfo)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = (Ticket)Session["Ticket"];
                string usuarioCreacion = ticket.UsuarioNombre;

                ofertaInfo.IdOferta = 13;
                //ofertaInfo.TipoConocimiento.IdListaValor = "ABE22";
                //ofertaInfo.Conocimiento = "conocimiento22";
                //ofertaInfo.NivelConocimiento.IdListaValor = "ABC22";
                //ofertaInfo.AniosExperiencia = 5;
                //ofertaInfo.EstadoOfertaInformacionAdicional.IdListaValor = "as22";
                ofertaInfo.ModificadoPor = ticket.UsuarioNombre;

                LNOfertaInformacionAdicional lnOfertaInfo = new LNOfertaInformacionAdicional();
                lnOfertaInfo.Actualizar(ofertaInfo);

                List<OfertaInformacionAdicional> lista = lnInfoAdicional.ObtenerInformacionAdicional(13, 0);

                return PartialView("_OfertaInformacionAdicional", lista);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .ToList();

                int a = 0;
            
            }

            return PartialView("_OfertaInformacionAdicionalEditar", ofertaInfo);
        }
    }
}