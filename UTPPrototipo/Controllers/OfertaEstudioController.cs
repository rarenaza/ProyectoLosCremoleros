using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTPPrototipo.Common;

namespace UTPPrototipo.Controllers
{
    [LogPortal]
    public class OfertaEstudioController : Controller
    {
        LNOfertaEstudio lnOfertaEstudio = new LNOfertaEstudio();
        private int idOfertaEstudiosTodos = 0; //Al enviar 0 se obtiene todas los estudios de la oferta.
        LNGeneral lnGeneral = new LNGeneral();

        public ActionResult ObtenerEstudios(int idOferta)
        {
            //Se guarda el Id de la oferta
            ViewBag.IdOferta = idOferta;

            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudiosNoUniversitarios(idOferta, idOfertaEstudiosTodos);

            return PartialView("_OfertaEstudio", lista);
        }


        public void Insertar(OfertaEstudio ofertaEstudio)
        {
            lnOfertaEstudio.Insertar(ofertaEstudio);
        }


        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaEstudioCrear(int id)
        {
            OfertaEstudio ofertaEstudio = new OfertaEstudio();
            ofertaEstudio.IdOferta = id;

            ViewBag.TipoDeEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValorOfertaTipoEstudiosEspecificos(Constantes.IDLISTA_TIPO_DE_ESTUDIO), "IdListaValor", "Valor");
            ViewBag.EstadoDelEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_DEL_ESTUDIO), "IdListaValor", "Valor");
            ViewBag.Estudio = new SelectList(lnGeneral.ObtenerListaValorPorIdPadre(Constantes.TIPO_ESTUDIO_PRINCIPAL), "Valor", "Valor");
            
            return PartialView("_OfertaEstudioCrear", ofertaEstudio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaEstudioCrear(OfertaEstudio ofertaEstudio)
        {
            //Aldo 09FEB: Si el tipo de estudio es distinto a universitario se reemplaza el campo EstudioTexto en Estudio
            //Si el campo TipoDeEstudioIdListaValor es TEUNIV entonces el dato de estudio sí se encuentra en ofertaEstudio.Estudio y no se necesita reemplazar.
            if (ofertaEstudio.TipoDeEstudioIdListaValor != Constantes.TIPO_ESTUDIO_PRINCIPAL)
            {
                ModelState.Remove("Estudio"); //Se quita el campo porque tiene null.
                ofertaEstudio.Estudio = ofertaEstudio.EstudioTexto;
            }
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];
                //string usuarioCreacion = ticket.UsuarioNombre;

                //ofertaEstudio.IdOferta = 13;
                //ofertaEstudio.CicloEstudio = "AWE";
                //ofertaEstudio.Estudio = "Estudios";
                //ofertaEstudio.TipoDeEstudio.IdListaValor = "ABC";
                //ofertaEstudio.EstadoDelEstudio.IdListaValor = "AWE";
                ofertaEstudio.EstadoOfertaEstudio.IdListaValor = "OFESAC"; //Estado oferta estudio activa.
                ofertaEstudio.CreadoPor = ticket.Usuario;

                LNOfertaEstudio lnOfertaEstudio = new LNOfertaEstudio();                
                lnOfertaEstudio.Insertar(ofertaEstudio);

                List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudiosNoUniversitarios(ofertaEstudio.IdOferta, idOfertaEstudiosTodos);

                ViewBag.IdOferta = ofertaEstudio.IdOferta;

                return PartialView("_OfertaEstudio", lista);
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                int a = 0;
            }
            return PartialView("_OfertaEstudioCrear", ofertaEstudio);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaEstudioEditar(int id)
        {
            //Obtener los datos del modelo de editar y pasarlo como parámetro.
            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(0, id);

            OfertaEstudio ofertaEstudio = lista[0];

            ViewBag.TipoDeEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValorOfertaTipoEstudiosEspecificos(Constantes.IDLISTA_TIPO_DE_ESTUDIO), "IdListaValor", "Valor", ofertaEstudio.TipoDeEstudioIdListaValor);
            ViewBag.EstadoDelEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_DEL_ESTUDIO), "IdListaValor", "Valor", ofertaEstudio.EstadoDelEstudioIdListaValor);
            ViewBag.Estudio = new SelectList(lnGeneral.ObtenerListaValorPorIdPadre(Constantes.TIPO_ESTUDIO_PRINCIPAL), "Valor", "Valor", ofertaEstudio.Estudio);

            return PartialView("_OfertaEstudioEditar", ofertaEstudio);
        }
        
        [ValidateAntiForgeryToken] // this action takes the viewModel from the modal
        public PartialViewResult _OfertaEstudioEditar(OfertaEstudio ofertaEstudio) //Este es como el submit
        {
            if (ofertaEstudio.TipoDeEstudioIdListaValor != Constantes.TIPO_ESTUDIO_PRINCIPAL)
            {
                ModelState.Remove("Estudio"); //Se quita el campo porque tiene null.
                ofertaEstudio.Estudio = ofertaEstudio.EstudioTexto;
            }
            if (ModelState.IsValid)
            {
                TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];                

                //Datos DEMO
                //ofertaEstudio.IdOferta = 13;
                //ofertaEstudio.CicloEstudio = "AWE2";
                //ofertaEstudio.Estudio = "Estudios2";
                //ofertaEstudio.TipoDeEstudio.IdListaValor = "ABC2";
                //ofertaEstudio.EstadoDelEstudio.IdListaValor = "AWE2";
                //ofertaEstudio.EstadoOfertaEstudio.IdListaValor = "ABC2";
                ofertaEstudio.ModificadoPor = ticket.Usuario;
                

                lnOfertaEstudio.Actualizar(ofertaEstudio);

                List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudiosNoUniversitarios(ofertaEstudio.IdOferta, idOfertaEstudiosTodos);  //TODO: Obtener el Id de la oferta.

                ViewBag.IdOferta = ofertaEstudio.IdOferta;
                return PartialView("_OfertaEstudio", lista);              
            }

            return PartialView("_OfertaEstudioEditar", ofertaEstudio);
        }


        [HttpGet] 
        public PartialViewResult _OfertaEstudioEliminar(int id)
        {
            List<OfertaEstudio> listaEstudios = lnOfertaEstudio.ObtenerEstudios(0, id);
            
            OfertaEstudio ofertaEstudio = listaEstudios[0];

            lnOfertaEstudio.Eliminar(id);

            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudiosNoUniversitarios(ofertaEstudio.IdOferta, idOfertaEstudiosTodos);  //TODO: Obtener el Id de la oferta.

            ViewBag.IdOferta = ofertaEstudio.IdOferta;

            return PartialView("_OfertaEstudio", lista);                     
        }

        [HttpPost]
        public JsonResult ListarEstudio(string query)
        {
            var resultado = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_DE_CARRERA);
            var result = resultado.Where(s => s.Valor.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdListaValor, Label = c.Valor }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}