using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNGeneral
    {
        ADGeneral adGeneral = new ADGeneral();

        public List<ListaValor> ObtenerListaValor(int idLista)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValor(idLista);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
                item.IdLista = Convert.ToInt32(fila["IDLista"]);
                item.IdListaValor = Convert.ToString(fila["IDListaValor"]);
                item.IdListaValorPadre = Convert.ToString(fila["IDListaValorPadre"]); ;
                item.Valor = Convert.ToString(fila["Valor"]); ;
                item.DescripcionValor = Convert.ToString(fila["DescripcionValor"]); ;
                item.Icono = Convert.ToString(fila["Icono"]); ;
                item.Peso = Convert.ToInt32(fila["Peso"] == DBNull.Value ? 0 : fila["Peso"]); ;
                item.ValorUTP = Convert.ToString(fila["ValorUTP"]); ;
                item.EstadoValor = Convert.ToString(fila["EstadoValor"]); ;

                lista.Add(item);
            }

            return lista;
        }
        public List<ListaValor> ObtenerListaValorPorIdPadre( string idListaPadre)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValorPorIdPadre(idListaPadre);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
                item.IdListaValor = Funciones.ToString(fila["IDListaValor"]);
                item.Valor = Funciones.ToString(fila["Valor"]); ;
                lista.Add(item);
            }

            return lista;
        }

    }
}
