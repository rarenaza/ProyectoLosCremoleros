using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class UTPUsuario : Auditoria
    {
        public int IdUTPUsuario { get; set; }

         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string NombreUsuario { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Nombres { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Apellidos { get; set; }

        public string SexoDescripcion { get; set; }
        public string SexoIdListaValor { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Correo { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoAnexo { get; set; }

        public string TelefonoCelular { get; set; }
            [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]        
        public string EstadoUsuarioDescripcion { get; set; }
             [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string EstadoUsuarioIdListaValor { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string RolDescripcion { get; set; }
         [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]

        public string RolIdListaValor { get; set; }
        public string TipoUsuarioIdListaValor { get; set; }

        public int CantidadTotal { get; set; }

        public int idcod { get; set; }

    }
}
