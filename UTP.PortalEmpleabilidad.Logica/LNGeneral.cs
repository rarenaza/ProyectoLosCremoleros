using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
using System.Configuration;

namespace UTP.PortalEmpleabilidad.Logica
{
    public static class LNLog
    {        
        public static void InsertarLog(Error error)
        {
            try
            {
                ADGeneral adGeneral = new ADGeneral();
                adGeneral.InsertarLog(error);
            }
            catch
            {

            }
            
        }
    }
    public class LNGeneral
    {
        ADGeneral adGeneral = new ADGeneral();

        public DataTable Home_Departamento(int IDLista)
        {
            return adGeneral.Home_Departamento(IDLista);
        }

        public DataTable Modo_Presentacion(int IDLista)
        {
            return adGeneral.ObtenerListaValor(IDLista);
        }


        public List<Hunting> EmpresaHuntingBuscarSimple(string nombre, int nroPagina, int filasPorPagina)
        {
            List<Hunting> listaEjemplo = new List<Hunting>();


            DataTable dtResultado = adGeneral.EmpresaHuntingBuscarSimple(nombre, nroPagina, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Hunting vista = new Hunting();

                vista.IdAlumno = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                vista.Nombres = dtResultado.Rows[i]["Nombres"].ToString();
                vista.Apellidos = dtResultado.Rows[i]["Apellidos"].ToString();
                vista.Estudios = dtResultado.Rows[i]["Estudio"].ToString();
                vista.ValorEstadoEstudio = dtResultado.Rows[i]["ValorEstadoEstudio"].ToString();
                vista.ValorSectorEmpresarial = dtResultado.Rows[i]["ValorSectorEmpresarial"].ToString();
                vista.TotalMesesExperiencia = Convert.ToInt32(dtResultado.Rows[i]["TotalMesesExperiencia"]);
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }

        public List<Hunting> EmpresaHuntingBuscarAvanzada(string TipoDeEstudio, string Estudio, string EstadoEstudio,
            string SectorEmpresarial, int AnosExperiencia, string NombreCargo, string TipoInformacionAdicional, string Conocimiento, string Distrito,
            int nroPagina, int filasPorPagina)
        {
            List<Hunting> listaEjemplo = new List<Hunting>();


            DataTable dtResultado = adGeneral.EmpresaHuntingBuscarAvanzada(TipoDeEstudio,  Estudio,  EstadoEstudio,
             SectorEmpresarial,  AnosExperiencia,  NombreCargo,  TipoInformacionAdicional,  Conocimiento,  Distrito,
             nroPagina, filasPorPagina);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Hunting vista = new Hunting();

                vista.IdAlumno = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                vista.Nombres = dtResultado.Rows[i]["Nombres"].ToString();
                vista.Apellidos = dtResultado.Rows[i]["Apellidos"].ToString();
                vista.Estudios = dtResultado.Rows[i]["Estudio"].ToString();
                vista.ValorEstadoEstudio = dtResultado.Rows[i]["ValorEstadoEstudio"].ToString();
                vista.ValorSectorEmpresarial = dtResultado.Rows[i]["ValorSectorEmpresarial"].ToString();
                vista.TotalMesesExperiencia = Convert.ToInt32(dtResultado.Rows[i]["TotalMesesExperiencia"] == DBNull.Value ? 0 : dtResultado.Rows[i]["TotalMesesExperiencia"]);
                vista.CantidadTotal = Convert.ToInt32(dtResultado.Rows[i]["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }
        public DataTable Home_ListarDistritos(string IDListaValorPadre)
        {
            return adGeneral.Home_ListarDistritos(IDListaValorPadre);
        }

        public List<ReporteEquivalente> ObtenerReporteEquivalente()
        {
            List<ReporteEquivalente> lista = new List<ReporteEquivalente>();

            DataTable dtResultado = adGeneral.ObtenerReporteEquivalente();

            foreach (DataRow fila in dtResultado.Rows)
            {
                ReporteEquivalente item = new ReporteEquivalente();
                item.DatoOrigen = Convert.ToString(fila["DatoOrigen"]);
                item.DatoEquivalente = Convert.ToString(fila["DatoEquivalente"]);
                item.Orden = Convert.ToString(fila["Orden"]);               

                lista.Add(item);
            }

            return lista;
        
        }
        public List<ListaValor> ObtenerListaValor(int idLista)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValor(idLista);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
                item.IdLista = Convert.ToInt32(fila["IDLista"]);
                item.IdListaValor = Convert.ToString(fila["IDListaValor"]);
                item.IdListaValorPadre = Convert.ToString(fila["IDListaValorPadre"]); ;
                item.Valor = Convert.ToString(fila["Valor"]); ;
                item.DescripcionValor = Convert.ToString(fila["DescripcionValor"]); ;
                item.Icono = Convert.ToString(fila["Icono"]); ;
                item.Peso = Convert.ToInt32(fila["Peso"] == DBNull.Value ? 0 : fila["Peso"]); ;
                item.ValorUTP = Convert.ToString(fila["ValorUTP"]); ;
                item.EstadoValor = Convert.ToString(fila["EstadoValor"]); ;

                lista.Add(item);
            }

            return lista;
        }

        public DataTable ObtenerListaValor2(int idLista)
        {
            return adGeneral.ObtenerListaValor2(idLista);
        }

        public List<ListaValor> EMPRESA_BUSCAR_INFORMACIONADICIONAL(string IDListaValorPadre)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.EMPRESA_BUSCAR_INFORMACIONADICIONAL(IDListaValorPadre);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
          
                item.IdListaValor = Convert.ToString(fila["IDListaValor"]);
                item.IdListaValorPadre = Convert.ToString(fila["IDListaValorPadre"]); ;
                item.Valor = Convert.ToString(fila["Valor"]); ;

                lista.Add(item);
            }

            return lista;
        }


