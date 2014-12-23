﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADUTP
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["UTPConexionBD"].ConnectionString;

        public DataTable OfertasObtenerPendientes()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ofertas_ObtenerPendientes";
            
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable EmpresaObtenerPendientes()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Empresa_ObtenerPendientes";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }


        public DataTable Empresa_ObtenerPorNombre(string nombre)
        {
  
            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresas_ObtenerNombre";
            cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));

            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }
   

        public DataTable Empresa_BusquedaAvanzada(VistaEmpresListarOfertas entidad)
        {

            ADConexion cnn = new ADConexion();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Empresa_BusquedaAvanzada";
            cmd.Parameters.Add(new SqlParameter("@Nombre", entidad.NombreComercial == null ? "" : entidad.NombreComercial));
            cmd.Parameters.Add(new SqlParameter("@Valor", entidad.IdEstadoEmpresa == null ? "" : entidad.IdEstadoEmpresa));
            cmd.Parameters.Add(new SqlParameter("@Sector", entidad.IdSector == null ? "" : entidad.IdSector));
            cmd.Parameters.Add(new SqlParameter("@Razon", entidad.RazonSocial == null ? "" : entidad.RazonSocial));
            //cmd.Parameters.Add(new SqlParameter("@Ruc", entidad.RUC == null ? "" : entidad.RUC));

            //cmd.Parameters.Add(new SqlParameter("@EstadoOferta", entidad.EstadoOferta));




            cmd.Connection = cnn.cn;
            cnn.Conectar();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable UTP_ListarUltimosAlumnos()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ListarUltimosAlumnos";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_ObtenerUltimosAlumnos(string Dato)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerUltimosAlumnos";
                cmd.Parameters.Add(new SqlParameter("@Dato", Dato));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void ActualizarEstadoYUsuarioEC(Empresa empresa)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Empresa_ActualizarEstadoYUsuarioEC";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresa.IdEmpresa));
                    cmd.Parameters.Add(new SqlParameter("@Estado", empresa.EstadoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEC", empresa.UsuarioEC));                    

                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

    }
}
