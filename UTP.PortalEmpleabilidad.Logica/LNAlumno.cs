using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNAlumno
    {
        ADAlumno ad = new ADAlumno();

        public Alumno ObtenerAlumnoPorCodigo(string codigoAlumno)
        {
            Alumno alumno = new Alumno();

            DataTable dtResultado = ad.ObtenerAlumnoPorCodigo(codigoAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                alumno.CodAlumnoUTP = dtResultado.Rows[0]["CodAlumnoUTP"].ToString();
                alumno.Usuario = dtResultado.Rows[0]["Usuario"].ToString();
                alumno.NumeroDocumento = dtResultado.Rows[0]["NumeroDocumento"].ToString();
                alumno.Nombres = dtResultado.Rows[0]["Nombres"].ToString();
                alumno.Apellidos = dtResultado.Rows[0]["Apellidos"].ToString();
                alumno.CorreoElectronico1 = dtResultado.Rows[0]["CorreoElectronico"].ToString();
                alumno.CorreoElectronico2 = dtResultado.Rows[0]["CorreoElectronico2"].ToString();
                alumno.DireccionLinea1 = dtResultado.Rows[0]["DireccionLinea1"].ToString();
                alumno.TelefonoFijoCasa = dtResultado.Rows[0]["TelefonoFijoCasa"].ToString();
                alumno.TelefonoCelular = dtResultado.Rows[0]["TelefonoCelular"].ToString();
            }

            return alumno;
        }
    }
}
