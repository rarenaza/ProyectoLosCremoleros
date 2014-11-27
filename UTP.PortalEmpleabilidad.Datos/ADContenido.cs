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
       
        
        public DataTable Contenido_Mostrar()
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Contenido_Mostrar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();


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
        public bool Contenido_Insertar(Contenido contenido)
        {
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Contenido_Insertar";
                cmd.Connection = cnn.cn;
         
                cmd.Parameters.Add(new SqlParameter("@Titulo", SqlDbType.VarChar, 50)).Value = contenido.Titulo;
                cmd.Parameters.Add(new SqlParameter("@SubTitulo", SqlDbType.VarChar, 50)).Value = contenido.SubTitulo;
                cmd.Parameters.Add(new SqlParameter("@Descripcion", SqlDbType.VarChar, -1)).Value = contenido.Descripcion;
                cmd.Parameters.Add(new SqlParameter("@Imagen", SqlDbType.VarChar,50)).Value = contenido.Imagen;
                cmd.Parameters.Add(new SqlParameter("@EnPantallaPrincipal", SqlDbType.Bit)).Value = contenido.EnPantallaPrincipal;
                cmd.Parameters.Add(new SqlParameter("@CodMenu", SqlDbType.VarChar,50)).Value = contenido.Menu;
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = contenido.CreadoPor;

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


    }
}
