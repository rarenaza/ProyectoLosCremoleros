using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno
{
    public class VistaAlumnoHunting
    {
        public string PalabraClave { get; set; }
        public List<ListaValor> ListaTipoEstudio { get; set; }
        public string IdTipoEstudio { get; set; }
        public string EstudioRequerido { get; set; }
        public List<ListaValor> ListaEstadoEstudio { get; set; }
        public string IdEstadoEstudio { get; set; }
        public List<ListaValor> ListaSectorEmpresarial { get; set; }
        public string IdSectorEmpresarial { get; set; }
        
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta numeros.")]
        public int? AniosExperiencia { get; set; }
        public List<ListaValor> ListaTipoCargo { get; set; }
        public string IdTipoCargo { get; set; }
        public List<ListaValor> ListaInformacionAdicional { get; set; }
        public string IdInformacionAdicional { get; set; }
        public string Conocimiento { get; set; }
        public string Distrito { get; set; }
        public List<AlumnoCV> ListaAlumnos { get; set; }
        public string NombreCargo { get; set; }

        public int IdAlumno { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Estudios { get; set; }
        public string ValorEstadoEstudio { get; set; }
        public string ValorSectorEmpresarial { get; set; }
        public int AnosExperiencia { get; set; }

        public List<Hunting> ListaBusqueda { get; set; }
    }

}
