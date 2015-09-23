using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public partial class AlumnoEstudio
    {
        public AlumnoEstudio()
        {
            FechaInicioAno = 0;
            FechaFinAno = null;
            FechaInicioMes = 0;
            FechaFinMes = null;
            IdEstudio = 0;
            IdAlumno = 0;
            
            Estudio = "";
            TipoDeEstudio = "";
            EstadoDelEstudio = "";
            Institucion = "";
            CicloEquivalente = null;

        }
        public int IdEstudio { get; set; }
        public int IdAlumno { get; set; }
        [Required(ErrorMessage = "Falta la institución de estudio")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-ZÀ-ÿa-zÑñ,. ]+", ErrorMessage = "Este campo sólo acepta letras y numeros")]
        public string Institucion { get; set; }
        [Required(ErrorMessage = "Falta el estudio")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-ZÀ-ÿa-zÑñ,. ]+", ErrorMessage = "Este campo sólo acepta letras y numeros")]
        public string Estudio { get; set; }
        [Required(ErrorMessage = "Falta el tipo estudio")]
        public string TipoDeEstudio { get; set; }
        [Required(ErrorMessage = "Falta el estado del estudio")]
        public string EstadoDelEstudio { get; set; }
        [Required(ErrorMessage = "Falta el mes de inicio de estudio")]
        
        public int FechaInicioMes { get; set; }

        [Required(ErrorMessage = "Falta el año de inicio de estudio")]
        
        [Range(1900,2100, ErrorMessage = "Coloque un valor entre 1900 y 2100")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros")]
        
        public int FechaInicioAno { get; set; }
        public int? FechaFinMes { get; set; }
        [Range(1900, 2100, ErrorMessage = "Coloque un valor entre 1900 y 2100")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros.")]
        public int? FechaFinAno { get; set; }
        //[Required(ErrorMessage = "Falta el ciclo de estudio")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta numeros.")]
        [Range(0, 99, ErrorMessage = "Este campo sólo maximo 2 digitos.")]
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
        public string IconoTipoDeEstudio { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool DatoCargado { get; set; }
    }
}