        /// <summary>
        /// Se agrega el parámetro de filtro para obtener los datos que contenga este valor.
        /// Por ejemplo: filtro = EMPRESA, filtra todos aquellos en donde la columna VALOR like '%FILTRO%'
        /// </summary>
        /// <param name="idLista"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<ListaValor> ObtenerListaValor(int idLista, string filtro)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValor(idLista);

            foreach (DataRow fila in dtResultado.Rows)
            {
                if (Convert.ToString(fila["IDListaValor"]).ToUpper().Contains(filtro.ToUpper()))
                {
                    ListaValor item = new ListaValor();
                    item.IdLista = Convert.ToInt32(fila["IDLista"]);
                    item.IdListaValor = Convert.ToString(fila["IDListaValor"]);
                    item.IdListaValorPadre = Convert.ToString(fila["IDListaValorPadre"]); ;
                    item.Valor = Convert.ToString(fila["Valor"]); ;
                    item.DescripcionValor = Convert.ToString(fila["DescripcionValor"]); ;
                    item.Icono = Convert.ToString(fila["Icono"]); ;
                    item.Peso = Convert.ToInt32(fila["Peso"] == DBNull.Value ? 0 : fila["Peso"]); ;
                    item.ValorUTP = Convert.ToString(fila["ValorUTP"]); ;
                    item.EstadoValor = Convert.ToString(fila["EstadoValor"]); ;

                    lista.Add(item);
                }
            }

            return lista;
        }

