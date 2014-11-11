using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class Lista
    {
       public int IDLista { get; set; }
       public string IDListaValor { get; set; }
       public string IDListaValorPadre { get; set; }
       public string NombreLista { get; set; }
       public string  Valor { get; set; }
       public string DescripcionLista { get; set; }
       public int Modificable { get; set; }
       public string  Creadopor { get; set; }
       public DateTime  FechaCreacion { get; set; }
       public string Modificadopor { get; set; }
       public DateTime FechaModificacion { get; set; }

    }
}
