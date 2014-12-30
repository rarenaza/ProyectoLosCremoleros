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
                alumno.DireccionLinea1 = dtResultado.Rows[0]["Direccion"].ToString();
                alumno.TelefonoFijoCasa = dtResultado.Rows[0]["TelefonoFijoCasa"].ToString();
                alumno.TelefonoCelular = dtResultado.Rows[0]["TelefonoCelular"].ToString();
                alumno.Carrera = dtResultado.Rows[0]["Carrera"].ToString();
                alumno.Foto =Funciones.ToBytes( dtResultado.Rows[0]["Foto"]);
                alumno.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[0]["ArchivoMimeType"]);

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

        public void ModifcarDatos(Alumno alumno)
        {
            ad.ModifcarDatos(alumno);
        }

        public Alumno ObtenerAlumnoPorIdAlumno(int IdAlumno)
        {
            Alumno alumno = new Alumno();

            DataTable dtResultado = ad.ObtenerAlumnoPorIdAlumno(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                alumno.IdAlumno = Funciones.ToInt(dtResultado.Rows[0]["IdAlumno"]);
                alumno.Usuario = Funciones.ToString(dtResultado.Rows[0]["Usuario"]);
                alumno.Nombres = Funciones.ToString(dtResultado.Rows[0]["Nombres"]);
                alumno.Apellidos = Funciones.ToString(dtResultado.Rows[0]["Apellidos"]);
                alumno.TipoDocumentoIdListaValor = Funciones.ToString(dtResultado.Rows[0]["TipoDocumento"]);
                alumno.NumeroDocumento = Funciones.ToString(dtResultado.Rows[0]["NumeroDocumento"]);
                alumno.FechaNacimiento = Funciones.ToDateTime(dtResultado.Rows[0]["FechaNacimiento"]);
                alumno.SexoIdListaValor = Funciones.ToString(dtResultado.Rows[0]["Sexo"]);
                alumno.Direccion = Funciones.ToString(dtResultado.Rows[0]["Direccion"]);
                alumno.DireccionRegion = Funciones.ToString(dtResultado.Rows[0]["DireccionRegion"]);
                alumno.DireccionCiudad = Funciones.ToString(dtResultado.Rows[0]["DireccionCiudad"]);
                alumno.DireccionDistrito = Funciones.ToString(dtResultado.Rows[0]["DireccionDistrito"]);
                alumno.CorreoElectronico1 = Funciones.ToString(dtResultado.Rows[0]["CorreoElectronico"]);
                alumno.CorreoElectronico2 = Funciones.ToString(dtResultado.Rows[0]["CorreoElectronico2"]);
                alumno.TelefonoCelular = Funciones.ToString(dtResultado.Rows[0]["TelefonoCelular"]);
                alumno.TelefonoFijoCasa = Funciones.ToString(dtResultado.Rows[0]["TelefonoFijoCasa"]);
                alumno.Foto = Funciones.ToBytes(dtResultado.Rows[0]["Foto"]);
                alumno.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[0]["ArchivoMimeType"]);
            }

            return alumno;
        }
    }
}
