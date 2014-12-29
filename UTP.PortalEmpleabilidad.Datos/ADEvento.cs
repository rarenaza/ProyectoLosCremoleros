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
  public   class ADEvento
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();


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
                cmd.Parameters.Add(new SqlParameter("@PosterGrandeEvento", SqlDbType.Image)).Value = evento.PosterGrandeEvento;
                cmd.Parameters.Add(new SqlParameter("@PosterMedianoEvento", SqlDbType.Image)).Value = evento.PosterMedianoEvento;
                cmd.Parameters.Add(new SqlParameter("@PosterChicoEvento", SqlDbType.Image)).Value = evento.PosterChicoEvento;
                cmd.Parameters.Add(new SqlParameter("@FechaEvento", SqlDbType.DateTime)).Value = evento.FechaEvento;
                cmd.Parameters.Add(new SqlParameter("@DireccionEvento", SqlDbType.VarChar,-1)).Value = evento.DireccionEvento;
                cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", SqlDbType.VarChar,100)).Value = evento.DireccionDistrito;
                cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", SqlDbType.VarChar,100)).Value = evento.DireccionCiudad;
                cmd.Parameters.Add(new SqlParameter("@DireccionRegion", SqlDbType.VarChar,100)).Value = evento.DireccionRegion;
                cmd.Parameters.Add(new SqlParameter("@DireccionPais", SqlDbType.VarChar,6)).Value = evento.DireccionPais;
                cmd.Parameters.Add(new SqlParameter("@AsistentesEsperados", SqlDbType.Int)).Value = evento.AsistentesEsperados;
                cmd.Parameters.Add(new SqlParameter("@AsistentesReales", SqlDbType.Int)).Value = evento.AsistentesReales;
                cmd.Parameters.Add(new SqlParameter("@ImagenTicket", SqlDbType.Image)).Value = evento.ImagenTicket;
                cmd.Parameters.Add(new SqlParameter("@RegistraAlumnos", SqlDbType.Bit)).Value = evento.RegistraAlumnos;
                cmd.Parameters.Add(new SqlParameter("@RegistraUsuariosEmpresa", SqlDbType.Bit)).Value = evento.RegistraUsuariosEmpresa;
                cmd.Parameters.Add(new SqlParameter("@RegistraPublicoEnGeneral", SqlDbType.Bit)).Value = evento.RegistraPublicoEnGeneral;
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar,50)).Value = evento.CreadoPor;
                  
                  

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
    }
}
