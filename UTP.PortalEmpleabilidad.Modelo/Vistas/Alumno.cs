using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
   public partial  class Alumno
    {
       public bool IncluirTelefonoFijo { get; set; }
       public bool IncluirCorreoElectronico2 { get; set; }
       public bool IncluirFoto { get; set; }
       public bool IncluirDireccion { get; set; }
       public bool IncluirNombre1 { get; set; }
       public bool IncluirNombre2 { get; set; }
       public bool IncluirNombre3 { get; set; }
       public bool IncluirNombre4 { get; set; }
       public string Perfil { get; set; }
       public string[] ListaNombres { get; set; }
       public int IdOferta { get; set; }
	   public string FaseOferta { get; set; }
	   public DateTime FechaPostulacion { get; set; }
       public string CargoOfrecido { get; set; }

       public string FaseOfertaDescripcion { get; set; }
       public int Cumplimiento { get; set; }
       public int IdOfertaPostulante { get; set; }
    }
}
