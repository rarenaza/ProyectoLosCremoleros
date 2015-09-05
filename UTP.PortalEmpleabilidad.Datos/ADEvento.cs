using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
  public   class ADEvento: ADBase
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();


        public bool Evento_insertar(Evento evento)
        {
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_Insertar";
                cmd.Connection = cnn.cn;
                cmd.Parameters.Add(new SqlParameter("@NombreEvento", SqlDbType.VarChar, 100)).Value = evento.NombreEvento;
                cmd.Parameters.Add(new SqlParameter("@EstadoEvento", SqlDbType.VarChar, 6)).Value = evento.EstadoEvento;
                cmd.Parameters.Add(new SqlParameter("@TipoEvento", SqlDbType.VarChar, 6)).Value = evento.TipoEvento;
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", SqlDbType.Int)).Value = evento.IdEmpresa;
                cmd.Parameters.Add(new SqlParameter("@DescripcionEvento", SqlDbType.VarChar, -1)).Value = evento.DescripcionEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEvento", SqlDbType.DateTime)).Value = evento.FechaEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEventoFin", SqlDbType.DateTime)).Value = evento.FechaEventoFin;
                cmd.Parameters.Add(new SqlParameter("@DiasEvento", SqlDbType.VarChar, 50)).Value = evento.DiasEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEventoTexto", SqlDbType.VarChar, 100)).Value = evento.FechaEventoTexto;
                cmd.Parameters.Add(new SqlParameter("@LugarEvento", SqlDbType.VarChar, 200)).Value = evento.LugarEvento;

                cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", SqlDbType.VarChar, 100)).Value = evento.DireccionDistrito;
                cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", SqlDbType.VarChar, 100)).Value = evento.DireccionCiudad;
                cmd.Parameters.Add(new SqlParameter("@DireccionRegion", SqlDbType.VarChar, 100)).Value = evento.DireccionRegion;
                cmd.Parameters.Add(new SqlParameter("@DireccionEvento", SqlDbType.VarChar, -1)).Value = evento.DireccionEvento;
                cmd.Parameters.Add(new SqlParameter("@AsistentesEsperados", SqlDbType.Int)).Value = evento.AsistentesEsperados;


                cmd.Parameters.Add(new SqlParameter("@RegistraAlumnos", SqlDbType.Bit)).Value = evento.RegistraAlumnos;
                cmd.Parameters.Add(new SqlParameter("@RegistraUsuariosEmpresa", SqlDbType.Bit)).Value = evento.RegistraUsuariosEmpresa;
                cmd.Parameters.Add(new SqlParameter("@RegistraPublicoEnGeneral", SqlDbType.Bit)).Value = evento.RegistraPublicoEnGeneral;
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = evento.CreadoPor;

                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public DataTable EVENTO_OBTENERPORID(int id)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EVENTO_OBTENERPORID";
                cmd.Parameters.Add(new SqlParameter("@IdEvento", id));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_INSCRITOS_EVENTOS(int id)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_INSCRITOS_EVENTOS";
                cmd.Parameters.Add(new SqlParameter("@IdEvento", id));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public bool EVENTO_ACTUALIZAR_IMAGENEVENTO(Evento  evento)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EVENTO_ACTUALIZAR_IMAGENEVENTO";
                cmd.Connection = cnn.cn;

                cmd.Parameters.Add(new SqlParameter("@ImagenEvento", SqlDbType.Binary)).Value = (evento.ImagenEvento == null ? new byte[] { } : evento.ImagenEvento);

                cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginalImagenEvento", SqlDbType.VarChar, 100)).Value = (evento.ArchivoNombreOriginalImagenEvento == null ? "" : evento.ArchivoNombreOriginalImagenEvento);
                cmd.Parameters.Add(new SqlParameter("@ArchivoMimeTypeImagenEvento", SqlDbType.VarChar, 100)).Value = (evento.ArchivoMimeTypeImagenEvento == null ? "" : evento.ArchivoMimeTypeImagenEvento);
                cmd.Parameters.Add(new SqlParameter("@IdEvento", SqlDbType.VarChar, 50)).Value = evento.IdEvento;
                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool EVENTO_ACTUALIZAR_IMAGENTICKECT(Evento evento)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EVENTO_ACTUALIZAR_IMAGENTICKECT";
                cmd.Connection = cnn.cn;

                cmd.Parameters.Add(new SqlParameter("@ImagenTicket", SqlDbType.Binary)).Value = (evento.ImagenTicket == null ? new byte[] { } : evento.ImagenTicket);

                cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginalImagenTicket", SqlDbType.VarChar, 100)).Value = (evento.ArchivoNombreOriginalImagenTicket == null ? "" : evento.ArchivoNombreOriginalImagenTicket);
                cmd.Parameters.Add(new SqlParameter("@ArchivoMimeTypeImagenTicket", SqlDbType.VarChar, 100)).Value = (evento.ArchivoMimeTypeImagenEventoTicket == null ? "" : evento.ArchivoMimeTypeImagenEventoTicket);
                cmd.Parameters.Add(new SqlParameter("@IdEvento", SqlDbType.VarChar, 50)).Value = evento.IdEvento;
                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public DataTable Evento_Mostrar()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Evento_Mostrar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
        public DataTable Evento_MostrarUltimos()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Evento_ListarUltimos";
            cmd.Connection = cnn.cn;
            cnn.Conectar();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }




        public bool Contenido_Insertar(Contenido contenido)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Contenido_Insertar";
                cmd.Connection = cnn.cn;

                cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.VarChar, 500)).Value = (contenido.Titulo == null ? "" : contenido.Titulo);

                cmd.Parameters.Add(new SqlParameter("@SubTitulo", SqlDbType.VarChar, 500)).Value = (contenido.SubTitulo == null ? "" : contenido.SubTitulo);

                cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, -1)).Value = (contenido.Descripcion == null ? "" : contenido.Descripcion);

                cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.Binary)).Value = (contenido.Imagen == null ? new byte[] { } : contenido.Imagen);

                cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit)).Value = contenido.Activo;

                cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginal", SqlDbType.VarChar, 100)).Value = (contenido.ArchivoNombreOriginal == null ? "" : contenido.ArchivoNombreOriginal);
                cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 100)).Value = (contenido.ArchivoMimeType == null ? "" : contenido.ArchivoMimeType);

                cmd.Parameters.Add(new SqlParameter("@EnPantallaPrincipal", SqlDbType.Bit)).Value = contenido.EnPantallaPrincipal;
                cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar, 50)).Value = contenido.Menu;
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = contenido.CreadoPor;

                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    

        


        public bool Evento_Actualizar(Evento evento)
        {
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_Actualizar";
                cmd.Connection = cnn.cn;
                cmd.Parameters.Add(new SqlParameter("@IdEvento", SqlDbType.Int)).Value = evento.IdEvento;
                cmd.Parameters.Add(new SqlParameter("@NombreEvento", SqlDbType.VarChar, 100)).Value = evento.NombreEvento;
                cmd.Parameters.Add(new SqlParameter("@EstadoEvento", SqlDbType.VarChar, 6)).Value = evento.EstadoEvento;
                cmd.Parameters.Add(new SqlParameter("@TipoEvento", SqlDbType.VarChar, 6)).Value = evento.TipoEvento;
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", SqlDbType.Int)).Value = evento.IdEmpresa;
                cmd.Parameters.Add(new SqlParameter("@DescripcionEvento", SqlDbType.VarChar, -1)).Value = evento.DescripcionEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEvento", SqlDbType.DateTime)).Value = evento.FechaEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEventoFin", SqlDbType.DateTime)).Value = (Object)evento.FechaEventoFin ?? DBNull.Value;
                cmd.Parameters.Add(new SqlParameter("@DiasEvento", SqlDbType.VarChar, 50)).Value = evento.DiasEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEventoTexto", SqlDbType.VarChar, 100)).Value = evento.FechaEventoTexto;
                cmd.Parameters.Add(new SqlParameter("@LugarEvento", SqlDbType.VarChar, 200)).Value = evento.LugarEvento;

                cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", SqlDbType.VarChar, 100)).Value = evento.DireccionDistrito;
                cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", SqlDbType.VarChar, 100)).Value = evento.DireccionCiudad;
                cmd.Parameters.Add(new SqlParameter("@DireccionRegion", SqlDbType.VarChar, 100)).Value = evento.DireccionRegion;
                cmd.Parameters.Add(new SqlParameter("@DireccionEvento", SqlDbType.VarChar, -1)).Value = evento.DireccionEvento;
                cmd.Parameters.Add(new SqlParameter("@AsistentesEsperados", SqlDbType.Int)).Value = evento.AsistentesEsperados;


                cmd.Parameters.Add(new SqlParameter("@RegistraAlumnos", SqlDbType.Bit)).Value = evento.RegistraAlumnos;
                cmd.Parameters.Add(new SqlParameter("@RegistraUsuariosEmpresa", SqlDbType.Bit)).Value = evento.RegistraUsuariosEmpresa;
                cmd.Parameters.Add(new SqlParameter("@RegistraPublicoEnGeneral", SqlDbType.Bit)).Value = evento.RegistraPublicoEnGeneral;
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", SqlDbType.VarChar, 50)).Value = evento.ModificadoPor;

                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

       

        public DataTable Listar_Eventos()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Evento_Mostrar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
        public DataSet EventosPorUsuario(string usuario)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EventosPorUsuario";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsResultado);
                conexion.Close();
            }

            return dsResultado;
        }
        public DataSet EventoPorUsuario(int idevento, string usuario)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_Mostrar";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdEvento", idevento));
                cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsResultado);
                conexion.Close();
            }

            return dsResultado;
        }
        public void InsertarEventoAsistente(int idEvento, string usuario, string creadoPor )
        {
             try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    conexion.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "EventoAsistente_Insertar";

                    cmd.Parameters.Add(new SqlParameter("@IdEvento", idEvento));
                    cmd.Parameters.Add(new SqlParameter("@Usuario", usuario));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", creadoPor));

                    cmd.Connection = conexion;
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
        }

        public DataTable ObtenerAsistentes(int idEvento, string tipoAsistente)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_ObtenerAsistentes";
                cmd.Parameters.Add(new SqlParameter("@IdEvento", idEvento));
                cmd.Parameters.Add(new SqlParameter("@TipoAsistente", tipoAsistente));

                cmd.Connection = conexion;
                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }
        public void ActualizaEstadoTicket(int idEventoAsistente, string estadoTicket)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    conexion.Open();
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Evento_ActualizaEstadoTicket";

                    cmd.Parameters.Add(new SqlParameter("@IdEventoAsistente", idEventoAsistente));
                    cmd.Parameters.Add(new SqlParameter("@EstadoTicket", estadoTicket));

                    cmd.Connection = conexion;
                    cmd.ExecuteNonQuery();
                    conexion.Close();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
