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
   public  class LNAlumnoCVEstudio
    {
       ADAlumnoCVEstudio acve = new ADAlumnoCVEstudio();

       private List<AlumnoCVEstudio> ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(int IdCV, int IdEstudio)
        {
            List<AlumnoCVEstudio> listaalumnocvestudio = new List<AlumnoCVEstudio>();

            DataTable dtResultado = acve.ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(IdCV,IdEstudio);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1;i++ )
                {
                    AlumnoCVEstudio alumnoestudio = new AlumnoCVEstudio();
                    alumnoestudio.IdCVEstudio = Funciones.ToInt(dtResultado.Rows[i]["IdCVEstudio"]);
                    alumnoestudio.IdCV = Funciones.ToInt(dtResultado.Rows[i]["IdCV"]);
                    alumnoestudio.IdEstudio = Funciones.ToInt(dtResultado.Rows[i]["IdEstudio"]);
                    listaalumnocvestudio.Add(alumnoestudio);
                }
            }
            return listaalumnocvestudio;
        }
       public List<AlumnoEstudio> ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(int IdCV, List<AlumnoEstudio> listaalumnoestudio)
       {
           for (int x = 0; x <= listaalumnoestudio.Count - 1; x++)
           {
               if (ObtenerAlumnoCVEstudioPorIdCVYIdEstudio(IdCV, listaalumnoestudio[x].IdEstudio).Count > 0)
               {
                   listaalumnoestudio[x].Incluir = true;
               }
           }
           return listaalumnoestudio;

       }

    }
}
