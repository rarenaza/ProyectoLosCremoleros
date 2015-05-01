using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Usuario : Auditoria
    {
        public int id { get; set; }
        
        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string NombreUsuario { get; set; }
        public ListaValor TipoUsuario { get; set; }
        public ListaValor EstadoUsuario { get; set; }
        public ListaValor Rol { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [DataType(DataType.Password)]
        public string Contrasena { get; set; }

       
        public string NombreCompleto { get; set; }

        public string TipoUsuarioIdListaValor { get; set; }
        public string EstadoUsuarioIdListaValor { get; set; }

        public Usuario()
        {
            TipoUsuario = new ListaValor();
            EstadoUsuario = new ListaValor();
            Rol = new ListaValor();
        }
        public string Captcha { get; set; }

        public string Token { get; set; }
    }
}
