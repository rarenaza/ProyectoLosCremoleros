using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOferta
    {

        ADOferta adOferta = new ADOferta();
        LNAlumnoCV lnalumnocv = new LNAlumnoCV();
        public void BuscarCumplimientoOfertasAlumno(ref VistaOfertaAlumno vistaofertaalumno)
        {

            DataSet dsResultado = adOferta.BuscarCumplimientoOfertasAlumno(vistaofertaalumno.Oferta.IdAlumno, vistaofertaalumno.Oferta.IdOferta);

            //Datos generales de la empresa.
            if (dsResultado.Tables.Count > 0)
            {
                List<Oferta> ListaEstudios = new List<Oferta>();
                List<Oferta> ListaSectorEmpresarial = new List<Oferta>();
                List<Oferta> ListaInformacionAdicional = new List<Oferta>();

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsResultado.Tables[0].Rows)
                    {
                        Oferta of = new Oferta();
                        of.Requisito = Funciones.ToString(row["Caracteristica"]);
                        of.Cumplimiento = Funciones.ToInt(row["Estado"]);
                        of.Tipo = Funciones.ToInt(row["Tipo2"]);
                        of.Line = Funciones.ToInt(row["Line"]);
                        ListaEstudios.Add(of);
                    }
                    vistaofertaalumno.ListadoEstudios = ListaEstudios;
                }
                if (dsResultado.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow row in dsResultado.Tables[1].Rows)
                    {
                        Oferta of = new Oferta();
                        of.Requisito = Funciones.ToString(row["Caracteristica"]);
                        of.Cumplimiento = Funciones.ToInt(row["Estado"]);
                        of.Tipo = Funciones.ToInt(row["Tipo2"]);
                        of.Line = Funciones.ToInt(row["Line"]);
                        ListaSectorEmpresarial.Add(of);
                    }
                    vistaofertaalumno.ListadoSectorEmpresarial = ListaSectorEmpresarial;
                }
                if (dsResultado.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow row in dsResultado.Tables[2].Rows)
                    {
                        Oferta of = new Oferta();
                        of.Requisito = Funciones.ToString(row["Caracteristica"]);
                        of.Cumplimiento = Funciones.ToInt(row["Estado"]);
                        of.Tipo = Funciones.ToInt(row["Tipo2"]);
                        of.Line = Funciones.ToInt(row["Line"]);
                        ListaInformacionAdicional.Add(of);
                    }
                    vistaofertaalumno.ListadoInformacionAdicional = ListaInformacionAdicional;
                }

            }
        }
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
                oferta.DuracionContrato = Funciones.ToInt(dtResultado.Rows[0]["DuracionContrato"] == System.DBNull.Value ? null : dtResultado.Rows[0]["DuracionContrato"]);
                oferta.Horario = Funciones.ToString(dtResultado.Rows[0]["Horario"]);
                oferta.RemuneracionOfrecida = Funciones.ToDecimal(dtResultado.Rows[0]["RemuneracionOfrecida"]);
                oferta.AreaEmpresa = Funciones.ToString(dtResultado.Rows[0]["AreaEmpresa"]);
                oferta.NumeroVacantes = Funciones.ToInt(dtResultado.Rows[0]["NumeroVacantes"]);
                oferta.NombreLocacion = Funciones.ToString(dtResultado.Rows[0]["NombreLocacion"]);
                oferta.IdEmpresa = Funciones.ToInt(dtResultado.Rows[0]["IdEmpresa"]);
                oferta.IdentificadorTributario = Funciones.ToString(dtResultado.Rows[0]["IdentificadorTributario"]);
                oferta.Compatible = Funciones.ToDecimal(dtResultado.Rows[0]["Compatible"]);
                oferta.IdOferta = idOferta;
                oferta.Postulacion = Funciones.ToInt(dtResultado.Rows[0]["Postulacion"]);
                oferta.FechaPostulacion = Funciones.ToDateTime(dtResultado.Rows[0]["FechaPostulacion"]);
                oferta.NombreCV = Funciones.ToString(dtResultado.Rows[0]["NombreCV"]);
                oferta.IdCV = Funciones.ToInt(dtResultado.Rows[0]["IdCV"]);
                oferta.IdAlumno = idAlumno;
                oferta.LogoEmpresa = Funciones.ToBytes(dtResultado.Rows[0]["LogoEmpresa"]); ;
                oferta.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[0]["ArchivoMimeType"]); ;
                oferta.AnoCreacion = Funciones.ToInt(dtResultado.Rows[0]["AnoCreacion"]);
                oferta.DesSectorEmpresarial = Funciones.ToString(dtResultado.Rows[0]["DesSectorEmpresarial"]);
                oferta.IdOfertaPostulante = Funciones.ToInt(dtResultado.Rows[0]["IdOfertaPostulante"]);
                oferta.RecibeCorreosIdListaValor = Funciones.ToString(dtResultado.Rows[0]["RecibeCorreos"]);
                oferta.CorreoElectronicoUsuarioEmpresa = Funciones.ToString(dtResultado.Rows[0]["CorreoElectronicoUsuarioEmpresa"]);
                oferta.EstadoOferta = Funciones.ToString(dtResultado.Rows[0]["EstadoOferta"]);
                oferta.ExperienciaGeneral = Funciones.ToInt(dtResultado.Rows[0]["MesesExperienciaTotal"]);
                oferta.ExperienciaPosicionesSimilares = Funciones.ToInt(dtResultado.Rows[0]["MesesExperienciaTipoTrabajo"]);
                oferta.CumpleExperienciaGeneral = Funciones.ToBoolean(dtResultado.Rows[0]["CumpleMesesExperienciaTotal"]);
                oferta.CumpleExperienciaPosicionesSimilares = Funciones.ToBoolean(dtResultado.Rows[0]["CumpleMesesExperienciaTipoTrabajo"]);
                //06MAR15: Se agregan los campos EstadoCarreaUTP y CicloMinimoUTP
                oferta.EstadoCarreraUTPDescripcion = Funciones.ToString(dtResultado.Rows[0]["EstadoCarreraUTPDescripcion"]);
                oferta.CicloMinimoCarreraUTP = Funciones.ToInt(dtResultado.Rows[0]["CicloMinimoCarreraUTP"]);
            }

            return oferta;
        }

        public VistaOfertaAlumno OfertaAlumnoPostulacion(int IdOferta, int IdAlumno)
        {
            VistaOfertaAlumno vistaofertalumno = new VistaOfertaAlumno();
            Alumno alumno = new Alumno();
            vistaofertalumno.Oferta = ObtenerOfertasAlumnoPorID(IdOferta, IdAlumno);
            vistaofertalumno.ListaAlumnoCV = lnalumnocv.ObtenerAlumnoCVPorIdAlumnoCompleto(IdAlumno);
            vistaofertalumno.ListaOfertas = BuscarSimilaresOfertasAlumno(IdOferta);
            BuscarCumplimientoOfertasAlumno(ref vistaofertalumno);

            //06MAR15: Se agrega el listado de carreras UTP y otros estudios
            Oferta oferta = ObtenerPorId(IdOferta);
            vistaofertalumno.ListaEstudiosOtros = oferta.ListaEstudios;
            vistaofertalumno.ListaEstudiosUTP = oferta.CarrerasSeleccionadas;

            //06MAR15: Se establece el campo Cumplimiento para los estudios.
            //Armar pares de datos: Estudio  y TipoEstudio
            //if (vistaofertalumno.ListadoEstudios != null && vistaofertalumno.ListadoEstudios.Count > 0)
            //{ 
            //    //Se recorre la lista de estudios con el dato de cumplimiento:
            //    for (int i = 0; i < vistaofertalumno.ListadoEstudios.Count-3; i++)
            //    {
            //        //Se obtiene los datos de la fila:
            //        string tipoEstudio = vistaofertalumno.ListadoEstudios[i].Requisito;
            //        int cumplimiento = vistaofertalumno.ListadoEstudios[i].Cumplimiento;
            //        string estudio = vistaofertalumno.ListadoEstudios[i+1].Requisito;

            //        //Se avanza en bloques de 4 porque la lista agrupa TipoEstudio(0), Carrera(1), EstadoEstudio(2), Línea vacía(3).
            //        i = i + 4;

            //        //Se busca la oferta en a listaEstudiosUTP
            //        var estudioOfertaUTP = vistaofertalumno.ListaEstudiosUTP.Where(m => m.TipoDeEstudio.Valor == tipoEstudio && m.Estudio == estudio).FirstOrDefault();
            //        if (estudioOfertaUTP != null)
            //        {
            //            estudioOfertaUTP.Cumplimiento = cumplimiento;
            //        }

            //        //Se busca la oferta en a listaEstudiosOtros
            //        var estudioOfertaOtros = vistaofertalumno.ListaEstudiosOtros.Where(m => m.TipoDeEstudio.Valor == tipoEstudio && m.Estudio == estudio).FirstOrDefault();
            //        if (estudioOfertaOtros != null)
            //        {
            //            estudioOfertaOtros.Cumplimiento = cumplimiento;
            //        }
            //    }
            //}

            return vistaofertalumno;
        }
        public List<Oferta> BuscarFiltroOfertasAlumno(int IdAlumno, string PalabraClave, string TipoTrabajoUTP, int PagActual, int NumRegistros)
        {
            List<Oferta> listaOferta = new List<Oferta>();

            DataTable dtResultado = adOferta.BuscarFiltroOfertasAlumno(IdAlumno, PalabraClave, TipoTrabajoUTP, PagActual, NumRegistros);

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
                oferta.TotalRegistros = Funciones.ToInt(dtResultado.Rows[i]["CantidadTotal"]);


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
                oferta.LogoEmpresa = Funciones.ToBytes(dtResultado.Rows[i]["LogoEmpresa"]);
                oferta.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[i]["ArchivoMimeType"]);

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
                oferta.TotalRegistros = Funciones.ToInt(dtResultado.Rows[i]["CantidadTotal"]);
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
                oferta.LogoEmpresa = Funciones.ToBytes(dtResultado.Rows[i]["LogoEmpresa"]);
                oferta.ArchivoMimeType = Funciones.ToString(dtResultado.Rows[i]["ArchivoMimeType"]);

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
                oferta.Horario = dtResultado.Rows[i]["Horario"].ToString();

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

                postulacion.FechaPublicacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPublicacion"]);
                postulacion.FechaPostulacion = Funciones.ToDateTime(dtResultado.Rows[i]["FechaPostulacion"]);
                postulacion.Empresa = Funciones.ToString(dtResultado.Rows[i]["Empresa"]);
                postulacion.CargoOfrecido = Funciones.ToString(dtResultado.Rows[i]["CargoOfrecido"]);
                postulacion.TipoTrabajo = Funciones.ToString(dtResultado.Rows[i]["TipoTrabajo"]);
                postulacion.Horario = Funciones.ToString(dtResultado.Rows[i]["Horario"]);
                postulacion.RemuneracionOfrecida = Funciones.ToDecimal(dtResultado.Rows[i]["RemuneracionOfrecida"]);
                postulacion.EstadoOferta = Funciones.ToString(dtResultado.Rows[i]["EstadoOferta"]);

                listapostulacion.Add(postulacion);
            }
            return listapostulacion;
        }

        //Obtiene las Listas de opciones (Todo los Combos)
        public DataTable ObtenerLista_ListaValor(int cod)
        {
            return adOferta.ObtenerLista_ListaValor(cod);
        }


        public List<VistaOfertaEmpresa> Obtener_PanelEmpresa(int idEmpresa, string filtroBusqueda, string rolIdListaValor, string usuario, int nroPaginaActual, int filasPorPagina)
        {
            List<VistaOfertaEmpresa> lista = new List<VistaOfertaEmpresa>();

            DataTable dtResultados = adOferta.Obtener_PanelEmpresa(idEmpresa, filtroBusqueda, rolIdListaValor, usuario, nroPaginaActual, filasPorPagina);

            foreach (DataRow fila in dtResultados.Rows)
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
                vista.UsuarioPropietarioEmpresa = Convert.ToString(fila["UsuarioPropietarioEmpresa"]);
                vista.EstadoOfertaIdListaValor = Convert.ToString(fila["EstadoOfertaIdListaValor"]);
                vista.CantidadTotal = Convert.ToInt32(fila["CantidadTotal"]);

                lista.Add(vista);
            }

            return lista;
        }

        public Oferta ObtenerPorId(int idOferta)
        {
            Oferta oferta = null;

            //18FEB: Se obtiene las carreras de UTP y se quitan las ya seleccionadas.
            LNGeneral lnGeneral = new LNGeneral();
            List<ListaValor> listaCarrerasUTP = lnGeneral.ObtenerListaValor(Constantes.IDLISTA_DE_CARRERA).Where(m => m.IdListaValorPadre == Constantes.TIPO_ESTUDIO_PRINCIPAL).ToList(); //Se obtienen todas las carreras


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
                
                if (dsResultado.Tables[0].Rows[0]["RemuneracionOfrecida"] == System.DBNull.Value)
                {
                    oferta.RemuneracionOfrecida = null;
                }
                else
                {
                    oferta.RemuneracionOfrecida = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["RemuneracionOfrecida"]);
                }

                oferta.Horario = Convert.ToString(dsResultado.Tables[0].Rows[0]["Horario"]);
                oferta.AreaEmpresa = Convert.ToString(dsResultado.Tables[0].Rows[0]["AreaEmpresa"]);

                if (dsResultado.Tables[0].Rows[0]["NumeroVacantes"] == System.DBNull.Value)
                {
                    oferta.NumeroVacantes = null;
                }
                else
                {
                    oferta.NumeroVacantes = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["NumeroVacantes"]);
                }
              
                oferta.IdEmpresaLocacion = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IdEmpresaLocacion"]);
                oferta.RecibeCorreosIdListaValor = Convert.ToString(dsResultado.Tables[0].Rows[0]["RecibeCorreos"]);
                oferta.FechaFinRecepcionCV = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaFinRecepcionCV"]);
                oferta.AreaEmpresa = Convert.ToString(dsResultado.Tables[0].Rows[0]["AreaEmpresa"]);

                if (dsResultado.Tables[0].Rows[0]["DuracionContrato"] == System.DBNull.Value)
                {
                    oferta.DuracionContrato = null;
                }
                else
                {
                    oferta.DuracionContrato = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["DuracionContrato"]);
                }

                oferta.Empresa.NombreComercial = Convert.ToString(dsResultado.Tables[0].Rows[0]["EmpresaNombreComercial"]);
                oferta.NombreLocacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["NombreLocacion"]);

                oferta.CreadoPor = Convert.ToString(dsResultado.Tables[0].Rows[0]["CreadoPor"]);
                oferta.ModificadoPor = Convert.ToString(dsResultado.Tables[0].Rows[0]["ModificadoPor"]);
                oferta.FechaCreacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaCreacion"]);
                oferta.FechaModificacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaModificacion"]);

                oferta.EstadoOferta = Convert.ToString(dsResultado.Tables[0].Rows[0]["EstadoOferta"]);
                oferta.UsuarioPropietarioEmpresa = Convert.ToString(dsResultado.Tables[0].Rows[0]["UsuarioPropietarioEmpresa"]);
                oferta.FechaPublicacion = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaPublicacion"]);

                oferta.CicloMinimoCarreraUTP = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["CicloMinimoCarreraUTP"]);
                oferta.EstadoCarreraUTP = Convert.ToString(dsResultado.Tables[0].Rows[0]["EstadoCarreraUTP"]);


                if (dsResultado.Tables[0].Rows[0]["MesesExperienciaTotal"] == System.DBNull.Value)
                {
                    oferta.ExperienciaGeneral = null;
                }
                else
                {
                    oferta.ExperienciaGeneral = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MesesExperienciaTotal"]);
                }


                if (dsResultado.Tables[0].Rows[0]["MesesExperienciaTipoTrabajo"] == System.DBNull.Value)
                {
                    oferta.ExperienciaPosicionesSimilares = null;
                }
                else
                {
                    oferta.ExperienciaPosicionesSimilares = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["MesesExperienciaTipoTrabajo"]);
                }

                //05MAR15: Para las nuevas ofertas este campo siempre está con data. Se agrega la validación para compatibilidad con ofertas anteriores.
                oferta.FechaFinProceso = Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaFinProceso"] == System.DBNull.Value ? new DateTime(1, 1, 1900) : dsResultado.Tables[0].Rows[0]["FechaFinProceso"]);
            }

            //Tabla Index 1: Lista de estudios.
            //Se instancia las listas:
            oferta.ListaEstudios = new List<OfertaEstudio>();
            oferta.CarrerasSeleccionadas = new List<OfertaEstudio>();
            foreach (DataRow filaEstudio in dsResultado.Tables[1].Rows)
            {

                OfertaEstudio estudio                   = new OfertaEstudio();
                estudio.IdOfertaEstudio                 = Convert.ToInt32(filaEstudio["IdOfertaEstudio"] == System.DBNull.Value ? null : filaEstudio["IdOfertaEstudio"]);
                estudio.IdOferta                        = Convert.ToInt32(filaEstudio["IdOfertaEstudio"] == System.DBNull.Value ? null : filaEstudio["IdOfertaEstudio"]);
                estudio.CicloEstudio                    = Convert.ToInt32(filaEstudio["CicloEstudio"] == System.DBNull.Value ? null : filaEstudio["CicloEstudio"]);
                estudio.Estudio                         = Convert.ToString(filaEstudio["Estudio"] == System.DBNull.Value ? null : filaEstudio["Estudio"]);
                estudio.TipoDeEstudio.IdListaValor =    Convert.ToString(filaEstudio["TipoDeEstudio"] == System.DBNull.Value ? null : filaEstudio["TipoDeEstudio"]);
                estudio.TipoDeEstudioIdListaValor =     Convert.ToString(filaEstudio["TipoDeEstudio"] == System.DBNull.Value ? null : filaEstudio["TipoDeEstudio"]);
                estudio.TipoDeEstudio.Valor             = Convert.ToString(filaEstudio["TipoDeEstudioDescripcion"]);
                estudio.EstadoDelEstudio.Valor          = Convert.ToString(filaEstudio["EstadoDelEstudioDescripcion"]);
                estudio.EstadoOfertaEstudio.Valor       = Convert.ToString(filaEstudio["EstadoOfertaEstudioDescripcion"]);
                estudio.CreadoPor                       = Convert.ToString(filaEstudio["CreadoPor"]);
                estudio.ModificadoPor                   = Convert.ToString(filaEstudio["ModificadoPor"]);
                estudio.FechaCreacion                   = Convert.ToDateTime(filaEstudio["FechaCreacion"]);
                estudio.FechaModificacion               = Convert.ToDateTime(filaEstudio["FechaModificacion"]);
                 
                #region Se separan las carrearas universitarias UTP.
                if (estudio.TipoDeEstudio.IdListaValor == Constantes.TIPO_ESTUDIO_PRINCIPAL) //Tipo de Estudio Universitario de UTP.
                {
                    var carreraEncontrada = listaCarrerasUTP.Where(m => m.Valor == estudio.Estudio).FirstOrDefault();

                    if (carreraEncontrada != null)
                    {
                        estudio.CodigoCarrera = carreraEncontrada.IdListaValor; //Se guarda el código de la carrera.
                        oferta.CarrerasSeleccionadas.Add(estudio);
                    }
                }
                else //Otros estudios.
                {
                    oferta.ListaEstudios.Add(estudio);
                }
                #endregion

            }

            //Tabla Index 2: Lista de experiencia por sector
            oferta.ListaSectores = new List<OfertaSectorEmpresarial>();
            foreach (DataRow filaSector in dsResultado.Tables[2].Rows)
            {
                OfertaSectorEmpresarial sector = new OfertaSectorEmpresarial();
                sector.IdOfertaSectorEmpresarial = Convert.ToInt32(filaSector["IdOfertaSectorEmpresarial"]);
                sector.IdOferta = Convert.ToInt32(filaSector["IdOferta"]);
                sector.SectorEmpresarial.Valor = Convert.ToString(filaSector["SectorEmpresarialDescripcion"]);
                sector.AniosTrabajados = Convert.ToInt32(filaSector["AniosTrabajados"] == System.DBNull.Value ? null : filaSector["AniosTrabajados"]);
                sector.ExperienciaExcluyente = Convert.ToBoolean(filaSector["ExperienciaExcluyente"]);
                sector.EstadoOfertaSectorEmpresarial.Valor = Convert.ToString(filaSector["EstadoOfertaSectorEmpresarialDescripcion"]);
                sector.CreadoPor = Convert.ToString(filaSector["CreadoPor"]);
                sector.ModificadoPor = Convert.ToString(filaSector["ModificadoPor"]);
                sector.FechaCreacion = Convert.ToDateTime(filaSector["FechaCreacion"]);
                sector.FechaModificacion = Convert.ToDateTime(filaSector["FechaModificacion"]);

                oferta.ListaSectores.Add(sector);

            }

            //Tabla Index 3: Lista de información adicional
            oferta.ListaInformacionAdicional = new List<OfertaInformacionAdicional>();
            foreach (DataRow filaInfoAdicional in dsResultado.Tables[3].Rows)
            {
                OfertaInformacionAdicional infoAdicional = new OfertaInformacionAdicional();
                infoAdicional.IdOfertaInformacionAdicional = Convert.ToInt32(filaInfoAdicional["IdOfertaInformacionAdicional"]);
                infoAdicional.IdOferta = Convert.ToInt32(filaInfoAdicional["IdOferta"]);
                infoAdicional.TipoConocimiento.Valor = Convert.ToString(filaInfoAdicional["TipoConocimientoDescripcion"]);
                infoAdicional.Conocimiento = Convert.ToString(filaInfoAdicional["Conocimiento"]);
                infoAdicional.NivelConocimiento.Valor = Convert.ToString(filaInfoAdicional["NivelConocimientoDescripcion"]);
                infoAdicional.AniosExperiencia = Convert.ToInt32(filaInfoAdicional["AniosExperiencia"] == System.DBNull.Value ? null : filaInfoAdicional["AniosExperiencia"]);
                infoAdicional.EstadoOfertaInformacionAdicional.Valor = Convert.ToString(filaInfoAdicional["EstadoOfertaInformacionAdicionalDescripcion"]);
                infoAdicional.CreadoPor = Convert.ToString(filaInfoAdicional["CreadoPor"]);
                infoAdicional.ModificadoPor = Convert.ToString(filaInfoAdicional["ModificadoPor"]);
                infoAdicional.FechaCreacion = Convert.ToDateTime(filaInfoAdicional["FechaCreacion"]);
                infoAdicional.FechaModificacion = Convert.ToDateTime(filaInfoAdicional["FechaModificacion"]);

                oferta.ListaInformacionAdicional.Add(infoAdicional);
            }


            //Tabla Index 4: Lista de Postulantes
            oferta.Postulantes = new List<OfertaPostulante>();
            foreach (DataRow filaPostulante in dsResultado.Tables[4].Rows)
            {
                OfertaPostulante postulante = new OfertaPostulante();
                postulante.Alumno.IdAlumno = Convert.ToInt32(filaPostulante["IdAlumno"]);
                postulante.Alumno.Nombres = Convert.ToString(filaPostulante["AlumnoNombres"]);
                postulante.Alumno.Apellidos = Convert.ToString(filaPostulante["AlumnoApellidos"]);
                postulante.FechaPostulacion = Convert.ToDateTime(filaPostulante["FechaPostulacion"]);
                postulante.FaseOferta.Valor = Convert.ToString(filaPostulante["FaseOferta"]);
                postulante.NombreCV = Convert.ToString(filaPostulante["NombreCV"]);
                postulante.NivelDeMatch = Convert.ToInt32(filaPostulante["NivelDeMatch"]);
                postulante.CreadoPor = Convert.ToString(filaPostulante["CreadoPor"]);
                postulante.ModificadoPor = Convert.ToString(filaPostulante["ModificadoPor"]);
                postulante.FechaCreacion = Convert.ToDateTime(filaPostulante["FechaCreacion"]);
                postulante.FechaModificacion = Convert.ToDateTime(filaPostulante["FechaModificacion"]);

                oferta.Postulantes.Add(postulante);
            }

            //Fases de la oferta
            oferta.OfertaFases = Obtener_OfertaFase(idOferta);
                        
            
            //Se instancia la lista:
            oferta.CarrerasDisponibles = new List<ListaValor>(); //Esta lista va al lado izquierdo
            //Se recorren las carreras.
            foreach (var carreraUTP in listaCarrerasUTP)
            {
                //Si la carreraUTP ya está seleccionada entonces NO se agrega. Si la búsqueda es null => no está, por lo tanto, sí se agrega.
                if (oferta.CarrerasSeleccionadas.Where(m => m.Estudio == carreraUTP.Valor).FirstOrDefault() == null)
                { 
                    ListaValor carreraDisponible = copiarListaValor(carreraUTP);
                    oferta.CarrerasDisponibles.Add(carreraDisponible);
                }
            }

            return oferta;
        }

        public Oferta ObtenerSeguimientoPorId(int idOferta)
        {
            Oferta oferta = null;

            DataSet dsResultado = adOferta.ObtenerSeguimientoPorId(idOferta);

            //Tabla Index 0: Datos de la oferta.
            if (dsResultado.Tables[0].Rows.Count > 0)
            {
                oferta = new Oferta();

                oferta.IdOferta = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["IdOferta"]);
                oferta.FechaSeguimiento = (dsResultado.Tables[0].Rows[0]["FechaSeguimiento"] == System.DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dsResultado.Tables[0].Rows[0]["FechaSeguimiento"]));
                oferta.NumeroPostulantes = dsResultado.Tables[0].Rows[0]["NumeroPostulantes"] == System.DBNull.Value ? (Int32?)null : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["NumeroPostulantes"]);
                oferta.NumeroInvitados = dsResultado.Tables[0].Rows[0]["NumeroInvitados"] == System.DBNull.Value ? (Int32?)null : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["NumeroInvitados"]);
                oferta.NumeroEntrevistados = dsResultado.Tables[0].Rows[0]["NumeroEntrevistados"] == System.DBNull.Value ? (Int32?)null : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["NumeroEntrevistados"]);
                oferta.NumeroContratados = dsResultado.Tables[0].Rows[0]["NumeroContratados"] == System.DBNull.Value ? (Int32?)null : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["NumeroContratados"]);
                oferta.ConvenioRegistrado = Convert.ToBoolean(dsResultado.Tables[0].Rows[0]["ConvenioRegistrado"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["ConvenioRegistrado"]);
                oferta.Contacto = Convert.ToString(dsResultado.Tables[0].Rows[0]["Contacto"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["Contacto"]);
                oferta.DatosContacto = Convert.ToString(dsResultado.Tables[0].Rows[0]["DatosContacto"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["DatosContacto"]);
                oferta.MedioComunicacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["MedioComunicacion"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["MedioComunicacion"]);
                //Encuesta:
                oferta.Calificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["EncuestaCalificacion"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["EncuestaCalificacion"]);
                oferta.NroPostulantes = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["EncuestaNroPostulantes"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["EncuestaNroPostulantes"]);
                oferta.ContratadosUTP = Convert.ToInt32(dsResultado.Tables[0].Rows[0]["EncuestaContratadosUTP"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["EncuestaContratadosUTP"]);
                oferta.ContratadosOtros = Convert.ToString(dsResultado.Tables[0].Rows[0]["EncuestaContratadosOtros"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["EncuestaContratadosOtros"]);
                oferta.TipoTrabajoUTP = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoTrabajoUTP"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["TipoTrabajoUTP"]);
                //Seguimiento:
                oferta.SeguimientoCalificacion = Convert.ToString(dsResultado.Tables[0].Rows[0]["SeguimientoCalificacion"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["SeguimientoCalificacion"]);
                oferta.SeguimientoNroInvitados = dsResultado.Tables[0].Rows[0]["SeguimientoNroInvitados"] == System.DBNull.Value ? (Int32?)null : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SeguimientoNroInvitados"]);
                oferta.SeguimientoContratados = dsResultado.Tables[0].Rows[0]["SeguimientoContratados"] == System.DBNull.Value ? (Int32?)null : Convert.ToInt32(dsResultado.Tables[0].Rows[0]["SeguimientoContratados"]);
                oferta.SeguimientoContratadosOtros = Convert.ToString(dsResultado.Tables[0].Rows[0]["SeguimientoContratadosOtros"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["SeguimientoContratadosOtros"]);

                oferta.TipoTrabajoUTP = Convert.ToString(dsResultado.Tables[0].Rows[0]["TipoTrabajoUTP"] == System.DBNull.Value ? null : dsResultado.Tables[0].Rows[0]["TipoTrabajoUTP"]);

            }
            return oferta;
        }


        public DataTable ObtenerPostulacionesPorEmpresa(int idEmpresa, string usuario)
        {
            return adOferta.ObtenerPostulacionesPorEmpresa(idEmpresa, usuario);
        }

        public List<OfertaPostulante> ObtenerPostulantesPorIdOferta(int idOferta)
        {
            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();

            DataTable dtResultado = adOferta.ObtenerPostulantesPorIdOferta(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaPostulante postulante = new OfertaPostulante();
                postulante.IdAlumno = Convert.ToInt32(fila["IdAlumno"]);
                postulante.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                postulante.IdOfertaPostulante = Convert.ToInt32(fila["IdOfertaPostulante"]);
                postulante.FaseOferta.Valor = Convert.ToString(fila["FaseOfertaDescripcion"]);
                postulante.FechaPostulacion = Convert.ToDateTime(fila["FechaPostulacion"]);
                postulante.Alumno = new Alumno() { Nombres = Convert.ToString(fila["AlumnoNombres"]), Apellidos = Convert.ToString(fila["AlumnoApellidos"]) };
                postulante.NivelDeMatch = Convert.ToInt32(fila["NivelDeMatch"]);
                postulante.CorreoElectronico = Convert.ToString(fila["CorreoElectronico"]);
                postulante.Usuario = Convert.ToString(fila["Usuario"]);
                postulante.FaseOferta.Peso = Convert.ToInt32(fila["FasePeso"]);

                postulantes.Add(postulante);
            }

            return postulantes;
        }

        public List<OfertaPostulante> ObtenerPostulantesPorIdOfertaSimple(int idOferta)
        {
            List<OfertaPostulante> postulantes = new List<OfertaPostulante>();

            DataTable dtResultado = adOferta.ObtenerPostulantesPorIdOfertaSimple(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                OfertaPostulante postulante = new OfertaPostulante();
                postulante.IdAlumno = Convert.ToInt32(fila["IdAlumno"]);
                postulante.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                postulante.IdOfertaPostulante = Convert.ToInt32(fila["IdOfertaPostulante"]);
                postulante.FaseOferta.Valor = Convert.ToString(fila["FaseOfertaDescripcion"]);
                postulante.FechaPostulacion = Convert.ToDateTime(fila["FechaPostulacion"]);
                postulante.Alumno = new Alumno() { Nombres = Convert.ToString(fila["AlumnoNombres"]), Apellidos = Convert.ToString(fila["AlumnoApellidos"]) };
                postulante.CorreoElectronico = Convert.ToString(fila["CorreoElectronico"]);
                postulante.Usuario = Convert.ToString(fila["Usuario"]);
                postulante.FaseOferta.Peso = Convert.ToInt32(fila["FasePeso"]);

                postulantes.Add(postulante);
            }

            return postulantes;
        }

        public DataTable ObtenerPostulantesPorIdOfertaExcel(int idOferta)
        {
            return adOferta.ObtenerPostulantesPorIdOferta(idOferta);
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

            //ListaOfertaFase listaRetorno = new ListaOfertaFase();
            //listaRetorno.ListaFasesDeLaOferta = listaOfertaFase;

            return listaOfertaFase;
        }

        public void ActualizarOfertaFase(List<OfertaFase> listaOfertaFase)
        {
            adOferta.ActualizarOfertaFase(listaOfertaFase);
        }

        public List<OfertaFase> Obtener_OfertaFaseActivas(int idOferta)
        {
            List<OfertaFase> listaOfertaFase = new List<OfertaFase>();

            DataTable dtResultado = adOferta.Obtener_OfertaFase(idOferta);

            foreach (DataRow fila in dtResultado.Rows)
            {
                if (Convert.ToBoolean(fila["Incluir"]))
                {
                    OfertaFase oFase = new OfertaFase();
                    oFase.IdOfertaFase = Convert.ToInt32(fila["IdOfertaFase"]);
                    oFase.IdOferta = Convert.ToInt32(fila["IdOfertaFase"]);
                    oFase.Incluir = Convert.ToBoolean(fila["Incluir"]);
                    oFase.IdListaValor = Convert.ToString(fila["IdListaValor"]);
                    oFase.FaseOferta = Convert.ToString(fila["FaseOferta"]);

                    listaOfertaFase.Add(oFase);
                }
            }

            return listaOfertaFase;
        }

        public void ActualizarFaseDePostulantes(List<OfertaPostulante> postulantes, string faseOferta)
        {
            adOferta.ActualizarFaseDePostulantes(postulantes, faseOferta);
        }


        public void CambiarEstado(int idOferta, string estadoOferta)
        {
            adOferta.CambiarEstado(idOferta, estadoOferta);
        }

        public List<VistaEmpresaOferta> ObtenerOfertasPorIdEmpresa(int idEmpresa)
        {
            List<VistaEmpresaOferta> lista = new List<VistaEmpresaOferta>();

            DataTable dtResultado = new DataTable();

            dtResultado = adOferta.ObtenerOfertasPorIdEmpresa(idEmpresa);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaEmpresaOferta vista = new VistaEmpresaOferta();

                vista.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                vista.IdEmpresa = Convert.ToInt32(fila["IdEmpresa"]);
                vista.FechaPublicacion = Convert.ToString(fila["FechaPublicacion"]);
                vista.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                vista.CantidadPostulantes = Convert.ToInt32(fila["Postulantes"]);
                vista.NombreEstado = Convert.ToString(fila["EstadoOferta"]);
                vista.NombreEstadoOfertaDescripcion = Convert.ToString(fila["EstadoOfertaDescripcion"]);
                vista.UsuarioPropietarioEmpresa = Convert.ToString(fila["UsuarioPropietarioEmpresa"]);
                vista.UsuarioPropietarioEmpresaCorreo = Convert.ToString(fila["UsuarioPropietarioEmpresaCorreo"]);
                vista.FechaFinOferta = Convert.ToDateTime(fila["FechaFinProceso"] == DBNull.Value ? "19000101" : fila["FechaFinProceso"]);

                lista.Add(vista);
            }

            return lista;
        }

        public DataTable AlertaCvAlumno(string Usuario)
        {
            return adOferta.AlertaCvAlumno(Usuario);
        }

        public DataTable AlertaCvAlumnoDia(string Usuario)
        {
            return adOferta.AlertaCvAlumnoDia(Usuario);
        }
        public DataTable AlertaCvAlumnoMes(string Usuario)
        {
            return adOferta.AlertaCvAlumnoMes(Usuario);
        }

        public void AsignarUsuario(int idOferta, string usuario)
        {
            adOferta.AsignarUsuario(idOferta, usuario);
        }

        public DataTable ObtenerDatosParaMensaje(int idOferta)
        {
            return adOferta.ObtenerDatosParaMensaje(idOferta);
        }

        public List<OfertaEstudio> ObtenerCarrerasDisponibles(int idOferta)
        {
            List<OfertaEstudio> listaCarrerasDisponibles = new List<OfertaEstudio>();

            return listaCarrerasDisponibles;
        }

        public List<OfertaEstudio> ObtenerCarrerasSeleccionadas(int idOferta)
        {
            List<OfertaEstudio> listaCarrerasSeleccionadas = new List<OfertaEstudio>();

            return listaCarrerasSeleccionadas;
        }

        private ListaValor copiarListaValor(ListaValor origen)
        {
            ListaValor destino = new ListaValor();

            destino.IdListaValor = origen.IdListaValor;
            destino.Valor = origen.Valor;

            return destino;
        }

        public void CompletarEncuesta(OfertaEncuesta encuesta)
        {
            if (encuesta.ContratadosOtros == null) encuesta.ContratadosOtros = "";
            
            adOferta.CompletarEncuesta(encuesta);
        }
    }

}
