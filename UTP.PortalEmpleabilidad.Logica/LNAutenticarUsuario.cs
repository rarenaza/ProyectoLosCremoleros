using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;

namespace UTP.PortalEmpleabilidad.Logica
{
 public   class LNAutenticarUsuario
    {
     ADAutenticar ad = new ADAutenticar();


     public DataSet Autenticar_Usuario(string Usuario, string Contraseña)
     {

         return ad.Autenticar_Usuario(Usuario, Contraseña);
     }
     //public DataTable  Autenticar_Usuario(string Usuario, string Contraseña)
     //{

     //    return ad.Autenticar_Usuario(Usuario, Contraseña);
     //}



    }
}
