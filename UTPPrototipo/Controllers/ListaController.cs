using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Controllers
{
    public class ListaController : Controller
    {
        LNLista ad = new LNLista();

        // GET: Lista
        //public ActionResult Index()
        //{
        //    List<Lista> lista = new List<Lista>();

        //    lista = ad.Evento_Mostrar();           

        //    return View(lista); 
        //}
        //public ActionResult Lista_insertar()
        //{
    
        //    Lista listavalor = new Lista();
        //     ad.Lista_insertar(listavalor.IDLista, listavalor.NombreLista, listavalor.DescripcionLista, listavalor.Modificable, listavalor.Creadopor);
        //    return View();
        //}
        //public ActionResult Lista_Buscar()
        //{
        //    List<Lista> lista = new List<Lista>();
        //    Lista  listaValor = new Lista();
                 
        //    return View(ad.Lista_Buscar(listaValor.IDLista)); 
            
        //}
       
    }
}