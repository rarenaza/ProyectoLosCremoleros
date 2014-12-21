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
    public class LNAlumnoCV
    {
        ADAlumnoCV acv = new ADAlumnoCV();

        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumno(int IdAlumno)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();

            DataTable dtResultado = acv.ObtenerAlumnoCVPorIdAlumno(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoCV alumnocv = new AlumnoCV();
                    alumnocv.IdCV = int.Parse(dtResultado.Rows[i]["IdCV"].ToString());
                    alumnocv.NombreCV = dtResultado.Rows[i]["NombreCV"].ToString();
                    alumnocv.IdPlantillaCV = int.Parse(dtResultado.Rows[i]["IdPlantillaCV"].ToString());
                    listaAlumnoCV.Add(alumnocv);
                }


            }

            return listaAlumnoCV;
        }
        public AlumnoCV ObtenerAlumnoCVPorIdAlumnoYIdCV(int IdAlumno, int IdCV)
        {
            AlumnoCV alumnocv = new AlumnoCV();

            DataTable dtResultado = acv.ObtenerAlumnoCVPorIdAlumnoYIdCV(IdAlumno,IdCV);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    alumnocv.IncluirCorreoElectronico2 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirCorreoElectronico2"]);
                    alumnocv.IncluirFoto = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirFoto"]);
                    alumnocv.IncluirTelefonoFijo = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirTelefonoFijo"]);
                    alumnocv.IncluirDireccion = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirDireccion"]);
                    alumnocv.Perfil = Funciones.ToString(dtResultado.Rows[i]["Perfil"]);

                }
            }

            return alumnocv;
        }



        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumno(AlumnoCV alumnocv)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();
            listaAlumnoCV = ObtenerAlumnoCVPorIdAlumno(alumnocv.IdAlumno);
            if (listaAlumnoCV ==null || listaAlumnoCV.Count == 0)
            {
                ADAlumnoCV acv = new ADAlumnoCV();
                acv.Insertar(alumnocv);
                listaAlumnoCV = ObtenerAlumnoCVPorIdAlumno(alumnocv.IdAlumno);
            }
            return listaAlumnoCV;

        }

        public void UpdateInfo(VistaAlumnoCV vistaalumnocv)
        {
            ADAlumnoCVEstudio acve = new ADAlumnoCVEstudio();
            ADAlumnoCVExperienciaCargo acvs = new ADAlumnoCVExperienciaCargo();
            ADAlumnoCVInformacionAdicional acvc = new ADAlumnoCVInformacionAdicional();
            acv.Update(vistaalumnocv);
            acve.DesactivarPorCV(vistaalumnocv.IdCV);
            acvs.DesactivarPorCV(vistaalumnocv.IdCV);
            acvc.DesactivarPorCV(vistaalumnocv.IdCV);
            foreach (AlumnoEstudio modelo in vistaalumnocv.Estudios)
            {
                acve.AgregarOrModificar(vistaalumnocv.IdCV, modelo.IdEstudio, vistaalumnocv.Usuario);
            }

            foreach (AlumnoExperienciaCargo modelo in vistaalumnocv.Experiencias)
            {
                acvs.AgregarOrModificar(vistaalumnocv.IdCV, modelo.IdExperienciaCargo, vistaalumnocv.Usuario);
            }

            foreach (AlumnoInformacionAdicional modelo in vistaalumnocv.Conocimientos)
            {
                acvc.AgregarOrModificar(vistaalumnocv.IdCV, modelo.IdInformacionAdicional, vistaalumnocv.Usuario);
            }




        }
    }
}
