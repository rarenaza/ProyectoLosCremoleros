using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
namespace UTPPrototipo.Common
{
    public class Util
    {
        public static string ObtenerSettings(string key)
        {
            string valor = string.Empty;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                valor = ConfigurationManager.AppSettings[key].ToString(); 
            }
            return valor;
        }
    }
}