using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOfertaPostulante
      
    {
        ADAlumno adAlumno = new ADAlumno();
        ADOfertaPostulante adOfertaPostulante = new ADOfertaPostulante();

        public void Insertar(OfertaPostulante ofertapostulante)
        {
            adOfertaPostulante.Insertar(ofertapostulante);
            if (ofertapostulante.RecibeCorreos == "OFCOPO") 
            {
                DataTable dt = adAlumno.ObtenerAlumnoPorIdAlumno(ofertapostulante.IdAlumno);
                if (dt.Rows.Count > 0)
                {
                    Mensaje mensaje = new Mensaje();

                    mensaje.DeUsuarioCorreoElectronico = Constantes.CORREO_PRINCIPAL;
                    mensaje.ParaUsuarioCorreoElectronico = ofertapostulante.CorreoElectronicoUsuarioEmpresa;
                    mensaje.Asunto = "Nuevo Postulante: " + dt.Rows[0]["Nombres"] + " " + dt.Rows[0]["Apellidos"] + " - Oferta " + ofertapostulante.CargoOfrecido;
                    mensaje.MensajeTexto = "Ha recibido un Postulante nuevo para su Oferta: " + ofertapostulante.CargoOfrecido + "\r\n\r\n" + dt.Rows[0]["Nombres"] + " " + dt.Rows[0]["Apellidos"];
                    LNCorreo.EnviarCorreo(mensaje);
                }
            }
        }

        public void Actualizar(OfertaPostulante ofertapostulante)
        {
            adOfertaPostulante.Actualizar(ofertapostulante);
        }

        public byte[] OfertaPostulante_DescaragarCV(int IdAlumno, int IdOferta)
        {
            byte[] file=null;

            DataTable dtResultado = adOfertaPostulante.OfertaPostulante_DescaragarCV(IdAlumno, IdOferta);

            if (dtResultado.Rows.Count > 0)
            {
                file = (byte[]) dtResultado.Rows[0]["DocumentoCV"];

            }

            return file;
        }

        public List<VistaPostulacionAlumno> ObtenerPostulantesPorIDAlumno(int IdAlumno, string PalabraClave, int PaginaActual, int NumeroRegistros)
        {
            List<VistaPostulacionAlumno> listapostulacion = new List<VistaPostulacionAlumno>();

            DataTable dtResultado = adOfertaPostulante.ObtenerPostulantesPorIDAlumno(IdAlumno, PalabraClave, PaginaActual, NumeroRegistros);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                VistaPostulacionAlumno postulacion = new VistaPostulacionAlumno();

                postulacion.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                postulacion.FechaPostulacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPostulacion"]);
                postulacion.Empresa = Funciones.ToString(dtResultado.Rows[i]["Empresa"]);
                postulacion.CargoOfrecido = Funciones.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                postulacion.TipoTrabajo = Funciones.ToString(dtResultado.Rows[i]["TipoTrabajo"]);
                postulacion.Horario = Funciones.ToString(dtResultado.Rows[i]["Horario"]);
                postulacion.RemuneracionOfrecida = Funciones.ToDecimal(dtResultado.Rows[i]["RemuneracionOfrecida"]);
                postulacion.EstadoOferta = Funciones.ToString(dtResultado.Rows[i]["EstadoOferta"]);
                postulacion.IdOferta = Funciones.ToInt(dtResultado.Rows[i]["IdOferta"]);
                postulacion.Mensajes = Funciones.ToInt(dtResultado.Rows[i]["Mensajes"]);
                postulacion.IdEmpresa = Funciones.ToInt(dtResultado.Rows[i]["IdEmpresa"]);
                postulacion.MaxPagina = Funciones.ToInt(dtResultado.Rows[i]["MaxPagina"]);                

                listapostulacion.Add(postulacion);
            }
            return listapostulacion;
        }

        public DataTable OfertaPostulantesListar()
        {
            return adOfertaPostulante.OfertaPostulantesListar();
        }

    }
}
