using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class Oferta
    {

        public int IdOferta { get; set; }
        public int IdEmpresa { get; set; }
        public string UsuarioPropietarioEmpresa { get; set; }
        public string  EstadoOferta { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }


        [Obsolete]
        public DateTime FechaFinRecepcionCV { get; set; }

        [DataType(DataType.Date)]    
        public DateTime FechaFinProceso { get; set; }
        public int IdEmpresaLocacion { get; set; }
        public string  DescripcionOferta { get; set; }
        public string TipoTrabajo { get; set; }
        public string TipoContrato { get; set; }
        public int DuracionContrato { get; set; }
        public string TipoCargo { get; set; }
        public string CargoOfrecido { get; set; }
  
        public decimal RemuneracionOfrecida { get; set; }
       
        public DateTime FechaInicioLabores { get; set; }
 
        public string Horario { get; set; }
        public string AreaEmpresa { get; set; }
        public int NumeroVacantes { get; set; }
        public int RequiereExperienciaLaboral { get; set; }
        public string GradoMinimoEstudio { get; set; }
        public string CreadoPor { get; set; }
        public DateTime  FechaCreacion{ get; set; }
        public string ModificadoPor { get; set; }
        public DateTime  FechaModificacion{ get; set; }

        public List<OfertaInformacionAdicional> listaInformacionAdicional { get; set; }
        


    }
}
