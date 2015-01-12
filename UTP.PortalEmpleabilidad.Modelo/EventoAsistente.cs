using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class EventoAsistente
    {
       public int IdEvento { get; set; }
       public DateTime  FechaCreacion { get; set; }
       public string Usuario { get; set; }
       public string Nombres { get; set; }
       public string Sexo { get; set; }
       public string DocIdentidad { get; set; }
       public string Ticket { get; set; }
       public DateTime FechaAsistencia { get; set; }
       public string NombreEvento { get; set; }
    }
}
