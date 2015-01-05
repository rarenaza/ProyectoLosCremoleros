using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public class AlumnoInformacionAdicional
    {
        public int IdInformacionAdicional { get; set; }
        public string DesTipoConocimiento { get; set; }
        public string DesNivelConocimiento { get; set; }
        [Required(ErrorMessage = "Falta el conocimiento")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-Za-zÑñ. ]+", ErrorMessage = "Este campo sólo acepta letras y numeros.")]
        public string Conocimiento { get; set; }
        //[Required(ErrorMessage = "Falta el mes de inicio")]
        //[StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo sólo maximo 2 digitos.")]
        public int FechaConocimientoDesdeMes { get; set; }
        //[Required(ErrorMessage = "Falta el año de inicio")]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "Este campo sólo acepta 4 digitos.")]
        //[RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros.")]
        public int? FechaConocimientoDesdeAno { get; set; }
        public int FechaConocimientoHastaMes { get; set; }
        public int? FechaConocimientoHastaAno { get; set; }
        public string NomPais { get; set; }
        [Required(ErrorMessage = "Falta la ciudad")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[A-Za-zÑñ]+", ErrorMessage = "Este campo sólo acepta letras.")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "Falta la institución de estudio")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-Za-zÑñ,. ]+", ErrorMessage = "Este campo sólo acepta letras y numeros.")]
        public string InstituciónDeEstudio { get; set; }
        ////[Required(ErrorMessage = "Falta los años de experiencia")]
        ////[RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta numeros.")]
        ////[StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo sólo maximo 2 digitos.")]
        public int? AñosExperiencia { get; set; }
        public bool Incluir { get; set; }

        public List<ListaValor> ListaTipoConocimiento { get; set; }
        public List<ListaValor> ListaPais { get; set; }
        public List<ListaValor> ListaNivelConocimiento { get; set; }
        public int IdAlumno { get; set; }
       [Required(ErrorMessage = "Falta el tipo de conocimiento")]
        public string TipoConocimientoIdListaValor { get; set; }
       [Required(ErrorMessage = "Falta el nivel de conocimiento")]
        public string NivelConocimientoIdListaValor { get; set; }
       [Required(ErrorMessage = "Falta el país")]
        public string PaisIdListaValor { get; set; }

        //public string CreadoPor { get; set; }
        public string Usuario { get; set; }

        public int IdCV { get; set; }
        public int Movimiento { get; set; }
        public int Cumple { get; set; }

    }
}
