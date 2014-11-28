using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using System.Data;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOfertaEstudio
    {
        ADOfertaEstudio adOfertaEstudio = new ADOfertaEstudio();

        public List<OfertaEstudio> ObtenerEstudios(int idOferta)
        {
            List<OfertaEstudio> lista = new List<OfertaEstudio>();

            DataTable dtResultado = adOfertaEstudio.ObtenerEstudios(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaEstudio estudio = new OfertaEstudio();
                estudio.IdOfertaEstudio = Convert.ToInt32(fila["IdOfertaEstudio"]);
                estudio.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                estudio.CicloEstudio = Convert.ToString(fila["CicloEstudio"]);
                estudio.Estudio = Convert.ToString(fila["Estudio"]);
                estudio.TipoDeEstudio.Valor = Convert.ToString(fila["TipoDeEstudioDescripcion"]);
                estudio.EstadoDelEstudio.Valor = Convert.ToString(fila["EstadoDelEstudioDescripcion"]);
                estudio.EstadoOfertaEstudio.Valor = Convert.ToString(fila["EstadoOfertaEstudioDescripcion"]);
                estudio.CreadoPor = Convert.ToString(fila["CreadoPor"]);

            }

            return lista;
        }

     
    }
}
