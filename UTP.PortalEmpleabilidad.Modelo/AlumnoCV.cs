using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
namespace UTP.PortalEmpleabilidad.Modelo
{
    public class AlumnoCV
    {
        public int IdCV { get; set; }
        public int IdAlumno { get; set; }
        public int IdPlantillaCV { get; set; }
        [Required(ErrorMessage="Falta el Nombre de CV. ")]
        [StringLength(50, ErrorMessage = "Este campo sólo acepta maximo 50 caracteres.")]
        [RegularExpression(@"[0-9A-Za-zÑñ ]+", ErrorMessage = "Este campo sólo acepta números y letras.")]
        public string NombreCV { get; set; }
        public bool IncluirTelefonoFijo { get; set; }
        public bool IncluirCorreoElectronico2 { get; set; }
        public bool IncluirDireccion { get; set; }
        public bool IncluirFoto { get; set; }
        public string Perfil { get; set; }
        public string EstadoCV { get; set; }
        public string CreadoPor { get; set; }
        public string Plantilla { get; set; }


        public bool IncluirCorreoElectronico1 { get; set; }



        public string Usuario { get; set; }
        public bool IncluirNombre1 { get; set; }
        public bool IncluirNombre2 { get; set; }
        public bool IncluirNombre3 { get; set; }
        public bool IncluirNombre4 { get; set; }
        public IEnumerable<VistaAlumnoEstudio> Estudios { get; set; }
        public IEnumerable<VistaAlumnoExperienciaCargo> Experiencias { get; set; }
        public IEnumerable<VistaAlumnoConocimiento> Conocimientos { get; set; }

        public IEnumerable<int> Estudios2 { get; set; }



    }
}
