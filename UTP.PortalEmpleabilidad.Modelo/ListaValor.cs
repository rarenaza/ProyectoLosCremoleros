using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class ListaValor
    {
       public string IdListaValor { get; set; }
       public int IdLista { get; set; }       
       public string IdListaValorPadre { get; set; }
       public string Valor { get; set; }
       public string DescripcionValor { get; set; }
       public string Icono { get; set; }
       public int Peso { get; set; }
       public string ValorUTP { get; set; }
       public string EstadoValor { get; set; }
       public string  Creadopor { get; set; }
       public DateTime  FechaCreacion { get; set; }
       public string Modificadopor { get; set; }
       public DateTime FechaModificacion { get; set; }

    }
}
