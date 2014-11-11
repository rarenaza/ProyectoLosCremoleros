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
  public   class LNLista
    {
      ADLista ad = new ADLista();
            
      public List<Lista> MostrarLista()
      {
          List<Lista> lista = new List<Lista>();

          DataTable dtResultado = ad.MostrarLista();


          for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
          {
              Lista listavalor = new Lista();
              listavalor.IDLista = Convert.ToInt32(dtResultado.Rows[i]["IdLista"]);
              listavalor.NombreLista = dtResultado.Rows[i]["NombreLista"].ToString();
              listavalor.DescripcionLista = dtResultado.Rows[i]["DescripcionLista"].ToString();
              listavalor.Modificable = Convert.ToInt32(dtResultado.Rows[i]["Modificable"]);
              listavalor.Creadopor = dtResultado.Rows[i]["CreadoPor"].ToString();
              listavalor.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"].ToString());
     
              lista.Add(listavalor);
          }
          return lista;
      }

      public bool Lista_insertar(int IDLista, string NombreLista, string DescripcionLista, int Modificable, string CreadoPor)
      {

          Lista lista = new Lista();
          if (ad.Lista_insertar(lista.IDLista,lista.NombreLista,lista.DescripcionLista,lista .Modificable,lista.Creadopor) == true)
          {
              return true;
          }
          else
          {
              return false;
          }

      }

      //public List<Lista> Lista_Buscar(int IdLista)
      //{
      //    List<Lista> lista = new List<Lista>();


      //    DataTable dtResultado = ad.Lista_Buscar(IdLista);

      //    for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
      //    {
      //        Lista listavalor = new Lista();
      //        listavalor.IDLista = Convert.ToInt32(dtResultado.Rows[i]["IdLista"]);
      //        listavalor.NombreLista = dtResultado.Rows[i]["NombreLista"].ToString();
      //        listavalor.DescripcionLista = dtResultado.Rows[i]["DescripcionLista"].ToString();
      //        listavalor.Modificable = Convert.ToInt32(dtResultado.Rows[i]["Modificable"]);
      //        listavalor.Creadopor = dtResultado.Rows[i]["CreadoPor"].ToString();
      //        listavalor.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"].ToString());

      //        lista.Add(listavalor);

      //    }


      //    return lista;
      //}

      public Lista Lista_Buscar(int IdLista)
      {
          Lista listavalor = new Lista();

          DataTable dtResultado = ad.Lista_Buscar(IdLista);

          if (dtResultado.Rows.Count > 0)
          {
              listavalor.IDLista = Convert.ToInt32(dtResultado.Rows[0]["IdLista"]);
              listavalor.NombreLista = dtResultado.Rows[0]["NombreLista"].ToString();
              listavalor.DescripcionLista = dtResultado.Rows[0]["DescripcionLista"].ToString();
              listavalor.Modificable = Convert.ToInt32(dtResultado.Rows[0]["Modificable"]);
              listavalor.Creadopor = dtResultado.Rows[0]["CreadoPor"].ToString();
              listavalor.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"].ToString());
          }

          return listavalor;
      }




      //public List<Lista> Lista_ObtenerPorId(int IdLista)
      //{
      //    List<Lista> lista = new List<Lista>();


      //    DataTable dtResultado = ad.Lista_ObtenerPorId(IdLista);

      //    for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
      //    {
      //        Lista listavalor = new Lista();
      //        listavalor.IDLista = Convert.ToInt32(dtResultado.Rows[i]["IDLista"]);
      //        listavalor.IDListaValor = dtResultado.Rows[i]["IDListaValor"].ToString();
      //        listavalor.IDListaValorPadre = dtResultado.Rows[i]["IDListaValorPadre"] == null ? "" : dtResultado.Rows[i]["IDListaValorPadre"].ToString();
      //        listavalor.NombreLista = dtResultado.Rows[i]["NombreLista"].ToString();
      //        listavalor.Valor = dtResultado.Rows[i]["Valor"].ToString();

      //        lista.Add(listavalor);

      //    }


      //    return lista;
      //}



 }
}
