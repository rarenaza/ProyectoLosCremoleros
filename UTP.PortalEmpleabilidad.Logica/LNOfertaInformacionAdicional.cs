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

        public List<OfertaInformacionAdicional> ObtenerInformacionAdicional(int idOferta, int idOfertaInformacionAdicional)
        {
            List<OfertaInformacionAdicional> lista = new List<OfertaInformacionAdicional>();

            DataTable dtResultado = adOfertaInfoAdicional.ObtenerInformacionAdicional(idOferta, idOfertaInformacionAdicional);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaInformacionAdicional infoAdicional = new OfertaInformacionAdicional();
                infoAdicional.IdOfertaInformacionAdicional = Convert.ToInt32(fila["IdOfertaInformacionAdicional"]);
                infoAdicional.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                infoAdicional.Conocimiento = Convert.ToString(fila["Conocimiento"]);
                infoAdicional.TipoConocimiento.IdListaValor = Convert.ToString(fila["TipoConocimiento"]);
                infoAdicional.TipoConocimiento.Valor = Convert.ToString(fila["TipoConocimientoDescripcion"]);
                infoAdicional.NivelConocimiento.IdListaValor = Convert.ToString(fila["NivelConocimiento"]);
                infoAdicional.NivelConocimiento.Valor = Convert.ToString(fila["NivelConocimientoDescripcion"]);
                infoAdicional.AniosExperiencia = Convert.ToInt32(fila["AniosExperiencia"] == System.DBNull.Value ? null : fila["AniosExperiencia"]);
                if (infoAdicional.AniosExperiencia == 0) infoAdicional.AniosExperiencia = null;
                infoAdicional.EstadoOfertaInformacionAdicional.IdListaValor = Convert.ToString(fila["EstadoOfertaInformacionAdicional"]);
                infoAdicional.EstadoOfertaInformacionAdicional.Valor = Convert.ToString(fila["EstadoOfertaInformacionAdicionalDescripcion"]);
                infoAdicional.CreadoPor = Convert.ToString(fila["CreadoPor"]);

                lista.Add(infoAdicional);
            }

            return lista;
        }

        public void Insertar(OfertaInformacionAdicional ofertaInformacionAdicional)
        {
            if (ofertaInformacionAdicional.NivelConocimientoIdListaValor == null) ofertaInformacionAdicional.NivelConocimientoIdListaValor = "";

            adOfertaInfoAdicional.InsertarInformacionAdicional(ofertaInformacionAdicional);
        }

        public void Actualizar(OfertaInformacionAdicional ofertaInformacionAdicional)
        {
            if (ofertaInformacionAdicional.NivelConocimientoIdListaValor == null) ofertaInformacionAdicional.NivelConocimientoIdListaValor = "";


            adOfertaInfoAdicional.Actualizar(ofertaInformacionAdicional);
        }

        public void Eliminar(int idOfertaInfoAdicional)
        {
            adOfertaInfoAdicional.Eliminar(idOfertaInfoAdicional);
        }
    }
}
