using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Convenio
    {
        public int IdConvenio { get; set; }
        public string Alumno { get; set; }
        public string IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Carrera { get; set; }
        public string NivelAcademico { get; set; }
        public string TelefonoFijoCasa { get; set; }
        public string TelefonoCelular { get; set; }
        public int Ciclo { get; set; }
        public int IdEmpresa { get; set; }
        public string NombreComercial { get; set; }
        public int? IdExperienciaCargo { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoCargo { get; set; }
        public string ContactoCorreoElectronico { get; set; }
        public string ContactoTelefono { get; set; }
        public string ContactoCelular { get; set; }
        public string TipoTrabajo { get; set; }
        public int DuracionContrato { get; set; }
        public decimal? SalarioOfrecido { get; set; }
        public string CargoOfrecido { get; set; }
        public string AreaEmpresa { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string FuenteConvenio { get; set; }
        public string Observaciones { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
