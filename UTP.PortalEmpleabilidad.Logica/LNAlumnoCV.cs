using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNAlumnoCV
    {
        ADAlumnoCV acv = new ADAlumnoCV();

        public VistaOfertaPostulante ObtenerPostulanteCV(int IdCV)
        {
            VistaOfertaPostulante vistaofertapostulante = new VistaOfertaPostulante();

            DataSet dsResultado = acv.ObtenerPostulanteCV(IdCV);
            Alumno alumnocv = new Alumno();
            List<AlumnoEstudio> alumnoestudiocv =new List<AlumnoEstudio>();
            List<AlumnoExperiencia> alumnoexperienciacv = new List<AlumnoExperiencia>();
            List<AlumnoInformacionAdicional> alumnoinformacionadicionalcv = new List<AlumnoInformacionAdicional>();
            List<AlumnoPostulaciones> alumnopostulaciones = new List<AlumnoPostulaciones>();

            if (dsResultado.Tables.Count > 0)
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    for(int n=0;n<=dsResultado.Tables[0].Rows.Count -1;n++){
                        alumnocv.IdOfertaPostulante = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdOfertaPostulante"]);
                        alumnocv.IdCV = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdCV"]);
                        alumnocv.Perfil = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Perfil"]);
                        alumnocv.CodAlumnoUTP = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CodAlumnoUtp"]);
                        alumnocv.Nombres = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Nombres"]);
                        alumnocv.Apellidos = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Apellidos"]);
                        alumnocv.TelefonoCelular = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TelefonoCelular"]);
                        alumnocv.CorreoElectronico1 = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CorreoElectronico"]);
                        alumnocv.CorreoElectronico2 = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CorreoElectronico2"]);
                        alumnocv.TelefonoFijoCasa = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TelefonoFijoCasa"]);
                        alumnocv.Direccion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Direccion"]);
                        alumnocv.DireccionRegion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DireccionRegion"]);
                        alumnocv.DireccionCiudad = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DireccionCiudad"]);
                        alumnocv.DireccionDistrito = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DireccionDistrito"]);
                        alumnocv.Foto = Funciones.ToBytes(dsResultado.Tables[0].Rows[n]["Foto"]);
                        alumnocv.IdAlumno = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdAlumno"]);
                        alumnocv.IdOferta = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdOferta"]);
			            alumnocv.FaseOferta = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FaseOferta"]);
                        alumnocv.FechaPostulacion = Funciones.ToDateTime(dsResultado.Tables[0].Rows[n]["FechaPostulacion"]);
                        alumnocv.CargoOfrecido = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CargoOfrecido"]);
                        alumnocv.FaseOfertaDescripcion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FaseOfertaDescripcion"]);
                        alumnocv.Cumplimiento = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["Cumplimiento"]);
                        break;
                    }
                }
                if (dsResultado.Tables[1].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[1].Rows.Count - 1; n++)
                    {
                        AlumnoEstudio alumnoestudio = new AlumnoEstudio();
                        alumnoestudio.Institucion = Funciones.ToString(dsResultado.Tables[1].Rows[n]["Institucion"]);
                        alumnoestudio.Estudio = Funciones.ToString(dsResultado.Tables[1].Rows[n]["Estudio"]);
                        alumnoestudio.TipoDeEstudio = Funciones.ToString(dsResultado.Tables[1].Rows[n]["TipoDeEstudio"]);
                        alumnoestudio.EstadoDelEstudio = Funciones.ToString(dsResultado.Tables[1].Rows[n]["EstadoDelEstudio"]);
                        alumnoestudio.Observacion = Funciones.ToString(dsResultado.Tables[1].Rows[n]["Observacion"]);
                        alumnoestudio.FechaInicioMes = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioMes"]);
                        alumnoestudio.FechaInicioAno = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioAno"]);
                        alumnoestudio.FechaFinMes = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinMes"]);
                        alumnoestudio.FechaFinAno = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinAno"]);
                        alumnoestudio.CicloEquivalente = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["CicloEquivalente"]);
                        alumnoestudio.Cumple = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["Cumple"]);
                        alumnoestudiocv.Add(alumnoestudio);
                    }
                }
                if (dsResultado.Tables[2].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[2].Rows.Count - 1; n++)
                    {
                        AlumnoExperiencia alumnoexperiencia = new AlumnoExperiencia();
                        alumnoexperiencia.Empresa = Funciones.ToString(dsResultado.Tables[2].Rows[n]["Empresa"]);
                        alumnoexperiencia.DescripcionEmpresa = Funciones.ToString(dsResultado.Tables[2].Rows[n]["DescripcionEmpresa"]);
                        alumnoexperiencia.SectorEmpresarial = Funciones.ToString(dsResultado.Tables[2].Rows[n]["SectorEmpresarial"]);
                        alumnoexperiencia.SectorEmpresarial2 = Funciones.ToString(dsResultado.Tables[2].Rows[n]["SectorEmpresarial2"]);
                        alumnoexperiencia.SectorEmpresarial3 = Funciones.ToString(dsResultado.Tables[2].Rows[n]["SectorEmpresarial3"]);
                        alumnoexperiencia.Pais = Funciones.ToString(dsResultado.Tables[2].Rows[n]["Pais"]);
                        alumnoexperiencia.Ciudad = Funciones.ToString(dsResultado.Tables[2].Rows[n]["Ciudad"]);
                        alumnoexperiencia.NombreCargo = Funciones.ToString(dsResultado.Tables[2].Rows[n]["NombreCargo"]);
                        alumnoexperiencia.FechaInicioCargoMes = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaInicioCargoMes"]);
                        alumnoexperiencia.FechaInicioCargoAno = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaInicioCargoAno"]);
                        alumnoexperiencia.FechaFinCargoMes = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaFinCargoMes"]);
                        alumnoexperiencia.FechaFinCargoAno = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaFinCargoAno"]);
                        alumnoexperiencia.TipoCargo = Funciones.ToString(dsResultado.Tables[2].Rows[n]["TipoCargo"]);
                        alumnoexperiencia.DescripcionCargo = Funciones.ToString(dsResultado.Tables[2].Rows[n]["DescripcionCargo"]).Replace("\n", "<br>");
                        alumnoexperiencia.Cumple = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["Cumple"]);
                        alumnoexperienciacv.Add(alumnoexperiencia);
                    }
                }
                if (dsResultado.Tables[3].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[3].Rows.Count - 1; n++)
                    {
                        AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();
                        alumnoinformacionadicional.DesTipoConocimiento = Funciones.ToString(dsResultado.Tables[3].Rows[n]["TipoConocimiento"]);
                        alumnoinformacionadicional.Conocimiento = Funciones.ToString(dsResultado.Tables[3].Rows[n]["Conocimiento"]);
                        alumnoinformacionadicional.DesNivelConocimiento = Funciones.ToString(dsResultado.Tables[3].Rows[n]["NivelConocimiento"]);
                        alumnoinformacionadicional.FechaConocimientoDesdeMes = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoDesdeMes"]);
                        alumnoinformacionadicional.FechaConocimientoDesdeAno = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoDesdeAno"]);
                        alumnoinformacionadicional.FechaConocimientoHastaMes = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoHastaMes"]);
                        alumnoinformacionadicional.FechaConocimientoHastaAno = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoHastaAno"]);
                        alumnoinformacionadicional.NomPais = Funciones.ToString(dsResultado.Tables[3].Rows[n]["Pais"]);
                        alumnoinformacionadicional.Ciudad = Funciones.ToString(dsResultado.Tables[3].Rows[n]["Ciudad"]);
                        alumnoinformacionadicional.InstituciónDeEstudio = Funciones.ToString(dsResultado.Tables[3].Rows[n]["InstituciónDeEstudio"]);
                        alumnoinformacionadicional.AnosExperiencia = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["AñosExperiencia"]);
                        alumnoinformacionadicional.Cumple = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["Cumple"]);
                        
                        alumnoinformacionadicionalcv.Add(alumnoinformacionadicional);
                    }
                }
                if (dsResultado.Tables[4].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[4].Rows.Count - 1; n++)
                    {
                        AlumnoPostulaciones alumnopostulacionesdata = new AlumnoPostulaciones();
                        alumnopostulacionesdata.IdOferta = Funciones.ToInt(dsResultado.Tables[4].Rows[n]["IdOferta"]);
                        alumnopostulacionesdata.CargoOfrecido = Funciones.ToString(dsResultado.Tables[4].Rows[n]["CargoOfrecido"]);
                        alumnopostulacionesdata.FechaPostulacion = Funciones.ToDateTime(dsResultado.Tables[4].Rows[n]["FechaPostulacion"]);
                        alumnopostulacionesdata.IdOfertaPostulante = Funciones.ToInt(dsResultado.Tables[4].Rows[n]["IdOfertaPostulante"]);

                        alumnopostulaciones.Add(alumnopostulacionesdata);
                    }
                }

            }
            vistaofertapostulante.alumnocv = alumnocv;
            vistaofertapostulante.alumnoestudiocv = alumnoestudiocv;
            vistaofertapostulante.alumnoexperienciacv = alumnoexperienciacv;
            vistaofertapostulante.alumnoinformacionadicionalcv = alumnoinformacionadicionalcv;
            vistaofertapostulante.alumnopostulacionesdata = alumnopostulaciones;
            return vistaofertapostulante;
        }
        public AlumnoCV ObtenerAlumnoCVPorIdAlumnoYIdCV(int IdAlumno, int IdCV)
        {
            AlumnoCV alumnocv = new AlumnoCV();

            DataTable dtResultado = acv.ObtenerAlumnoCVPorIdAlumnoYIdCV(IdAlumno, IdCV);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    alumnocv.IncluirCorreoElectronico2 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirCorreoElectronico2"]);
                    alumnocv.IncluirFoto = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirFoto"]);
                    alumnocv.IncluirTelefonoFijo = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirTelefonoFijo"]);
                    alumnocv.IncluirDireccion = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirDireccion"]);
                    alumnocv.IncluirNombre1 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre1"]);
                    alumnocv.IncluirNombre2 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre2"]);
                    alumnocv.IncluirNombre3 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre3"]);
                    alumnocv.IncluirNombre4 = Funciones.ToBoolean(dtResultado.Rows[i]["IncluirNombre4"]);

                    alumnocv.Perfil = Funciones.ToString(dtResultado.Rows[i]["Perfil"]);

                }
            }

            return alumnocv;
        }


        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumno(int IdAlumno)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();

            DataTable dtResultado = acv.ObtenerAlumnoCVPorIdAlumno(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoCV alumnocv = new AlumnoCV();
                    alumnocv.IdCV = int.Parse(dtResultado.Rows[i]["IdCV"].ToString());
                    alumnocv.NombreCV = dtResultado.Rows[i]["NombreCV"].ToString();
                    alumnocv.IdPlantillaCV = int.Parse(dtResultado.Rows[i]["IdPlantillaCV"].ToString());
                    alumnocv.PorcentajeCV = int.Parse(dtResultado.Rows[i]["PorcentajeCV"].ToString());
                    listaAlumnoCV.Add(alumnocv);
                }


            }

            return listaAlumnoCV;
        }
        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumnoCompleto(int IdAlumno)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();

            DataTable dtResultado = acv.ObtenerAlumnoCVPorIdAlumnoCompleto(IdAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    AlumnoCV alumnocv = new AlumnoCV();
                    alumnocv.IdCV = int.Parse(dtResultado.Rows[i]["IdCV"].ToString());
                    alumnocv.NombreCV = dtResultado.Rows[i]["NombreCV"].ToString();
                    alumnocv.IdPlantillaCV = int.Parse(dtResultado.Rows[i]["IdPlantillaCV"].ToString());
                    listaAlumnoCV.Add(alumnocv);
                }


            }

            return listaAlumnoCV;
        }
        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumno(AlumnoCV alumnocv)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();
            listaAlumnoCV = ObtenerAlumnoCVPorIdAlumno(alumnocv.IdAlumno);
            if (listaAlumnoCV == null || listaAlumnoCV.Count == 0)
            {
                ADAlumnoCV acv = new ADAlumnoCV();
                acv.Insertar(alumnocv);
                listaAlumnoCV = ObtenerAlumnoCVPorIdAlumno(alumnocv.IdAlumno);
            }
            return listaAlumnoCV;

        }
        public List<AlumnoCV> ObtenerAlumnoCVPorIdAlumnoCompleto(AlumnoCV alumnocv)
        {
            List<AlumnoCV> listaAlumnoCV = new List<AlumnoCV>();
            listaAlumnoCV = ObtenerAlumnoCVPorIdAlumnoCompleto(alumnocv.IdAlumno);
            if (listaAlumnoCV == null || listaAlumnoCV.Count == 0)
            {
                ADAlumnoCV acv = new ADAlumnoCV();
                acv.Insertar(alumnocv);
                listaAlumnoCV = ObtenerAlumnoCVPorIdAlumnoCompleto(alumnocv.IdAlumno);
            }
            return listaAlumnoCV;

        }
        public void UpdateInfo(AlumnoCV alumnocv, int PorcentajeCV)
        {
            ADAlumnoCVEstudio acve = new ADAlumnoCVEstudio();
            ADAlumnoCVExperienciaCargo acvs = new ADAlumnoCVExperienciaCargo();
            ADAlumnoCVInformacionAdicional acvc = new ADAlumnoCVInformacionAdicional();
            
            acve.DesactivarPorCV(alumnocv.IdCV);
            acvs.DesactivarPorCV(alumnocv.IdCV);
            acvc.DesactivarPorCV(alumnocv.IdCV);
            if (alumnocv.Estudios != null)
            {
                foreach (VistaAlumnoEstudio modelo in alumnocv.Estudios)
                {
                    acve.AgregarOrModificar(alumnocv.IdCV, modelo.IdEstudio, alumnocv.Usuario);
                }
            }

            if (alumnocv.Experiencias != null)
            {
                foreach (VistaAlumnoExperienciaCargo modelo in alumnocv.Experiencias)
                {
                    acvs.AgregarOrModificar(alumnocv.IdCV, modelo.IdExperienciaCargo, alumnocv.Usuario);
                }
            }

            if (alumnocv.Conocimientos != null)
            {
                foreach (VistaAlumnoConocimiento modelo in alumnocv.Conocimientos)
                {
                    acvc.AgregarOrModificar(alumnocv.IdCV, modelo.IdInformacionAdicional, alumnocv.Usuario);
                }
            }

            acv.Update(alumnocv, PorcentajeCV);

        }

        public bool RegistrarCV(ref AlumnoCV alumnocv)
        {
            bool existe = false;
            if (acv.ValidarExistencia(alumnocv.IdAlumno, alumnocv.NombreCV) == false)
            {
              existe=  acv.RegistrarCV(ref alumnocv);
            }
            return existe;
        }

        public VistaOfertaPostulante ObtenerDatosCV(int idAlumno)
        {
            VistaOfertaPostulante vistaofertapostulante = new VistaOfertaPostulante();

            DataSet dsResultado = acv.ObtenerDatosCV(idAlumno);
            Alumno alumnocv = new Alumno();
            List<AlumnoEstudio> alumnoestudiocv = new List<AlumnoEstudio>();
            List<AlumnoExperiencia> alumnoexperienciacv = new List<AlumnoExperiencia>();
            List<AlumnoInformacionAdicional> alumnoinformacionadicionalcv = new List<AlumnoInformacionAdicional>();
            List<AlumnoPostulaciones> alumnopostulaciones = new List<AlumnoPostulaciones>();

            if (dsResultado.Tables.Count > 0)
            {
                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[0].Rows.Count - 1; n++)
                    {
                        //alumnocv.IdOfertaPostulante = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdOfertaPostulante"]);
                        alumnocv.IdCV = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdCV"]);
                        alumnocv.Perfil = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Perfil"]);
                        alumnocv.CodAlumnoUTP = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CodAlumnoUtp"]);
                        alumnocv.Nombres = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Nombres"]);
                        alumnocv.Apellidos = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Apellidos"]);
                        alumnocv.TelefonoCelular = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TelefonoCelular"]);
                        alumnocv.CorreoElectronico1 = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CorreoElectronico"]);
                        alumnocv.CorreoElectronico2 = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CorreoElectronico2"]);
                        alumnocv.TelefonoFijoCasa = Funciones.ToString(dsResultado.Tables[0].Rows[n]["TelefonoFijoCasa"]);
                        alumnocv.Direccion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["Direccion"]);
                        alumnocv.DireccionRegion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DireccionRegion"]);
                        alumnocv.DireccionCiudad = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DireccionCiudad"]);
                        alumnocv.DireccionDistrito = Funciones.ToString(dsResultado.Tables[0].Rows[n]["DireccionDistrito"]);
                        alumnocv.Foto = Funciones.ToBytes(dsResultado.Tables[0].Rows[n]["Foto"]);
                        alumnocv.IdAlumno = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdAlumno"]);
                        //alumnocv.IdOferta = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["IdOferta"]);
                        //alumnocv.FaseOferta = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FaseOferta"]);
                        //alumnocv.FechaPostulacion = Funciones.ToDateTime(dsResultado.Tables[0].Rows[n]["FechaPostulacion"]);
                        //alumnocv.CargoOfrecido = Funciones.ToString(dsResultado.Tables[0].Rows[n]["CargoOfrecido"]);
                        //alumnocv.FaseOfertaDescripcion = Funciones.ToString(dsResultado.Tables[0].Rows[n]["FaseOfertaDescripcion"]);
                        //alumnocv.Cumplimiento = Funciones.ToInt(dsResultado.Tables[0].Rows[n]["Cumplimiento"]);
                        break;
                    }
                }
                if (dsResultado.Tables[1].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[1].Rows.Count - 1; n++)
                    {
                        AlumnoEstudio alumnoestudio = new AlumnoEstudio();
                        alumnoestudio.Institucion = Funciones.ToString(dsResultado.Tables[1].Rows[n]["Institucion"]);
                        alumnoestudio.Estudio = Funciones.ToString(dsResultado.Tables[1].Rows[n]["Estudio"]);
                        alumnoestudio.TipoDeEstudio = Funciones.ToString(dsResultado.Tables[1].Rows[n]["TipoDeEstudio"]);
                        alumnoestudio.EstadoDelEstudio = Funciones.ToString(dsResultado.Tables[1].Rows[n]["EstadoDelEstudio"]);
                        alumnoestudio.Observacion = Funciones.ToString(dsResultado.Tables[1].Rows[n]["Observacion"]);
                        alumnoestudio.FechaInicioMes = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioMes"]);
                        alumnoestudio.FechaInicioAno = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaInicioAno"]);
                        alumnoestudio.FechaFinMes = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinMes"]);
                        alumnoestudio.FechaFinAno = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["FechaFinAno"]);
                        alumnoestudio.CicloEquivalente = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["CicloEquivalente"]);
                        alumnoestudio.Cumple = Funciones.ToInt(dsResultado.Tables[1].Rows[n]["Cumple"]);
                        alumnoestudiocv.Add(alumnoestudio);
                    }
                }
                if (dsResultado.Tables[2].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[2].Rows.Count - 1; n++)
                    {
                        AlumnoExperiencia alumnoexperiencia = new AlumnoExperiencia();
                        alumnoexperiencia.Empresa = Funciones.ToString(dsResultado.Tables[2].Rows[n]["Empresa"]);
                        alumnoexperiencia.DescripcionEmpresa = Funciones.ToString(dsResultado.Tables[2].Rows[n]["DescripcionEmpresa"]);
                        alumnoexperiencia.SectorEmpresarial = Funciones.ToString(dsResultado.Tables[2].Rows[n]["SectorEmpresarial"]);
                        alumnoexperiencia.SectorEmpresarial2 = Funciones.ToString(dsResultado.Tables[2].Rows[n]["SectorEmpresarial2"]);
                        alumnoexperiencia.SectorEmpresarial3 = Funciones.ToString(dsResultado.Tables[2].Rows[n]["SectorEmpresarial3"]);
                        alumnoexperiencia.Pais = Funciones.ToString(dsResultado.Tables[2].Rows[n]["Pais"]);
                        alumnoexperiencia.Ciudad = Funciones.ToString(dsResultado.Tables[2].Rows[n]["Ciudad"]);
                        alumnoexperiencia.NombreCargo = Funciones.ToString(dsResultado.Tables[2].Rows[n]["NombreCargo"]);
                        alumnoexperiencia.FechaInicioCargoMes = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaInicioCargoMes"]);
                        alumnoexperiencia.FechaInicioCargoAno = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaInicioCargoAno"]);
                        alumnoexperiencia.FechaFinCargoMes = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaFinCargoMes"]);
                        alumnoexperiencia.FechaFinCargoAno = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["FechaFinCargoAno"]);
                        alumnoexperiencia.TipoCargo = Funciones.ToString(dsResultado.Tables[2].Rows[n]["TipoCargo"]);
                        alumnoexperiencia.DescripcionCargo = Funciones.ToString(dsResultado.Tables[2].Rows[n]["DescripcionCargo"]).Replace("\n", "<br>");
                        alumnoexperiencia.Cumple = Funciones.ToInt(dsResultado.Tables[2].Rows[n]["Cumple"]);
                        alumnoexperienciacv.Add(alumnoexperiencia);
                    }
                }
                if (dsResultado.Tables[3].Rows.Count > 0)
                {
                    for (int n = 0; n <= dsResultado.Tables[3].Rows.Count - 1; n++)
                    {
                        AlumnoInformacionAdicional alumnoinformacionadicional = new AlumnoInformacionAdicional();
                        alumnoinformacionadicional.DesTipoConocimiento = Funciones.ToString(dsResultado.Tables[3].Rows[n]["TipoConocimiento"]);
                        alumnoinformacionadicional.Conocimiento = Funciones.ToString(dsResultado.Tables[3].Rows[n]["Conocimiento"]);
                        alumnoinformacionadicional.DesNivelConocimiento = Funciones.ToString(dsResultado.Tables[3].Rows[n]["NivelConocimiento"]);
                        alumnoinformacionadicional.FechaConocimientoDesdeMes = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoDesdeMes"]);
                        alumnoinformacionadicional.FechaConocimientoDesdeAno = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoDesdeAno"]);
                        alumnoinformacionadicional.FechaConocimientoHastaMes = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoHastaMes"]);
                        alumnoinformacionadicional.FechaConocimientoHastaAno = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["FechaConocimientoHastaAno"]);
                        alumnoinformacionadicional.NomPais = Funciones.ToString(dsResultado.Tables[3].Rows[n]["Pais"]);
                        alumnoinformacionadicional.Ciudad = Funciones.ToString(dsResultado.Tables[3].Rows[n]["Ciudad"]);
                        alumnoinformacionadicional.InstituciónDeEstudio = Funciones.ToString(dsResultado.Tables[3].Rows[n]["InstituciónDeEstudio"]);
                        alumnoinformacionadicional.AnosExperiencia = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["AñosExperiencia"]);
                        alumnoinformacionadicional.Cumple = Funciones.ToInt(dsResultado.Tables[3].Rows[n]["Cumple"]);

                        alumnoinformacionadicionalcv.Add(alumnoinformacionadicional);
                    }
                }
                //if (dsResultado.Tables[4].Rows.Count > 0)
                //{
                //    for (int n = 0; n <= dsResultado.Tables[4].Rows.Count - 1; n++)
                //    {
                //        AlumnoPostulaciones alumnopostulacionesdata = new AlumnoPostulaciones();
                //        alumnopostulacionesdata.IdOferta = Funciones.ToInt(dsResultado.Tables[4].Rows[n]["IdOferta"]);
                //        alumnopostulacionesdata.CargoOfrecido = Funciones.ToString(dsResultado.Tables[4].Rows[n]["CargoOfrecido"]);
                //        alumnopostulacionesdata.FechaPostulacion = Funciones.ToDateTime(dsResultado.Tables[4].Rows[n]["FechaPostulacion"]);
                //        alumnopostulacionesdata.IdOfertaPostulante = Funciones.ToInt(dsResultado.Tables[4].Rows[n]["IdOfertaPostulante"]);

                //        alumnopostulaciones.Add(alumnopostulacionesdata);
                //    }
                //}

            }
            vistaofertapostulante.alumnocv = alumnocv;
            vistaofertapostulante.alumnoestudiocv = alumnoestudiocv;
            vistaofertapostulante.alumnoexperienciacv = alumnoexperienciacv;
            vistaofertapostulante.alumnoinformacionadicionalcv = alumnoinformacionadicionalcv;
            //vistaofertapostulante.alumnopostulacionesdata = alumnopostulaciones;
            return vistaofertapostulante;
        }
    }
}
