using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistaObtenerEventosUTP
    {
        public int IdEvento { get; set; }
        public string NombreEvento { get; set; }
        public string LugarEvento { get; set; }
        public string Expositor { get; set; }
        public string DireccionEvento { get; set; }
        public int AsistentesEsperados { get; set; }
        public string FechaEvento { get; set; }
    }
}