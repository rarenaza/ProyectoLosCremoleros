using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Evento;

namespace UTP.PortalEmpleabilidad.Logica
{
   public  class LNEvento
    {
       ADEvento ad = new ADEvento();

       public DataTable EVENTO_OBTENERPORID(int id)
       {
           return ad.EVENTO_OBTENERPORID(id);
       }
       public DataTable UTP_INSCRITOS_EVENTOS(int id)
       {
           return ad.UTP_INSCRITOS_EVENTOS(id);
       }
       public bool Evento_insertar(Evento evento)
       {
           //Se agrega las condiciones de los campos que pueden ser null.
           if (evento.IdEmpresa == null) evento.IdEmpresa = 0;
           if (evento.DescripcionEvento == null) evento.DescripcionEvento = "";
           if (evento.DireccionEvento == null) evento.DireccionEvento = "";
           if (evento.DireccionDistrito == null) evento.DireccionDistrito = "";
           if (evento.AsistentesEsperados == null) evento.AsistentesEsperados = 0;
           if (evento.DiasEvento == null) evento.DiasEvento = "";
           
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
           //Se agrega las condiciones de los campos que pueden ser null.
           if (evento.IdEmpresa == null) evento.IdEmpresa = 0;
           if (evento.DescripcionEvento == null) evento.DescripcionEvento = "";
           if (evento.DireccionEvento == null) evento.DireccionEvento = "";
           if (evento.DireccionDistrito == null) evento.DireccionDistrito = "";
           if (evento.AsistentesEsperados == null) evento.AsistentesEsperados = 0;
           if (evento.DiasEvento == null) evento.DiasEvento = "";

           if (ad.Evento_Actualizar(evento))
           {
               return true;
           }
           else
           {
               return false;
           }

       }
       public bool EVENTO_ACTUALIZAR_IMAGENEVENTO(Evento  evento)
       {


           if (ad.EVENTO_ACTUALIZAR_IMAGENEVENTO(evento) == true)
           {
               return true;
           }
           else
           {
               return false;
           }

       }

       public bool EVENTO_ACTUALIZAR_IMAGENTICKECT(Evento evento)
       {


           if (ad.EVENTO_ACTUALIZAR_IMAGENTICKECT(evento) == true)
           {
               return true;
           }
           else
           {
               return false;
           }

       }

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
      

