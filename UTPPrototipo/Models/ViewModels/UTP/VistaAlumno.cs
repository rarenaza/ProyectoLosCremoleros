using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistaAlumno
    {
        public int id { get; set; }
        public string FechaRegistro { get; set; }

        public DateTime FechaperiodoRegistro { get; set; }

        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Carrera { get; set; }
        public string Ciclo { get; set; }
        public int idAlumno { get; set; }
        public string EstadoAlumno { get; set; }

        public int completitud { get; set; }

        public int? IdPeriodoRegistro { get; set; }

        public Dictionary<int, string> PeriodoRegistro { get; set; }

        public string PalabraClave { get; set; }
        public int nroPaginaActual { get; set; }
        public int filasPorPagina { get; set; }

        //Paginación:
        public int CantidadTotal { get; set; }
    }
}