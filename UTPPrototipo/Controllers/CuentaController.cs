using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class CuentaController : Controller
    {
        //
        // GET: /Cuenta/

        public ActionResult Ingresar()
        {
            return View();
        }

        public ActionResult Autenticar()
        {

            return PartialView("_Login");
        }

        [HttpPost]
        public ActionResult Autenticar(Usuario usuario)
        {
            if (usuario.NombreUsuario == "alumno")
            {
                return RedirectToAction("Index", "Alumno");
            }
            else
                if (usuario.NombreUsuario == "empresa")
                {
                    return RedirectToAction("Index", "Empresa");
                }
                else
                    if (usuario.NombreUsuario == "utp")
                    {

                    }

            return PartialView("_Login");
        }
	}
}