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
    public class LNOfertaInformacionAdicional
    {
        ADOfertaInformacionAdicional adOfertaInfoAdicional = new ADOfertaInformacionAdicional();

        public List<OfertaInformacionAdicional> ObtenerInformacionAdicional(int idOferta)
        {
            List<OfertaInformacionAdicional> lista = new List<OfertaInformacionAdicional>();

            DataTable dtResultado = adOfertaInfoAdicional.ObtenerInformacionAdicional(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaInformacionAdicional infoAdicional = new OfertaInformacionAdicional();
                infoAdicional.IdOfertaInformacionAdicional = Convert.ToInt32(fila["IdOfertaInformacionAdicional"]);
                infoAdicional.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                infoAdicional.Conocimiento = Convert.ToString(fila["Conocimiento"]);
                infoAdicional.TipoConocimiento = Convert.ToString(fila["TipoConocimiento"]);
                infoAdicional.NivelConocimiento = Convert.ToString(fila["NivelConocimiento"]);
                infoAdicional.AniosExperiencia = Convert.ToInt32(fila["AniosExperiencia"]);
                infoAdicional.EstadoOfertaInformacionAdicional = Convert.ToString(fila["EstadoOfertaInformacionAdicional"]);
                infoAdicional.CreadoPor = Convert.ToString(fila["CreadoPor"]);

            }

            return lista;
        }

        public void InsertarInformacionAdicional(OfertaInformacionAdicional ofertaInformacionAdicional)
        {
            adOfertaInfoAdicional.InsertarInformacionAdicional(ofertaInformacionAdicional);
        }
    }
}
