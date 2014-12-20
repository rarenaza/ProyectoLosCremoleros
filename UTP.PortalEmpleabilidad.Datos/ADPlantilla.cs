using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADPlantilla
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();



        public DataTable MostrarPlantillaCV()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PlantillaCV_Mostrar";
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }

            return dtResultado;
        }

        public DataSet ObtenerDatosParaPlantilla(int IdCV)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoCV_ObtenerDatosParaPlantilla";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@IdCV", SqlDbType.Int)).Value = IdCV;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dsResultado = new DataSet();
                da.Fill(dsResultado);
                conexion.Close();
            }

            return dsResultado;
        }
    }
}
