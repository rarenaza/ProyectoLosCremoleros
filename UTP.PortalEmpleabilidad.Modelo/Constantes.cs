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

        public static int IDLISTA_TIPO_CARGO = 9;
        public static int IDLISTA_TIPO_OTRO_CONOCIMIENTO = 10;
        public static int IDLISTA_TIPO_TRABAJO = 29;
        public static int IDLISTA_TIPO_CONTRATO = 30;
        public static int IDLISTA_FASE_OFERTA = 32;


        #endregion

    }
}
