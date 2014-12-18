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
    public class LNAlumnoEstudio
    {
        ADAlumnoEstudio aed = new ADAlumnoEstudio();
        public List<AlumnoEstudio> ObtenerAlumnoEstudioPorIdAlumno(int IdAlumno)
        {
            List<AlumnoEstudio> listaAlumno = new List<AlumnoEstudio>();

            DataTable dtResultado = aed.ObtenerAlumnoEstudioPorIdAlumno(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoEstudio alumnoestudio = new AlumnoEstudio();
                    alumnoestudio.IdEstudio = int.Parse(dtResultado.Rows[i]["IdEstudio"].ToString());
                    alumnoestudio.IdAlumno = int.Parse(dtResultado.Rows[i]["IdAlumno"].ToString());
                    alumnoestudio.Institucion = dtResultado.Rows[i]["Institucion"].ToString();
                    alumnoestudio.Estudio = dtResultado.Rows[i]["Estudio"].ToString();
                    alumnoestudio.EstadoDelEstudio = dtResultado.Rows[i]["EstadoDelEstudio"].ToString();
                    alumnoestudio.TipoDeEstudio = dtResultado.Rows[i]["TipoDeEstudio"].ToString();
                    alumnoestudio.Observacion = dtResultado.Rows[i]["Observacion"].ToString();
                    alumnoestudio.CicloEquivalente = int.Parse(dtResultado.Rows[i]["CicloEquivalente"].ToString());
                    alumnoestudio.FechInicio = DateTime.Parse(dtResultado.Rows[i]["FechInicio"].ToString());
                    alumnoestudio.FechFin = DateTime.Parse(dtResultado.Rows[i]["FechFin"].ToString());
                    listaAlumno.Add(alumnoestudio);
                }


            }

            return listaAlumno;
        }
        public void Insertar(AlumnoEstudio alumnoestudio)
        {
            aed.Insertar(alumnoestudio);
        }

    }
}
