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

        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Calificacion { get; set; }

        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public int? NroPostulantes { get; set; }

        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public int? ContratadosUTP { get; set; }
       
        public string ContratadosOtros { get; set; }

        public string Estado { get; set; }
        public string ModificadoPor { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string ContratadosIDAT { get; set; }
        public string NombreYApellido { get; set; }
        public string ModalidadContrato { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Puntualidad { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string ImagenPersonal { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Conocimientos { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Experiencia { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Disponibilidad { get; set; }


       
    }
}
