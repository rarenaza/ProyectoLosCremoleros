using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string NombreComercial { get; set; }
        public string RazonSocial { get; set; }
        public ListaValor Pais { get; set; }
        public string IdentificadorTributario { get; set; }
        public string DescripcionEmpresa { get; set; }
        public string PresentacionEmpresa { get; set; }
        public string LinkVideo { get; set; }
        public int AnoCreacion { get; set; }
        public ListaValor NumeroEmpleados { get; set; }        
        public ListaValor EstadoEmpresa { get; set; }
        public ListaValor SectorEmpresarial { get; set; }
        public ListaValor SectorEmpresarial2 { get; set; }
        public ListaValor SectorEmpresarial3 { get; set; }
        public string CreadoPor { get; set; }
        public string UsuarioEC { get; set; }

        public List<EmpresaLocacion> Locaciones { get; set; }
        public List<EmpresaUsuario> Usuarios { get; set; }

        public Empresa()
        {
            Pais = new ListaValor();
            SectorEmpresarial = new ListaValor();
            SectorEmpresarial2 = new ListaValor();
            SectorEmpresarial3 = new ListaValor();
            NumeroEmpleados = new ListaValor();
            EstadoEmpresa = new ListaValor();
            Locaciones = new List<EmpresaLocacion>();
            Usuarios = new List<EmpresaUsuario>();
        }

    }
}
