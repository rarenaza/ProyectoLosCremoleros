using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public partial class AlumnoEstudio
    {
        public int IdEstudio { get; set; }
        public int IdAlumno { get; set; }
        public string Institucion { get; set; }
        public string Estudio { get; set; }
        public string TipoDeEstudio { get; set; }
        public string EstadoDelEstudio { get; set; }
        public int FechaInicioMes { get; set; }
        public int? FechaInicioAno { get; set; }
        public int FechaFinMes { get; set; }
        public int? FechaFinAno { get; set; }
        public int? CicloEquivalente { get; set; }
        public string Observacion { get; set; }
        public bool DatoUTP { get; set; }
        public string CreadoPor { get; set; }
        public List<ListaValor> ListaEstudios { get; set; }
        public List<ListaValor> ListaTipoEstudios { get; set; }
        public List<ListaValor> ListaEstadoEstudio { get; set; }
        public List<ListaValor> ListaObservacionEstudios { get; set; }
        public int IdCV { get; set; }
        public int Movimiento { get; set; }
    }
}
