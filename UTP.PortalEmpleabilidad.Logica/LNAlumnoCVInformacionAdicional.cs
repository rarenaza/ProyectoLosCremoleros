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
    public class LNAlumnoCVInformacionAdicional
    {
        ADAlumnoCVInformacionAdicional acviad = new ADAlumnoCVInformacionAdicional();
        private List<AlumnoCVInformacionAdicional> ObtenerAlumnoCVInformacionAdicionalPorIdCV(int IdCV, int IdInformacionAdicional)
        {
            List<AlumnoCVInformacionAdicional> listaAlumnoCVInformacionAdicional = new List<AlumnoCVInformacionAdicional>();
            DataTable dtResultado = acviad.ObtenerAlumnoCVInformacionAdicionalPorIdCVYIdEstudio(IdCV, IdInformacionAdicional);
            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoCVInformacionAdicional alumnocvinformacionadicional = new AlumnoCVInformacionAdicional();
                    alumnocvinformacionadicional.IdCVInformacionAdicional = Funciones.ToInt(dtResultado.Rows[i]["IdCVInformacionAdicional"]);
                    alumnocvinformacionadicional.IdCV = Funciones.ToInt(dtResultado.Rows[i]["IdCV"]);
                    alumnocvinformacionadicional.IdInformacionAdicional = Funciones.ToInt(dtResultado.Rows[i]["IdInformacionAdicional"]);
                    listaAlumnoCVInformacionAdicional.Add(alumnocvinformacionadicional);
                }
            }
            return listaAlumnoCVInformacionAdicional;
        }

        public List<AlumnoInformacionAdicional> ObtenerAlumnoCVInformacionAdicionalPorIdCV(int IdCV, List<AlumnoInformacionAdicional> listaalumnoinformacionadicional)
        {
            for (int x = 0; x <= listaalumnoinformacionadicional.Count - 1; x++)
            {
                if (ObtenerAlumnoCVInformacionAdicionalPorIdCV(IdCV, listaalumnoinformacionadicional[x].IdInformacionAdicional).Count > 0)
                {
                    listaalumnoinformacionadicional[x].Incluir = true;
                }
            }
            return listaalumnoinformacionadicional;

        }
    }
}
