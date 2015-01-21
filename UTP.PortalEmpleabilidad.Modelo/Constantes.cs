using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public static class Constantes
    {
        public static string ESTADO_EMPRESA_PENDIENTE_DE_APROBACION = "EMPRRV";

        //Estado del alumno que postula:  CV's pendientes de revisión.
        public static string ESTADO_ALUMNO_CV_PENDIENTE_DE_REVISION = "OFFAPR";   //Fase de una oferta.


        #region Estados de la Empresa

        public static string ESTADO_EMPRESA_PENDIENTE_APROBACION = "EMPRRV";

        #endregion

        #region ID Lista

        public static int IDLISTA_TIPO_DOCUMENTO = 1;
        public static int IDLISTA_SEXO = 2;
        public static int IDLISTA_DE_CARRERA = 5;
        public static int IDLISTA_TIPO_DE_ESTUDIO = 7;
        public static int IDLISTA_SECTOR_EMPRESARIAL = 8;
        public static int IDLISTA_TIPO_CARGO = 9;
        public static int IDLISTA_TIPO_OTRO_CONOCIMIENTO = 10;
        public static int IDLISTA_NIVEL_CONOCIMIENTOS = 16;
        public static int IDLISTA_PAIS = 17;
        public static int IDLISTA_NRO_EMPLEADOS = 19;
        public static int IDLISTA_ESTADO_EMPRESA = 20;
        public static int IDLISTA_TIPO_LOCACION = 21;
        public static int IDLISTA_ESTADO_LOCACION = 22;
        public static int IDLISTA_ESTADO_OFERTA = 28;
        public static int IDLISTA_TIPO_TRABAJO = 29;
        public static int IDLISTA_TIPO_CONTRATO = 30;
        public static int IDLISTA_FASE_OFERTA = 32;
        public static int IDLISTA_ESTADO_USUARIO = 39;
        public static int IDLISTA_ROL_USUARIO = 40;

        public static int IDLISTA_ESTADO_DEL_ESTUDIO = 43;
        public static int IDLISTA_OFERTA_RECIBECORREOS = 45;
        public static int IDLISTA_DISTRITO_PERU = 49;
        public static int IDLISTA_AREA_EMPRESA = 50;
              

        #endregion

        public const string MSJ_CAMPO_OBLIGATORIO = "Este campo es obligatorio";

        

        #region Estado del Usuario
        //IdLista = 39. Estado del Usuario
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_ACTIVO = "USEMAC";
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_FINALIZADO = "USEMFI";
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_NOACTIVO = "USEMNO";
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_PENDIENTEDEVALIDACION = "USEMPE";

        #endregion

        #region Roles del Usuario
        //IdLista = 40. Roles del Usuario

        public static string LISTAVALOR_ROL_DEL_USUARIO_ADMINISTRADOREMPRESA = "ROLADE";
        public static string LISTAVALOR_ROL_DEL_USUARIO_ADMINISTRADOR = "ROLADM";
        public static string LISTAVALOR_ROL_DEL_USUARIO_USUARIOEMPRESACORREO = "ROLECO";
        public static string LISTAVALOR_ROL_DEL_USUARIO_USUARIOEMPRESAVISTA = "ROLEVI";
        public static string LISTAVALOR_ROL_DEL_USUARIO_SUPERVISOREMPRESA = "ROLSUE";
        public static string LISTAVALOR_ROL_DEL_USUARIO_SUPERVISORUTP = "ROLSUT";
        public static string LISTAVALOR_ROL_DEL_USUARIO_USUARIOEMPRESA = "ROLUEM";
        public static string LISTAVALOR_ROL_DEL_USUARIO_USUARIOUTP = "ROLUTP";

        #endregion

        #region Mensajes

        public const string MENSAJES_EMPRESA_INDEX = "EMPRESA_INDEX";
        public const string MENSAJES_ALUMNO_INDEX = "ALUMNO_INDEX";
        public const string MENSAJES_EMPRESA_OFERTA = "EMPRESA_OFERTA";
        public const string MENSAJES_ALUMNO_OFERTA = "ALUMNO_OFERTA";
        public const string MENSAJES_UTP_INDEX = "UTP_INDEX";
        public const string MENSAJES_UTP_EMPRESA = "UTP_EMPRESA";
        public const string MENSAJES_UTP_ALUMNO = "UTP_ALUMNO";
        public const string MENSAJES_UTP_OFERTA = "UTP_OFERTA";
        public const string MENSAJES_EMPRESA_CONTACTO = "EMPRESA_CONTACTO";
        public const string MENSAJES_ALUMNO_CONTACTO = "ALUMNO_CONTACTO";
        public const string MENSAJES_INICIO = "INICIO";
        public const string MENSAJES_EMPRESA_EVENTO = "EMPRESA_EVENTO";
        public const string MENSAJES_ALUMNO_EVENTO = "ALUMNO_EVENTO";
        public const string MENSAJES_UTP_EVENTO = "UTP_EVENTO";
        public const string MENSAJES_EMPRESA_HUNTING = "EMPRESA_HUNTING";

        #endregion

        #region Paginación

        public const int FILAS_POR_PAGINA = 10;

        #endregion

    }
}
