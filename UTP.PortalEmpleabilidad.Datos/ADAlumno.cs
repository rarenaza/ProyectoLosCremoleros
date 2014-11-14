using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADAlumno
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();

        public DataTable ObtenerUsuarioPorId(string nombreUsuario)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerUsuarioPorId";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar)).Value = nombreUsuario;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable ObtenerAlumnoPorCodigo(string codigoAlumno)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Alumno_ObtenerPorCodigo";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@CodAlumnoUtp", SqlDbType.NVarChar)).Value = codigoAlumno;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
    }
}
