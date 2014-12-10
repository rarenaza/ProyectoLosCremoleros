using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace UTPPrototipo.Common
{
    public static class FormatHelper
    {
        public static string FormatoFrecuencia(this HtmlHelper helper, DateTime fecha)
        {
            DateTime fechaactual = DateTime.Now;
            TimeSpan ts = fechaactual - fecha;
            string fechatexto = "01/01/2014";
            if (ts.Seconds <= 15 && ts.Minutes == 0 && ts.Hours == 0 && ts.Days == 0)
            {
                fechatexto = "Hace un momento";
            }
            else
            {
                if (ts.Seconds <= 59 && ts.Minutes == 0 && ts.Hours == 0 && ts.Days == 0)
                {
                    fechatexto = "Hace " + ts.Seconds + " segundo";
                }
                else
                {
                    if (ts.Minutes <= 59 && ts.Hours == 0 && ts.Days == 0)
                    {
                        fechatexto = "Hace " + ts.Minutes + " minutos";
                    }
                    else
                    {
                        if (ts.Hours <= 23 && ts.Days == 0)
                        {
                            fechatexto = "Hace " + ts.Hours + " horas";
                        }
                        else
                        {
                            if (ts.Days <= 30)
                            {
                                fechatexto = "Hace " + ts.Days + " dias";
                            }
                            else
                            {
                                fechatexto = fecha.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                }
            }




            return fechatexto;
        }

        public static string Cumple(this HtmlHelper helper,bool estado){
            string cumple=string.Empty;
            if (estado)
                cumple = "Si Cumple";
            else
                cumple = "No Cumple";
            return cumple;
        }
        public static string CompatibilidaOferta(this HtmlHelper helper, int compatiblidad)
        {
            string fechatexto = "01/01/2014";

            return fechatexto;
        }


    }

}