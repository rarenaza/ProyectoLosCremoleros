using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaEncuesta
    {
        
        public int IdOferta { get; set; }

        public List<ListaValor> Calificaciones { get; set; }

        [Required]
        public string Calificacion { get; set; }

        [Required]
        public int NroPostulantes { get; set; }

        [Required]
        public int ContratadosUTP { get; set; }

        [Required]
        public int ContratadosOtros { get; set; }

        public string Estado { get; set; }
        public string ModificadoPor { get; set; }
       
    }
}
