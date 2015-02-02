using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Models.ViewModels
{
    public class VistaPanelAlumnoMiCV
    {
        public Alumno alumno { get; set; }
        public List<AlumnoCV> ListaAlumnoCV { get; set; }
        public int IdCV { get; set; }
        public List<PlantillaCV> ListaPlantillaCV { get; set; }
        public int IdPlantillaCV { get; set; }
        public int IdAlumno { get; set; }

        public List<AlumnoEstudio> ListaAlumnoEstudio { get; set; }
        public List<AlumnoExperiencia> ListaAlumnoExperiencia { get; set; }
        //public List<AlumnoExpericiencia> ListaAlumnoExperiencia { get; set; }
        public int PorcentajeCV { get; set; }
         

    }

}
