using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using System.Data;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOfertaSectorEmpresarial
    {
        ADOfertaSectorEmpresarial adOfertaSectorEmpresarial = new ADOfertaSectorEmpresarial();

        public List<OfertaSectorEmpresarial> ObtenerSectoresEmpresariales(int idOferta)
        {
            List<OfertaSectorEmpresarial> lista = new List<OfertaSectorEmpresarial>();

            DataTable dtResultado = adOfertaSectorEmpresarial.ObtenerSectoresEmpresariales(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaSectorEmpresarial sector = new OfertaSectorEmpresarial();
                sector.IdOfertaSectorEmpresarial = Convert.ToInt32(fila["IdOfertaSectorEmpresarial"]);
                sector.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                sector.SectorEmpresarial = Convert.ToString(fila["SectorEmpresarial"]);
                sector.ExperienciaExcluyente = Convert.ToBoolean(fila["ExperienciaExcluyente"]);
                sector.AniosTrabajados = Convert.ToInt32(fila["AniosTrabajados"]);
                sector.EstadoOfertaSectorEmpresarial = Convert.ToString(fila["EstadoOfertaSectorEmpresarial"]);
                sector.CreadorPor = Convert.ToString(fila["CreadorPor"]);
            }

            return lista;
        }
    }
}
