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
    public class ADAlumnoExperienciaCargo
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();
        public DataTable ObtenerAlumnoExperienciaCargoPorIdAlumno(int IdAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoExperienciaCargo_ObtenerPorIdAlumno";
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

        public void Registrar(AlumnoExperienciaCargo alumnoexperienciacargo)
        {

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoExperienciaCargo_Registrar";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@IdExperiencia", alumnoexperienciacargo.IdExperiencia));
                cmd.Parameters.Add(new SqlParameter("@NombreCargo", alumnoexperienciacargo.NombreCargo));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioCargoMes", alumnoexperienciacargo.FechaInicioCargoMes));
                cmd.Parameters.Add(new SqlParameter("@FechaInicioCargoAno", alumnoexperienciacargo.FechaInicioCargoAno));
                cmd.Parameters.Add(new SqlParameter("@FechaFinCargoMes", alumnoexperienciacargo.FechaFinCargoMes));
                cmd.Parameters.Add(new SqlParameter("@FechaFinCargoAno", alumnoexperienciacargo.FechaFinCargoAno));
                cmd.Parameters.Add(new SqlParameter("@TipoCargo", alumnoexperienciacargo.TipoCargo));
                cmd.Parameters.Add(new SqlParameter("@DescripcionCargo", alumnoexperienciacargo.DescripcionCargo));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", alumnoexperienciacargo.CreadoPor));
                cmd.ExecuteNonQuery();

                conexion.Close();
            }


        }

        public DataTable ObtenerAlumnoExperienciaCargoPorId(int IdExperienciaCargo)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoExperienciaCargo_ObtenerPorID";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdExperienciaCargo", SqlDbType.Int)).Value = IdExperienciaCargo;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }

        public void Update(AlumnoExperienciaCargo alumnoexperienciacargo)
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
                    cmd.CommandText = "AlumnoExperienciaCargo_Update";
                    cmd.Parameters.Add(new SqlParameter("@IdExperiencia", alumnoexperienciacargo.IdExperiencia));
                    cmd.Parameters.Add(new SqlParameter("@IdExperienciaCargo", alumnoexperienciacargo.IdExperienciaCargo));
                    cmd.Parameters.Add(new SqlParameter("@Empresa", alumnoexperienciacargo.Empresa));
                    cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", alumnoexperienciacargo.DescripcionEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresa", alumnoexperienciacargo.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", alumnoexperienciacargo.SectorEmpresarial));
                    cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", alumnoexperienciacargo.SectorEmpresarial2));
                    cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", alumnoexperienciacargo.SectorEmpresarial3));
                    cmd.Parameters.Add(new SqlParameter("@Pais", alumnoexperienciacargo.Pais));
                    cmd.Parameters.Add(new SqlParameter("@Ciudad", alumnoexperienciacargo.Ciudad));
                    cmd.Parameters.Add(new SqlParameter("@NombreCargo", alumnoexperienciacargo.NombreCargo));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioCargoMes", alumnoexperienciacargo.FechaInicioCargoMes));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioCargoAno", alumnoexperienciacargo.FechaInicioCargoAno));
                    cmd.Parameters.Add(new SqlParameter("@FechaFinCargoMes", alumnoexperienciacargo.FechaFinCargoMes));
                    cmd.Parameters.Add(new SqlParameter("@FechaFinCargoAno", alumnoexperienciacargo.FechaFinCargoAno));
                    cmd.Parameters.Add(new SqlParameter("@TipoCargo", alumnoexperienciacargo.TipoCargo));
                    cmd.Parameters.Add(new SqlParameter("@DescripcionCargo", alumnoexperienciacargo.DescripcionCargo));
                    cmd.Parameters.Add(new SqlParameter("@ModificadoPor", alumnoexperienciacargo.Usuario));
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

        public void Desactivar(int IdExperiencia, int IdExperienciaCargo)
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
                    cmd.CommandText = "AlumnoExperienciaCargo_Desactivar";
                    cmd.Parameters.Add(new SqlParameter("@IdExperiencia", SqlDbType.Int)).Value = IdExperiencia;
                    cmd.Parameters.Add(new SqlParameter("@IdExperienciaCargo", SqlDbType.Int)).Value = IdExperienciaCargo;

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
