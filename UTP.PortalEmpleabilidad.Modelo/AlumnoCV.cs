using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class AlumnoCV
    {
        public int IdCV { get; set; }
        public int IdAlumno { get; set; }
        public int IdPlantillaCV { get; set; }
        public string NombreCV { get; set; }
        public bool IncluirTelefonoFijo { get; set; }
        public bool IncluirCorreoElectronico2 { get; set; }
        public bool IncluirDireccion { get; set; }
        public bool IncluirFoto { get; set; }
        public string Perfil { get; set; }
        public string EstadoCV { get; set; }
        public string CreadoPor { get; set; }
    }
}
