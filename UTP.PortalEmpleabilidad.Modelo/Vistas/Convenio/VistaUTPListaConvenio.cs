using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Convenio
{
    public class VistaUTPListaConvenio
    {
        public int IdConvenio { get; set; }     
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Carrera { get; set; }
        public string NombreComercial { get; set; }
        public string TipoTrabajo { get; set; }
        public int DuracionContrato { get; set; }
        public decimal SalarioOfrecido{ get; set; }
        public string AreaEmpresa { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
