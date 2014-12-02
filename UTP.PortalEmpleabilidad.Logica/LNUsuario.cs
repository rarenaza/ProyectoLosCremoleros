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
    }
}
