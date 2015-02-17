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

        [Required(ErrorMessage = "Falta Dirección")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Falta Distrito")]
        public string DireccionDistrito { get; set; }

        [Required(ErrorMessage = "Falta Ciudad")]
        public string DireccionCiudad { get; set; }

        [Required(ErrorMessage = "Falta Departamento")]
        public string DireccionDepartamento { get; set; }
        public ListaValor EstadoLocacion { get; set; }

        [Required(ErrorMessage = "Falta Tipo de Ubicación")]
        public string TipoLocacionIdListaValor { get; set; }

        [Required(ErrorMessage = "Falta Nombre de Ubicación")]
        public string NombreLocacion { get; set; }

        [Required(ErrorMessage = "Falta Estado de Ubicación")]
        public string EstadoLocacionIdListaValor { get; set; }

        public string TextoDepartamento { get; set; }
        public string TextoCiudad { get; set; }
        public string TextDistrito { get; set; }

  


        public EmpresaLocacion() {
            TipoLocacion = new ListaValor();
            EstadoLocacion = new ListaValor();
        }

    }
}
