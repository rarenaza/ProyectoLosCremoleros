using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOferta
    {

        ADOferta adOferta = new ADOferta();
        LNAlumnoCV lnalumnocv = new LNAlumnoCV();

        private Oferta ObtenerOfertasAlumnoPorID(int idOferta, int idAlumno)
        {
            
            Oferta oferta = new Oferta();
            DataTable dtResultado = adOferta.ObtenerOfertasAlumnoPorID(idOferta, idAlumno);
            if (dtResultado.Rows.Count > 0)
            {
                oferta.NombreComercial = Funciones.ToString(dtResultado.Rows[0]["NombreComercial"]);
                oferta.SitioWeb = Funciones.ToString(dtResultado.Rows[0]["SitioWeb"]);
                oferta.DescripcionEmpresa = Funciones.ToString(dtResultado.Rows[0]["DescripcionEmpresa"]);
                oferta.DesNumeroEmpleados = Funciones.ToString(dtResultado.Rows[0]["DesNumeroEmpleados"]);
                oferta.CargoOfrecido = Funciones.ToString(dtResultado.Rows[0]["CargoOfrecido"]);
                oferta.Funciones = Funciones.ToString(dtResultado.Rows[0]["Funciones"]);
                oferta.Competencias = Funciones.ToString(dtResultado.Rows[0]["Competencias"]);
                oferta.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[0]["FechaPublicacion"]);
                oferta.FechaFinRecepcionCV = Funciones.ToDateTime(dtResultado.Rows[0]["FechaFinRecepcionCV"]);
                oferta.FechaFinProceso = Funciones.ToDateTime(dtResultado.Rows[0]["FechaFinProceso"]);
                oferta.DesTipoCargo = Funciones.ToString(dtResultado.Rows[0]["DesTipoCargo"]);
                oferta.DesTipoTrabajo = Funciones.ToString(dtResultado.Rows[0]["DesTipoTrabajo"]);
                oferta.DesTipoContrato = Funciones.ToString(dtResultado.Rows[0]["DesTipoContrato"]);
                oferta.DuracionContrato = Funciones.ToInt(dtResultado.Rows[0]["DuracionContrato"]);
                oferta.Horario = Funciones.ToString(dtResultado.Rows[0]["Horario"]);
                oferta.RemuneracionOfrecida = Funciones.ToDecimal(dtResultado.Rows[0]["RemuneracionOfrecida"]);
                oferta.AreaEmpresa = Funciones.ToString(dtResultado.Rows[0]["AreaEmpresa"]);
                oferta.NumeroVacantes = Funciones.ToInt(dtResultado.Rows[0]["NumeroVacantes"]);
                oferta.NombreLocacion = Funciones.ToString(dtResultado.Rows[0]["NombreLocacion"]);
                oferta.IdEmpresa = Funciones.ToInt(dtResultado.Rows[0]["IdEmpresa"]);
                oferta.IdentificadorTributario = Funciones.ToString(dtResultado.Rows[0]["IdentificadorTributario"]);
                oferta.Compatible = Funciones.ToDecimal(dtResultado.Rows[0]["Compatible"]);

            }

            return oferta;
        }

        public VistaOfertaAlumno OfertaAlumnoPostulacion(int IdOferta,int IdAlumno)
        {
            VistaOfertaAlumno vistaofertalumno = new VistaOfertaAlumno();
            Alumno alumno=new Alumno();
            vistaofertalumno.Oferta = ObtenerOfertasAlumnoPorID(IdOferta, IdAlumno);
            vistaofertalumno.ListaAlumnoCV = lnalumnocv.ObtenerAlumnoCVPorIdAlumno(IdAlumno);
            vistaofertalumno.ListaOfertas = BuscarSimilaresOfertasAlumno(IdOferta);


           
            return vistaofertalumno;
        }
        public List<Oferta> BuscarFiltroOfertasAlumno(int IdAlumno, string PalabraClave)
        {
            List<Oferta> listaOferta = new List<Oferta>();

            DataTable dtResultado = adOferta.BuscarFiltroOfertasAlumno(IdAlumno, PalabraClave);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Oferta oferta = new Oferta();
                oferta.IdOferta = Funciones.ToInt(dtResultado.Rows[i]["IdOferta"]);
                oferta.Compatible = Funciones.ToDecimal(dtResultado.Rows[i]["Compatible"]);
                oferta.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                oferta.NombreComercial = Funciones.ToString(dtResultado.Rows[i]["NombreComercial"]);
                oferta.CargoOfrecido = Funciones.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                oferta.DesTipoTrabajo = Funciones.ToString(dtResultado.Rows[i]["DesTipoTrabajo"]);
                oferta.Horario = Funciones.ToString(dtResultado.Rows[i]["Horario"]);
                oferta.RemuneracionOfrecida = Funciones.ToDecimal(dtResultado.Rows[i]["RemuneracionOfrecida"]);
                oferta.Mensaje = Funciones.ToInt(dtResultado.Rows[i]["Mensajes"]);
                oferta.IdEmpresa = Funciones.ToInt(dtResultado.Rows[i]["IdEmpresa"]);
                listaOferta.Add(oferta);
            }
            return listaOferta;
        }

        public List<Oferta> BuscarSimilaresOfertasAlumno(int IdOferta)
        {
            List<Oferta> listaOferta = new List<Oferta>();

            DataTable dtResultado = adOferta.BuscarSimilaresOfertasAlumno(IdOferta);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Oferta oferta = new Oferta();
                oferta.IdOferta = Funciones.ToInt(dtResultado.Rows[i]["IdOferta"]);
                oferta.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                oferta.CargoOfrecido = Funciones.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                oferta.IdEmpresa = Funciones.ToInt(dtResultado.Rows[i]["IdEmpresa"]);
                listaOferta.Add(oferta);
            }
            return listaOferta;
        }

        public List<Oferta> BuscarAvanzadoOfertasAlumno(VistaOfertaAlumno entidad)
        {
            List<Oferta> listaOferta = new List<Oferta>();

            DataTable dtResultado = adOferta.BuscarAvanzadoOfertasAlumno(entidad);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Oferta oferta = new Oferta();
                oferta.IdOferta = Funciones.ToInt(dtResultado.Rows[i]["IdOferta"]);
                oferta.Compatible = Funciones.ToDecimal(dtResultado.Rows[i]["Compatible"]);
                oferta.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                oferta.NombreComercial = Funciones.ToString(dtResultado.Rows[i]["NombreComercial"]);
                oferta.CargoOfrecido = Funciones.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                oferta.DesTipoTrabajo = Funciones.ToString(dtResultado.Rows[i]["DesTipoTrabajo"]);
                oferta.Horario = Funciones.ToString(dtResultado.Rows[i]["Horario"]);
                oferta.RemuneracionOfrecida = Funciones.ToDecimal(dtResultado.Rows[i]["RemuneracionOfrecida"]);
                oferta.Mensaje = Funciones.ToInt(dtResultado.Rows[i]["Mensajes"]);
                oferta.IdEmpresa = Funciones.ToInt(dtResultado.Rows[i]["IdEmpresa"]);
                listaOferta.Add(oferta);
            }
            return listaOferta;
        }

        public List<Oferta> MostrarUltimasOfertas(int IdAlumno)
        {
            List<Oferta> listaOferta = new List<Oferta>();

            DataTable dtResultado = adOferta.MostrarUltimasOfertas(IdAlumno);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Oferta oferta = new Oferta();
                oferta.IdOferta = Funciones.ToInt(dtResultado.Rows[i]["IdOferta"]);
                oferta.Compatible = Funciones.ToDecimal(dtResultado.Rows[i]["Compatible"]);
                oferta.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                oferta.NombreComercial = Funciones.ToString(dtResultado.Rows[i]["NombreComercial"]);
                oferta.CargoOfrecido = Funciones.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                oferta.IdEmpresa = Funciones.ToInt(dtResultado.Rows[i]["IdEmpresa"]);
                listaOferta.Add(oferta);
            }
            return listaOferta;
        }

          
        public List<VistaOfertaAlumno> Oferta_Mostrar()
        {
            List<VistaOfertaAlumno> listaOferta = new List<VistaOfertaAlumno>();

            DataTable dtResultado = adOferta.Obtener();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                VistaOfertaAlumno oferta = new VistaOfertaAlumno();

                oferta.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                oferta.Empresa = dtResultado.Rows[i]["Empresa"].ToString();

                oferta.CargoOfrecido = dtResultado.Rows[i]["CargoOfrecido"].ToString();
                oferta.Horario = dtResultado.Rows[i]["Horario"].ToString ();

                oferta.RemuneracionOfrecida = Convert.ToDecimal(dtResultado.Rows[i]["RemuneracionOfrecida"]);

                oferta.EstadoOferta = dtResultado.Rows[i]["EstadoOferta"].ToString();

                listaOferta.Add(oferta);
            }
            return listaOferta;
        }

        public List<VistaPostulacionAlumno> ObtenerPostulantes()
        {
            List<VistaPostulacionAlumno> listapostulacion = new List<VistaPostulacionAlumno>();

            DataTable dtResultado = adOferta.ObtenerPostulantes();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                VistaPostulacionAlumno postulacion = new VistaPostulacionAlumno();

                postulacion.FechaPublicacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                postulacion.FechaPostulacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaPostulacion"]);
                postulacion.Empresa = dtResultado.Rows[i]["Empresa"].ToString();
                postulacion.CargoOfrecido = dtResultado.Rows[i]["CargoOfrecido"].ToString();
                postulacion.TipoTrabajo = dtResultado.Rows[i]["TipoTrabajo"].ToString();
                postulacion.Horario = Convert.ToDateTime(dtResultado.Rows[i]["Horario"]);
                postulacion.RemuneracionOfrecida = Convert.ToDecimal(dtResultado.Rows[i]["RangoRemuneracionDesde"]);
                postulacion.EstadoOferta = dtResultado.Rows[i]["EstadoOferta"].ToString();

                listapostulacion.Add(postulacion);
            }
            return listapostulacion;
        }

        //Obtiene las Listas de opciones (Todo los Combos)
        public DataTable ObtenerLista_ListaValor(int cod)
        {
            return adOferta.ObtenerLista_ListaValor(cod);
        }


        public List<VistaOfertaEmpresa> Obtener_PanelEmpresa(int idEmpresa, string filtroBusqueda)
        {
            List<VistaOfertaEmpresa> lista = new List<VistaOfertaEmpresa>();

            DataTable dtResultados = adOferta.Obtener_PanelEmpresa(idEmpresa, filtroBusqueda);

            foreach(DataRow fila in dtResultados.Rows)
            {
                VistaOfertaEmpresa vista = new VistaOfertaEmpresa();
                vista.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                vista.FechaPublicacion = Convert.ToDateTime(fila["FechaPublicacion"]);
                vista.Cargo = Convert.ToString(fila["CargoOfrecido"]);
                vista.CVPendientesRevision = Convert.ToInt32(fila["CVPendientesRevision"]);
                vista.CVTotales = Convert.ToInt32(fila["CVTotales"]);
                vista.FaseActual = Convert.ToString(fila["FaseActual"]);
                vista.EstadoOferta = Convert.ToString(fila["EstadoOferta"]);
                vista.MensajesNoLeidos = Convert.ToInt32(fila["MensajesNoLeidos"]);
                vista.MensajesTotales = Convert.ToInt32(fila["MensajesTotales"]);

                lista.Add(vista);
            }

            return lista;
        }
     
        public Oferta ObtenerPorId(int idOferta)
        {
            Oferta oferta = null;

            DataSet dsResultado = adOferta.ObtenerPorId(idOferta);

            //Tabla Index 0: Datos de la oferta.
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                oferta = new Oferta();

                oferta.IdOferta = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IdOferta"]);
                oferta.IdEmpresa = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IdEmpresa"]);
                oferta.CargoOfrecido = Convert.ToString(dsResultado.Tables[0].Rows[0]["CargoOfrecido"]);
                oferta.Funciones = Convert.ToString(dsResultado.Tables[0].Rows[0]["Funciones"]);
                oferta.Competencias = Convert.ToString(dsResultado.Tables[0].Rows[0]["Competencias"]);

                oferta.TipoTrabajoIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoTrabajo"]);
                oferta.TipoTrabajo.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoTrabajoDescripcion"]);
                oferta.TipoCargoIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoCargo"]);
                oferta.TipoCargo.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoCargoDescripcion"]);
                oferta.TipoContratoIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoContrato"]);
                oferta.TipoContrato.Valor = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoContratoDescripcion"]);

                oferta.RemuneracionOfrecida = Convert.ToDecimal(dsResultado.Tables[0].Rows[0]["RemuneracionOfrecida"]);
                oferta.Horario = Convert.ToString(dsResultado.Tables[0].Rows[0]["Horario"]);
                oferta.AreaEmpresa = Convert.ToString(dsResultado.Tables[0].Rows[0]["AreaEmpresa"]);
                oferta.NumeroVacantes = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["NumeroVacantes"]);

                oferta.IdEmpresaLocacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IdEmpresaLocacion"]);
                oferta.RecibeCorreosIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["RecibeCorreos"]);
                oferta.FechaFinRecepcionCV = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaFinRecepcionCV"]);
                oferta.AreaEmpresa = Convert.ToString(dsResultado.Tables[0].Rows[0]["AreaEmpresa"]);
                oferta.DuracionContrato = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DuracionContrato"]);
            }

            //Tabla Index 1: Lista de estudios.
            foreach (DataRow filaEstudio in dsResultado.Tables[1].Rows)
            {
                OfertaEstudio estudio                   = new OfertaEstudio();
                estudio.IdOfertaEstudio                 = Convert.ToInt32(filaEstudio["IdOfertaEstudio"]);
                estudio.IdOferta                        = Convert.ToInt32(filaEstudio["IdOfertaEstudio"]);
                estudio.CicloEstudio                    = Convert.ToInt32(filaEstudio["CicloEstudio"]);
                estudio.Estudio                         = Convert.ToString(filaEstudio["Estudio"]);
                estudio.TipoDeEstudio.Valor             = Convert.ToString(filaEstudio["TipoDeEstudioDescripcion"]);
                estudio.EstadoDelEstudio.Valor          = Convert.ToString(filaEstudio["EstadoDelEstudioDescripcion"]);
                estudio.EstadoOfertaEstudio.Valor       = Convert.ToString(filaEstudio["EstadoOfertaEstudioDescripcion"]);

                oferta.ListaEstudios.Add(estudio);
            }

            //Tabla Index 2: Lista de experiencia por sector
            foreach (DataRow filaSector in dsResultado.Tables[2].Rows)
            {
                OfertaSectorEmpresarial sector = new OfertaSectorEmpresarial();
                sector.IdOfertaSectorEmpresarial    = Convert.ToInt32(filaSector["IdOfertaSectorEmpresarial"]);
                sector.IdOferta                     = Convert.ToInt32(filaSector["IdOferta"]);
                sector.SectorEmpresarial.Valor      = Convert.ToString(filaSector["SectorEmpresarialDescripcion"]);
                sector.AniosTrabajados              = Convert.ToInt32(filaSector["AniosTrabajados"]);

                oferta.ListaSectores.Add(sector);

            }

            //Tabla Index 3: Lista de información adicional
            foreach (DataRow filaInfoAdicional in dsResultado.Tables[3].Rows)
            {
                OfertaInformacionAdicional infoAdicional = new OfertaInformacionAdicional();
                infoAdicional.IdOfertaInformacionAdicional  = Convert.ToInt32(filaInfoAdicional["IdOfertaInformacionAdicional"]);
                infoAdicional.IdOferta                      = Convert.ToInt32(filaInfoAdicional["IdOferta"]);
                infoAdicional.TipoConocimiento.Valor        = Convert.ToString(filaInfoAdicional["TipoConocimientoDescripcion"]);
                infoAdicional.Conocimiento                  = Convert.ToString(filaInfoAdicional["Conocimiento"]);
                infoAdicional.NivelConocimiento.Valor       = Convert.ToString(filaInfoAdicional["NivelConocimientoDescripcion"]);
                infoAdicional.AniosExperiencia              = Convert.ToInt32(filaInfoAdicional["AniosExperiencia"]);

                oferta.ListaInformacionAdicional.Add(infoAdicional);
            }
            return oferta;
        }
       
        public DataTable ObtenerPostulacionesPorEmpresa(int idEmpresa)
        {
            return adOferta.ObtenerPostulacionesPorEmpresa(idEmpresa);
        }

        public List<OfertaPostulante> ObtenerPostulantesPorIdOferta(int idOferta)
        {
            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();

            DataTable dtResultado = adOferta.ObtenerPostulantesPorIdOferta(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaPostulante postulante = new OfertaPostulante();
                postulante.IdOfertaPostulante = Convert.ToInt32(fila["IdOfertaPostulante"]);
                postulante.FaseOferta.Valor = Convert.ToString(fila["FaseOfertaDescripcion"]);
                postulante.FechaPostulacion = Convert.ToDateTime(fila["FechaPostulacion"]);
                postulante.Alumno = new Alumno() { Nombres = Convert.ToString(fila["AlumnoNombres"]), Apellidos = Convert.ToString(fila["AlumnoApellidos"]) };
                postulante.NivelDeMatch = Convert.ToInt32(fila["NivelDeMatch"]);

                postulantes.Add(postulante);
            }

            return postulantes;
        }

        public List<OfertaFase> Obtener_OfertaFase(int idOferta)
        {
            List<OfertaFase> listaOfertaFase = new List<OfertaFase>();

            DataTable dtResultado = adOferta.Obtener_OfertaFase(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaFase oFase = new OfertaFase();
                oFase.IdOfertaFase = Convert.ToInt32(fila["IdOfertaFase"]);
                oFase.IdOferta = Convert.ToInt32(fila["IdOfertaFase"]);
                oFase.Incluir = Convert.ToBoolean(fila["Incluir"]);
                oFase.IdListaValor = Convert.ToString(fila["IdListaValor"]);
                oFase.FaseOferta = Convert.ToString(fila["FaseOferta"]);

                listaOfertaFase.Add(oFase);                
            }
            return listaOfertaFase; 
        }

        public void ActualizarOfertaFase(List<OfertaFase> listaOfertaFase)
        {
            adOferta.ActualizarOfertaFase(listaOfertaFase);
        }
    }

}
