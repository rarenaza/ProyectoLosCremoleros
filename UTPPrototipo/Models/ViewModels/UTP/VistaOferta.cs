using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistaOferta
    {
        public DateTime FechaPublicacion { get; set; }
        public string NombreComercial { get; set; }
        public string Clasificación { get; set; }

        public string CargoOfrecido { get; set; }
        public string NumeroVacantes { get; set; }
        public int NumeroPostulante { get; set; }
        public string Sueldos { get; set; }
        public string FacultadPrincipal { get; set; }
        public string EjecutivoUTP { get; set; }
        public string Nrocv { get; set; }

        public int IdOferta { get; set; }
        public string  Estado { get; set; }
        public List<ListaValor> ListaTipoCargo { get; set; }
        public List<ListaValor> ListaSectorEmpresarial { get; set; }
        public List<ListaValor> ListaTipoContrato { get; set; }
        public List<ListaValor> ListaTipoEstudio { get; set; }

        public List<ListaValor> ListaInformacionAdicional { get; set; }

        public List<ListaValor> ListaCarrera { get; set; }
        public string PalabraClave { get; set; }
        public string Cargo { get; set; }
        public string IdTipoCargoutp { get; set; }
        public string IdSectorutp { get; set; }
        public string IdTipoContratoutp { get; set; }
        public string IdTipoEstudioutp { get; set; }
        public string Carrera { get; set; }
        public string TipoTrabajoUTP { get; set; }
        public string InformacionAdicional { get; set; }
        public string Conocimientos { get; set; }
        public int RemuneracionOfrecida { get; set; }
        

        //public DateTime FechaSeguimiento { get; set; }
        public string FechaSeguimiento { get; set; }
        public string Comentarios { get; set; }
        public int AExperiencia { get; set; }

        public int nroPaginaActual { get; set; }
        public int filasPorPagina { get; set; }


        //Paginación:
        public int CantidadTotal { get; set; }

        public List<ListaValor> ListaEstadoOferta { get; set; }
        public string IdEstadoOferta { get; set; }

    }
}