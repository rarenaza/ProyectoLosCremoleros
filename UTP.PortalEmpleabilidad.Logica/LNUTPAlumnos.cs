using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using System.Data;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNUTPAlumnos
    {
        ADUTPAlumnos adUTPAlumnos = new ADUTPAlumnos();

        public DataSet ObtenerDatosPorCodigo(string codigo)
        {
            return adUTPAlumnos.ObtenerDatosPorCodigo(codigo);
        }

        public int InsertarDatosDeAlumno(DataSet dsDatosAlumno)
        {
            Alumno alumno = new Alumno();
            Usuario usuario = new Usuario();
            List<AlumnoEstudio> alumnoEstudio = new List<AlumnoEstudio>();


            //Tabla 0 = Datos del alumno y usuario
            //Usuario:
            usuario.NombreUsuario = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Codigo"]);
            usuario.TipoUsuarioIdListaValor = "USERAL";  //Tipo de usuario Alumno.
            usuario.EstadoUsuarioIdListaValor = "USEMAC"; //Usuario activo.
            usuario.CreadoPor = "sistema";

            //Alumno:
            alumno.CodAlumnoUTP = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Codigo"]);
            alumno.Nombres = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Nombres"]);
            alumno.Apellidos = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Apellidos"]);
            alumno.TipoDocumentoIdListaValor = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["TipoDocumento"]).Substring(0, 2);
            alumno.NumeroDocumento = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["NumeroDocumento"]);
            alumno.FechaNacimiento = ConvertirFecha(Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["FechaNacimiento"]));
            alumno.SexoIdListaValor = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Sexo"]);
            alumno.Direccion = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Direccion"]);
            alumno.DireccionCiudad = "";
            alumno.DireccionDistrito = "";
            alumno.DireccionRegion = "";
            alumno.Foto = new byte[10];

            alumno.CorreoElectronico1 = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["CorreoInstitucional"]);
            alumno.CorreoElectronico2 = "";

            alumno.TelefonoFijoCasa = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Telefonos"]);
            alumno.TelefonoCelular = Convert.ToString(dsDatosAlumno.Tables[0].Rows[0]["Celular"]);
            alumno.EstadoAlumnoIdListaValor = "ALACT"; //Estado del alumno ACTIVO.
            alumno.CreadoPor = "sistema";

            string EstudioGrado = "";
            bool EsEstudiante = false;
            //Tabla 1 = Datos del estudio

            Boolean bEstudioPrincipal = true;

            for (int i = 0; i <= dsDatosAlumno.Tables[1].Rows.Count - 1; i++)
            {
                AlumnoEstudio alumnoEstudioItem = new AlumnoEstudio();
                alumnoEstudioItem.Institucion = Constantes.NOMBRE_UTP;
                alumnoEstudioItem.Estudio = Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["CarreraEgreso"]);

                alumnoEstudioItem.TipoDeEstudio = Constantes.TIPO_ESTUDIO_PRINCIPAL; //Realizar función de conversión.
                EstudioGrado = Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["Grado"]).Substring(0, 6);

                alumnoEstudioItem.EstadoDelEstudio = EstudioGrado;

                alumnoEstudioItem.Observacion = "";
                alumnoEstudioItem.FechaInicioAno = ConvertirFecha(Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["FechaInicio"])).Year;
                alumnoEstudioItem.FechaInicioMes = ConvertirFecha(Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["FechaInicio"])).Month;

                //Cuando un alumno es estudiante, las fechas de fin son nulas
                if (!alumnoEstudioItem.EstadoDelEstudio.Equals("EDEEST"))
                {
                    alumnoEstudioItem.FechaFinAno = ConvertirFecha(Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["FechaFinal"])).Year;
                    alumnoEstudioItem.FechaFinMes = ConvertirFecha(Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["FechaFinal"])).Month;
                }

                if (!String.IsNullOrEmpty(Convert.ToString(dsDatosAlumno.Tables[1].Rows[i]["Ciclo"])))
                    alumnoEstudioItem.CicloEquivalente = Convert.ToInt32(dsDatosAlumno.Tables[1].Rows[i]["Ciclo"]);

                alumnoEstudioItem.DatoUTP = bEstudioPrincipal;

                bEstudioPrincipal = false;
                alumnoEstudioItem.DatoCargado = true;
                alumnoEstudioItem.CreadoPor = "sistema";
                alumnoEstudio.Add(alumnoEstudioItem);
            }
            int idAlumno = adUTPAlumnos.InsertarDatosDeAlumno(alumno, usuario, alumnoEstudio);
            return idAlumno;
        }

        public DateTime ConvertirFecha(string fechaCadena)
        {
            DateTime fecha = new DateTime();

            int anio = Convert.ToInt32(fechaCadena.Substring(0, 4));
            int mes = Convert.ToInt32(fechaCadena.Substring(4, 2));
            int dia = Convert.ToInt32(fechaCadena.Substring(6, 2));

            fecha = new DateTime(anio, mes, dia);

            return fecha;
        }

        public DataTable AlumnoUTP_ObtenerDatosPorCodigo(int id)
        {
            return adUTPAlumnos.AlumnoUTP_ObtenerDatosPorCodigo(id);
        }
        public DataTable UTP_BUSCARLISTAVALORPADRE(int id)
        {
            return adUTPAlumnos.UTP_BUSCARLISTAVALORPADRE(id);
        }



        public DataTable AlumnoUtp_obtenerEstudios(int id)
        {
            return adUTPAlumnos.AlumnoUtp_obtenerEstudios(id);
        }

        public DataTable UtpAlumnoMenuMostrar()
        {
            return adUTPAlumnos.UtpAlumnoMenuMostrar();
        }


        public bool UTPAlumnos_ActualizarEstadoAlumno(UtpAlumnoDetalle alumno)
        {


            if (adUTPAlumnos.UTPAlumnos_ActualizarEstadoAlumno(alumno) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataTable AlumnoUtp_obtenerExperiencia(int id)
        {
            return adUTPAlumnos.AlumnoUtp_obtenerExperiencia(id);
        }

        public DataTable AlumnoUtp_obtenerInformacionAdicional(int id)
        {
            return adUTPAlumnos.AlumnoUtp_obtenerInformacionAdicional(id);
        }




    }
}
