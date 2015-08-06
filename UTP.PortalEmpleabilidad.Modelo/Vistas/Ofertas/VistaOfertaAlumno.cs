using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
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

        public string EstadoOferta { get; set; }
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
        public string TipoTrabajoUTP { get; set; }
        public int IdAlumno { get; set; }

        public Dictionary<int, string> PeriodoPublicacion { get; set; }
        public string AreaEmpresa { get; set; }
        public int? AniosExperiencia { get; set; }
        [RegularExpression(@"[0-9]+", ErrorMessage = "Este campo sólo acepta numeros.")]
        public int? Sueldo { get; set; }
        public string Ubicacion { get; set; }
        public bool IncluirMensaje { get; set; }
        public int? IdPeriodoPublicacion { get; set; }
        public string PalabraClave { get; set; }
        public List<Oferta> ListaOfertas { get; set; }   

        public List<AlumnoCV> ListaAlumnoCV { get; set; }
        public Oferta Oferta { get; set; }
        public decimal Compatible { get; set; }

        public int PaginaActual { get; set; }
        public int NumeroRegistros { get; set; }
        public int MaxPagina { get; set; }
        //public int ExperienciaGeneral { get; set; }
        //public int ExperienciaPosicionesSimilares { get; set; }
        public List<Oferta> ListadoEstudios { get; set; }
        public List<Oferta> ListadoSectorEmpresarial { get; set; }
        public List<Oferta> ListadoInformacionAdicional { get; set; }

        //06MAR15: Se agregan estas dos propiedades para obtener el lista de estudios de UTP y otros estudios.
        public List<OfertaEstudio> ListaEstudiosOtros { get; set; }
        public List<OfertaEstudio> ListaEstudiosUTP { get; set; }

    }
}
