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
        
        public List<Oferta> Oferta_Mostrar()
        {
            List<Oferta> listaOferta = new List<Oferta>();

            DataTable dtResultado = adOferta.Obtener();


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Oferta oferta = new Oferta();
                   
                

                oferta.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
              
                oferta.CargoOfrecido = dtResultado.Rows[i]["CargoOfrecido"].ToString();
                oferta.Horario = Convert.ToDateTime(dtResultado.Rows[i]["Horario"]);
            
                oferta.RemuneracionOfrecida = Convert.ToDecimal(dtResultado.Rows[i]["RangoRemuneracionDesde"]);
     
                oferta.EstadoOferta = dtResultado.Rows[i]["EstadoOferta"].ToString();

                listaOferta.Add(oferta);
            }
            return listaOferta;

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
