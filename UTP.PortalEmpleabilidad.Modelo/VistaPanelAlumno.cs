using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class VistaPanelAlumno
    {
        public Alumno Alumno { get; set; }
        public List<Evento> ListaEventos { get; set; }
        public List<Oferta> ListaOfertas{ get; set; }


        //Otras propiedades.
    }
}
