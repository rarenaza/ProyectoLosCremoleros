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
    public class LNAlumnoCVExperienciaCargo
    {
        ADAlumnoCVExperienciaCargo acvecd = new ADAlumnoCVExperienciaCargo();
        private List<AlumnoCVExperienciaCargo> ObtenerAlumnoCVExperienciaCargoPorIdAlumno(int IdCV, int IdExperienciaCargo)
        {
            List<AlumnoCVExperienciaCargo> listaAlumnoCVExperienciaCargo = new List<AlumnoCVExperienciaCargo>();
            DataTable dtResultado = acvecd.ObtenerAlumnoCVExperienciaCargoPorIdCVYIdEstudio(IdCV, IdExperienciaCargo);
            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoCVExperienciaCargo alumnocvexperienciacargo = new AlumnoCVExperienciaCargo();
                    alumnocvexperienciacargo.IdCVExperienciaCargo = Funciones.ToInt(dtResultado.Rows[i]["IdCVExperienciaCargo"]);
                    alumnocvexperienciacargo.IdCV = Funciones.ToInt(dtResultado.Rows[i]["IdCV"]);
                    alumnocvexperienciacargo.IdExperienciaCargo = Funciones.ToInt(dtResultado.Rows[i]["IdExperienciaCargo"]);
                    listaAlumnoCVExperienciaCargo.Add(alumnocvexperienciacargo);
                }
            }
            return listaAlumnoCVExperienciaCargo;
        }

        public List<AlumnoExperienciaCargo> ObtenerAlumnoCVExperienciaCargoPorIdAlumno(int IdCV, List<AlumnoExperienciaCargo> listaalumnoexperienciacargo)
        {
            for (int x = 0; x <= listaalumnoexperienciacargo.Count - 1; x++)
            {
                if (ObtenerAlumnoCVExperienciaCargoPorIdAlumno(IdCV, listaalumnoexperienciacargo[x].IdExperienciaCargo).Count > 0)
                {
                    listaalumnoexperienciacargo[x].Incluir = true;
                }
            }
            return listaalumnoexperienciacargo;

        }
    }
}

