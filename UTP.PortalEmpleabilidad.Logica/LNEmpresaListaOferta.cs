using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Logica
{
  public   class LNEmpresaListaOferta
    {
      ADEmpresaListaOferta adEmpresa = new ADEmpresaListaOferta();
      public List<VistaEmpresListarOfertas> ObtenerEmpresaListaOfertas()
      {
          List<VistaEmpresListarOfertas> listaEmpresa = new List<VistaEmpresListarOfertas>();

          DataTable dtResultado = adEmpresa.Empresa_ListaOfertas();

          for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
          {
              VistaEmpresListarOfertas vista = new VistaEmpresListarOfertas();


              vista.NombreComercial = dtResultado.Rows[i]["Nombre"].ToString();
              vista.RazonSocial = dtResultado.Rows[i]["Razon"].ToString();
              vista.RUC = dtResultado.Rows[i]["RUC"].ToString();
              vista.Estado = dtResultado.Rows[i]["Estado"].ToString();
              vista.SectorEmpresarial = dtResultado.Rows[i]["SectorEmpresarial"].ToString();
              vista.Ofertas = dtResultado.Rows[i]["Ofertas"].ToString();

              listaEmpresa.Add(vista);
          }
          return listaEmpresa;
      }
    }
}
