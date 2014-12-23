using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistaAlumnoUtp_obtenerExperiencia
    {
       
        public int IdAlumno { get; set; }
        public string NombreComercial { get; set; }
        public string Sector { get; set; }
        public string NombreCargo { get; set; }
        public string FechaInicio { get; set; }
        public string Fechafin { get; set; }
        public string TipoCargo { get; set; }
    }
}