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
    public class LNAlumnoInformacionAdicional
    {
        ADAlumnoInformacionAdicional aiad = new ADAlumnoInformacionAdicional();
        public List<AlumnoInformacionAdicional> ObtenerAlumnoInformacionAdicionalPorIdAlumno(int IdAlumno)
        {
            List<AlumnoInformacionAdicional> listaAlumnoInformacionAdicional = new List<AlumnoInformacionAdicional>();
            DataTable dtResultado = aiad.ObtenerAlumnoInformacionAdicionalPorIdAlumno(IdAlumno);
            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();
                    alumnoinformacionadicional.IdInformacionAdicional = Funciones.ToInt(dtResultado.Rows[i]["IdInformacionAdicional"]);
                    alumnoinformacionadicional.DesTipoConocimiento = Funciones.ToString(dtResultado.Rows[i]["DesTipoConocimiento"]);
                    alumnoinformacionadicional.DesNivelConocimiento= Funciones.ToString(dtResultado.Rows[i]["DesNivelConocimiento"]);
                    alumnoinformacionadicional.Conocimiento = Funciones.ToString(dtResultado.Rows[i]["Conocimiento"]);
                    alumnoinformacionadicional.FechaConocimientoDesdeMes = Funciones.ToInt(dtResultado.Rows[i]["FechaConocimientoDesdeMes"]);
                    alumnoinformacionadicional.FechaConocimientoDesdeAno = Funciones.ToInt(dtResultado.Rows[i]["FechaConocimientoDesdeAno"]);
                    alumnoinformacionadicional.FechaConocimientoHastaMes = Funciones.ToInt(dtResultado.Rows[i]["FechaConocimientoHastaMes"]);
                    alumnoinformacionadicional.FechaConocimientoHastaAno = Funciones.ToInt(dtResultado.Rows[i]["FechaConocimientoHastaAno"]);
                    alumnoinformacionadicional.NomPais = Funciones.ToString(dtResultado.Rows[i]["NomPais"]);
                    alumnoinformacionadicional.Ciudad = Funciones.ToString(dtResultado.Rows[i]["Ciudad"]);
                    alumnoinformacionadicional.InstituciónDeEstudio = Funciones.ToString(dtResultado.Rows[i]["InstituciónDeEstudio"]);
                    alumnoinformacionadicional.AñosExperiencia = Funciones.ToInt(dtResultado.Rows[i]["AñosExperiencia"]);
                    listaAlumnoInformacionAdicional.Add(alumnoinformacionadicional);
                }
            }
            return listaAlumnoInformacionAdicional;
        }

        public void Registrar(AlumnoInformacionAdicional alumnoinformacionadicional)
        {
            

            aiad.Registrar(alumnoinformacionadicional);

        }

        public AlumnoInformacionAdicional ObtenerAlumnoEstudioPorId(int IdInforacionAdicional)
        {
            DataTable dtResultado = new DataTable();
            dtResultado = aiad.ObtenerAlumnoInformacionAdicionalPorId(IdInforacionAdicional);
            AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();

            if (dtResultado.Rows.Count > 0)
            {
                alumnoinformacionadicional.IdInformacionAdicional = Funciones.ToInt(dtResultado.Rows[0]["IdInformacionAdicional"]);
                alumnoinformacionadicional.TipoConocimientoIdListaValor = Funciones.ToString(dtResultado.Rows[0]["TipoConocimiento"]);
                alumnoinformacionadicional.Conocimiento = Funciones.ToString(dtResultado.Rows[0]["Conocimiento"]);
                alumnoinformacionadicional.NivelConocimientoIdListaValor = Funciones.ToString(dtResultado.Rows[0]["NivelConocimiento"]);
                alumnoinformacionadicional.FechaConocimientoDesdeMes = Funciones.ToInt(dtResultado.Rows[0]["FechaConocimientoDesdeMes"]);
                alumnoinformacionadicional.FechaConocimientoDesdeAno = Funciones.ToInt(dtResultado.Rows[0]["FechaConocimientoDesdeAno"]);
                alumnoinformacionadicional.FechaConocimientoHastaMes = Funciones.ToInt(dtResultado.Rows[0]["FechaConocimientoHastaMes"]);
                alumnoinformacionadicional.FechaConocimientoHastaAno = Funciones.ToInt(dtResultado.Rows[0]["FechaConocimientoHastaAno"]);
                alumnoinformacionadicional.PaisIdListaValor = Funciones.ToString(dtResultado.Rows[0]["Pais"]);
                alumnoinformacionadicional.Ciudad = Funciones.ToString(dtResultado.Rows[0]["Ciudad"]);
                alumnoinformacionadicional.InstituciónDeEstudio = Funciones.ToString(dtResultado.Rows[0]["InstituciónDeEstudio"]);
                alumnoinformacionadicional.AñosExperiencia = Funciones.ToInt(dtResultado.Rows[0]["AñosExperiencia"]);
            }
            return alumnoinformacionadicional;
        }

        public void Update(AlumnoInformacionAdicional alumnoinformacionadicional)
        {
            aiad.Update(alumnoinformacionadicional);

        }

        public void Desactivar(int IdInforacionAdicional, string Usuario)
        {
            aiad.Desactivar(IdInforacionAdicional, Usuario);

        }
    }
}
