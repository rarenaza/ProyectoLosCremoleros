using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace UTP.PortalEmpleabilidad.Modelo
{
    public partial class AlumnoEstudio
    {
        public AlumnoEstudio()
        {
            FechaInicioAno = 0;
            FechaFinAno = 0;
            FechaInicioMes = 0;
            FechaFinMes = 0;
            IdEstudio = 0;
            IdAlumno = 0;
            Institucion = "";
            Estudio = "";
            TipoDeEstudio = "";
            EstadoDelEstudio = "";
            Institucion = "";
            CicloEquivalente = 0;

        }
        public int IdEstudio { get; set; }
        public int IdAlumno { get; set; }
        [Required(ErrorMessage = "Falta la institución de estudio")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-Za-zÑñ,. ]+", ErrorMessage = "Este campo sólo acepta letras y numeros.")]
        public string Institucion { get; set; }
        [Required(ErrorMessage = "Falta el estudio")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-Za-zÑñ,. ]+", ErrorMessage = "Este campo sólo acepta letras y numeros.")]
        public string Estudio { get; set; }
        [Required(ErrorMessage = "Falta el tipo estudio")]
        public string TipoDeEstudio { get; set; }
        [Required(ErrorMessage = "Falta el estado del estudio")]
        public string EstadoDelEstudio { get; set; }
        [Required(ErrorMessage = "Falta el mes de inicio de estudio")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo sólo maximo 2 digitos.")]
        public int FechaInicioMes { get; set; }
        [Required(ErrorMessage = "Falta el año de inicio de estudio")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Este campo sólo acepta 4 digitos.")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros.")]
        public int? FechaInicioAno { get; set; }
        public int FechaFinMes { get; set; }
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Este campo sólo acepta 4 digitos.")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros.")]
        public int? FechaFinAno { get; set; }
        [Required(ErrorMessage = "Falta el ciclo de estudio")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta numeros.")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo sólo maximo 2 digitos.")]
        public int? CicloEquivalente { get; set; }
        public string Observacion { get; set; }
        public bool DatoUTP { get; set; }
        public string CreadoPor { get; set; }
        public List<ListaValor> ListaEstudios { get; set; }
        public List<ListaValor> ListaTipoEstudios { get; set; }
        public List<ListaValor> ListaEstadoEstudio { get; set; }
        public List<ListaValor> ListaObservacionEstudios { get; set; }
        public int IdCV { get; set; }
        public int Movimiento { get; set; }
    }
}
