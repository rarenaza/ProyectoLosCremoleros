using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo.UTP;

namespace UTP.PortalEmpleabilidad.Logica
{
 public   class LNAutenticarUsuario
    {
     ADAutenticar ad = new ADAutenticar();


     public DataSet Autenticar_Usuario(string Usuario)
     {

         return ad.Autenticar_Usuario(Usuario);
     }
     public VistaPanelCabeceraUTP ObtenerPanelCabeceraUTP(string usuarioUTP)
     {
         VistaPanelCabeceraUTP ticketUtp = new VistaPanelCabeceraUTP();


         DataTable dtResultado = ad.ObtenerCabeceraPorCodigoUTP(usuarioUTP);

         if (dtResultado.Rows.Count > 0)
         {

             ticketUtp.Usuario = dtResultado.Rows[0]["Usuario"].ToString();

             ticketUtp.Nombre = dtResultado.Rows[0]["Nombre"].ToString();


             ticketUtp.CorreoElectronico = dtResultado.Rows[0]["CorreoElectronico"].ToString();


             ticketUtp.TelefonoCelular = dtResultado.Rows[0]["TelefonoCelular"].ToString();


             ticketUtp.TipoUsuario = dtResultado.Rows[0]["TipoUsuario"].ToString();

         }

         else
         {
             ticketUtp.Usuario = "Sin datos DEMO";
             ticketUtp.Nombre = "Sin Datos DEMO";

         }

         return ticketUtp;
     }

     /// <summary>
     /// Método para validar si el alumno esta matriculado o es egresado
     /// </summary>
     /// <param name="nombreUsuario">Nombre del usuario</param>
     /// <returns></returns>
     public DataTable ValidarAlumno(string Usuario)
     {
         return ad.ValidarAlumno(Usuario);
     }
    }
}
