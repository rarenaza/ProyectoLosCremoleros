using System;
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

        public DataTable UTP_ObtenerOfertasporActivar(string oferta)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_Obtenerofertasporactivar";
                cmd.Parameters.Add(new SqlParameter("@oferta", oferta));
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

                    cmd.Parameters.Add(new SqlParameter("@Clasificacion", empresa.Clasificacion == null ? "" : empresa.Clasificacion));
                    cmd.Parameters.Add(new SqlParameter("@NivelDeRelacion", empresa.NivelDeRelacion == null ? "" : empresa.NivelDeRelacion));
                    cmd.Parameters.Add(new SqlParameter("@FacultadPrincipal", empresa.FacultadPrincipal == null ? "" : empresa.FacultadPrincipal));
                    cmd.Parameters.Add(new SqlParameter("@FacultadSecundaria", empresa.FacultadSecundaria == null ? "" : empresa.FacultadSecundaria));
                    cmd.Parameters.Add(new SqlParameter("@Usuario", empresa.Usuario));
           
                    cmd.Parameters.Add(new SqlParameter("@NivelDeFacturacion", empresa.NivelDeFacturacion));
        
               
            

                    cmd.Parameters.Add(new SqlParameter("@NuevoComentario", empresa.NuevoComentario == null?"" : empresa.NuevoComentario));

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


        #region Mantenimiento de Usuarios UTP

        public DataTable ObtenerUsuariosUTP()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_ObtenerLista";                
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable UTP_LISTAVALORPADRE()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_LISTAVALORPADRE";
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable ObtenerUsuarioUTPPorId(int idUTPUsuario)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_ObtenerPorId";
                cmd.Parameters.Add(new SqlParameter("@IdUTPUsuario", idUTPUsuario));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public void Insertar(UTPUsuario utpUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_Insertar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@Usuario", utpUsuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", utpUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", utpUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", utpUsuario.SexoIdListaValor == null ? "" : utpUsuario.SexoIdListaValor));

            
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", utpUsuario.Correo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", utpUsuario.TelefonoFijo == null ? "" : utpUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", utpUsuario.TelefonoAnexo == null ? "" : utpUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", utpUsuario.TelefonoCelular == null ? "" : utpUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", utpUsuario.EstadoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Rol", utpUsuario.RolIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@TipoUsuario", utpUsuario.TipoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CreadoPor", utpUsuario.CreadoPor));

                cmd.Connection = conexion;

                conexion.Open();
                
                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public void Actualizar(UTPUsuario utpUsuario)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPUsuario_Actualizar";

                //Parámetros:
                cmd.Parameters.Add(new SqlParameter("@Usuario", utpUsuario.NombreUsuario));
                cmd.Parameters.Add(new SqlParameter("@Nombres", utpUsuario.Nombres));
                cmd.Parameters.Add(new SqlParameter("@Apellidos", utpUsuario.Apellidos));
                cmd.Parameters.Add(new SqlParameter("@Sexo", utpUsuario.SexoIdListaValor == null ? "" : utpUsuario.SexoIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", utpUsuario.Correo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoFijo", utpUsuario.TelefonoFijo == null ? "" : utpUsuario.TelefonoFijo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoAnexo", utpUsuario.TelefonoAnexo == null ? "" : utpUsuario.TelefonoAnexo));
                cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", utpUsuario.TelefonoCelular == null ? "" : utpUsuario.TelefonoCelular));
                cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", utpUsuario.EstadoUsuarioIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@Rol", utpUsuario.RolIdListaValor));
                cmd.Parameters.Add(new SqlParameter("@ModificadoPor", utpUsuario.ModificadoPor));

                cmd.Connection = conexion;

                conexion.Open();

                cmd.ExecuteNonQuery();

                conexion.Close();
            }
        }

        public int UsuarioSistemaUTP_Exitencia(string Usuario)
        {
            int cantidad=0;
            try
            {
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UsuarioSistemaUTP_Exitencia";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@Usuario", Usuario));
                

                    cmd.Connection = conexion;
                    conexion.Open();
                    cmd.ExecuteScalar();

                    object resultado=cmd.ExecuteScalar ();
                    cantidad =Convert.ToInt32 (resultado);

                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cantidad;
        }

              

        public DataTable UTP_ObtenerEventosObtenerBuscar(string evento)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTP_ObtenerEventosObtenerBuscar";
                cmd.Parameters.Add(new SqlParameter("@Evento", evento));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable Evento_ListaEstado()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_ListaEstado";
            
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable Evento_ListaTipoEvento()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Evento_ListaTipoEvento";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        public DataTable EMPRESA_LISTAEMPRESA()
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "EMPRESA_LISTAEMPRESA";

                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();

            }

            return dtResultado;
        }

        #endregion
    

    }
}
