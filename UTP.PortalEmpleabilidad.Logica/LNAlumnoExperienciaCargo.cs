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
   public class LNAlumnoExperienciaCargo
    {
       ADAlumnoExperienciaCargo aecd = new ADAlumnoExperienciaCargo();
       public List<AlumnoExperienciaCargo> ObtenerAlumnoExperienciaCargoPorIdAlumno(int IdAlumno)
        {
            List<AlumnoExperienciaCargo> listaAlumnoExperienciaCargo = new List<AlumnoExperienciaCargo>();

            DataTable dtResultado = aecd.ObtenerAlumnoExperienciaCargoPorIdAlumno(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoExperienciaCargo alumnoexperienciacargo = new AlumnoExperienciaCargo();
                    alumnoexperienciacargo.IdExperienciaCargo = Funciones.ToInt(dtResultado.Rows[i]["IdExperienciaCargo"]);
                    alumnoexperienciacargo.NombreComercial = Funciones.ToString(dtResultado.Rows[i]["NombreComercial"]);
                    alumnoexperienciacargo.NombreCargo = Funciones.ToString(dtResultado.Rows[i]["NombreCargo"]);
                    alumnoexperienciacargo.DesTipoCargo = Funciones.ToString(dtResultado.Rows[i]["DesTipoCargo"]);
                    alumnoexperienciacargo.DescripcionCargo = Funciones.ToString(dtResultado.Rows[i]["DescripcionCargo"]);
                    alumnoexperienciacargo.SectorEmpresarial = Funciones.ToString(dtResultado.Rows[i]["SectorEmpresarial"]);
                    alumnoexperienciacargo.NomPais = Funciones.ToString(dtResultado.Rows[i]["NomPais"]);
                    alumnoexperienciacargo.Ciudad = Funciones.ToString(dtResultado.Rows[i]["Ciudad"]);
                    alumnoexperienciacargo.FechaInicioCargoMes = Funciones.ToInt(dtResultado.Rows[i]["FechaInicioCargoMes"]);
                    alumnoexperienciacargo.FechaInicioCargoAno = Funciones.ToInt(dtResultado.Rows[i]["FechaInicioCargoAno"]);
                    alumnoexperienciacargo.FechaFinCargoMes = Funciones.ToInt(dtResultado.Rows[i]["FechaFinCargoMes"]);
                    alumnoexperienciacargo.FechaFinCargoAno = Funciones.ToInt(dtResultado.Rows[i]["FechaFinCargoAno"]);

                    listaAlumnoExperienciaCargo.Add(alumnoexperienciacargo);
                }


            }

            return listaAlumnoExperienciaCargo;
        }
    }
}
