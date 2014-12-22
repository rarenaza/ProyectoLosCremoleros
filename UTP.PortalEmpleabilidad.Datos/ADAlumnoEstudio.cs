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
    public class ADAlumnoEstudio
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();
        public DataTable ObtenerAlumnoEstudioPorIdAlumno(int IdAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoEstudio_ObtenerPorIdAlumno";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(AlumnoEstudio alumnoestudio)
        {
            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                conexion.Open();

                SqlTransaction transaccion;
                transaccion = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AlumnoEstudio_Insertar";
                    cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = alumnoestudio.IdAlumno;
                    cmd.Parameters.Add(new SqlParameter("@Institucion", SqlDbType.VarChar, 100)).Value = alumnoestudio.Institucion;
                    cmd.Parameters.Add(new SqlParameter("@Estudio", SqlDbType.VarChar, 100)).Value = alumnoestudio.Estudio;
                    cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", SqlDbType.VarChar, 6)).Value = alumnoestudio.TipoDeEstudio;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDelEstudio", SqlDbType.VarChar, 6)).Value = alumnoestudio.EstadoDelEstudio;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", SqlDbType.VarChar, 6)).Value = alumnoestudio.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioMes", SqlDbType.Int)).Value = alumnoestudio.FechaInicioMes;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAno", SqlDbType.Int)).Value = alumnoestudio.FechaInicioAno;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinMes", SqlDbType.Int)).Value = alumnoestudio.FechaFinMes;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAno", SqlDbType.Int)).Value = alumnoestudio.FechaFinAno;
                    cmd.Parameters.Add(new SqlParameter("@CicloEquivalente", SqlDbType.Int)).Value = alumnoestudio.CicloEquivalente;
                    cmd.Parameters.Add(new SqlParameter("@DatoUTP", SqlDbType.Bit)).Value = alumnoestudio.DatoUTP;
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = alumnoestudio.CreadoPor;
                    cmd.ExecuteNonQuery();
                    transaccion.Commit();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }
            }

        }

        public DataTable ObtenerAlumnoEstudioPorId(int IdEstudio)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoEstudio_ObtenerPorId";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdEstudio", SqlDbType.Int)).Value = IdEstudio;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }

        public void Update(AlumnoEstudio alumnoestudio)
        {
            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                conexion.Open();

                SqlTransaction transaccion;
                transaccion = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AlumnoEstudio_Update";
                    cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = alumnoestudio.IdAlumno;
                    cmd.Parameters.Add(new SqlParameter("@Institucion", SqlDbType.VarChar, 100)).Value = alumnoestudio.Institucion;
                    cmd.Parameters.Add(new SqlParameter("@Estudio", SqlDbType.VarChar, 100)).Value = alumnoestudio.Estudio;
                    cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", SqlDbType.VarChar, 6)).Value = alumnoestudio.TipoDeEstudio;
                    cmd.Parameters.Add(new SqlParameter("@EstadoDelEstudio", SqlDbType.VarChar, 6)).Value = alumnoestudio.EstadoDelEstudio;
                    cmd.Parameters.Add(new SqlParameter("@Observacion", SqlDbType.VarChar, 6)).Value = alumnoestudio.Observacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioMes", SqlDbType.Int)).Value = alumnoestudio.FechaInicioMes;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAno", SqlDbType.Int)).Value = alumnoestudio.FechaInicioAno;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinMes", SqlDbType.Int)).Value = alumnoestudio.FechaFinMes;
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAno", SqlDbType.Int)).Value = alumnoestudio.FechaFinAno;
                    cmd.Parameters.Add(new SqlParameter("@CicloEquivalente", SqlDbType.Int)).Value = alumnoestudio.CicloEquivalente;
                    cmd.Parameters.Add(new SqlParameter("@ModificadoPor", SqlDbType.VarChar, 50)).Value = alumnoestudio.CreadoPor;
                    cmd.ExecuteNonQuery();
                    transaccion.Commit();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }
            }

        }

        public void Desactivar(int IdEstudio)
        {
            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                conexion.Open();

                SqlTransaction transaccion;
                transaccion = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AlumnoEstudio_Desactivar";
                    cmd.Parameters.Add(new SqlParameter("@IdEstudio", SqlDbType.Int)).Value = IdEstudio;
                    cmd.ExecuteNonQuery();
                    transaccion.Commit();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }
            }

        }



    }
}
