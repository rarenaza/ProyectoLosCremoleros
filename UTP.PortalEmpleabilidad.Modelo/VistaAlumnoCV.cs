using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class VistaAlumnoCV
    {
        public int IdCV { get; set; }
        public int IdPlantillaCV { get; set; }
        public int IdAlumno { get; set; }
        public bool IncluirCorreoElectronico1 { get; set; }
        public bool IncluirTelefonoFijo { get; set; }
        public bool IncluirFoto { get; set; }
        public bool IncluirDireccion { get; set; }
        public bool Incluirnombre1 { get; set; }
        public bool Incluirnombre2 { get; set; }
        public bool Incluirnombre3 { get; set; }
        public bool Incluirnombre4 { get; set; }
        public string Perfil { get; set; }
        public string Usuario { get; set; }
        public bool IncluirNombre1 { get; set; }
        public bool IncluirNombre2 { get; set; }
        public bool IncluirNombre3 { get; set; }
        public bool IncluirNombre4 { get; set; }
        public IEnumerable<AlumnoEstudio> Estudios { get; set; }
        public IEnumerable<AlumnoExperienciaCargo> Experiencias { get; set; }
        public IEnumerable<AlumnoInformacionAdicional> Conocimientos { get; set; }
    }
}
