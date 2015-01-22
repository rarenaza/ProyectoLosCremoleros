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
    public class ADGeneral
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable ObtenerListaValor(int idLista)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable ObtenerListaValor2(int idLista)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable EMPRESA_BUSCAR_INFORMACIONADICIONAL(string IDListaValorPadre)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EMPRESA_BUSCAR_INFORMACIONADICIONAL";
                cmd.Parameters.Add(new SqlParameter("@IDListaValorPadre", IDListaValorPadre));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }



        public DataTable ObtenerListaValorPorIdPadre( string idListaPadre)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValorPorIdPadre";
                cmd.Parameters.Add(new SqlParameter("@idListaPadre", idListaPadre));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataTable EmpresaHuntingBuscarSimple(string Nombre)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaHuntingBuscar";
                cmd.Parameters.Add(new SqlParameter("@PalabraClave", Nombre));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable EmpresaHuntingBuscarAvanzada(string TipoDeEstudio, string Estudio, string EstadoEstudio,
            string SectorEmpresarial, int AnosExperiencia, string NombreCargo, string TipoInformacionAdicional, string Conocimiento, string Distrito)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EmpresaHuntingBuscarAvanzado";
                cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", TipoDeEstudio));
                cmd.Parameters.Add(new SqlParameter("@Estudio", Estudio));
                cmd.Parameters.Add(new SqlParameter("@EstadoEstudio", EstadoEstudio));
                cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial", SectorEmpresarial));
                cmd.Parameters.Add(new SqlParameter("@AnosExperiencia", AnosExperiencia));
                cmd.Parameters.Add(new SqlParameter("@NombreCargo", NombreCargo));
                cmd.Parameters.Add(new SqlParameter("@TipoInformacionAdicional", TipoInformacionAdicional));
                cmd.Parameters.Add(new SqlParameter("@Conocimiento", Conocimiento));
                cmd.Parameters.Add(new SqlParameter("@Distrito", Distrito));

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable Home_Departamento(int idLista)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Lista_ListaValor";
                cmd.Parameters.Add(new SqlParameter("@IdLista", idLista));
                cmd.Connection = conexion;
                conexion.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                conexion.Close();
            }
            return dtResultado;
        }

        public DataTable Home_ListarDistritos(string IDListaValorPadre)
        {
            DataTable dtResultado = new DataTable();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Home_ListarDistritos";
                cmd.Parameters.Add(new SqlParameter("@IDListaValorPadre", IDListaValorPadre));
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
