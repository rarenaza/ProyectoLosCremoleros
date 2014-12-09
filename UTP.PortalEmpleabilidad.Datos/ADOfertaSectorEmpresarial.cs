using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADOfertaSectorEmpresarial
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable ObtenerSectoresEmpresariales(int idOferta, int idOfertaSectorEmpresarial)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaSectorEmpresarial_Obtener";
                cmd.Parameters.Add(new SqlParameter("@IdOferta", idOferta));
                cmd.Parameters.Add(new SqlParameter("@IdOfertaSectorEmpresarial", idOfertaSectorEmpresarial));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(OfertaSectorEmpresarial ofertaSectorEmpresarial)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaSectorEmpresarial_Insertar";

                //Parámetros:            
                cmd.Parameters.Add(new SqlParameter("@IdOferta", ofertaSectorEmpresarial.IdOferta));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", ofertaSectorEmpresarial.SectorEmpresarial.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@ExperienciaExcluyente", ofertaSectorEmpresarial.ExperienciaExcluyente));
                cmd.Parameters.Add(new SqlParameter("@AniosTrabajados", ofertaSectorEmpresarial.AniosTrabajados));
                cmd.Parameters.Add(new SqlParameter("@EstadoOfertaSectorEmpresarial", ofertaSectorEmpresarial.EstadoOfertaSectorEmpresarial.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", ofertaSectorEmpresarial.CreadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void Actualizar(OfertaSectorEmpresarial ofertaSectorEmpresarial)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaSectorEmpresarial_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@IdOfertaSectorEmpresarial", ofertaSectorEmpresarial.IdOfertaSectorEmpresarial));
                cmd.Parameters.Add(new SqlParameter("@IdOferta", ofertaSectorEmpresarial.IdOferta));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", ofertaSectorEmpresarial.SectorEmpresarial.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@ExperienciaExcluyente", ofertaSectorEmpresarial.ExperienciaExcluyente));
                cmd.Parameters.Add(new SqlParameter("@AniosTrabajados", ofertaSectorEmpresarial.AniosTrabajados));
                cmd.Parameters.Add(new SqlParameter("@EstadoOfertaSectorEmpresarial", ofertaSectorEmpresarial.EstadoOfertaSectorEmpresarial.IdListaValor));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", ofertaSectorEmpresarial.ModificadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }        
    }
}