       public List<Evento> Listar_Eventos(string palabra)
       {
           List<Evento> lista = new List<Evento>();

           DataTable dtResultado = ad.Evento_Mostrar();


           for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
           {
               Evento listaEvento = new Evento();
               listaEvento.FechaEvento = Convert.ToString(dtResultado.Rows[i]["FechaEvento"].ToString());
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
       public Evento EventoPorUsuario(int idevento, string usuario)
       {
           Evento eventoporusuario = new Evento();

           DataSet dsResultado = ad.EventoPorUsuario(idevento,usuario);
           
           if (dsResultado.Tables[0].Rows.Count > 0) 
           {
               eventoporusuario.IdEvento = Funciones.ToInt(dsResultado.Tables[0].Rows[0]["IdEvento"]);
               eventoporusuario.NombreEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["NombreEvento"]);
               eventoporusuario.EstadoEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["EstadoEvento"]);
               eventoporusuario.ValorEstadoEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["ValorEstadoEvento"]);
               eventoporusuario.TipoEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["TipoEvento"]);
               eventoporusuario.ValorTipoEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["ValorTipoEvento"]);
               eventoporusuario.IdEmpresa = Funciones.ToInt(dsResultado.Tables[0].Rows[0]["IdEmpresa"]);
               eventoporusuario.NombreComercial = Funciones.ToString(dsResultado.Tables[0].Rows[0]["NombreComercial"]);
               eventoporusuario.DescripcionEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["DescripcionEvento"]);
               eventoporusuario.FechaEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["FechaEvento"]);
               eventoporusuario.FechaEventoFin = Funciones.ToString(dsResultado.Tables[0].Rows[0]["FechaEventoFin"]);
               eventoporusuario.DiasEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["DiasEvento"]);
               eventoporusuario.FechaEventoTexto = Funciones.ToString(dsResultado.Tables[0].Rows[0]["FechaEventoTexto"]);
               eventoporusuario.LugarEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["LugarEvento"]);
               eventoporusuario.DireccionEvento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["DescripcionEvento"]);
               eventoporusuario.DireccionDistrito = Funciones.ToString(dsResultado.Tables[0].Rows[0]["DireccionDistrito"]);
               eventoporusuario.DireccionCiudad = Funciones.ToString(dsResultado.Tables[0].Rows[0]["DireccionCiudad"]);
               eventoporusuario.DireccionRegion = Funciones.ToString(dsResultado.Tables[0].Rows[0]["DireccionRegion"]);
               eventoporusuario.AsistentesEsperados = Funciones.ToInt(dsResultado.Tables[0].Rows[0]["AsistentesEsperados"]);
               eventoporusuario.AsistentesReales = Funciones.ToInt(dsResultado.Tables[0].Rows[0]["AsistentesReales"]);
               eventoporusuario.RegistraAlumnos = Funciones.ToBoolean(dsResultado.Tables[0].Rows[0]["RegistraAlumnos"]);
               eventoporusuario.RegistraUsuariosEmpresa = Funciones.ToBoolean(dsResultado.Tables[0].Rows[0]["RegistraUsuariosEmpresa"]);
               eventoporusuario.RegistraPublicoEnGeneral = Funciones.ToBoolean(dsResultado.Tables[0].Rows[0]["RegistraPublicoEnGeneral"]);
               if (dsResultado.Tables[0].Rows[0]["IdEventoAsistente"] != null) 
               { 
                    eventoporusuario.IdEventoAsistente = Funciones.ToInt(dsResultado.Tables[0].Rows[0]["IdEventoAsistente"]);
               }
               else
               {
                   eventoporusuario.IdEventoAsistente = 0;
               }
               eventoporusuario.FechaInscripcion = Funciones.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaInscripcion"]);
               eventoporusuario.Nombres = Funciones.ToString(dsResultado.Tables[0].Rows[0]["Nombres"]);
               eventoporusuario.Apellidos = Funciones.ToString(dsResultado.Tables[0].Rows[0]["Apellidos"]);
               eventoporusuario.EstadoTicket = Funciones.ToString(dsResultado.Tables[0].Rows[0]["EstadoTicket"]);
               eventoporusuario.ValorEstadoTicket = Funciones.ToString(dsResultado.Tables[0].Rows[0]["ValorEstadoTicket"]);
               eventoporusuario.TipoDocumento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["TipoDocumento"]);
               eventoporusuario.ValorTipoDocumento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["ValorTipoDocumento"]);
               eventoporusuario.NumeroDocumento = Funciones.ToString(dsResultado.Tables[0].Rows[0]["NumeroDocumento"]);
               eventoporusuario.FechaAsistencia = Funciones.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaAsistencia"]);
           }
           return eventoporusuario;
       }
       public void InsertarEventoAsistente(int idEvento, string usuario, string creadoPor)
       {
           ad.InsertarEventoAsistente(idEvento, usuario, creadoPor );
       }

       public void ActualizaEstadoTicket(int idEventoAsistente, string estadoTicket)
       {
           ad.ActualizaEstadoTicket(idEventoAsistente, estadoTicket);
       }

       public List<VistaAsistente> ObtenerAsistentes(int idEvento, string tipoAsistente)
       {
           List<VistaAsistente> asistentes = new List<VistaAsistente>();

           DataTable dtResultado = ad.ObtenerAsistentes(idEvento, tipoAsistente);

           foreach (DataRow fila in dtResultado.Rows)
           {
               VistaAsistente asistente = new VistaAsistente();
               asistente.Nombres = Convert.ToString(fila["Nombres"]);
               asistente.Apellidos = Convert.ToString(fila["Apellidos"]);
               asistente.Usuario = Convert.ToString(fila["Usuario"]);
               asistente.CorreoElectronico = Convert.ToString(fila["CorreoElectronico"]);

               asistentes.Add(asistente);

           }

           return asistentes;
       }

      
    }
}
