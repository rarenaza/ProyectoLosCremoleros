using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class EmpresaLocacion : Auditoria
    {
        public int IdEmpresaLocacion { get; set; }
        public int IdEmpresa { get; set; }
        public ListaValor TipoLocacion { get; set; }

       
        public string CorreoElectronico { get; set; }
        public string TelefonoFijo { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string DireccionDistrito { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string DireccionCiudad { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string DireccionDepartamento { get; set; }
        public ListaValor EstadoLocacion { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string TipoLocacionIdListaValor { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string NombreLocacion { get; set; }

        [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string EstadoLocacionIdListaValor { get; set; }

        public EmpresaLocacion() {
            TipoLocacion = new ListaValor();
            EstadoLocacion = new ListaValor();
        }

    }
}
