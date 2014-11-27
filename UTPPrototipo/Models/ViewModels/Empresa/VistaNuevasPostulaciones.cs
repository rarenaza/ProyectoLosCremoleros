using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.Empresa
{
    public class VistaNuevasPostulaciones
    {
        public string CargaOfrecido { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public string AlumnoNombres { get; set; }
        public string AlumnoApellidos { get; set; }
        public int Cumplimiento { get; set; }
    }
}