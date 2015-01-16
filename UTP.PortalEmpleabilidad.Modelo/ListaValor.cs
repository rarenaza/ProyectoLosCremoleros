using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public  class ListaValor
    {
       public int Id { get; set; }
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string IdListaValor { get; set; }
       public int IdLista { get; set; }       
       public string IdListaValorPadre { get; set; }
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string Valor { get; set; }
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string DescripcionValor { get; set; }
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string Icono { get; set; }
        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public int Peso { get; set; }
       public string ValorUTP { get; set; }
       public string Padre { get; set; }
       [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
       public string EstadoValor { get; set; }
       public string  Creadopor { get; set; }
       public DateTime  FechaCreacion { get; set; }
       public string Modificadopor { get; set; }
       public DateTime FechaModificacion { get; set; }

    }
}
