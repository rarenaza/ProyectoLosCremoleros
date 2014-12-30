using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;

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

            DataTable dtResultado = acv.ObtenerAlumnoCVPorIdAlumnoYIdCV(IdAlumno, IdCV);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    alumnocv.IncluirCorreoElectronico2 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirCorreoElectronico2"]);
                    alumnocv.IncluirFoto = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirFoto"]);
                    alumnocv.IncluirTelefonoFijo = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirTelefonoFijo"]);
                    alumnocv.IncluirDireccion = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirDireccion"]);
                    alumnocv.IncluirNombre1 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre1"]);
                    alumnocv.IncluirNombre2 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre2"]);
                    alumnocv.IncluirNombre3 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre3"]);
                    alumnocv.IncluirNombre4 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre4"]);

                    alumnocv.Perfil = Funciones.ToString(dtResultado.Rows[i]["Perfil"]);

                }
            }

            return alumnocv;
        }



        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumno(AlumnoCV alumnocv)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();
            listaAlumnoCV = ObtenerAlumnoCVPorIdAlumno(alumnocv.IdAlumno);
            if (listaAlumnoCV == null || listaAlumnoCV.Count == 0)
            {
                ADAlumnoCV acv = new ADAlumnoCV();
                acv.Insertar(alumnocv);
                listaAlumnoCV = ObtenerAlumnoCVPorIdAlumno(alumnocv.IdAlumno);
            }
            return listaAlumnoCV;

        }

        public void UpdateInfo(AlumnoCV alumnocv)
        {
            ADAlumnoCVEstudio acve = new ADAlumnoCVEstudio();
            ADAlumnoCVExperienciaCargo acvs = new ADAlumnoCVExperienciaCargo();
            ADAlumnoCVInformacionAdicional acvc = new ADAlumnoCVInformacionAdicional();
            acv.Update(alumnocv);
            acve.DesactivarPorCV(alumnocv.IdCV);
            acvs.DesactivarPorCV(alumnocv.IdCV);
            acvc.DesactivarPorCV(alumnocv.IdCV);
            if (alumnocv.Estudios != null)
            {
                foreach (VistaAlumnoEstudio modelo in alumnocv.Estudios)
                {
                    acve.AgregarOrModificar(alumnocv.IdCV, modelo.IdEstudio, alumnocv.Usuario);
                }
            }

            if (alumnocv.Experiencias != null)
            {
                foreach (VistaAlumnoExperienciaCargo modelo in alumnocv.Experiencias)
                {
                    acvs.AgregarOrModificar(alumnocv.IdCV, modelo.IdExperienciaCargo, alumnocv.Usuario);
                }
            }

            if (alumnocv.Conocimientos != null)
            {
                foreach (VistaAlumnoConocimiento modelo in alumnocv.Conocimientos)
                {
                    acvc.AgregarOrModificar(alumnocv.IdCV, modelo.IdInformacionAdicional, alumnocv.Usuario);
                }
            }





        }

        public bool RegistrarCV(ref AlumnoCV alumnocv)
        {
            bool existe = false;
            if (acv.ValidarExistencia(alumnocv.IdAlumno, alumnocv.NombreCV) == false)
            {
              existe=  acv.RegistrarCV(ref alumnocv);
            }
            return existe;
        }
    }
}
