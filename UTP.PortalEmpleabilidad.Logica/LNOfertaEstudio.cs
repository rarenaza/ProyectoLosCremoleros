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

        public List<OfertaEstudio> ObtenerEstudios(int idOferta, int idOfertaEstudio)
        {
            List<OfertaEstudio> lista = new List<OfertaEstudio>();

            DataTable dtResultado = adOfertaEstudio.ObtenerEstudios(idOferta, idOfertaEstudio);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaEstudio estudio = new OfertaEstudio();
                estudio.IdOfertaEstudio = Convert.ToInt32(fila["IdOfertaEstudio"]);
                estudio.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                estudio.CicloEstudio = Convert.ToInt32(fila["CicloEstudio"] == System.DBNull.Value ? null : fila["CicloEstudio"]);
                estudio.Estudio = Convert.ToString(fila["Estudio"]);
                estudio.EstudioTexto = Convert.ToString(fila["Estudio"]);
                estudio.TipoDeEstudio.Valor = Convert.ToString(fila["TipoDeEstudioDescripcion"]);
                estudio.TipoDeEstudioIdListaValor = Convert.ToString(fila["TipoDeEstudio"]);
                estudio.EstadoDelEstudio.Valor = Convert.ToString(fila["EstadoDelEstudioDescripcion"]);
                estudio.EstadoDelEstudioIdListaValor = Convert.ToString(fila["EstadoDelEstudio"]);
                estudio.EstadoOfertaEstudio.Valor = Convert.ToString(fila["EstadoOfertaEstudioDescripcion"]);
                estudio.CreadoPor = Convert.ToString(fila["CreadoPor"]);

                lista.Add(estudio);
            }

            return lista;
        }

        public List<OfertaEstudio> ObtenerEstudiosNoUniversitarios(int idOferta, int idOfertaEstudio)
        {
            List<OfertaEstudio> lista = new List<OfertaEstudio>();

            DataTable dtResultado = adOfertaEstudio.ObtenerEstudios(idOferta, idOfertaEstudio);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaEstudio estudio = new OfertaEstudio();
                estudio.IdOfertaEstudio = Convert.ToInt32(fila["IdOfertaEstudio"]);
                estudio.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                estudio.CicloEstudio = Convert.ToInt32(fila["CicloEstudio"] == System.DBNull.Value ? null : fila["CicloEstudio"]);
                estudio.Estudio = Convert.ToString(fila["Estudio"]);
                estudio.EstudioTexto = Convert.ToString(fila["Estudio"]);
                estudio.TipoDeEstudio.Valor = Convert.ToString(fila["TipoDeEstudioDescripcion"]);
                estudio.TipoDeEstudioIdListaValor = Convert.ToString(fila["TipoDeEstudio"]);
                estudio.EstadoDelEstudio.Valor = Convert.ToString(fila["EstadoDelEstudioDescripcion"]);
                estudio.EstadoDelEstudioIdListaValor = Convert.ToString(fila["EstadoDelEstudio"]);
                estudio.EstadoOfertaEstudio.Valor = Convert.ToString(fila["EstadoOfertaEstudioDescripcion"]);
                estudio.CreadoPor = Convert.ToString(fila["CreadoPor"]);

                lista.Add(estudio);
            }

            return lista.Where(m => m.TipoDeEstudioIdListaValor != Constantes.TIPO_ESTUDIO_PRINCIPAL).ToList(); ;
        }

        public void Insertar(OfertaEstudio ofertaEstudio)
        {

            adOfertaEstudio.Insertar(ofertaEstudio);
        }

        public void Actualizar(OfertaEstudio ofertaEstudio)
        {
            adOfertaEstudio.Actualizar(ofertaEstudio);
        }

        public void Eliminar(int idOfertaEstudio)
        {
            adOfertaEstudio.Eliminar(idOfertaEstudio);
        }
    }
}
