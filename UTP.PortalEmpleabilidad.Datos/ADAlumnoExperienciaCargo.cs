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
    }
}
