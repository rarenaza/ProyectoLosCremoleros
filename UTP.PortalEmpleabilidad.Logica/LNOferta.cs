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

    }

}
