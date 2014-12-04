using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UTP.PortalEmpleabilidad.Modelo;
using System.Configuration;


namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADEmpresaLocacion : ADBase
    {
        public DataTable ObtenerLocaciones(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaLocacion_ObtenerLocaciones";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(EmpresaLocacion empresaLocacion, string usuarioCreacion)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaLocacion_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresaLocacion.IdEmpresa));
                cmd.Parameters.Add(new SqlParameter("@TipoLocacion", empresaLocacion.TipoLocacion.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@NombreLocacion", empresaLocacion.NombreLocacion));
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", empresaLocacion.CorreoElectronico));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", empresaLocacion.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@Direccion", empresaLocacion.Direccion));
                cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", empresaLocacion.DireccionDistrito));
                cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", empresaLocacion.DireccionCiudad));
                cmd.Parameters.Add(new SqlParameter("@DireccionRegion", empresaLocacion.DireccionRegion));
                cmd.Parameters.Add(new SqlParameter("@EstadoLocacion", empresaLocacion.EstadoLocacion.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", usuarioCreacion));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

    }
}
