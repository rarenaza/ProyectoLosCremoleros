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
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Funciones { get; set; }
        public string Competencias { get; set; }
        public string  EstadoOferta { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime FechaPublicacion { get; set; }
        public DateTime FechaPostulacion { get; set; }


        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFinRecepcionCV { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaFinProceso { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public int IdEmpresaLocacion { get; set; }
        public string  DescripcionOferta { get; set; }
        //public string TipoTrabajo { get; set; }
        //public string TipoContrato { get; set; }
        public int? DuracionContrato { get; set; }
        //public string TipoCargo { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public decimal? RemuneracionOfrecida { get; set; }
       
        public DateTime FechaInicioLabores { get; set; }
 
        public string Horario { get; set; }
        public string AreaEmpresa { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public int? NumeroVacantes { get; set; }
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
        public string CorreoElectronicoUsuarioEmpresa { get; set; }
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
        public int AnoCreacion { get; set; }
        public string DesSectorEmpresarial { get; set; }
        public Empresa Empresa { get; set; }
        public List<OfertaPostulante> Postulantes { get; set; }

        public int MaxPagina { get; set; }

        public int IdOfertaPostulante { get; set; }

        public int TotalRegistros { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaSeguimiento { get; set; }
        public int? NumeroPostulantes { get; set; }
        public int? NumeroInvitados { get; set; }
        public int? NumeroEntrevistados { get; set; }
        public int? NumeroContratados { get; set; }
        public Boolean ConvenioRegistrado { get; set; }
        public string Contacto { get; set; }
        public string DatosContacto { get; set; }
        public string MedioComunicacion { get; set; }

        public virtual List<OfertaFase> OfertaFases { get; set; }

        public List<ListaValor> CarrerasDisponibles { get; set; }
        public List<OfertaEstudio> CarrerasSeleccionadas { get; set; }

        //Datos para agregar carreras de UTP:
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string EstadoCarreraUTP { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public int? CicloMinimoCarreraUTP { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Este campo sólo acepta números.")]
        public int? ExperienciaGeneral { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Este campo sólo acepta números.")]
        public int? ExperienciaPosicionesSimilares { get; set; }
        public bool CumpleExperienciaGeneral { get; set; }
        public bool CumpleExperienciaPosicionesSimilares { get; set; }
        public string EstadoCarreraUTPDescripcion { get; set; }

        //Campos para la encuesta:
        public string Calificacion { get; set; }
        public int NroPostulantes { get; set; }
        public int ContratadosUTP { get; set; }
        public string ContratadosOtros { get; set; }

        //Campos para el seguimiento: El seguimiento obtiene los valores a partir de la encuesta
        public string SeguimientoCalificacion { get; set; }
        public int? SeguimientoNroInvitados { get; set; }
        public int? SeguimientoContratados { get; set; }
        public string SeguimientoContratadosOtros { get; set; }

       public Oferta() {

           TipoCargo = new ListaValor();
           TipoTrabajo = new ListaValor();
           TipoContrato = new ListaValor();
           ListaEstudios = new List<OfertaEstudio>();
           ListaSectores = new List<OfertaSectorEmpresarial>();
           ListaInformacionAdicional = new List<OfertaInformacionAdicional>();
           Empresa = new Empresa();
           OfertaFases = new List<OfertaFase>();
        }

       public string TipoTrabajoUTP { get; set;}
    }
}
