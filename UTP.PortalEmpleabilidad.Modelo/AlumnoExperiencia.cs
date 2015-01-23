using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class AlumnoExperiencia
    {
        public int IdExperiencia { get; set; }
        public int IdExperienciaCargo { get; set; }

        public int IdAlumno { get; set; }
        public int IdEstudio { get; set; }

        [Required(ErrorMessage = "Falta la empresa")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[0-9A-ZÀ-ÿa-zÑñ ]+", ErrorMessage = "Este campo sólo acepta letras y numeros2.")]           
        public string Empresa { get; set; }
        
        [Required(ErrorMessage = "Falta la descripcion de la empresa")]
        [StringLength(500, ErrorMessage = "Este campo sólo acepta máximo 500 caracteres.")]
        [RegularExpression(@"[0-9A-ZÀ-ÿa-zÑñ ,.]+", ErrorMessage = "Este campo sólo acepta letras y numeros.")]
        public string DescripcionEmpresa { get; set; }
        
        public int IdEmpresa { get; set; }
        
        [Required(ErrorMessage = "Falta el principal sector empresarial")]
        public string SectorEmpresarial { get; set; }
        public string SectorEmpresarial2 { get; set; }
        public string SectorEmpresarial3 { get; set; }
        public string Pais { get; set; }

        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[A-ZÀ-ÿa-zÑñ ]+", ErrorMessage = "Este campo sólo acepta letras.")]
        public string Ciudad { get; set; }
        public string CreadoPor { get; set; }
        public string NombreComercial { get; set; }
        
        [Required(ErrorMessage = "Falta el cargo")]
        [StringLength(100, ErrorMessage = "Este campo sólo acepta máximo 100 caracteres.")]
        [RegularExpression(@"[A-ZÀ-ÿa-zÑñ ,.]+", ErrorMessage = "Este campo sólo acepta letras.")]
        public string NombreCargo { get; set; }
        
        [Required(ErrorMessage = "Falta el mes de inicio del cargo")]
        //[StringLength(2, MinimumLength = 1, ErrorMessage = "Este campo sólo maximo 2 digitos.")] //ERROR
        public int FechaInicioCargoMes { get; set; }

        [Required(ErrorMessage = "Falta el año de inicio del cargo")]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "Este campo sólo acepta 4 digitos.")] //ERROR
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros.")]
        public int FechaInicioCargoAno { get; set; }
        public int? FechaFinCargoMes { get; set; }

        //[StringLength(4, MinimumLength = 4, ErrorMessage = "Este campo sólo acepta 4 digitos.")] //ERROR
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta años con 4 numeros.")]
        public int? FechaFinCargoAno { get; set; }

        [Required(ErrorMessage = "Falta el tipo cargo")]
        public string TipoCargo { get; set; }
        ////[Required(ErrorMessage = "Falta la descripcion del cargo")]

        [StringLength(500, ErrorMessage = "Este campo sólo acepta máximo 500 caracteres.")]
        [RegularExpression(@"[0-9A-ZÀ-ÿa-zÑñ ,.\r?\n]+", ErrorMessage = "Este campo sólo acepta letras y números.")]
        public string DescripcionCargo { get; set; }
        
        public int IdCV { get; set; }
        public string Usuario { get; set; }
        public int Movimiento { get; set; }
        public int Cumple { get; set; }

        public string RazonSocial { get; set; }

    }

}
