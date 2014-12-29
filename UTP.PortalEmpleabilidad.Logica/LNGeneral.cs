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
                item.IdLista            = Convert.ToInt32   (fila["IDLista"]);
                item.IdListaValor       = Convert.ToString  (fila["IDListaValor"]);
                item.IdListaValorPadre  = Convert.ToString  (fila["IDListaValorPadre"]); ;
                item.Valor              = Convert.ToString  (fila["Valor"]); ;
                item.DescripcionValor   = Convert.ToString  (fila["DescripcionValor"]); ;
                item.Icono              = Convert.ToString  (fila["Icono"]); ;
                item.Peso               = Convert.ToInt32   (fila["Peso"] == DBNull.Value ? 0 : fila["Peso"]); ;
                item.ValorUTP           = Convert.ToString  (fila["ValorUTP"]); ;
                item.EstadoValor        = Convert.ToString  (fila["EstadoValor"]); ;                

                lista.Add(item);
            }

            return lista;
        }

        /// <summary>
        /// Se agrega el parámetro de filtro para obtener los datos que contenga este valor.
        /// Por ejemplo: filtro = EMPRESA, filtra todos aquellos en donde la columna VALOR like '%FILTRO%'
        /// </summary>
        /// <param name="idLista"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ListaValor> ObtenerListaValor(int idLista, string filtro)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValor(idLista);

            foreach (DataRow fila in dtResultado.Rows)
            {
                if (Convert.ToString(fila["Valor"]).ToUpper().Contains(filtro.ToUpper()))
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
            }

            return lista;
        }
    }
}
