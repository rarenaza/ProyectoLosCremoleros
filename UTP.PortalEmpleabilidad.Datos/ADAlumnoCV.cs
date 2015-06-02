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
    public class ADAlumnoCV:ADBase
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();
        public DataTable ObtenerAlumnoCVPorIdAlumno(int IdAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ObtenerPorIdAlumno";
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
        public DataTable ObtenerAlumnoCVPorIdAlumnoCompleto(int IdAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ObtenerPorIdAlumnoCompleto";
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
        public DataTable ObtenerAlumnoCVPorIdAlumnoYIdCV(int IdAlumno, int IdCV)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ObtenerPorIdAlumnoYIdCV";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
                cmd.Parameters.Add(new SqlParameter("@IdCV", SqlDbType.Int)).Value = IdCV;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(AlumnoCV alumnocv)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AlumnoCV_Insertar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = alumnocv.IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@IdPlantillaCV", SqlDbType.Int)).Value = alumnocv.IdPlantillaCV;
            cmd.Parameters.Add(new SqlParameter("@NombreCV", SqlDbType.VarChar, 50)).Value = alumnocv.NombreCV;
            cmd.Parameters.Add(new SqlParameter("@IncluirTelefonoFijo", SqlDbType.Bit)).Value = alumnocv.IncluirTelefonoFijo;
            cmd.Parameters.Add(new SqlParameter("@IncluirCorreoElectronico2", SqlDbType.Bit)).Value = alumnocv.IncluirCorreoElectronico2;
            cmd.Parameters.Add(new SqlParameter("@IncluirFoto", SqlDbType.Bit)).Value = alumnocv.IncluirFoto;
            cmd.Parameters.Add(new SqlParameter("@Perfil", SqlDbType.VarChar, 100)).Value = alumnocv.Perfil;
            cmd.Parameters.Add(new SqlParameter("@EstadoCV", SqlDbType.VarChar, 6)).Value = alumnocv.EstadoCV;
            cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value = alumnocv.CreadoPor;

            cmd.ExecuteNonQuery();
            cnn.Desconectar();
        }
        public void Update(AlumnoCV alumnocv, int PorcentajeCV)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AlumnoCV_Update";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdCV", SqlDbType.Int)).Value = alumnocv.IdCV;
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = alumnocv.IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@IdPlantillaCV", SqlDbType.Int)).Value = alumnocv.IdPlantillaCV;
            cmd.Parameters.Add(new SqlParameter("@IncluirTelefonoFijo", SqlDbType.Bit)).Value = alumnocv.IncluirTelefonoFijo;
            cmd.Parameters.Add(new SqlParameter("@IncluirCorreoElectronico2", SqlDbType.Bit)).Value = alumnocv.IncluirCorreoElectronico1;
            cmd.Parameters.Add(new SqlParameter("@IncluirFoto", SqlDbType.Bit)).Value = alumnocv.IncluirFoto;
            cmd.Parameters.Add(new SqlParameter("@Perfil", SqlDbType.VarChar, 5000)).Value = alumnocv.Perfil == null ? "" : alumnocv.Perfil;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre1", SqlDbType.Bit)).Value = alumnocv.IncluirNombre1;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre2", SqlDbType.Bit)).Value = alumnocv.IncluirNombre2;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre3", SqlDbType.Bit)).Value = alumnocv.IncluirNombre3;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre4", SqlDbType.Bit)).Value = alumnocv.IncluirNombre4;
            cmd.Parameters.Add(new SqlParameter("@ModificadoPor", SqlDbType.VarChar, 50)).Value = alumnocv.Usuario;
            cmd.Parameters.Add(new SqlParameter("@IncluirDireccion", SqlDbType.Bit)).Value = alumnocv.IncluirDireccion;
            cmd.Parameters.Add(new SqlParameter("@FechaCvCompleto", SqlDbType.DateTime)).Value = (PorcentajeCV == 100) ? (object)DateTime.Now : DBNull.Value;
            
               
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
        }

        public bool RegistrarCV(ref AlumnoCV alumnocv)
        {
            bool existe = false;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AlumnoCV_RegistrarCV";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@NombreCV", SqlDbType.VarChar, 100)).Value = alumnocv.NombreCV;
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = alumnocv.IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@IdPlantillaCV", SqlDbType.Int)).Value = alumnocv.IdPlantillaCV;
            cmd.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 50)).Value = alumnocv.Usuario;
            cmd.Parameters.Add(new SqlParameter("@IdCV", SqlDbType.Int)).Direction = ParameterDirection.Output;
            if (cmd.ExecuteNonQuery() > 0)
            {
                alumnocv.IdCV = (int)cmd.Parameters["@IdCV"].Value;
                if (alumnocv.IdCV > 0)
                {
                    existe = true;
                }
            }
            cnn.Desconectar();
            return existe;
        }
        public bool ValidarExistencia(int IdAlumno, string NombreCV)
        {
            bool existe = false;

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataReader dr = null;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ValidarExitencia";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
                cmd.Parameters.Add(new SqlParameter("@NombreCV", SqlDbType.VarChar, 50)).Value = NombreCV;
                dr = cmd.ExecuteReader();
                existe = dr.HasRows;
                conexion.Close();
            }

            return existe;
        }

        public DataSet ObtenerPostulanteCV(int IdPostulacion)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ObtenerDatosParaCV";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdPostulacion", SqlDbType.Int)).Value = IdPostulacion;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsResultado);
                conexion.Close();
            }

            return dsResultado;
        }

        public DataSet ObtenerDatosCV(int idAlumno)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ObtenerDatosCV_VistaEmpresa";
                cmd.Connection = conexion;
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", idAlumno));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dsResultado);
                conexion.Close();
            }

            return dsResultado;
        }
    }
}
