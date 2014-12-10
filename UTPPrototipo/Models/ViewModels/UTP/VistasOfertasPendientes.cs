using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistasOfertasPendientes
    {
        public DateTime  FechaPublicacion { get; set; }
        public string NombreComercial { get; set; }
        public string CargoOfrecido { get; set; }
        public int IdOferta { get; set; } 
   
    }
}