        public List<ListaValor> ObtenerListaValorPorIdPadre( string idListaPadre)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValorPorIdPadre(idListaPadre);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
                item.IdListaValor = Funciones.ToString(fila["IDListaValor"]);
                item.Valor = Funciones.ToString(fila["Valor"]); ;
                lista.Add(item);

            }

            return lista;
        }
      

        public void EnviarOfertaCorreosPendientes()
        {
            DataTable dt = adGeneral.ObtenerOfertaCorreoPendientes();

            foreach (DataRow fila in dt.Rows)
            {
                int idOfertaCorreo = Convert.ToInt32(fila["IdOfertaCorreo"]);
                string destinatario = Convert.ToString(fila["Destinatario"]);
                string asunto = Convert.ToString(fila["Asunto"]);
                string mensajeTexto = Convert.ToString(fila["Mensaje"]);

                Mensaje mensaje = new Mensaje();
                mensaje.DeUsuarioCorreoElectronico = ConfigurationManager.AppSettings["OfertaCorreoRemitente"];
                mensaje.ParaUsuarioCorreoElectronico = destinatario;
                mensaje.Asunto = asunto;
                mensaje.MensajeTexto = mensajeTexto;

                //Se envia el mensaje:
                LNCorreo.EnviarCorreo(mensaje);

                //Se marca el mensaje como enviado.
                int enviado = 1; //se envía el valor 1 para marcar el registro como enviado.
                adGeneral.ActualizarOfertaCorreo(idOfertaCorreo, enviado);
            }
        }

        /// <summary>
        /// Actualizar el estado de la oferta a finalizado si se cumplen las siguientes condiciones:
        /// 1. La oferta está activa.
        /// 2. La fecha de fin de recepción de cv es mayor a la fecha actual.
        /// </summary>
        public void FinalizarOfertasPorFechaDeRecepcion()
        {
            DataTable dtOfertas = adGeneral.FinalizarOfertaPorFechaCV();
            LNOferta lnOferta = new LNOferta();

            foreach (DataRow fila in dtOfertas.Rows)
            {
                int idOferta = Convert.ToInt32(fila["IdOferta"]);

                DataTable dtDatos = lnOferta.ObtenerDatosParaMensaje(Convert.ToInt32(idOferta));
                string para = Convert.ToString(dtDatos.Rows[0]["CorreoUsuarioEmpresa"]);
                string nombreEmpresa = Convert.ToString(dtDatos.Rows[0]["NombreEmpresa"]);
                string nombreOferta = Convert.ToString(dtDatos.Rows[0]["NombreOferta"]);

                Mensaje mensaje = new Mensaje();
                mensaje.DeUsuarioCorreoElectronico = ConfigurationManager.AppSettings["OfertaCorreoRemitente"];
                mensaje.ParaUsuarioCorreoElectronico = para;
                mensaje.MensajeTexto = "La oferta " + nombreOferta + " ha finalizado";
                mensaje.Asunto = nombreOferta + " - Oferta Finalizada";
                LNCorreo.EnviarCorreo(mensaje);
                

            }
        }

        public List<ListaValor> ObtenerListaValorOfertaEstudiosUTP(int idLista)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValor(idLista);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
                item.IdLista = Convert.ToInt32(fila["IDLista"]);
                item.IdListaValor = Convert.ToString(fila["IDListaValor"]);
                item.IdListaValorPadre = Convert.ToString(fila["IDListaValorPadre"]); ;
                item.Valor = Convert.ToString(fila["Valor"]); ;
                item.DescripcionValor = Convert.ToString(fila["DescripcionValor"]); ;
                item.Icono = Convert.ToString(fila["Icono"]); ;
                item.Peso = Convert.ToInt32(fila["Peso"] == DBNull.Value ? 0 : fila["Peso"]); ;
                item.ValorUTP = Convert.ToString(fila["ValorUTP"]); ;
                item.EstadoValor = Convert.ToString(fila["EstadoValor"]); ;

                //EDEEST = estudiante, EDEEGR = egresado, EDETIT = Titulado.
                if (item.IdListaValor == "EDEEST" || item.IdListaValor == "EDEEGR" || item.IdListaValor == "EDETIT")
                { 
                    lista.Add(item);
                }
            }

            return lista.OrderBy(m => m.Peso).ToList();
        }

        public List<ListaValor> ObtenerListaValorOfertaTipoEstudiosEspecificos(int idLista)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = adGeneral.ObtenerListaValor(idLista);

            foreach (DataRow fila in dtResultado.Rows)
            {
                ListaValor item = new ListaValor();
                item.IdLista = Convert.ToInt32(fila["IDLista"]);
                item.IdListaValor = Convert.ToString(fila["IDListaValor"]);
                item.IdListaValorPadre = Convert.ToString(fila["IDListaValorPadre"]); ;
                item.Valor = Convert.ToString(fila["Valor"]); ;
                item.DescripcionValor = Convert.ToString(fila["DescripcionValor"]); ;
                item.Icono = Convert.ToString(fila["Icono"]); ;
                item.Peso = Convert.ToInt32(fila["Peso"] == DBNull.Value ? 0 : fila["Peso"]); ;
                item.ValorUTP = Convert.ToString(fila["ValorUTP"]); ;
                item.EstadoValor = Convert.ToString(fila["EstadoValor"]); ;

                //TEDOCT = Doctorado, TEESCO = Escolar, TEPOST = Post-Grado, TETECN = Técnico, TEUNIV = Grado Universitario
                //Sólo se agregan los tipos de estudio que son distintos a Escolar y Universitario.
                if (item.IdListaValor != "TEESCO" && item.IdListaValor != Constantes.TIPO_ESTUDIO_PRINCIPAL)
                {
                    lista.Add(item);
                }
            }

            return lista.OrderBy(m => m.Peso).ToList();
        }

        public void EnviarCorreoOfertasVencidas()
        {
            DataTable dtOfertas = adGeneral.OfertasVencidas();            
            LNMensaje lnMensaje = new LNMensaje();
            int idOferta = 0;
            string paraCorreo, paraNombre, nombreOferta = "";
            
            foreach (DataRow fila in dtOfertas.Rows)
            {
                idOferta = Convert.ToInt32(fila["IdOferta"]);
                paraCorreo = fila["CorreoElectronico"].ToString();
                paraNombre = fila["CreadoPor"].ToString();
                nombreOferta = fila["CargoOfrecido"].ToString();

                Mensaje mensaje = new Mensaje();
                mensaje.DeUsuarioCorreoElectronico = ConfigurationManager.AppSettings["OfertaCorreoRemitente"];
                mensaje.ParaUsuarioCorreoElectronico = paraCorreo;
                mensaje.MensajeTexto = "Estimado Usuario Empresa:</br>" +
                "Le recordamos que tiene una Oferta Laboral pendiente por cerrar.</br>" +
                "Por favor, ingrese a la Oferta Laboral en el Portal y en la sección Administración de la Oferta haga clic en el botón 'Cerrar Oferta'.</br></br>" +
                "Muchas gracias,</br>" +
                "Área de Empleabilidad";
                mensaje.Asunto = nombreOferta + " - Cierre de Oferta";

                mensaje.IdOfertaMensaje = idOferta;
                mensaje.FechaEnvio = DateTime.Now;
                mensaje.IdEvento = 0;
                mensaje.EstadoMensaje = "MSJNOL";  //Pendiente de ser leido
                mensaje.DeUsuario = "UTP";
                mensaje.CreadoPor = "UTP";
                mensaje.ParaUsuario = paraNombre;

                lnMensaje.Insertar(mensaje);
                
            }
        }
    }
}
