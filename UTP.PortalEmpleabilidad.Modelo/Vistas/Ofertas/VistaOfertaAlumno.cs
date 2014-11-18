using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas
{
    public class VistaOfertaAlumno
    {

        public DateTime FechaPublicacion { get; set; }

        public string  Empresa { get; set; }


        public string TipoTrabajo { get; set; }

        public int IdOferta { get; set; }
        public int IdEmpresa { get; set; }
        public string UsuarioPropietarioEmpresa { get; set; }
        public string EstadoOferta { get; set; }
        
        public DateTime FechaFinRecepcionCV { get; set; }
        public DateTime FechaFinProceso { get; set; }
        public int IdEmpresaLocacion { get; set; }
        public string DescripcionOferta { get; set; }
        public string TipoTrabajo { get; set; }
        public string TipoContrato { get; set; }
        public int DuracionContrato { get; set; }
        public string TipoCargo { get; set; }
  

        public decimal RemuneracionOfrecida { get; set; }


        public DateTime FechaInicioLabores { get; set; }

        public DateTime Horario { get; set; }
        public string AreaEmpresa { get; set; }
        public int NumeroVacantes { get; set; }
        public int RequiereExperienciaLaboral { get; set; }
        public string GradoMinimoEstudio { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }





    }
}
