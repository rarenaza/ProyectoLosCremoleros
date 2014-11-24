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
                    //Realizar la busqueda del usuario empresa en la BD y obtener su informacion
                    //El metodo que va a la BD debe devolver el tipo Sesion.
                    //Para propositos de desarrollo se crea temporalmente esa variable en la capa de presentación.

                    Ticket ticket = new Ticket();
                    ticket.UsuarioNombre = usuario.NombreUsuario;
                    ticket.IdEmpresa = 1;

                    Session["Ticket"] = ticket;

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