using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADMensaje : ADBase
    {
        public DataTable ObtenerPorIdEmpresa(int idEmpresa)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mensaje_ObtenerPorIdEmpresa";
                cmd.Parameters.Add(new SqlParameter("@IdEmpresa", idEmpresa));
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
