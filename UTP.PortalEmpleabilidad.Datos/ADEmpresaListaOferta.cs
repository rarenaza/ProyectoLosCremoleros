using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Datos
{
 public   class ADEmpresaListaOferta
    {
     private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;
     public DataTable Empresa_ListaOfertas()
     {
         DataTable dtResultado = new DataTable();

         using (SqlConnection conexion = new SqlConnection(cadenaConexion))
         {
             SqlCommand cmd = new SqlCommand();

             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "Empresa_ListaOfertas";

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
