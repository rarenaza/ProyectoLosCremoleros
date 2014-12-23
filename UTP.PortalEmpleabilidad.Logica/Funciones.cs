using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class Funciones
    {
        public static string ToString(object obj)
        {
            if (obj == null) return string.Empty;
            return obj.ToString().Trim();
        }

        public static int ToInt(object obj)
        {
            int result;
            int.TryParse(ToString(obj), out result);
            return result;
        }

        public static Int16 ToIntShort(object obj)
        {
            Int16 result;
            Int16.TryParse(ToString(obj), out result);
            return result;
        }

        public static float ToFloat(object obj)
        {
            float result;
            float.TryParse(ToString(obj), out result);
            return result;
        }

        public static decimal ToDecimal(object obj)
        {
            decimal result;
            decimal.TryParse(ToString(obj), out result);
            return result;
        }

        public static bool ToBoolean(object obj)
        {
            bool result;
            bool.TryParse(ToString(obj), out result);
            return result;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime dtime;
            DateTime.TryParse(ToString(obj), out dtime);
            return dtime;
        }

        public static byte[] ToBytes(object obj)
        {
            byte[] binary = null;
            if (!DBNull.Value.Equals(obj))
            {
                binary = (byte[])obj;
            }
            return binary;
        }


    }
}
