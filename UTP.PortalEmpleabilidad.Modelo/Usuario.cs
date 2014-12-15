using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Usuario
    {
        public int id { get; set; }
        
        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string NombreUsuario { get; set; }
        public ListaValor TipoUsuario { get; set; }
        public ListaValor EstadoUsuario { get; set; }
        public ListaValor Rol { get; set; }        
        
        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Contrasena { get; set; }

        public Usuario()
        {
            TipoUsuario = new ListaValor();
            EstadoUsuario = new ListaValor();
            Rol = new ListaValor();
        }

    }
}
