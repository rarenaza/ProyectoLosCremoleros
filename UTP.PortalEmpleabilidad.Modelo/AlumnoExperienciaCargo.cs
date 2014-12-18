using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class AlumnoExperienciaCargo
    {
        public int IdExperienciaCargo { get; set; }
        public string NombreComercial { get; set; }
        public string NombreCargo { get; set; }
        public string DesTipoCargo { get; set; }
        public string DescripcionCargo { get; set; }
        public string SectorEmpresarial { get; set; }
        public string NomPais { get; set; }
        public string Ciudad { get; set; }
        public int FechaInicioCargoMes { get; set; }
        public int FechaInicioCargoAno { get; set; }
        public int FechaFinCargoMes { get; set; }
        public int FechaFinCargoAno { get; set; }
        public bool Incluir { get; set; }
    }
}
