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

        public List<Usuario> ObtenerUsuariosPorTipo(string tipoUsuario)
        {
            List<Usuario> usuarios = new List<Usuario>();


            foreach (DataRow item in adUsuario.ObtenerUsuariosPorTipo(tipoUsuario).Rows)
            {
                Usuario usuario = new Usuario();

                usuario.NombreUsuario = Convert.ToString(item["Usuario"]);
                usuario.TipoUsuario.Valor = Convert.ToString(item["TipoUsuario"]);
                usuario.NombreCompleto = Convert.ToString(item["UsuarioNombreCompleto"]);

                usuarios.Add(usuario);
            }


            return usuarios;
        }
    }
}
