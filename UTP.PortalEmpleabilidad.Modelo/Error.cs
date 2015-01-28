using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Error
    {
        public string Usuario { get; set; }
        public string Accion { get; set; }
        public string Controlador { get; set; }
        public string IP { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
        public string ErrorInnerException { get; set; }
        public string ErrorSource { get; set; }
    }
}
