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

       //public List<Evento> Evento_Mostrar()
       // {
       //     List<Evento> lista = new List<Evento>();

       //     DataTable dtResultado = ad.Evento_Mostrar();


       //     for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
       //     {
       //         Evento listaEvento = new Evento();
       //         listaEvento.IdEvento = Convert.ToInt32(dtResultado.Rows[i]["IdEvento"]);
       //         listaEvento.NombreEvento = dtResultado.Rows[i]["NombreEvento"].ToString();
       //         listaEvento.TipoEvento = dtResultado.Rows[i]["TipoEvento"].ToString();
       //         listaEvento.DescripcionEvento = dtResultado.Rows[i]["DescripcionEvento"].ToString();

       //         //listaEvento.PosterGrandeEvento = dtResultado.Rows[i]["PosterGrandeEvento"].ToString();
       //         //listaEvento.PosterMedianoEvento = dtResultado.Rows[i]["PosterMedianoEvento"].ToString();
       //         //listaEvento.PosterChicoEvento = dtResultado.Rows[i]["PosterChicoEvento"].ToString();

       //         listaEvento.FechaEvento = Convert.ToDateTime(dtResultado.Rows[i]["FechaEvento"].ToString());
       //         listaEvento.DireccionEvento = dtResultado.Rows[i]["DireccionEvento"].ToString();
       //         listaEvento.DireccionDistrito = dtResultado.Rows[i]["DireccionDistrito"].ToString();
       //         listaEvento.DireccionCiudad = dtResultado.Rows[i]["DireccionCiudad"].ToString();
       //         listaEvento.DireccionRegion = dtResultado.Rows[i]["DireccionRegion"].ToString();
       //         listaEvento.DireccionPais = dtResultado.Rows[i]["DireccionPais"].ToString();
       //         listaEvento.AsistentesEsperados = Convert.ToInt32(dtResultado.Rows[i]["AsistentesEsperados"]);
       //         listaEvento.AsistentesReales = Convert.ToInt32(dtResultado.Rows[i]["AsistentesReales"]);

       //         //listaEvento.ImagenTicket = dtResultado.Rows[i]["PosterGrandeEvento"].ToString();
       //         listaEvento.RegistraAlumnos = Convert.ToInt32(dtResultado.Rows[i]["RegistraAlumnos"]);
       //         listaEvento.RegistraUsuariosEmpresa = Convert.ToInt32(dtResultado.Rows[i]["RegistraUsuariosEmpresa"]);
       //         listaEvento.RegistraPublicoEnGeneral = Convert.ToInt32(dtResultado.Rows[i]["RegistraPublicoEnGeneral"]);
        

            
       //         lista.Add(listaEvento);
       //     }
       //     return lista;
       // }

       public DataTable EVENTO_OBTENERPORID(int id)
       {
           return ad.EVENTO_OBTENERPORID(id);
       }


       public List<Evento> Evento_MostrarUltimos()
       {
           List<Evento> lista = new List<Evento>();
           DataTable dtResultado = ad.Evento_MostrarUltimos();
           for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
           {
               Evento listaEvento = new Evento();
               listaEvento.NombreEvento =Funciones.ToString( dtResultado.Rows[i]["NombreEvento"]);
               listaEvento.FechaEventoTexto = Funciones.ToString(dtResultado.Rows[i]["FechaEventoTexto"]);
               listaEvento.LugarEvento = Funciones.ToString(dtResultado.Rows[i]["LugarEvento"]);
               listaEvento.IdEvento = Funciones.ToInt(dtResultado.Rows[i]["IdEvento"]);

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

       public bool Evento_Actualizar(Evento evento)
       {


           if (ad.Evento_Actualizar(evento) == true)
           {
               return true;
           }
           else
           {
               return false;
           }

       }

       public List<Evento> Listar_Eventos(string palabra)
       {
           List<Evento> lista = new List<Evento>();

           DataTable dtResultado = ad.Evento_Mostrar();


           for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
           {
               Evento listaEvento = new Evento();
               listaEvento.FechaEvento = Convert.ToDateTime(dtResultado.Rows[i]["FechaEvento"].ToString());
               listaEvento.NombreEvento = dtResultado.Rows[i]["NombreEvento"].ToString();        
               listaEvento.DireccionEvento = dtResultado.Rows[i]["DireccionEvento"].ToString();
               listaEvento.AsistentesEsperados = Convert.ToInt32(dtResultado.Rows[i]["AsistentesEsperados"]);
               listaEvento.AsistentesReales = Convert.ToInt32(dtResultado.Rows[i]["AsistentesReales"]);

               lista.Add(listaEvento);
           }
           return lista;
       }
       public VistaEventosPorUsuario EventosPorUsuario(string usuario)
       {
           VistaEventosPorUsuario vistaeventosporusuario = new VistaEventosPorUsuario();

           DataSet dsResultado = ad.EventosPorUsuario(usuario);

           List<Evento> usuarioeventoposible = new List<Evento>();
           List<Evento> usuarioeventoactivo = new List<Evento>();
           List<Evento> usuarioeventopasado = new List<Evento>();

           if (dsResultado.Tables.Count > 0)
           {
               if (dsResultado.Tables[0].Rows.Count > 0)
               {
                   for (int n = 0; n <= dsResultado.Tables[0].Rows.Count - 1; n++)
                   {
                       Evento usuarioeventoposibledata = new Evento();
                       usuarioeventoposibledata.IdEvento = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdEvento"]);
                       usuarioeventoposibledata.NombreEvento = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NombreEvento"]);
                       usuarioeventoposibledata.FechaEventoTexto = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FechaEventoTexto"]);
                       usuarioeventoposibledata.LugarEvento = Funciones.ToString(dsResultado.Tables[0].Rows[n]["LugarEvento"]);
                       usuarioeventoposibledata.NombreComercial = Funciones.ToString(dsResultado.Tables[0].Rows[n]["NombreComercial"]);
                       usuarioeventoposibledata.EstadoEvento = Funciones.ToString(dsResultado.Tables[0].Rows[n]["EstadoEvento"]);
                       usuarioeventoposible.Add(usuarioeventoposibledata);
                   }
               }
               if (dsResultado.Tables[1].Rows.Count > 0)
               {
                   for (int n = 0; n <= dsResultado.Tables[1].Rows.Count - 1; n++)
                   {
                       Evento usuarioeventoactivodata = new Evento();
                       usuarioeventoactivodata.IdEvento = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["IdEvento"]);
                       usuarioeventoactivodata.NombreEvento = Funciones.ToString(dsResultado.Tables[1].Rows[n]["NombreEvento"]);
                       usuarioeventoactivodata.FechaEventoTexto = Funciones.ToString(dsResultado.Tables[1].Rows[n]["FechaEventoTexto"]);
                       usuarioeventoactivodata.LugarEvento = Funciones.ToString(dsResultado.Tables[1].Rows[n]["LugarEvento"]);
                       usuarioeventoactivodata.NombreComercial = Funciones.ToString(dsResultado.Tables[1].Rows[n]["NombreComercial"]);
                       usuarioeventoactivodata.FechaInscripcion = Funciones.ToDateTime(dsResultado.Tables[1].Rows[n]["FechaInscripcion"]);
                       usuarioeventoactivodata.ValorEstadoTicket = Funciones.ToString(dsResultado.Tables[1].Rows[n]["ValorEstadoTicket"]);
                       usuarioeventoactivodata.EstadoEvento = Funciones.ToString(dsResultado.Tables[1].Rows[n]["EstadoEvento"]);
                       usuarioeventoactivo.Add(usuarioeventoactivodata);
                   }
               }
               if (dsResultado.Tables[2].Rows.Count > 0)
               {
                   for (int n = 0; n <= dsResultado.Tables[2].Rows.Count - 1; n++)
                   {
                       Evento usuarioeventopasadodata = new Evento();
                       usuarioeventopasadodata.IdEvento = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["IdEvento"]);
                       usuarioeventopasadodata.NombreEvento = Funciones.ToString(dsResultado.Tables[2].Rows[n]["NombreEvento"]);
                       usuarioeventopasadodata.FechaEventoTexto = Funciones.ToString(dsResultado.Tables[2].Rows[n]["FechaEventoTexto"]);
                       usuarioeventopasadodata.LugarEvento = Funciones.ToString(dsResultado.Tables[2].Rows[n]["LugarEvento"]);
                       usuarioeventopasadodata.NombreComercial = Funciones.ToString(dsResultado.Tables[2].Rows[n]["NombreComercial"]);
                       usuarioeventopasadodata.FechaInscripcion = Funciones.ToDateTime(dsResultado.Tables[2].Rows[n]["FechaInscripcion"]);
                       usuarioeventopasadodata.EstadoTicket = Funciones.ToString(dsResultado.Tables[2].Rows[n]["EstadoTicket"]);
                       usuarioeventopasadodata.ValorEstadoTicket = Funciones.ToString(dsResultado.Tables[2].Rows[n]["ValorEstadoTicket"]);
                       usuarioeventopasadodata.FechaAsistencia = Funciones.ToDateTime(dsResultado.Tables[2].Rows[n]["FechaAsistencia"]);
                       usuarioeventopasadodata.EstadoEvento = Funciones.ToString(dsResultado.Tables[2].Rows[n]["EstadoEvento"]);
                       usuarioeventopasado.Add(usuarioeventopasadodata);
                   }
               }

           }
           vistaeventosporusuario.usuarioeventoposible = usuarioeventoposible;
           vistaeventosporusuario.usuarioeventoactivo = usuarioeventoactivo;
           vistaeventosporusuario.usuarioeventopasado = usuarioeventopasado;
           return vistaeventosporusuario;
       }

    }
}
