using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNAlumnoExperiencia
    {
        ADEmpresa adempresa = new ADEmpresa();
        ADAlumnoExperiencia adalumnoexperiencia = new ADAlumnoExperiencia();
        ADAlumnoExperienciaCargo adalumnoexpcargo = new ADAlumnoExperienciaCargo();

        public void Registrar(AlumnoExperiencia alumnoexperiencia)
        {
            if (alumnoexperiencia.SectorEmpresarial2 == null) alumnoexperiencia.SectorEmpresarial2 = "";
            if (alumnoexperiencia.SectorEmpresarial3 == null) alumnoexperiencia.SectorEmpresarial3 = "";

            if (alumnoexperiencia.IdEmpresa == 0)
            {
                Empresa empresa = new Empresa();
                empresa.NombreComercial = alumnoexperiencia.Empresa;
                empresa.PaisIdListaValor = alumnoexperiencia.Pais;
                empresa.DescripcionEmpresa = alumnoexperiencia.DescripcionEmpresa;
                empresa.SectorEmpresarial1IdListaValor = alumnoexperiencia.SectorEmpresarial;
                empresa.SectorEmpresarial2IdListaValor = alumnoexperiencia.SectorEmpresarial2;
                empresa.SectorEmpresarial3IdListaValor = alumnoexperiencia.SectorEmpresarial3;
                empresa.CreadoPor = alumnoexperiencia.CreadoPor;
                int IdEmpresa = adempresa.Registrar(empresa);
                if (IdEmpresa>0)
                {
                    alumnoexperiencia.IdEmpresa=IdEmpresa;
                }
            }
            int IdExperiencia=adalumnoexperiencia.ValidarExistePorIdEmpresa(alumnoexperiencia.IdEmpresa);
            if (IdExperiencia == 0)
            {
                alumnoexperiencia.IdExperiencia = adalumnoexperiencia.Registrar(alumnoexperiencia);
            }
            AlumnoExperienciaCargo alumnoexperienciacargo=new AlumnoExperienciaCargo();
            alumnoexperienciacargo.IdExperiencia = alumnoexperiencia.IdExperiencia;
            alumnoexperienciacargo.NombreCargo = alumnoexperiencia.NombreCargo;
            alumnoexperienciacargo.FechaInicioCargoMes = alumnoexperiencia.FechaInicioCargoMes;
            alumnoexperienciacargo.FechaInicioCargoAno = (int)alumnoexperiencia.FechaInicioCargoAno;
            alumnoexperienciacargo.FechaFinCargoMes = alumnoexperiencia.FechaFinCargoMes;
            alumnoexperienciacargo.FechaFinCargoAno = (int)alumnoexperiencia.FechaFinCargoAno;
            alumnoexperienciacargo.TipoCargo = alumnoexperiencia.TipoCargo;
            alumnoexperienciacargo.DescripcionCargo = alumnoexperiencia.DescripcionCargo;
            alumnoexperienciacargo.CreadoPor = alumnoexperiencia.CreadoPor;
            adalumnoexpcargo.Registrar(alumnoexperienciacargo);


        }

    }
}
