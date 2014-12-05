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

        public string CargoOfrecido { get; set; }
        

        public string TipoTrabajo { get; set; }
        public string Horario { get; set; }      
        public decimal RemuneracionOfrecida { get; set; }
        public string  EstadoOferta { get; set; }
        public List<ListaValor> ListaEstudios { get; set; }
        public List<ListaValor> ListaEstadoEstudio { get; set; }
        public List<ListaValor> ListaSectorEmpresarial { get; set; }
        public List<ListaValor> ListaTipoTrabajo { get; set; }
        public List<ListaValor> ListaContrato { get; set; }
        public List<ListaValor> ListaTipoCargo { get; set; }

        public string IdEstudio { get; set; }
        public string IdEstadoEstudio { get; set; }
        public string IdSectorEmpresarial { get; set; }
        public string IdTipoTrabajo { get; set; }
        public string IdContrato { get; set; }
        public string IdTipoCargo { get; set; }
        public int IdAlumno { get; set; }

        public Dictionary<int, string> PeriodoPublicacion { get; set; }
        public string AreaEmpresa { get; set; }
        public int? AniosExperiencia { get; set; }
        public int? Sueldo { get; set; }
        public string Ubicacion { get; set; }
        public bool IncluirMensaje { get; set; }
        public bool IdPeriodoPublicacion { get; set; }
        public string DescripcionOferta { get; set; }
        public List<Oferta> ListaOfertas { get; set; }





    }
}
