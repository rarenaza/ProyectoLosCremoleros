using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public partial class Oferta
    {
        public int IdOferta { get; set; }
        public int IdAlumno { get; set; }

        public int IdEmpresa { get; set; }
        public byte[] LogoEmpresa { get; set; }
        public string ArchivoMimeType { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string CargoOfrecido { get; set; }
        public string UsuarioPropietarioEmpresa { get; set; }
        
        public string Funciones { get; set; }
        public string Competencias { get; set; }
        public string  EstadoOferta { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaPostulacion { get; set; }


        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaFinRecepcionCV { get; set; }
        public DateTime FechaFinProceso { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
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
        public decimal Compatible { get; set; }
        public string NombreComercial { get; set; }
        public int Mensaje { get; set; }
        public string DesTipoTrabajo { get; set; }
        
        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string RecibeCorreosIdListaValor { get; set; }
        public string TipoCargoIdListaValor { get; set; }

         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string TipoTrabajoIdListaValor { get; set; }
        public string TipoContratoIdListaValor { get; set; }

        public string DesTipoCargo { get; set; }
        public string DesTipoContrato { get; set; }
        public string SitioWeb { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string DesNumeroEmpleados { get; set; }
        public string NombreLocacion { get; set; }
        public string IdentificadorTributario { get; set; }
        public int Postulacion { get; set; }

        public string NombreCV { get; set; }
        public int IdCV { get; set; }


        public string Requisito { get; set; }
        public int Cumplimiento { get; set; }
        public int Tipo { get; set; }
        public int Line { get; set; }


        public Empresa Empresa { get; set; }
        public List<OfertaPostulante> Postulantes { get; set; }

        public int MaxPagina { get; set; }
        

       public Oferta() {

           TipoCargo = new ListaValor();
           TipoTrabajo = new ListaValor();
           TipoContrato = new ListaValor();
           ListaEstudios = new List<OfertaEstudio>();
           ListaSectores = new List<OfertaSectorEmpresarial>();
           ListaInformacionAdicional = new List<OfertaInformacionAdicional>();
           Empresa = new Empresa();
        }
    }
}
