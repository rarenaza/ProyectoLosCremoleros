using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa
{
    public class VistaRegistroEmpresa
    {
        //Datos de Empresa

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(100)]
        public string NombreComercial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(200)]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Este campo sólo acepta 11 números.")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Este campo sólo acepta números.")]
        public string IdentificadorTributario { get; set; }

        [StringLength(500, ErrorMessage = "Este campo sólo acepta 500 caracteres.")]
        [DataType(DataType.MultilineText)]
        public string DescripcionEmpresa { get; set; }

        public string LinkVideo { get; set; }
        public int AnoCreacion { get; set; }
        public string CreadoPor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string PaisIdListaValor { get; set; }
        public string NumeroEmpleadosIdListaValor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string SectorEmpresarial1IdListaValor { get; set; }
        public string SectorEmpresarial2IdListaValor { get; set; }
        public string SectorEmpresarial3IdListaValor { get; set; }    
        public string EstadoIdListaValor { get; set; }

        //Locación

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string TipoLocacionIdListaValor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string NombreLocacion { get; set; }
        public string EmailLocacion { get; set; }
        public string TelefonoLocacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string DireccionDepartamentoLocacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string DireccionCiudadLocacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string DireccionDistritoLocacion { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string DireccionLocacion { get; set; }

        public string EstadoLocacionIdListaValor { get; set; }
        
        //Usuario

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string NombresUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string ApellidosUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string CuentaUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string TipoDocumentoIdListaValor { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string NumeroDocumento { get; set; }

        public string SexoIdListaValor { get; set; }
        public string TelefonoFijoUsuario { get; set; }
        public string AnexoUsuario { get; set; }
        public string CelularUsuario { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string EmailUsuario { get; set; }
        public string EstadoUsuarioIdListaValor { get; set; }
        public string RolIdListaValor { get; set; }
    }
}
