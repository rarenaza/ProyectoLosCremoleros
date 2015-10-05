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
        public static int IDLISTA_Departamento = 47;
        public static int IDLISTA_Provincia = 48;
        public static int IDLISTA_FUENTE_CONVENIO = 41;
        public static int IDLISTA_ESTADO_CONVENIO = 42;

        public static int IDLISTA_OFERTA_CALIFICACION_ENCUESTA = 51; //Verificar este nro en bd de UTP Producción.
        public static int IDLISTA_TIPO_TRABAJO_UTP = 52;
        public static int IDLISTA_MODO_PRESENTACION = 53;
        #endregion

        public const string MSJ_CAMPO_OBLIGATORIO = "Este campo es obligatorio";

        

        #region Estado del Usuario
        //IdLista = 39. Estado del Usuario
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_ACTIVO = "USEMAC";
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_FINALIZADO = "USEMFI";
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_NOACTIVO = "USEMNO";
        public static string LISTAVALOR_ESTADO_DEL_USUARIO_PENDIENTEDEVALIDACION = "USEUTP";

        #endregion

        #region Roles del Usuario
        //IdLista = 40. Roles del Usuario

        public static string LISTAVALOR_ROL_DEL_USUARIO_ADMINISTRADOREMPRESA = "ROLEAD";
        public static string LISTAVALOR_ROL_DEL_USUARIO_ADMINISTRADOR = "ROLADM";
        public static string LISTAVALOR_ROL_DEL_USUARIO_USUARIOEMPRESAVISTA = "ROLECO";
        public static string LISTAVALOR_ROL_DEL_USUARIO_SUPERVISORUTP = "ROLSUT";
        public static string LISTAVALOR_ROL_DEL_USUARIO_USUARIOEMPRESA = "ROLEUS";
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

        //Por defecto la paginacion de toda la aplicaicon es de 10 en 10, asi que se usa una constante, si existe una pagina que sea diferente entonces
        //ya no se usa este campo y se utiliza el que  sea necesario.
        public const int FILAS_POR_PAGINA = 10;
        public const int FILAS_POR_PAGINA_UTP = 20; //Paginación utilizada para el Módulo Administrador UTP.

        #endregion

        #region Estados de la oferta

        public static string OFERTA_ESTADO_ACTIVA = "OFERAC";
        public static string OFERTA_ESTADO_ENCONSTRUCCION = "OFERCO";
        public static string OFERTA_ESTADO_FINRECEPCIONCVS = "OFERCV";
        public static string OFERTA_ESTADO_FINALIZADA = "OFERFI";
        public static string OFERTA_ESTADO_PENDIENTEACTIVACION = "OFERPR";
        public static string OFERTA_ESTADO_SUSPENDIDA = "OFERSU";
        public static string OFERTA_ESTADO_BORRADOR = "OFERBO";

        #endregion

        #region Tipos de Estudio
        public static string TIPO_ESTUDIO_UNIVERSITARIO = "TEUNIV";
        public static string TIPO_ESTUDIO_PRINCIPAL = "TETECN";
        #endregion

        #region Colores Pie

        public static ColorPie ColorVino = new ColorPie { Color = "#B10400", Highlight = "#F3010A" };

        public static ColorPie ColorAzulPastel = new ColorPie { Color = "#3CACC9", Highlight = "#0065FF" };
        public static ColorPie ColorNaranja = new ColorPie { Color = "#E99522", Highlight = "#FFC900" };
        public static ColorPie ColorVerdeAgua = new ColorPie { Color = "#0E9E95", Highlight = "#8FBC8F" };
        public static ColorPie ColorVerdeAmarillo = new ColorPie { Color = "#C0BA00", Highlight = "#9ACD32" };
        public static ColorPie ColorGris = new ColorPie { Color = "#818286", Highlight = "#A9A9A9" };
        public static ColorPie ColorAzulOscuro = new ColorPie { Color = "#2B56B1", Highlight = "#6495ED" };

        public static ColorPie ColorPiel = new ColorPie { Color = "#FF9572", Highlight = "#FFD1B7" };
        public static ColorPie ColorMarron = new ColorPie { Color = "#9B6749", Highlight = "#998280" };

        public static ColorPie ColorAzul = new ColorPie { Color = "#191970", Highlight = "#0065FF" };
        public static ColorPie ColorRojo = new ColorPie { Color = "#FF0000", Highlight = "#FF7200" };
        public static ColorPie ColorCeleste = new ColorPie { Color = "#82F0FF", Highlight = "#CCFFF8" };
        
        public static ColorPie ColorCrema = new ColorPie { Color = "#FFF9A7", Highlight = "#FFF6ED" };
        public static ColorPie ColorVerde = new ColorPie { Color = "#4CFF00", Highlight = "#CBFF72" };
        public static ColorPie ColorTurquesa = new ColorPie { Color = "#60FCDC", Highlight = "#B6F9E6" };
        
        public static ColorPie ColorMorado = new ColorPie { Color = "#B200FF", Highlight = "#D468FF" };
        public static ColorPie ColorRosado = new ColorPie { Color = "#FF4CDC", Highlight = "#FF4CDC" };
        public static ColorPie ColorPlomo = new ColorPie { Color = "#A0A0A0", Highlight = "#C0C0C0" };
        public static ColorPie ColorAmarillo = new ColorPie { Color = "#FFFF00", Highlight = "#FFF9AA" };
        
        public static ColorPie ColorVerdeClaro = new ColorPie { Color = "#A8FFA8", Highlight = "#DBFFD6" };

        public static List<ColorPie> COLORES_PIE = new List<ColorPie> { ColorVino, ColorAzulPastel, ColorNaranja, ColorVerdeAgua, ColorVerdeAmarillo, ColorGris, ColorAzulOscuro, ColorPiel, ColorMarron, ColorAzul, ColorRojo, ColorCeleste, ColorCrema, ColorVerde, ColorTurquesa, ColorVino, ColorRosado, ColorPlomo, ColorMorado,ColorAmarillo, ColorVerdeClaro };
        

        #endregion

        #region contenidos
        public static string CONTENIDO_NOTICIAS_EVENTOS = "1";
        public static string CONTENIDO_AREA_EMPLEABILIDAD = "2";
        public static string CONTENIDO_SERVICIOS = "3";
        public static string CONTENIDO_NOTICIAS = "4";
        public static string CONTENIDO_TESTIMONIOS = "5";
        public static string CONTENIDO_EMPLEADORES = "6";
        public static string CONTENIDO_EVENTOS = "7";
        public static string CONTENIDO_EMPRESAS = "8";
        public static string CONTENIDO_ALUMNOS = "9";
        public static string CONTENIDO_STAFF = "10";
        public static string CONTENIDO_SE_VIENE = "11";
        public static string CONTENIDO_EMPRESAS_OPINAN = "12";
        public static string CONTENIDO_EXITO_IMPARABLE = "13";
        public static string CONTENIDO_NOS_CUENTAN = "14";
        #endregion

        public static string NOMBRE_UTP = "Instituto IDAT";
        public static string CORREO_PRINCIPAL = "empleos@idat.edu.pe";

        public const string TICKET_EMITIDO = "EVTKEM";

        public static string Estado_Activo_ListaValor = "Activo";
        public static string Estado_NoActivo_ListaValor = "No Activo";
    }
}
