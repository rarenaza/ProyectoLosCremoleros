using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADUsuario : ADBase
    {
        public bool ValidarNombreDeUsuario(string nombreUsuario)
        {
            bool existe = false;

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Usuario_ValidarNombreDeUsuario";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@NombreUsuario", nombreUsuario));
               
                object resultado = cmd.ExecuteScalar();

                if (resultado != null) existe = Convert.ToBoolean(resultado);

                conexion.Close();
            }

            return existe;
        }
    }
}
