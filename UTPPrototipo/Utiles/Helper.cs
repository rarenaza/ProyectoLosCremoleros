using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.Web.UI;

namespace UTPPrototipo.Utiles
{
    public static class Helper
    {
        public static List<SelectListItem> DevolverListaItems(List<ListaValor> lista)
        {
            List<SelectListItem> items= new List<SelectListItem>();

            foreach (var tipo in lista)
            {
                SelectListItem nuevoItem = new SelectListItem() { Text = tipo.Valor, Value = tipo.IdListaValor };
                items.Add(nuevoItem);
            }

            return items;
        }

        const string key = "password";

        public static string Encriptar(string input)
        {
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(key));

            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = TDESKey; // UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace('+', '-').Replace('/', '_').Replace('=', ','); ;
        }

        public static string Desencriptar(string input)
        {
            input = input.Replace('-', '+').Replace('_', '/').Replace(',', '=');
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(key));

            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = TDESKey; // UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static void Export2ExcelDownload(DataTable DT, string filename)
        {
            HttpResponse Response = HttpContext.Current.Response;
            Response.Clear();
            Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}.xls", filename));
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "";
          
            //Table table = new Table();

            GridView E = new GridView();                      
            E.DataSource = DT;
            E.DataBind();

            foreach (GridViewRow row in E.Rows)
                PrepareControlForExport(row);               

            E.RenderControl(new System.Web.UI.HtmlTextWriter(Response.Output));

            Response.End();
        }

        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "1" : "0"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
 
            }
        }
    }

}