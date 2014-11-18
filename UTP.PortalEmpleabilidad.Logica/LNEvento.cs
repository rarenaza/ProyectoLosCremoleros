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
   public  class LNEvento
    {
       ADEvento ad = new ADEvento();

       public List<Evento> Evento_Mostrar()
        {
            List<Evento> lista = new List<Evento>();

            DataTable dtResultado = ad.Evento_Mostrar();


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Evento listaEvento = new Evento();
                listaEvento.IdEvento = Convert.ToInt32(dtResultado.Rows[i]["IdEvento"]);
                listaEvento.NombreEvento = dtResultado.Rows[i]["NombreEvento"].ToString();
                listaEvento.TipoEvento = dtResultado.Rows[i]["TipoEvento"].ToString();
                listaEvento.DescripcionEvento = dtResultado.Rows[i]["DescripcionEvento"].ToString();

                //listaEvento.PosterGrandeEvento = dtResultado.Rows[i]["PosterGrandeEvento"].ToString();
                //listaEvento.PosterMedianoEvento = dtResultado.Rows[i]["PosterMedianoEvento"].ToString();
                //listaEvento.PosterChicoEvento = dtResultado.Rows[i]["PosterChicoEvento"].ToString();

                listaEvento.FechaEvento = Convert.ToDateTime(dtResultado.Rows[i]["FechaEvento"].ToString());
                listaEvento.DireccionEvento = dtResultado.Rows[i]["DireccionEvento"].ToString();
                listaEvento.DireccionDistrito = dtResultado.Rows[i]["DireccionDistrito"].ToString();
                listaEvento.DireccionCiudad = dtResultado.Rows[i]["DireccionCiudad"].ToString();
                listaEvento.DireccionRegion = dtResultado.Rows[i]["DireccionRegion"].ToString();
                listaEvento.DireccionPais = dtResultado.Rows[i]["DireccionPais"].ToString();
                listaEvento.AsistentesEsperados = Convert.ToInt32(dtResultado.Rows[i]["AsistentesEsperados"]);
                listaEvento.AsistentesReales = Convert.ToInt32(dtResultado.Rows[i]["AsistentesReales"]);

                //listaEvento.ImagenTicket = dtResultado.Rows[i]["PosterGrandeEvento"].ToString();
                listaEvento.RegistraAlumnos = Convert.ToInt32(dtResultado.Rows[i]["RegistraAlumnos"]);
                listaEvento.RegistraUsuariosEmpresa = Convert.ToInt32(dtResultado.Rows[i]["RegistraUsuariosEmpresa"]);
                listaEvento.RegistraPublicoEnGeneral = Convert.ToInt32(dtResultado.Rows[i]["RegistraPublicoEnGeneral"]);
        

            
                lista.Add(listaEvento);
            }
            return lista;
        }

       public bool Evento_insertar(Evento evento)
       {

         
           if (ad.Evento_insertar(evento) == true)
           {
               return true;
           }
           else
           {
               return false;
           }

       }
    }
}
