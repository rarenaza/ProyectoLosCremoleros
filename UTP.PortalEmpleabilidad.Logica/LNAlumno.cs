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
                alumno.IdAlumno = int.Parse( dtResultado.Rows[0]["IdAlumno"].ToString());
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
                alumno.Carrera = dtResultado.Rows[0]["Carrera"].ToString();
            }

            return alumno;
        }

     

        public VistaPanelAlumno ObtenerPanel(string codigoAlumno)
        {
            VistaPanelAlumno panel = new VistaPanelAlumno();
            LNEvento eventos = new LNEvento();
            LNOferta ofertas = new LNOferta();

            //Se llenan los datos del alumno.
            panel.Alumno = ObtenerAlumnoPorCodigo(codigoAlumno);
            panel.ListaEventos = eventos.Evento_MostrarUltimos();
            panel.ListaOfertas = ofertas.MostrarUltimasOfertas(panel.Alumno.IdAlumno);
            return panel;
        } 

        public VistaPanelAlumnoPostulaciones ObtenerPanelPostulaciones(string codigoAlumno)
        {
            VistaPanelAlumnoPostulaciones panel = new VistaPanelAlumnoPostulaciones();

            //Se llenan los datos del alumno.
            panel.Alumno = ObtenerAlumnoPorCodigo(codigoAlumno);

            return panel;
        }

        public VistaPanelAlumnoOfertas ObtenerPanelOfertas(string codigoAlumno)
        {
            VistaPanelAlumnoOfertas panel = new VistaPanelAlumnoOfertas();

            //Se llenan los datos del alumno.
            panel.Alumno = ObtenerAlumnoPorCodigo(codigoAlumno); 

            return panel;
        }


    }
}
