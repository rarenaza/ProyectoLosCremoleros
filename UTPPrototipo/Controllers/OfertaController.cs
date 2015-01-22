using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTPPrototipo.Models.ViewModels.Cuenta;


namespace UTPPrototipo.Controllers
{
    public class OfertaController : Controller
    {

        [HttpGet]
        public ActionResult FinalizarOferta(string idOferta, string estado)
        {
            LNOferta lnOferta = new LNOferta();

            lnOferta.CambiarEstado(Convert.ToInt32(idOferta), estado);  //Estado oferta finalizado.
            
            //Si el estado es pendiente de activación se debe mandar un aviso al ejecutivo de cuenta de la oferta.
            DataTable dtDatos = lnOferta.ObtenerDatosParaMensaje(Convert.ToInt32(idOferta));
            string para = Convert.ToString(dtDatos.Rows[0]["CorreoUsuarioEmpresa"]);

            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            Mensaje mensaje = new Mensaje ();
            mensaje.DeUsuarioCorreoElectronico = ticket.CorreoElectronico;
            mensaje.ParaUsuarioCorreoElectronico = para;
            mensaje.MensajeTexto = "Se ha creado una nueva oferta.";
            mensaje.Asunto = "Oferta pendiente de activación.";
            LNCorreo.EnviarCorreo(mensaje);

            //No debe retornar vistas.
            return Content("");
        }

        [HttpGet]
        public ActionResult AsignarUsuario(string idOferta, string usuario)
        {
            LNOferta lnOferta = new LNOferta();

            lnOferta.AsignarUsuario(Convert.ToInt32(idOferta), usuario);

            //No debe retornar vistas.
            return Content("");
        }

        [HttpGet]
        public ActionResult CambiarEstadoPostulante(int idOfertaPostulante, string faseOferta)
        {
            TicketEmpresa ticket = (TicketEmpresa)Session["TicketEmpresa"];

            LNOferta lnOferta = new LNOferta();

            List<OfertaPostulante> lista = new List<OfertaPostulante>();

            OfertaPostulante ofertaPostulante = new OfertaPostulante();
            ofertaPostulante.IdOfertaPostulante = idOfertaPostulante;
            ofertaPostulante.ModificadoPor = ticket.Usuario;
            ofertaPostulante.Seleccionado = true; //Valor agregado por compatibilidad con otro proceso. Se coloca True para indicar que Sí debe actualizar el campo. 
            lista.Add(ofertaPostulante);

            lnOferta.ActualizarFaseDePostulantes(lista, faseOferta);

            //Se obtiene la descripción de la oferta:
            LNGeneral lnGeneral = new LNGeneral();
            ListaValor listaValorFase = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_FASE_OFERTA).Where(m => m.IdListaValor == faseOferta).FirstOrDefault();

            //Se retorna la descripción de la fase seleccionada.
            return Content(listaValorFase.DescripcionValor);
        }
    }
}