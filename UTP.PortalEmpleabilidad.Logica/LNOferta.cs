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
    public class LNOferta
    {

        ADOferta adOferta = new ADOferta();
        
          
        public List<VistaOfertaAlumno> Oferta_Mostrar()
        {
            List<VistaOfertaAlumno> listaOferta = new List<VistaOfertaAlumno>();

            DataTable dtResultado = adOferta.Obtener();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                VistaOfertaAlumno oferta = new VistaOfertaAlumno();

                oferta.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                oferta.Empresa = dtResultado.Rows[i]["Empresa"].ToString();

                oferta.CargoOfrecido = dtResultado.Rows[i]["CargoOfrecido"].ToString();
                oferta.Horario = Convert.ToDateTime(dtResultado.Rows[i]["Horario"]);

                oferta.RemuneracionOfrecida = Convert.ToDecimal(dtResultado.Rows[i]["RangoRemuneracionDesde"]);

                oferta.EstadoOferta = dtResultado.Rows[i]["EstadoOferta"].ToString();

                listaOferta.Add(oferta);
            }
            return listaOferta;
        }

        public List<VistaPostulacionAlumno> ObtenerPostulantes()
        {
            List<VistaPostulacionAlumno> listapostulacion = new List<VistaPostulacionAlumno>();

            DataTable dtResultado = adOferta.ObtenerPostulantes();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                VistaPostulacionAlumno postulacion = new VistaPostulacionAlumno();

                postulacion.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                postulacion.FechaPostulacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPostulacion"]);
                postulacion.Empresa = dtResultado.Rows[i]["Empresa"].ToString();
                postulacion.CargoOfrecido = dtResultado.Rows[i]["CargoOfrecido"].ToString();
                postulacion.TipoTrabajo = dtResultado.Rows[i]["TipoTrabajo"].ToString();
                postulacion.Horario = Convert.ToDateTime(dtResultado.Rows[i]["Horario"]);
                postulacion.RemuneracionOfrecida = Convert.ToDecimal(dtResultado.Rows[i]["RangoRemuneracionDesde"]);
                postulacion.EstadoOferta = dtResultado.Rows[i]["EstadoOferta"].ToString();

                listapostulacion.Add(postulacion);
            }
            return listapostulacion;
        }

        //Obtiene las Listas de opciones (Todo los Combos)
        public DataTable ObtenerLista_ListaValor(int cod)
        {
            return adOferta.ObtenerLista_ListaValor(cod);
        }    


        public List<VistaOfertaEmpresa> Obtener_PanelEmpresa(int idEmpresa)
        {
            List<VistaOfertaEmpresa> lista = new List<VistaOfertaEmpresa>();

            DataTable dtResultados = adOferta.Obtener_PanelEmpresa(idEmpresa);

            foreach(DataRow fila in dtResultados.Rows)
            {
                VistaOfertaEmpresa vista = new VistaOfertaEmpresa();
                vista.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                vista.FechaPublicacion = Convert.ToDateTime(fila["FechaPublicacion"]);
                vista.Cargo = Convert.ToString(fila["CargoOfrecido"]);
                vista.CVPendientesRevision = Convert.ToInt32(fila["CVPendientesRevision"]);
                vista.CVTotales = Convert.ToInt32(fila["CVTotales"]);
                vista.FaseActual = Convert.ToString(fila["FaseActual"]);
                vista.EstadoOferta = Convert.ToString(fila["EstadoOferta"]);
                vista.MensajesNoLeidos = Convert.ToInt32(fila["MensajesNoLeidos"]);
                vista.MensajesTotales = Convert.ToInt32(fila["MensajesTotales"]);

                lista.Add(vista);
            }

            return lista;
        }
     

    }

}
