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

        #endregion

        public const string MSJ_CAMPO_OBLIGATORIO = "Este campo es obligatorio";
    }
}
