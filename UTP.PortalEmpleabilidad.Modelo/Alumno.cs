using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public partial class Alumno : Auditoria
    {
        
        public int IdAlumno { get; set; }
        public string CodAlumnoUTP { get; set; }
        public string Usuario { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string DireccionLinea1 { get; set; }
        public string CorreoElectronico1 { get; set; }
        [EmailAddress(ErrorMessage="Email inválido")]
        public string CorreoElectronico2 { get; set; }
        public string TelefonoFijoCasa { get; set; }

        public string TelefonoCelular { get; set; }

        public string Carrera { get; set; }

        public string TipoDocumentoIdListaValor { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string SexoIdListaValor { get; set; }
        [Required(ErrorMessage = "Falta la dirección.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Falta el distrito.")]
        public string DireccionDistrito { get; set; }
        [Required(ErrorMessage = "Falta la provincia.")]
        public string DireccionCiudad { get; set; }
        [Required(ErrorMessage = "Falta el departamento.")]
        public string DireccionRegion { get; set; }     
        public byte[] Foto { get; set; }
        public string EstadoAlumnoIdListaValor { get; set; }
        public string ArchivoMimeType { get; set; }
        public int IdCV { get; set; }
     
    }
}
