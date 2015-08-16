using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class ReporteEquivalente
    {
        public string DatoOrigen { get; set; }
        public string DatoEquivalente { get; set; }
        public string Orden { get; set; }
    }
}
