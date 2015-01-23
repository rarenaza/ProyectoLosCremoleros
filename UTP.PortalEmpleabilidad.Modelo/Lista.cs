using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string NombreLista { get; set; }
       public string  Valor { get; set; }
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string DescripcionLista { get; set; }
 
       public bool Modificable { get; set; }

       public string  Creadopor { get; set; }
       public DateTime  FechaCreacion { get; set; }
       public string Modificadopor { get; set; }
       public DateTime FechaModificacion { get; set; }

    }
}
