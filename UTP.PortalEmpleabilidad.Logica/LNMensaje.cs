using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Mensaje;

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

        public void Insertar(Mensaje mensaje)
        {
            adMensaje.Insertar(mensaje);
        }
    }
}
