using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;

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
                locacion.CorreoElectronico = Convert.ToString(fila["CorreoElectronico"] == System.DBNull.Value ? null : fila["CorreoElectronico"]);
                locacion.TelefonoFijo = Convert.ToString(fila["TelefonoFijo"] == System.DBNull.Value ? null : fila["TelefonoFijo"]);
                locacion.Direccion = Convert.ToString(fila["Direccion"]);                
                locacion.DireccionDistrito = Convert.ToString(fila["DireccionDistrito"]);
                locacion.DireccionCiudad = Convert.ToString(fila["DireccionCiudad"]);
                locacion.DireccionDepartamento = Convert.ToString(fila["DireccionDepartamento"]);
                locacion.EstadoLocacion.Valor = Convert.ToString(fila["EstadoLocacion"]);

                locaciones.Add(locacion);
            }
         
            return locaciones;
        }

        public EmpresaLocacion ObtenerLocacionPorId(int idEmpresaLocacion)
        {            
            DataTable dtResultado = adEmpresaLocacion.ObtenerLocacionPorId(idEmpresaLocacion);

            EmpresaLocacion locacion = new EmpresaLocacion();

            foreach (DataRow fila in dtResultado.Rows)
            {                
                locacion.IdEmpresaLocacion = Convert.ToInt32(fila["IdEmpresaLocacion"]);
                locacion.IdEmpresa = Convert.ToInt32(fila["IdEmpresa"]);
                locacion.TipoLocacionIdListaValor = Convert.ToString(fila["TipoLocacion"]);
                locacion.NombreLocacion = Convert.ToString(fila["NombreLocacion"]);
                locacion.CorreoElectronico = Convert.ToString(fila["CorreoElectronico"] == System.DBNull.Value ? null : fila["CorreoElectronico"]);
                locacion.TelefonoFijo = Convert.ToString(fila["TelefonoFijo"] == System.DBNull.Value ? null : fila["TelefonoFijo"]);
                locacion.Direccion = Convert.ToString(fila["Direccion"]);
                locacion.DireccionDistrito = Convert.ToString(fila["DireccionDistrito"]);
                locacion.DireccionCiudad = Convert.ToString(fila["DireccionCiudad"]);
                locacion.DireccionDepartamento = Convert.ToString(fila["DireccionDepartamento"]);
                locacion.DireccionDistritoId = Convert.ToString(fila["DireccionDistritoId"]);
                locacion.DireccionCiudadId = Convert.ToString(fila["DireccionCiudadId"]);
                locacion.DireccionDepartamentoId = Convert.ToString(fila["DireccionDepartamentoId"]);
                locacion.EstadoLocacionIdListaValor = Convert.ToString(fila["EstadoLocacion"]);

                break; //Sólo existe un registro.
            }

            return locacion;
        }

        public void Insertar(EmpresaLocacion empresaLocacion)
        {
            //if (empresaLocacion.CorreoElectronico == null) empresaLocacion.CorreoElectronico = "";
            //if (empresaLocacion.TelefonoFijo == null) empresaLocacion.TelefonoFijo = "";

            adEmpresaLocacion.Insertar(empresaLocacion);
        }

        public void Actualizar(EmpresaLocacion empresaLocacion)
        {
            //if (empresaLocacion.CorreoElectronico == null) empresaLocacion.CorreoElectronico = "";
            //if (empresaLocacion.TelefonoFijo == null) empresaLocacion.TelefonoFijo = "";

            adEmpresaLocacion.Actualizar(empresaLocacion);
        }

        public List<VistaEmpresaLocacion> ObtenerLocacionesPorIdEmpresa(int idEmpresa)
        {
            List<VistaEmpresaLocacion> lista = new List<VistaEmpresaLocacion>();

            DataTable dtResultado = new DataTable();

            dtResultado = adEmpresaLocacion.ObtenerLocacionesPorIdEmpresa(idEmpresa);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaEmpresaLocacion vista = new VistaEmpresaLocacion();

                vista.IdEmpresaLocacion = Convert.ToInt32(fila["IdEmpresaLocacion"]);
                vista.Idempresa = Convert.ToInt32(fila["IdEmpresa"]);
                vista.NombreLocacion = Convert.ToString(fila["NombreLocacion"]);
                vista.NombreTipoLocacion = Convert.ToString(fila["TipoLocacion"]);
                vista.NombreDistrito = Convert.ToString(fila["DireccionDistrito"]);
                vista.NombreTipoLocacion = Convert.ToString(fila["TipoLocacionDescripcion"]);
                vista.NombreEstado = Convert.ToString(fila["EstadoLocacion"]);
                vista.NombreEstadoLocacionDescripcion = Convert.ToString(fila["EstadoLocacionDescripcion"]);

                lista.Add(vista);
            }

            return lista;
        }


    }
}
