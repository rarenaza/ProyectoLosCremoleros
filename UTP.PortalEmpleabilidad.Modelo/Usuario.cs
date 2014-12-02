using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public ListaValor TipoUsuario { get; set; }
        public ListaValor EstadoUsuario { get; set; }
        public ListaValor Rol { get; set; }        
        public string Contrasena { get; set; }

    }
}
