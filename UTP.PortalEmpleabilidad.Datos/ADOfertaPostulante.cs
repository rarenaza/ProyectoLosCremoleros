using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Datos
{
    public class ADOfertaPostulante
    {
        ADConexion cnn = new ADConexion();
        ADConexion cnn2 = new ADConexion();
        SqlCommand cmd = new SqlCommand();
        SqlCommand cmd2 = new SqlCommand();
        public void Insertar(OfertaPostulante ofertapostulante)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "OfertaPostulante_Insertar";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdOferta", SqlDbType.Int)).Value = ofertapostulante.IdOferta;
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = ofertapostulante.IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@FaseOferta", SqlDbType.VarChar, 6)).Value = ofertapostulante.FaseOfertaPostulacion;
            cmd.Parameters.Add(new SqlParameter("@IdCV", SqlDbType.Int)).Value = ofertapostulante.IdCV;
            cmd.Parameters.Add(new SqlParameter("@DocumentoCV", ofertapostulante.DocumentoCV));
            cmd.Parameters.Add(new SqlParameter("@CreadoPor", SqlDbType.VarChar,50)).Value = ofertapostulante.CreadoPor;
            cmd.ExecuteNonQuery();
            cnn.Desconectar();
            if (ofertapostulante.RecibeCorreos == "OFCOCO") 
            {
                
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "OfertaCorreo_Insertar";
                cmd2.Connection = cnn2.cn;
                cnn2.Conectar();
                cmd2.Parameters.Add(new SqlParameter("@IdOferta", SqlDbType.Int)).Value = ofertapostulante.IdOferta;
                cmd2.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = ofertapostulante.IdAlumno;
                cmd2.Parameters.Add(new SqlParameter("@CorreoUsuarioEmpresa", SqlDbType.VarChar, 300)).Value = ofertapostulante.CorreoElectronicoUsuarioEmpresa;
                cmd2.Parameters.Add(new SqlParameter("@CargoOfrecido", SqlDbType.VarChar, 300)).Value = ofertapostulante.CargoOfrecido;
                cmd2.ExecuteNonQuery();
                cnn2.Desconectar();
            }
        }

        public DataTable OfertaPostulante_DescaragarCV(int IdAlumno, int IdOferta)
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "OfertaPostulante_DescargarCV";
            cmd.Connection = cnn.cn;
            cnn.Conectar();
            cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
            cmd.Parameters.Add(new SqlParameter("@IdOferta", SqlDbType.Int)).Value = IdOferta;


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);

            cnn.Desconectar();

            return dt;
        }

        public DataTable ObtenerPostulantesPorIDAlumno(int IdAlumno, string PalabraClave, int PaginaActual, int NumeroRegistros)
        {
            DataTable dtResultado = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cnn.Conexion()))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "OfertaPostulante_ObtenerPorIDAlumno";
                cmd.Parameters.Add(new SqlParameter("@IdAlumno", SqlDbType.Int)).Value = IdAlumno;
                cmd.Parameters.Add(new SqlParameter("@PalabraClave", SqlDbType.VarChar, 100)).Value = PalabraClave;
                cmd.Parameters.Add(new SqlParameter("@PagActual", SqlDbType.Int)).Value = PaginaActual;
                cmd.Parameters.Add(new SqlParameter("@NumRegistros", SqlDbType.Int)).Value = NumeroRegistros;
                cmd.Connection = cnn.cn;
                cnn.Conectar();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dtResultado = new DataTable();
                da.Fill(dtResultado);
                cnn.Desconectar();
            }

            return dtResultado;
        }
    }
}
