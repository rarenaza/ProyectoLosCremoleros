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
        public string ContactoNombre { get; set; }
        public string ContactoCargo { get; set; }
        public string ContactoCorreoElectronico { get; set; }
        public string ContactoTelefono { get; set; }
        public string ContactoCelular { get; set; }
        [Required(ErrorMessage = "Seleccione Tipo de trabajo")]
        public string TipoTrabajo { get; set; }
        [Required(ErrorMessage = "Ingrese la Duración del Contrato")]
        public int DuracionContrato { get; set; }
        public decimal? SalarioOfrecido { get; set; }
        public string CargoOfrecido { get; set; }
        public string AreaEmpresa { get; set; }
        public DateTime? FechaIngreso { get; set; }
        [Required(ErrorMessage = "Selecciones la Fuente del Convenio")]
        public string FuenteConvenio { get; set; }
        public string Observaciones { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string CodAlumnoUtp { get; set; }

    }
}
