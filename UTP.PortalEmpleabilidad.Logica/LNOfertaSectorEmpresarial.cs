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

        public List<OfertaSectorEmpresarial> ObtenerSectoresEmpresariales(int idOferta, int idOfertaSectorEmpresarial)
        {
            List<OfertaSectorEmpresarial> lista = new List<OfertaSectorEmpresarial>();

            DataTable dtResultado = adOfertaSectorEmpresarial.ObtenerSectoresEmpresariales(idOferta, idOfertaSectorEmpresarial);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaSectorEmpresarial sector = new OfertaSectorEmpresarial();
                sector.IdOfertaSectorEmpresarial = Convert.ToInt32(fila["IdOfertaSectorEmpresarial"]);
                sector.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                sector.SectorEmpresarial.Valor = Convert.ToString(fila["SectorEmpresarialDescripcion"]);
                sector.SectorEmpresarialIdListaValor = Convert.ToString(fila["SectorEmpresarial"]);
                sector.ExperienciaExcluyente = Convert.ToBoolean(fila["ExperienciaExcluyente"]);
                sector.AniosTrabajados = Convert.ToInt32(fila["AniosTrabajados"] == System.DBNull.Value ? null : fila["AniosTrabajados"]);
                sector.EstadoOfertaSectorEmpresarial.Valor = Convert.ToString(fila["EstadoOfertaSectorEmpresarialDescripcion"]);
                sector.CreadoPor = Convert.ToString(fila["CreadoPor"]);

                lista.Add(sector);
            }

            return lista;
        }

        public void Insertar(OfertaSectorEmpresarial ofertaSector)
        {
            adOfertaSectorEmpresarial.Insertar(ofertaSector);
        }

        public void Actualizar(OfertaSectorEmpresarial ofertaSector)
        {
            adOfertaSectorEmpresarial.Actualizar(ofertaSector);
        }

        public void Eliminar(int idOfertaSector)
        {
            adOfertaSectorEmpresarial.Eliminar(idOfertaSector);
        }
    }
}
