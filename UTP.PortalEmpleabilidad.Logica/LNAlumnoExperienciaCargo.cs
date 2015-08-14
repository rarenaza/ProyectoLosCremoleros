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
            LNGeneral lngeneral = new LNGeneral();
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
                    alumnoexperienciacargo.IconoTipoCargo = Funciones.ToString(dtResultado.Rows[i]["IconoTipoCargo"]);
                    listaAlumnoExperienciaCargo.Add(alumnoexperienciacargo);
                }


            }

            return listaAlumnoExperienciaCargo;
        }

       public AlumnoExperiencia ObtenerAlumnoExperienciaCargoPorId(int IdExperienciaCargo)
       {
           DataTable dtResultado = new DataTable();
           dtResultado = aecd.ObtenerAlumnoExperienciaCargoPorId(IdExperienciaCargo);
           AlumnoExperiencia alumnoexperiencia = new AlumnoExperiencia();

           if (dtResultado.Rows.Count > 0)
           {
               alumnoexperiencia.IdExperienciaCargo = Funciones.ToInt(dtResultado.Rows[0]["IdExperienciaCargo"]);
               alumnoexperiencia.IdExperiencia = Funciones.ToInt(dtResultado.Rows[0]["IdExperiencia"]);
               alumnoexperiencia.NombreCargo = Funciones.ToString(dtResultado.Rows[0]["NombreCargo"]);
               alumnoexperiencia.FechaInicioCargoMes = Funciones.ToInt(dtResultado.Rows[0]["FechaInicioCargoMes"]);
               alumnoexperiencia.Empresa = Funciones.ToString(dtResultado.Rows[0]["Empresa"]);
               alumnoexperiencia.DescripcionEmpresa = Funciones.ToString(dtResultado.Rows[0]["DescripcionEmpresa"]);
               alumnoexperiencia.IdEmpresa = Funciones.ToInt(dtResultado.Rows[0]["IdEmpresa"]);
               alumnoexperiencia.SectorEmpresarial = Funciones.ToString(dtResultado.Rows[0]["SectorEmpresarial"]);
               alumnoexperiencia.SectorEmpresarial2 = Funciones.ToString(dtResultado.Rows[0]["SectorEmpresarial2"]);
               alumnoexperiencia.SectorEmpresarial3 = Funciones.ToString(dtResultado.Rows[0]["SectorEmpresarial3"]);
               alumnoexperiencia.Pais = Funciones.ToString(dtResultado.Rows[0]["Pais"]);
               alumnoexperiencia.Ciudad = Funciones.ToString(dtResultado.Rows[0]["Ciudad"]);
               alumnoexperiencia.FechaInicioCargoAno = Funciones.ToInt(dtResultado.Rows[0]["FechaInicioCargoAno"]);
               alumnoexperiencia.FechaFinCargoMes = Funciones.ToInt(dtResultado.Rows[0]["FechaFinCargoMes"]);
               alumnoexperiencia.FechaFinCargoAno = Funciones.ToInt(dtResultado.Rows[0]["FechaFinCargoAno"]);
               alumnoexperiencia.TipoCargo = Funciones.ToString(dtResultado.Rows[0]["TipoCargo"]);
               alumnoexperiencia.DescripcionCargo = Funciones.ToString(dtResultado.Rows[0]["DescripcionCargo"]);
           }
           return alumnoexperiencia;
       }

       public void Update(AlumnoExperiencia alumnoexperiencia)
       {
           aecd.Update(alumnoexperiencia);

       }

       public void Desactivar( int IdExperienciaCargo, string Usuario)
       {
           aecd.Desactivar( IdExperienciaCargo,Usuario);

       }
    }
}
