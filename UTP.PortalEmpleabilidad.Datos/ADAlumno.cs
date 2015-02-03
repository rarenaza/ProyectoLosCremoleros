using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADAlumno:ADBase
    {
        ADConexion cnn = new ADConexion();


        public DataTable ObtenerUsuarioPorId(string nombreUsuario)
        {
            SqlCommand cmd = new SqlCommand();
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

        public DataTable Alumno_ObtenerFoto(int idAlumno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Alumno_ObtenerFoto";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = idAlumno;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
        public DataTable ObtenerAlumnoPorCodigo(string codigoAlumno)
        {
            SqlCommand cmd = new SqlCommand();
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

        public void ModifcarDatos(Alumno alumno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Alumno_ModificarDatos";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = alumno.IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 300)).Value = alumno.Direccion;
            cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", SqlDbType.VarChar, 6)).Value = alumno.DireccionDistrito;
            cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", SqlDbType.VarChar, 6)).Value = alumno.DireccionCiudad;
            cmd.Parameters.Add(new SqlParameter("@DireccionRegion", SqlDbType.VarChar, 6)).Value = alumno.DireccionRegion;
            cmd.Parameters.Add(new SqlParameter("@CorreoElectronico2", SqlDbType.VarChar, 300)).Value = alumno.CorreoElectronico2 == null ? "" : alumno.CorreoElectronico2;
            cmd.Parameters.Add(new SqlParameter("@TelefonoFijoCasa", SqlDbType.VarChar, 40)).Value = alumno.TelefonoFijoCasa == null ? "" : alumno.TelefonoFijoCasa;
            cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", SqlDbType.VarChar, 40)).Value = alumno.TelefonoCelular == null ? "" : alumno.TelefonoCelular;
            cmd.Parameters.Add(new SqlParameter("@ModificadoPor", SqlDbType.VarChar, 50)).Value = alumno.Usuario;
            cmd.Parameters.Add(new SqlParameter("@Foto", SqlDbType.VarBinary,-1)).Value = alumno.Foto;
            cmd.Parameters.Add(new SqlParameter("@ArchivoMimeType", SqlDbType.VarChar, 50)).Value = alumno.ArchivoMimeType;

            cmd.ExecuteNonQuery();
            cnn.Desconectar();

        }

        public DataTable ObtenerAlumnoPorIdAlumno(int IdAlumno)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Alumno_ObtenerPorIdAlumno";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.NVarChar)).Value = IdAlumno;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cnn.Desconectar();
            return dt;
        }
        public DataTable Utp_BuscarDatosListaEmpresas(int idempresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Utp_BuscarDatosListaEmpresas";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idempresa));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }
       
    }
}
