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
       public bool IncluirCorreoElectronico1 { get; set; }
       public bool IncluirFoto { get; set; }
       public bool IncluirDireccion { get; set; }
       public bool IncluirNombre1 { get; set; }
       public bool IncluirNombre2 { get; set; }
       public bool IncluirNombre3 { get; set; }
       public bool IncluirNombre4 { get; set; }
       public string Perfil { get; set; }
       public string[] ListaNombres { get; set; }
    }
}
