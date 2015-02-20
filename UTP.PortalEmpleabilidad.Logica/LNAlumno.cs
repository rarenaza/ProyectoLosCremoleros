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
                alumno.TipoDocumentoValor = dtResultado.Rows[0]["TipoDocumentoValor"].ToString();
                alumno.NumeroDocumento = dtResultado.Rows[0]["NumeroDocumento"].ToString();
                alumno.Nombres = dtResultado.Rows[0]["Nombres"].ToString();
                alumno.Apellidos = dtResultado.Rows[0]["Apellidos"].ToString();
                alumno.CorreoElectronico1 = dtResultado.Rows[0]["CorreoElectronico"].ToString();
                alumno.CorreoElectronico2 = dtResultado.Rows[0]["CorreoElectronico2"].ToString();
                alumno.DireccionLinea1 = dtResultado.Rows[0]["Direccion"].ToString();
                alumno.DireccionDistrito = dtResultado.Rows[0]["DireccionDistrito"].ToString();
                alumno.DireccionCiudad = dtResultado.Rows[0]["DireccionCiudad"].ToString();
                alumno.DireccionRegion = dtResultado.Rows[0]["DireccionRegion"].ToString();
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

        public Alumno Alumno_ObtenerFoto(int idAlumno)
        {
            Alumno registroAlumno = new Alumno();
            DataTable dtResultado = ad.Alumno_ObtenerFoto(idAlumno);
            if (dtResultado.Rows.Count > 0)
            {
                registroAlumno.Foto = Funciones.ToBytes(dtResultado.Rows[0]["Foto"]);
                registroAlumno.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[0]["ArchivoMimeType"]);
            }
            return registroAlumno;
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
                alumno.DireccionRegionId = Funciones.ToString(dtResultado.Rows[0]["DireccionRegionId"]);
                alumno.DireccionCiudadId = Funciones.ToString(dtResultado.Rows[0]["DireccionCiudadId"]);
                alumno.DireccionDistritoId = Funciones.ToString(dtResultado.Rows[0]["DireccionDistritoId"]);
                alumno.CorreoElectronico1 = Funciones.ToString(dtResultado.Rows[0]["CorreoElectronico"]);
                alumno.CorreoElectronico2 = Funciones.ToString(dtResultado.Rows[0]["CorreoElectronico2"]);
                alumno.TelefonoCelular = Funciones.ToString(dtResultado.Rows[0]["TelefonoCelular"]);
                alumno.TelefonoFijoCasa = Funciones.ToString(dtResultado.Rows[0]["TelefonoFijoCasa"]);
                alumno.Foto = Funciones.ToBytes(dtResultado.Rows[0]["Foto"]);
                alumno.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[0]["ArchivoMimeType"]);
                alumno.CodAlumnoUTP = Funciones.ToString(dtResultado.Rows[0]["CodAlumnoUtp"]);
                alumno.FechaCreacion = Funciones.ToString(dtResultado.Rows[0]["FechaCreacion"]);
                alumno.EstadoAlumno = Funciones.ToString(dtResultado.Rows[0]["EstadoAlumno"]);
            }

            return alumno;
        }


        public DataTable Utp_BuscarDatosListaEmpresas(int idempresa)
        {

            return ad.Utp_BuscarDatosListaEmpresas(idempresa);
        }


    }
}
