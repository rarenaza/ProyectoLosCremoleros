using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace UTPPrototipo.Common
{
    public class DescargaResult : ActionResult
    {
        public string FileName { get; set; }
        public byte[] Binario { get; set; }

        public DescargaResult(string Nombre, byte[] archivo)
        {
            FileName = Nombre;
            Binario = archivo;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + FileName);
            context.HttpContext.Response.BinaryWrite(Binario);
        }
    }
}