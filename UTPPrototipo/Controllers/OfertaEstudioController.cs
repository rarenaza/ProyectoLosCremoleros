﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Logica;
using UTPPrototipo.Models.ViewModels.Cuenta;

namespace UTPPrototipo.Controllers
{
    public class OfertaEstudioController : Controller
    {
        LNOfertaEstudio lnOfertaEstudio = new LNOfertaEstudio();
        private int idOfertaEstudiosTodos = 0; //Al enviar 0 se obtiene todas los estudios de la oferta.
        LNGeneral lnGeneral = new LNGeneral();

        public ActionResult ObtenerEstudios(int idOferta)
        {
            //Se guarda el Id de la oferta
            ViewBag.IdOferta = idOferta;

            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(idOferta, idOfertaEstudiosTodos);

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

            ViewBag.TipoDeEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DE_ESTUDIO), "IdListaValor", "Valor");
            ViewBag.EstadoDelEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_DEL_ESTUDIO), "IdListaValor", "Valor");
            
            return PartialView("_OfertaEstudioCrear", ofertaEstudio);
        }


        [ValidateAntiForgeryToken]
        public PartialViewResult _OfertaEstudioCrear(OfertaEstudio ofertaEstudio)
        {
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

                List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(ofertaEstudio.IdOferta, idOfertaEstudiosTodos);

                ViewBag.IdOferta = ofertaEstudio.IdOferta;

                return PartialView("_OfertaEstudio", lista);
            }

            return PartialView("_OfertaEstudioCrear", ofertaEstudio);
        }

        [HttpGet] // esta acción devuelve la vista parcial con los datos para cargar el modal.
        public PartialViewResult _OfertaEstudioEditar(int id)
        {
            //Obtener los datos del modelo de editar y pasarlo como parámetro.
            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(0, id);

            OfertaEstudio ofertaEstudio = lista[0];

            ViewBag.TipoDeEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DE_ESTUDIO), "IdListaValor", "Valor", ofertaEstudio.TipoDeEstudioIdListaValor);
            ViewBag.EstadoDelEstudioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_DEL_ESTUDIO), "IdListaValor", "Valor", ofertaEstudio.EstadoDelEstudioIdListaValor);
            
            return PartialView("_OfertaEstudioEditar", ofertaEstudio);
        }

        [ValidateAntiForgeryToken] // this action takes the viewModel from the modal
        public PartialViewResult _OfertaEstudioEditar(OfertaEstudio ofertaEstudio) //Este es como el submit
        {
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

                List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(ofertaEstudio.IdOferta, idOfertaEstudiosTodos);  //TODO: Obtener el Id de la oferta.

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

            List<OfertaEstudio> lista = lnOfertaEstudio.ObtenerEstudios(ofertaEstudio.IdOferta, idOfertaEstudiosTodos);  //TODO: Obtener el Id de la oferta.

            ViewBag.IdOferta = ofertaEstudio.IdOferta;

            return PartialView("_OfertaEstudio", lista);                     
        }
    }
}