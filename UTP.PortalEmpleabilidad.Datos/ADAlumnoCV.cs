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
    public class ADAlumnoCV
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();
        public DataTable ObtenerAlumnoCVPorIdAlumno(int IdAlumno)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
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
        public void Update(VistaAlumnoCV vistaalumnocv)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AlumnoCV_Update";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdCV", SqlDbType.Int)).Value = vistaalumnocv.IdCV;
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = vistaalumnocv.IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@IdPlantillaCV", SqlDbType.Int)).Value = vistaalumnocv.IdPlantillaCV;
            cmd.Parameters.Add(new SqlParameter("@IncluirTelefonoFijo", SqlDbType.Bit)).Value = vistaalumnocv.IncluirTelefonoFijo;
            cmd.Parameters.Add(new SqlParameter("@IncluirCorreoElectronico2", SqlDbType.Bit)).Value = vistaalumnocv.IncluirCorreoElectronico1;
            cmd.Parameters.Add(new SqlParameter("@IncluirFoto", SqlDbType.Bit)).Value = vistaalumnocv.IncluirFoto;
            cmd.Parameters.Add(new SqlParameter("@Perfil", SqlDbType.VarChar, 5000)).Value = vistaalumnocv.Perfil;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre1", SqlDbType.Bit)).Value = vistaalumnocv.IncluirNombre1;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre2", SqlDbType.Bit)).Value = vistaalumnocv.IncluirNombre2;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre3", SqlDbType.Bit)).Value = vistaalumnocv.IncluirNombre3;
            cmd.Parameters.Add(new SqlParameter("@IncluirNombre4", SqlDbType.Bit)).Value = vistaalumnocv.IncluirNombre4;
            cmd.Parameters.Add(new SqlParameter("@ModificadoPor", SqlDbType.VarChar, 50)).Value = vistaalumnocv.Usuario;
            cmd.Parameters.Add(new SqlParameter("@IncluirDireccion", SqlDbType.Bit)).Value = vistaalumnocv.IncluirDireccion;

            cmd.ExecuteNonQuery();
            cnn.Desconectar();
        }


    }
}
