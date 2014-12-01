using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNEmpresaLocacion
    {
        ADEmpresaLocacion adEmpresaLocacion = new ADEmpresaLocacion();

        public List<EmpresaLocacion> ObtenerLocaciones(int idEmpresa)
        {
            List<EmpresaLocacion> locaciones = new List<EmpresaLocacion>();

            DataTable dtResultado = adEmpresaLocacion.ObtenerLocaciones(idEmpresa);

            foreach(DataRow fila in dtResultado.Rows)
            {
                EmpresaLocacion locacion = new EmpresaLocacion();
                locacion.IdEmpresaLocacion = Convert.ToInt32(fila["IdEmpresaLocacion"]);
                locacion.IdEmpresa = Convert.ToInt32(fila["IdEmpresa"]);
                locacion.TipoLocacion.Valor = Convert.ToString(fila["TipoLocacion"]);
                locacion.NombreLocacion = Convert.ToString(fila["NombreLocacion"]);
                locacion.CorreoElectronico = Convert.ToString(fila["CorreoElectronico"]);
                locacion.TelefonoFijo = Convert.ToString(fila["TelefonoFijo"]);
                locacion.DireccionLinea1 = Convert.ToString(fila["DireccionLinea1"]);
                locacion.DireccionLinea2 = Convert.ToString(fila["DireccionLinea2"]);
                locacion.DireccionLinea3 = Convert.ToString(fila["DireccionLinea3"]);
                locacion.DireccionDistrito = Convert.ToString(fila["DireccionDistrito"]);
                locacion.DireccionCiudad = Convert.ToString(fila["DireccionCiudad"]);
                locacion.DireccionRegion = Convert.ToString(fila["DireccionRegion"]);
                locacion.EstadoLocacion.Valor = Convert.ToString(fila["EstadoLocacion"]);

                locaciones.Add(locacion);
            }
         
            return locaciones;
        }
        
    }
}
