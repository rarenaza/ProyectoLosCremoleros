using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class AlumnoUtp_obtenerInformacionAdicional
    {
        public int IdAlumno { get; set; }
        public string Tipo { get; set; }
        public string Conocimiento { get; set; }
        public string Nivel { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string AñosExperiencia { get; set; }
    }
}