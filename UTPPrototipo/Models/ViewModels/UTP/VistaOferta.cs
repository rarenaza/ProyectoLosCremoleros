using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistaOferta
    {
        public DateTime FechaPublicacion { get; set; }
        public string NombreComercial { get; set; }
        public string Clasificación { get; set; }

        public string CargoOfrecido { get; set; }
        public string NumeroVacantes { get; set; }
        public string FacultadPrincipal { get; set; }
        public string EjecutivoUTP { get; set; }
        public string Nrocv { get; set; }

        public int IdOferta { get; set; }
        public string  Estado { get; set; }
        public string Cargo { get; set; }
        //public DateTime FechaSeguimiento { get; set; }
        public string FechaSeguimiento { get; set; }
        public string Comentarios { get; set; }
        //Paginación:
        public int CantidadTotal { get; set; }

    }
}