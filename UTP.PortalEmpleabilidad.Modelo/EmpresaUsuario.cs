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

        [Required(ErrorMessage = "Falta Tipo de Documento")]
        public ListaValor TipoDocumento { get; set; }

        [Required(ErrorMessage = "Falta Número de Documento")]
        public string NumeroDocumento { get; set; }

        public ListaValor Sexo { get; set; }

        [Required(ErrorMessage = "Falta Fijo")]
        public string TelefonoFijo { get; set; }

        [Required(ErrorMessage = "Falta Anexo")]
        public string TelefonoAnexo { get; set; }

        [Required(ErrorMessage = "Falta Celular")]
        public string TelefonoCelular { get; set; }
        public EmpresaLocacion EmpresaLocacion { get; set; }

        [Required(ErrorMessage = "Falta Usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage="Falta Nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage="Falta Apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Falta Ubicación")]
        public int IdEmpresaLocacion { get; set; }
        
        [Required(ErrorMessage="Falta Correo electrónico")]
        public string CorreoElectronico { get; set; }

        public string SexoIdListaValor { get; set; }

        [Required(ErrorMessage = "Falta el Tipo de Documento")]
        public string TipoDocumentoIdListaValor { get; set; }

        [Required(ErrorMessage = "Falta Rol")]
        public string RolIdListaValor { get; set; }

        [Required(ErrorMessage = "Falta Estado")]
        public string EstadoUsuarioIdListaValor { get; set; }

        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$", ErrorMessage = "Contraseña no válida")]
        [Required(ErrorMessage = "Falta la Contraseña")]        
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
