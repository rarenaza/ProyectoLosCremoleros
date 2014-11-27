using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class Contenido
    {
        public int id { get; set; } 
        public int IdContenido { get; set; } 
        public string Titulo { get; set; }
        public string SubTitulo { get; set;}
        public string Descripcion { get; set; }
        public byte[] Imagen { get; set; }
        //public string  Imagen { get; set; }
        public string  Menu { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }

        public bool EnPantallaPrincipal { get; set; }
       
    }
}
