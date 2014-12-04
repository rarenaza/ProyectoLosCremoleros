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

    }
}
