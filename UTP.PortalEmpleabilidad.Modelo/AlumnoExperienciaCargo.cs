using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class AlumnoExperienciaCargo
    {
        public int IdExperiencia { get; set; }
        public int IdExperienciaCargo { get; set; }
        public string NombreComercial { get; set; }
        public string NombreCargo { get; set; }
        public string DesTipoCargo { get; set; }
        public string DescripcionCargo { get; set; }
        public string SectorEmpresarial { get; set; }
        public string NomPais { get; set; }
        public string Ciudad { get; set; }
        public int FechaInicioCargoMes { get; set; }
        public int FechaInicioCargoAno { get; set; }
        public int? FechaFinCargoMes { get; set; }
        public int? FechaFinCargoAno { get; set; }
        public bool Incluir { get; set; }

        public List<ListaValor> ListaSectorEmpresarial { get; set; }
        public List<ListaValor> ListaPais { get; set; }
        public List<ListaValor> ListaTipoCargo { get; set; }
        public string TipoCargo { get; set; }
        public string CreadoPor { get; set; }
        public string Usuario { get; set; }
        public string Empresa { get; set; }
        public string DescripcionEmpresa { get; set; }
        public int IdEmpresa { get; set; }
        public string SectorEmpresarial2 { get; set; }
        public string SectorEmpresarial3 { get; set; }
        public string Pais { get; set; }
        public string IconoTipoCargo { get; set; }

    }
}
