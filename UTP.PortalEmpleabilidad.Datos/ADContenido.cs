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
  public   class ADContenido
    {


      public DataTable Contenido_ObtenerPorCodMenu(int codMenu)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Contenido_ObtenerPorCodMenu";
            cmd.Parameters.Add(new SqlParameter("@CodMenu", codMenu));

            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        //public DataTable Contenido_Mostrar2(string id)
        //{
        //    ADConexion cnn = new ADConexion();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "Contenido_Mostrar2";
        //    cmd.Connection = cnn.cn;
        //    cnn.Conectar();
        //    cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar,50)).Value = id;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable();

        //    da.Fill(dt);

        //    cnn.Desconectar();
        //    return dt;
        //}


        public DataTable Contenido_Mostrar_Imagen(int Cod)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Contenido_Mostrar_imagen";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdContenido", SqlDbType.Int)).Value = Cod;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();
            return dt;
        }
        public DataTable ContenidoMenu_Mostrar()
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ContenidoMenu_Mostrar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable ContenidoPestana_Mostrar()
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ContenidoPestana_Mostrar";
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
                                             
                //cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, -1)).Value = contenido.Descripcion;

                cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, -1)).Value = (contenido.Descripcion == null ? "" : contenido.Descripcion);

                //cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.Binary)).Value = contenido.Imagen;

                cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.Binary)).Value = (contenido.Imagen == null ? new byte[] { } : contenido.Imagen);
                           

                cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit)).Value = contenido.Activo;




                //cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginal", SqlDbType.VarChar, 100)).Value = contenido.ArchivoNombreOriginal;
                cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginal", SqlDbType.VarChar, 100)).Value = (contenido.ArchivoNombreOriginal == null ? "" : contenido.ArchivoNombreOriginal);
                cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 100)).Value = (contenido.ArchivoMimeType == null ? "" : contenido.ArchivoMimeType);
                
                
                //cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 50)).Value = contenido.ArchivoMimeType;
                cmd.Parameters.Add(new SqlParameter("@EnPantallaPrincipal", SqlDbType.Bit)).Value = contenido.EnPantallaPrincipal;
                cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar,50)).Value = contenido.Menu;
                cmd.Parameters.Add(new SqlParameter("@CodPestana", SqlDbType.VarChar, 50)).Value = (contenido.Pestana == null ? "" : contenido.Pestana);
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

        public bool Contenido_Eliminar(int Cod)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Contenido_Eliminar";
                cmd.Connection = cnn.cn;
                cmd.Parameters.Add(new SqlParameter("@IdContenido", SqlDbType.VarChar, 50)).Value = Cod;
                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }


        }

      //muestra datos en el index solo lo que me instereza mostrar
        public DataTable Contenido_BuscarIndex(string Id)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Contenido_Buscar";
            cmd.Connection = cnn.cn;

            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar,50)).Value = Id;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable Contenido_BuscarNoticiasEventosOtros(string Id)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Contenido_BuscarNoticiasEventos";
            cmd.Connection = cnn.cn;

            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar, 50)).Value = Id;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
      //Busca Los Datos a Actualizar
        public DataTable ContenidoEDitar_Buscar(int Cod) 
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ContenidoEDitar_Buscar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdContenido", SqlDbType.Int)).Value = Cod;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();
            return dt;
        }

      
        //Actualiza Los datos Buscados
        public bool Contenido_Actualizar(Contenido contenido)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Contenido_Actualizar";
                cmd.Connection = cnn.cn;

                cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.VarChar, 500)).Value = contenido.Titulo;
                cmd.Parameters.Add(new SqlParameter("@SubTitulo", SqlDbType.VarChar, 500)).Value = (contenido.SubTitulo == null ? "" : contenido.SubTitulo);
                
                //cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, -1)).Value = contenido.Descripcion;
                cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, -1)).Value = (contenido.Descripcion == null ? "" : contenido.Descripcion);
                cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.Image)).Value = (contenido.Imagen == null ? new byte[] { } : contenido.Imagen);
                
                cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar, 50)).Value = contenido.Menu;
                cmd.Parameters.Add(new SqlParameter("@CodPestana", SqlDbType.VarChar, 50)).Value = (contenido.Pestana == null ? "" : contenido.Pestana);
                cmd.Parameters.Add(new SqlParameter("@EnPantallaPrincipal", SqlDbType.Bit)).Value = contenido.EnPantallaPrincipal;
                cmd.Parameters.Add(new SqlParameter("@Activo", SqlDbType.Bit)).Value = contenido.Activo;
                cmd.Parameters.Add(new SqlParameter("@ArchivoNombreOriginal", SqlDbType.VarChar, 100)).Value = (contenido.ArchivoNombreOriginal == null ? "" : contenido.ArchivoNombreOriginal);
                cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 100)).Value = (contenido.ArchivoMimeType == null ? "" : contenido.ArchivoMimeType);
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", SqlDbType.VarChar, 50)).Value = contenido.ModificadoPor;
                            
                cmd.Parameters.Add(new SqlParameter("@IdContenido", SqlDbType.VarChar, 50)).Value = contenido.IdContenido;
                cmd.Parameters.Add(new SqlParameter("@ImagenCambiada", SqlDbType.VarChar, 2)).Value = (contenido.Imagen==null ? "NO":"SI");

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

        public bool Contenido_RemoverImagen(int id)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Contenido_RemoverImagen";
                cmd.Connection = cnn.cn;
                              
                cmd.Parameters.Add(new SqlParameter("@IdContenido", SqlDbType.VarChar, 50)).Value = id;

                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }

        }

        public DataTable Contenido_Mostrar_imagen()
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Contenido_Mostrar_imagen";
            cmd.Connection = cnn.cn;
            cnn.Conectar();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();
            return dt;
        }


    }
}
