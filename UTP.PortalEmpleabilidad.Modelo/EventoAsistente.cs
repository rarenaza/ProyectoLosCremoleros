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
       public DateTime FechaInscripcion { get; set; }
       public string Usuario { get; set; }
       public string Nombres { get; set; }
       public string Apellidos { get; set; }
       
       public string Sexo { get; set; }
       public string ValorTipoDocumento { get; set; }
       public string NumeroDocumento { get; set; }

       public string ValorEstadoTicket { get; set; }
       public DateTime FechaAsistencia { get; set; }
       public string NombreEvento { get; set; }
    }
}
