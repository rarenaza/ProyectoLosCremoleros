using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class UTPUsuario : Auditoria
    {
        public int IdUTPUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string SexoDescripcion { get; set; }
        public string SexoIdListaValor { get; set; }
        public string Correo { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoAnexo { get; set; }
        public string TelefonoCelular { get; set; }
        public string EstadoUsuarioDescripcion { get; set; }
        public string EstadoUsuarioIdListaValor { get; set; }
        public string RolDescripcion { get; set; }
        public string RolIdListaValor { get; set; }
        public string TipoUsuarioIdListaValor { get; set; }

    }
}
