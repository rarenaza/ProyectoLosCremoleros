using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Mensaje;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNMensaje
    {
        ADMensaje adMensaje = new ADMensaje();

        public List<Mensaje> ObtenerPorIdEmpresaIdOferta(int idEmpresa, int idOferta)
        {
            List<Mensaje> lista = new List<Mensaje>();

            DataTable dtResultado = adMensaje.ObtenerPorIdEmpresa(idEmpresa, idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                Mensaje mensaje = new Mensaje();
                mensaje.FechaEnvio = Convert.ToDateTime(fila["FechaEnvio"]);
                mensaje.Asunto = Convert.ToString(fila["Asunto"]);
                mensaje.MensajeTexto = Convert.ToString(fila["Mensaje"]);
                mensaje.DeUsuario = Convert.ToString(fila["DeUsuario"]);
                mensaje.DeUsuarioCorreoElectronico = Convert.ToString(fila["DeUsuarioCorreoElectronico"]);
                mensaje.ParaUsuario = Convert.ToString(fila["ParaUsuario"]);
                mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(fila["ParaUsuarioCorreoElectronico"]);
                mensaje.Oferta.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                mensaje.IdMensaje = Convert.ToInt32(fila["IdMensaje"]);

                lista.Add(mensaje);                    
            }

            return lista;
        }

        public List<VistaPostulantePorMensaje> ObtenePostulantesPorIdOferta(int idOferta)
        {
            List<VistaPostulantePorMensaje> lista = new List<VistaPostulantePorMensaje>();

            DataTable dtResultado = adMensaje.ObtenePostulantesPorIdOferta(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaPostulantePorMensaje nuevoPostulante = new VistaPostulantePorMensaje();
                nuevoPostulante.IdOfertaPostulante = Convert.ToInt32(fila["IdOfertaPostulante"]);
                nuevoPostulante.AlumnoNombreCompleto = Convert.ToString(fila["AlumnoNombreCompleto"]);
                nuevoPostulante.AlumnoNombreUsuario = Convert.ToString(fila["AlumnoNombreUsuario"]);

                lista.Add(nuevoPostulante);
            }

            return lista;
        }

        public int Insertar(Mensaje mensaje)
        {
            if (mensaje.ParaUsuarioCorreoElectronico == null) mensaje.ParaUsuarioCorreoElectronico = "";
            if (mensaje.IdOfertaMensaje == null) mensaje.IdOfertaMensaje = 0;
            if (mensaje.IdEvento == null) mensaje.IdEvento = 0;

            int idMensaje = adMensaje.Insertar(mensaje);

            //Se envía el correo:
            try
            {
                LNCorreo.EnviarCorreo(mensaje);
            }
            catch (Exception ex)
            {
                //Manejar la excepción del envío de correo.
            }
            

            return idMensaje;
        }

        public void InsertarHunting(Mensaje mensaje)
        {
            if (mensaje.ParaUsuarioCorreoElectronico == null) mensaje.ParaUsuarioCorreoElectronico = "";
            if (mensaje.IdOfertaMensaje == null) mensaje.IdOfertaMensaje = 0;
            if (mensaje.IdEvento == null) mensaje.IdEvento = 0;

            //Se obtiene la lista de los códigos de alumno.
            string[] arrayIds = mensaje.ParaUsuarioCorreoElectronico.Split('-');

            //Se recorreo todos los Id's seleccionados y se envía los correos uno por uno.
            foreach (string idAlumnoCadena in arrayIds)
            {

                string correoPara = adMensaje.InsertarHunting(mensaje, Convert.ToInt32(idAlumnoCadena));
                mensaje.ParaUsuarioCorreoElectronico = correoPara;

                try
                {
                    LNCorreo.EnviarCorreo(mensaje);
                }
                catch (Exception ex)
                {
                    //Manejar la excepción del envío de correo.
                }
            }
        }

        public Mensaje ObtenerPorIdMensaje(int idMensaje)
        {
            Mensaje mensaje = new Mensaje();

            DataTable dtResultado = adMensaje.ObtenerPorIdMensaje(idMensaje);

            foreach (DataRow fila in dtResultado.Rows)
            {
                mensaje = new Mensaje();
                mensaje.FechaEnvio      = Convert.ToDateTime(fila["FechaEnvio"]);
                mensaje.Asunto          = Convert.ToString(fila["Asunto"]);
                mensaje.MensajeTexto    = Convert.ToString(fila["Mensaje"]);
                mensaje.DeUsuario       = Convert.ToString(fila["DeUsuario"]);
                mensaje.DeUsuarioCorreoElectronico = Convert.ToString(fila["DeUsuarioCorreoElectronico"]);
                mensaje.ParaUsuario     = Convert.ToString(fila["ParaUsuario"]);
                mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(fila["ParaUsuarioCorreoElectronico"]);
                mensaje.Oferta.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                mensaje.IdOferta        = Convert.ToInt32(fila["IdOferta"]);
                mensaje.IdMensaje       = Convert.ToInt32(fila["IdMensaje"]);
                mensaje.IdEmpresa       = Convert.ToInt32(fila["IdEmpresa"]);
                mensaje.IdEvento        = Convert.ToInt32(fila["IdEvento"]);   

                //Datos del mensaje anterior
                mensaje.MensajeAnteriorIdMensaje = Convert.ToInt32(fila["MensajeAnteriorIdMensaje"]);
                mensaje.MensajeAnteriorAsunto = Convert.ToString(fila["MensajeAnteriorAsunto"]);
                mensaje.MensajeAnteriorFechaEnvio = Convert.ToDateTime(fila["MensajeAnteriorFechaEnvio"]);

                //Datos del mensaje posterior
                mensaje.MensajePosteriorIdMensaje = Convert.ToInt32(fila["MensajePosteriorIdMensaje"]);
                mensaje.MensajePosteriorAsunto = Convert.ToString(fila["MensajePosteriorAsunto"]);
                mensaje.MensajePosteriorFechaEnvio = Convert.ToDateTime(fila["MensajePosteriorFechaEnvio"]);

                break;
            }

            return mensaje;
        }

        public List<Mensaje> ObtenerPorUsuario(string usuarioAlumno)
        {
            List<Mensaje> lista = new List<Mensaje>();

            DataTable dtResultado = adMensaje.ObtenerPorUsuario(usuarioAlumno);

            foreach (DataRow fila in dtResultado.Rows)
            {
                Mensaje mensaje = new Mensaje();
                mensaje.FechaEnvio = Convert.ToDateTime(fila["FechaEnvio"]);
                mensaje.Asunto = Convert.ToString(fila["Asunto"]);
                mensaje.MensajeTexto = Convert.ToString(fila["Mensaje"]);
                mensaje.DeUsuario = Convert.ToString(fila["DeUsuario"]);
                mensaje.DeUsuarioCorreoElectronico = Convert.ToString(fila["DeUsuarioCorreoElectronico"]);
                mensaje.ParaUsuario = Convert.ToString(fila["ParaUsuario"]);
                mensaje.ParaUsuarioCorreoElectronico = Convert.ToString(fila["ParaUsuarioCorreoElectronico"]);
                mensaje.Oferta.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                mensaje.Oferta.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                mensaje.IdMensaje = Convert.ToInt32(fila["IdMensaje"]);
                mensaje.EstadoMensaje = Convert.ToString(fila["EstadoMensaje"]);
                mensaje.IdEvento = Convert.ToInt32(fila["IdEvento"]);

                lista.Add(mensaje);
            }

            return lista;
        }

        public List<VistaAlumnoOferta> ObtenerOfertasPorIdAlumno(int idAlumno)
        {
            List<VistaAlumnoOferta> lista = new List<VistaAlumnoOferta>();

            DataTable dtResultado = new DataTable();

            dtResultado = adMensaje.ObtenerOfertasPorIdAlumno(idAlumno);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaAlumnoOferta vista = new VistaAlumnoOferta();

                vista.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                vista.IdAlumno = Convert.ToInt32(fila["IdAlumno"]);
                vista.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
               
                lista.Add(vista);
            }

            return lista;
        }

        public VistaMensajeUsuarioEmpresaOferta ObtenerUsuarioEmpresaOfertaPorId(int idOferta)
        {
            VistaMensajeUsuarioEmpresaOferta oferta = new VistaMensajeUsuarioEmpresaOferta();

            DataTable dtResultado = adMensaje.ObtenerUsuarioEmpresaOfertaPorId(idOferta);

            if (dtResultado.Rows.Count > 0)
            {
                oferta.IdOferta = Convert.ToInt32(dtResultado.Rows[0]["IdOferta"]);
                oferta.UsuarioPropietarioEmpresa = Convert.ToString(dtResultado.Rows[0]["UsuarioPropietarioEmpresa"]);
                oferta.UsuarioPropietarioEmpresaCorreo = Convert.ToString(dtResultado.Rows[0]["CorreoElectronico"]);

            }

            return oferta;
        }
        public int ObtenerIdEmpresaPorIdOferta(int idOferta)
        {
            return adMensaje.ObtenerIdEmpresaPorIdOferta(idOferta);
        }

        public void ActualizarEstadoMensaje(int idMensaje, string estadoMensaje)
        {
            adMensaje.ActualizarEstadoMensaje(idMensaje, estadoMensaje);
        }

        public DataTable ObtenerUsuarioAdministradorUTP()
        {
            return adMensaje.ObtenerUsuarioAdministradorUTP();
        }

        public DataTable ObtenerUsuarioUTPAsignadoAEmpresa(int idEmpresa)
        {
            return adMensaje.ObtenerUsuarioUTPAsignadoAEmpresa(idEmpresa);
        }

        public DataTable ObtenerUsuarioEmpresaPorIdEmpresa(int idEmpresa)
        {
            return adMensaje.ObtenerUsuarioEmpresaPorIdEmpresa(idEmpresa);
        }

        public string ObtenerListaAlumnosHunting(string listaIdAlumnos)
        {
            if (listaIdAlumnos == "") return "";

            string[] arrayIds = listaIdAlumnos.Split('-');
            StringBuilder usuarios = new StringBuilder();

            foreach (string id in arrayIds)
            {
                DataTable dt = adMensaje.ObtenerDatosBasicosPorIdAlumno(Convert.ToInt32(id));
                string usuario = Convert.ToString(dt.Rows[0]["Apellidos"]) + " " + Convert.ToString(dt.Rows[0]["Nombres"]);
                usuarios.Append(usuario); 
                usuarios.Append(", ");
            }

            string resultado = usuarios.ToString().Substring(0, usuarios.Length - 2);

            return resultado;
        }
    }
}
