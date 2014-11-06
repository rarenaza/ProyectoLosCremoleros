using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace UTP.PortalEmpleabilidad.Datos
{
    class ADConexion
    {
        public SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString);
        
        public string Conexion()
        {
            return cn.ConnectionString;
        }
        public void Conectar()
        {
            cn.Open();
        }
        public void Desconectar()
        {
            cn.Close();
        }
    }
}
