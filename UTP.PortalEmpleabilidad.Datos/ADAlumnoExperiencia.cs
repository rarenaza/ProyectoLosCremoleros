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
    public class ADAlumnoExperiencia
    {
        ADConexion cnn = new ADConexion();
        public int ValidarExistePorIdEmpresa(int IdEmpresa, int IdAlumno)
        {
            int id = 0;
            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr = null;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoExperiencia_ValidarPorIdEmpresa";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", IdAlumno));

                object resultado = cmd.ExecuteScalar();

                if (resultado != null)
                {
                    id = Convert.ToInt32(resultado);
                }

                // dr = cmd.ExecuteReader();
                //if (dr.HasRows)
                //{
                //    id = dr.GetInt32(dr.GetOrdinal("@IdExperiencia"));
                //}
                //dr.Close();
                conexion.Close();
            }
            return id;

        }

        public int Registrar(AlumnoExperiencia alumnoexperiencia)
        {
            int id = 0;
            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoExperiencia_Registrar";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@IdAlumno", alumnoexperiencia.IdAlumno));
                cmd.Parameters.Add(new SqlParameter("@Empresa", alumnoexperiencia.Empresa));
                cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", alumnoexperiencia.DescripcionEmpresa));
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", alumnoexperiencia.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", alumnoexperiencia.SectorEmpresarial));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", alumnoexperiencia.SectorEmpresarial2));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3 ", alumnoexperiencia.SectorEmpresarial3));
                cmd.Parameters.Add(new SqlParameter("@Pais", alumnoexperiencia.Pais));
                cmd.Parameters.Add(new SqlParameter("@Ciudad", alumnoexperiencia.Ciudad));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", alumnoexperiencia.CreadoPor));
                cmd.Parameters.Add(new SqlParameter("@IdExperiencia", DbType.Int32)).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                id = (int)cmd.Parameters["@IdExperiencia"].Value;
                conexion.Close();
            }
            return id;

        }
    }
}
