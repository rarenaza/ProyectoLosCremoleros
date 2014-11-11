using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Datos
{
   public  class ADLista
    {
        ADConexion cnn = new ADConexion();
        SqlCommand cmd = new SqlCommand();


        public DataTable MostrarLista()
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Lista_Mostrar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
        public bool Lista_insertar(int IDLista, string NombreLista, string DescripcionLista, int Modificable, string CreadoPor)
        {
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_Insertar";
                cmd.Connection = cnn.cn;
                cmd.Parameters.Add(new SqlParameter("@IDLista", SqlDbType.Int)).Value = IDLista;
                cmd.Parameters.Add(new SqlParameter("@NombreLista", SqlDbType.VarChar, 100)).Value = NombreLista;
                cmd.Parameters.Add(new SqlParameter("@DescripcionLista", SqlDbType.VarChar, 500)).Value = DescripcionLista;
                cmd.Parameters.Add(new SqlParameter("@Modificable", SqlDbType.Bit)).Value = Modificable;
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 50)).Value =CreadoPor ;
               
                cnn.Conectar();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                cnn.Desconectar();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }

        }

        public DataTable Lista_Buscar(int IdLista)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Lista_Buscar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IDLista", SqlDbType.Int)).Value = IdLista;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }



       
    }
}
