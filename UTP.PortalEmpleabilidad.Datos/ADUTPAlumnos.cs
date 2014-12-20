﻿using System;
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
    public class ADUTPAlumnos : ADBase
    {
        public void InsertarDatosDeAlumno(Alumno alumno, Usuario usuario, AlumnoEstudio alumnoEstudio)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                conexion.Open();

                SqlTransaction transaccion;
                transaccion = conexion.BeginTransaction();
                cmd.Connection = conexion;
                cmd.Transaction = transaccion;

                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Usuario_Insertar";

                    //Usuario:
                    cmd.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 100)).Value = usuario.NombreUsuario;
                    cmd.Parameters.Add(new SqlParameter("@TipoUsuario", SqlDbType.VarChar, 200)).Value = usuario.TipoUsuarioIdListaValor;
                    cmd.Parameters.Add(new SqlParameter("@EstadoUsuario", SqlDbType.VarChar, 6)).Value = usuario.EstadoUsuarioIdListaValor;
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar, 20)).Value = usuario.CreadoPor;

                    cmd.ExecuteNonQuery();
                    
                    //Alumno:
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Alumno_Insertar";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@CodAlumnoUtp", alumno.CodAlumnoUTP));
                    cmd.Parameters.Add(new SqlParameter("@Usuario", alumno.CodAlumnoUTP));
                    cmd.Parameters.Add(new SqlParameter("@Nombres", alumno.Nombres));
                    cmd.Parameters.Add(new SqlParameter("@Apellidos", alumno.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", alumno.TipoDocumentoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@NumeroDocumento", alumno.NumeroDocumento));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", alumno.FechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@Sexo", alumno.SexoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", alumno.Direccion));
                    cmd.Parameters.Add(new SqlParameter("@DireccionDistrito", alumno.DireccionDistrito));
                    cmd.Parameters.Add(new SqlParameter("@DireccionCiudad", alumno.DireccionCiudad));
                    cmd.Parameters.Add(new SqlParameter("@DireccionRegion", alumno.DireccionRegion));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", alumno.CorreoElectronico1));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico2", alumno.CorreoElectronico2));
                    cmd.Parameters.Add(new SqlParameter("@TelefonoFijoCasa", alumno.TelefonoFijoCasa));
                    cmd.Parameters.Add(new SqlParameter("@TelefonoCelular", alumno.TelefonoCelular));
                    cmd.Parameters.Add(new SqlParameter("@Foto", alumno.Foto));
                    cmd.Parameters.Add(new SqlParameter("@EstadoAlumno", alumno.EstadoAlumnoIdListaValor));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", alumno.CreadoPor));

                    object resultadoIdAlumno = cmd.ExecuteScalar();
                    int idAlumno = 0;
                    if (resultadoIdAlumno != null) idAlumno = Convert.ToInt32(resultadoIdAlumno);

                    //Alumno estudio:
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "AlumnoEstudio_Insertar";

                    //Parámetros:
                    cmd.Parameters.Add(new SqlParameter("@IdAlumno", idAlumno));
                    cmd.Parameters.Add(new SqlParameter("@Institucion", alumnoEstudio.Institucion));
                    cmd.Parameters.Add(new SqlParameter("@Estudio", alumnoEstudio.Estudio));
                    cmd.Parameters.Add(new SqlParameter("@TipoDeEstudio", alumnoEstudio.TipoDeEstudio));
                    cmd.Parameters.Add(new SqlParameter("@EstadoDelEstudio", alumnoEstudio.EstadoDelEstudio));
                    cmd.Parameters.Add(new SqlParameter("@Observacion", alumnoEstudio.Observacion));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioMes", alumnoEstudio.FechaInicioMes));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicioAno", alumnoEstudio.FechaInicioAno));
                    cmd.Parameters.Add(new SqlParameter("@FechaFinMes", alumnoEstudio.FechaFinMes));
                    cmd.Parameters.Add(new SqlParameter("@FechaFinAno", alumnoEstudio.FechaFinAno));
                    cmd.Parameters.Add(new SqlParameter("@CicloEquivalente", alumnoEstudio.CicloEquivalente));
                    cmd.Parameters.Add(new SqlParameter("@DatoUTP", alumnoEstudio.DatoUTP));
                    cmd.Parameters.Add(new SqlParameter("@CreadoPor", alumnoEstudio.CreadoPor));

                    cmd.ExecuteNonQuery();

                    //Se actualiza la columna PrimerInicioDeSesion de la tabla UTPAlumnos
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "UTPAlumnos_ActualizarPrimeraSesion";
                    cmd.Parameters.Add(new SqlParameter("@Codigo", alumno.CodAlumnoUTP));
                    cmd.ExecuteNonQuery();
                   
                    transaccion.Commit();

                    conexion.Close();
                }
                catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;
                }
            }
        }

        //public void Actualizar(Empresa empresa)
        //{
        //    using (SqlConnection conexion = new SqlConnection(cadenaConexion))
        //    {
        //        SqlCommand cmd = new SqlCommand();

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Empresa_Actualizar";
        //        cmd.Connection = conexion;

        //        conexion.Open();

        //        cmd.Parameters.Add(new SqlParameter("@IdEmpresa", empresa.IdEmpresa));
        //        cmd.Parameters.Add(new SqlParameter("@NombreComercial", empresa.NombreComercial));
        //        cmd.Parameters.Add(new SqlParameter("@RazonSocial", empresa.RazonSocial));
        //        cmd.Parameters.Add(new SqlParameter("@Pais", empresa.PaisIdListaValor));
        //        cmd.Parameters.Add(new SqlParameter("@IdentificadorTributario", empresa.IdentificadorTributario));
        //        cmd.Parameters.Add(new SqlParameter("@DescripcionEmpresa", empresa.DescripcionEmpresa));
        //        cmd.Parameters.Add(new SqlParameter("@LinkVideo", empresa.LinkVideo));
        //        cmd.Parameters.Add(new SqlParameter("@AnoCreacion", empresa.AnoCreacion));
        //        cmd.Parameters.Add(new SqlParameter("@NumeroEmpleados", empresa.NumeroEmpleadosIdListaValor));
        //        //cmd.Parameters.Add(new SqlParameter("@EstadoEmpresa", empresa.EstadoEmpresaIdListaValor)).Value = empresa.EstadoEmpresa;
        //        cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial1", empresa.SectorEmpresarial1IdListaValor));
        //        cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial2", empresa.SectorEmpresarial2IdListaValor));
        //        cmd.Parameters.Add(new SqlParameter("@SectorEmpresarial3", empresa.SectorEmpresarial3IdListaValor));
        //        cmd.Parameters.Add(new SqlParameter("@ModificadoPor", empresa.ModificadoPor));
        //        cmd.ExecuteNonQuery();

        //        conexion.Close();
        //    }
           
        //}

        public DataSet ObtenerDatosPorCodigo(string codigo)
        {
            DataSet dsResultado = new DataSet();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UTPAlumnos_ObtenerPorCodigo";
                cmd.Connection = conexion;

                conexion.Open();

                cmd.Parameters.Add(new SqlParameter("@Codigo", codigo));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dsResultado);

                conexion.Close();
            }

            return dsResultado;
        }
        public DataTable AlumnoUTP_ObtenerDatosPorCodigo(int id)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Alumnos_obtenerPorID";
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", id));
                cmd.Connection = conexion;

                conexion.Open();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                dtResultado = new DataTable();

                da.Fill(dtResultado);

                conexion.Close();
            }

            return dtResultado;
        }

        public DataTable AlumnoUtp_obtenerEstudios(int id)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AlumnoUtp_obtenerEstudios";
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", id));
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
