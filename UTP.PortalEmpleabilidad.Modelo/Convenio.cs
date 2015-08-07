using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Convenio
    {
        public int IdConvenio { get; set; }
        [Required(ErrorMessage = "Ingrese un Alumno")]
        public string Alumno { get; set; }
        public int IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Ingrese la Carrera")]
        public string Carrera { get; set; }
        public string CorreoAlumno { get; set; }
        [Required(ErrorMessage = "Ingrese el Nivel Académico")]
        public string NivelAcademico { get; set; }
        [Required(ErrorMessage = "Ingrese Teléfono Fijo Casa")]
        public string TelefonoFijoCasa { get; set; }
        [Required(ErrorMessage = "Ingrese Teléfono Celular")]
        public string TelefonoCelular { get; set; }
        [Required(ErrorMessage = "Ingrese Ciclo")]
        public int Ciclo { get; set; }
        public int IdEmpresa { get; set; }
        [Required(ErrorMessage = "Ingrese una Empresa")]
        public string NombreComercial { get; set; }
        public int? IdExperienciaCargo { get; set; }
        [Required(ErrorMessage = "Ingrese Nombre y Apellido")]
        public string ContactoNombre { get; set; }
        [Required(ErrorMessage = "Ingrese Cargo")]
        public string ContactoCargo { get; set; }
        [Required(ErrorMessage = "Ingrese E-mail")]
        public string ContactoCorreoElectronico { get; set; }
        public string ContactoTelefono { get; set; }
        [Required(ErrorMessage = "Ingrese Teléfono")]
        public string ContactoCelular { get; set; }
        [Required(ErrorMessage = "Seleccione Tipo de trabajo")]
        public string TipoTrabajo { get; set; }
        [Required(ErrorMessage = "Ingrese la Duración del Contrato")]
        public int DuracionContrato { get; set; }
        [Required(ErrorMessage = "Ingrese Salario")]
        public decimal? SalarioOfrecido { get; set; }
        [Required(ErrorMessage = "Ingrese Cargo")]
        public string CargoOfrecido { get; set; }
        [Required(ErrorMessage = "Ingrese Area de Desempeño")]
        public string AreaEmpresa { get; set; }
        public DateTime? FechaIngreso { get; set; }
        [Required(ErrorMessage = "Seleccione la Fuente del Convenio")]
        public string FuenteConvenio { get; set; }
        public string Observaciones { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string CodAlumnoUtp { get; set; }
        public string RazonSocial { get; set; }
        [Required(ErrorMessage = "Ingrese RUC")]
        public string IdentificadorTributario { get; set; }
        public string SectorEmpresarial { get; set; }
        public string NuevaObservacion { get; set; }
        public IEnumerable<AlumnoExperiencia> Experiencias { get; set; }
        public int? FechaInicioCargoAno { get; set; }
        public int? FechaInicioCargoMes { get; set; }
        public int? FechaFinCargoAno { get; set; }
        public int? FechaFinCargoMes { get; set; }
        public string NombreCargo { get; set; }
        public string DescripcionCargo { get; set; }
        [Required(ErrorMessage = "Ingrese Fecha de Inicio")]
        public string FechaInicio { get; set; }
        [Required(ErrorMessage = "Ingrese Fecha de Fin")]
        public string FechaFin { get; set; }
        [Required(ErrorMessage = "Seleccione Estado de Convenio")]
        public string EstadoConvenio { get; set; }
        [Required(ErrorMessage = "Seleccione Clasificación")]
        public string Clasificacion { get; set; }
        [Required(ErrorMessage = "Ingrese Fecha de Registro")]
        public string FechaRegistro { get; set; }
        [Required(ErrorMessage = "Ingrese Envio de Evaluación de Jefe")]
        public string EnvioEvaluacionJefe { get; set; }
        [Required(ErrorMessage = "Ingrese Envio de Formato de Informe de Alumno")]
        public string EnvioFormatoInformeAlumno { get; set; }
        [Required(ErrorMessage = "Ingrese Envio de Evaluación de Evaluador")]
        public string EnvioEvaluacionEvaluador { get; set; }
        [Required(ErrorMessage = "Ingrese Recepción de Evaluación de Jefe")]
        public string RecepcionEvaluacionJefe { get; set; }
        [Required(ErrorMessage = "Ingrese Recepción de Informe de Alumno")]
        public string RecepcionInformeAlumno { get; set; }
        [Required(ErrorMessage = "Ingrese Recepción de Evaluación de Evaluador")]
        public string RecepcionEvaluacionEvaluador { get; set; }
        [Required(ErrorMessage = "Ingrese Nota de Evaluación de Jefe")]
        public string NotaEvaluacionJefe { get; set; }
        [Required(ErrorMessage = "Ingrese Evaluador de Alumno")]
        public string EvaluadorAlumno { get; set; }
        [Required(ErrorMessage = "Ingrese Nota de Evaluación de Evaluador")]
        public string NotaEvaluacionEvaluador { get; set; }
        public string MesInicio { get; set; }

    }
}
