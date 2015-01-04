using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class VistaOfertaPostulante
    {
        public Alumno alumnocv { get; set; }
        public IList<AlumnoEstudio> alumnoestudiocv { get; set; }
        public IList<AlumnoExperiencia> alumnoexperienciacv { get; set; }
        public IList<AlumnoInformacionAdicional> alumnoinformacionadicionalcv { get; set; }
        public IList<AlumnoPostulaciones> alumnopostulaciones { get; set; }

    }
}
