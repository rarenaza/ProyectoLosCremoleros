using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public partial class Oferta
    {       
        public int IdOferta { get; set; }
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage="No ha ingresado el cargo")]
        public string CargoOfrecido { get; set; }
        public string UsuarioPropietarioEmpresa { get; set; }
        
        public string Funciones { get; set; }
        public string Competencias { get; set; }
        public string  EstadoOferta { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }

        [Required(ErrorMessage = "No se ha ingresado la fecha de fin de recepción de CV's")]
        [DataType(DataType.Date)]    
        public DateTime FechaFinRecepcionCV { get; set; }
        
        public int IdEmpresaLocacion { get; set; }
        public string  DescripcionOferta { get; set; }
        //public string TipoTrabajo { get; set; }
        //public string TipoContrato { get; set; }
        public int DuracionContrato { get; set; }
        //public string TipoCargo { get; set; }
          
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

        public List<OfertaInformacionAdicional> ListaInformacionAdicional { get; set; }

        public List<OfertaEstudio> ListaEstudios { get; set; }
        public List<OfertaSectorEmpresarial> ListaSectores { get; set; }

        public ListaValor TipoCargo { get; set; }
        public ListaValor TipoTrabajo { get; set; }
        public ListaValor TipoContrato { get; set; }
        public string NombreComercial { get; set; }
       public Oferta() {

           TipoCargo = new ListaValor();
           TipoTrabajo = new ListaValor();
           TipoContrato = new ListaValor();
           ListaEstudios = new List<OfertaEstudio>();
           ListaSectores = new List<OfertaSectorEmpresarial>();
           ListaInformacionAdicional = new List<OfertaInformacionAdicional>();
        }
    }
}
