using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class EmpresaUsuario : Auditoria
    {
        public int IdEmpresaUsuario { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }

        
        public ListaValor TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public ListaValor Sexo { get; set; }
        
        public string TelefonoFijo { get; set; }
        public string TelefonoAnexo { get; set; }
        public string TelefonoCelular { get; set; }
        public EmpresaLocacion EmpresaLocacion { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Nombres { get; set; }

        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public int IdEmpresaLocacion { get; set; }
        
        [Required(ErrorMessage=Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string CorreoElectronico { get; set; }

        public string SexoIdListaValor { get; set; }
        public string TipoDocumentoIdListaValor { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string RolIdListaValor { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string EstadoUsuarioIdListaValor { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Contrasena { get; set; }

        public string RepetirContrasena { get; set; }

              

        public EmpresaUsuario() 
        {
            Empresa = new Empresa();
            Usuario = new Usuario();
            TipoDocumento = new ListaValor();
            Sexo = new ListaValor();
            EmpresaLocacion = new EmpresaLocacion();
        }

    }
}
