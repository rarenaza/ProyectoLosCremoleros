using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNUsuario
    {
        ADAlumno ad = new ADAlumno();
        ADUsuario adUsuario = new ADUsuario();

        public Usuario ObtenerUsuarioPorId(string nombreUsuario)
        {
            DataTable dtResultado = ad.ObtenerUsuarioPorId(nombreUsuario);
            Usuario usuario = new Usuario();

            if (dtResultado.Rows.Count > 0)
            {
                usuario.NombreUsuario = dtResultado.Rows[0]["Usuario"].ToString();
                usuario.TipoUsuario.Valor = dtResultado.Rows[0]["TipoUsuario"].ToString();
                usuario.EstadoUsuario.Valor = dtResultado.Rows[0]["EstadoUsuario"].ToString();
            }

            return usuario;
        }

        public bool ValidarNombreDeUsuario(string nombreUsuario)
        {
            return adUsuario.ValidarNombreDeUsuario(nombreUsuario);
        }
        public bool ValidarExistenciaEmpresa(string empresaPais, string empresaRUC)
        {
            return adUsuario.ValidarExistenciaEmpresa(empresaPais, empresaRUC);
        }

        public void InsertarToken(string token, string usuario, DateTime fechaExpira, DateTime fechaSolicito, string ip)
        {
            adUsuario.InsertarToken(token, usuario, fechaExpira, fechaSolicito, ip);
        }

        public string ObtenerToken(string usuario)
        {
            Usuario user = new Usuario();
            DataTable dtResultado = adUsuario.ObtenerToken(usuario);

            if (dtResultado != null && dtResultado.Rows.Count > 0)
                user.Token = dtResultado.Rows[0]["IdToken"].ToString();
            else
                user.Token = null;

            return user.Token;
        }
        
        public List<Usuario> ObtenerUsuariosPorTipo(string tipoUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();


            foreach (DataRow item in adUsuario.ObtenerUsuariosPorTipo(tipoUsuario).Rows)
            {
                Usuario usuario = new Usuario();

                usuario.NombreUsuario = Convert.ToString(item["Usuario"]);
                usuario.TipoUsuario.Valor = Convert.ToString(item["TipoUsuario"]);
                usuario.NombreCompleto = Convert.ToString(item["UsuarioNombreCompleto"]);

                //Modificado por junior porque no ejecutaba por falta de Script

                //usuario.EstadoUsuario.Valor = Convert.ToString(item["EstadoUsuario"]);

                usuarios.Add(usuario);
            }


            return usuarios;
        }

        public bool ActualizarTerminosCondiciones(string usuario)
        {
            return adUsuario.ActualizarTerminosCondiciones(usuario);
        }
    }
}
