using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public class AlumnoInformacionAdicional
    {
        public int IdInformacionAdicional { get; set; }
        public string DesTipoConocimiento { get; set; }
        public string DesNivelConocimiento { get; set; }
        public string Conocimiento { get; set; }
        public int FechaConocimientoDesdeMes { get; set; }
        public int FechaConocimientoDesdeAno { get; set; }
        public int FechaConocimientoHastaMes { get; set; }
        public int FechaConocimientoHastaAno { get; set; }
        public string NomPais { get; set; }
        public string Ciudad { get; set; }
        public string InstituciónDeEstudio { get; set; }
        public int AñosExperiencia { get; set; }
        public bool Incluir { get; set; }

        public List<ListaValor> ListaTipoConocimiento { get; set; }
        public List<ListaValor> ListaPais { get; set; }
        public List<ListaValor> ListaNivelConocimiento { get; set; }

    }
}
