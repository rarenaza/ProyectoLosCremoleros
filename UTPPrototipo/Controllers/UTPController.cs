using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.UTP;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Alumno;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Convenio;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
//using UTP.PortalEmpleabilidad.Modelo.Vistas.Eventos;
using UTPPrototipo.Common;
using UTPPrototipo.Models;
using UTPPrototipo.Models.ViewModels.Contenido;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Models.ViewModels.UTP;

namespace UTPPrototipo.Controllers
{

    
    [VerificarSesion, LogPortal]
    public class UTPController : Controller
    {
       
        LNContenido ln = new LNContenido();
        LNAutenticarUsuario lnAutenticar = new LNAutenticarUsuario();
        LNUTP lnUtp = new LNUTP();
        LNEmpresaListaOferta lnEmpresa = new LNEmpresaListaOferta();
        LNOferta lnoferta = new LNOferta();
        LNUTPAlumnos lnutpalumno = new LNUTPAlumnos();
        LNAlumno lnalumno = new LNAlumno();
        LNEvento lnEventos = new LNEvento();
        // GET: UTP
        public ActionResult Index()
        {

            return View();
                       
     
        }


        public ActionResult Portal()
        {
            
            LNContenido ln = new LNContenido();

            DataTable dtresultado = ln.ContenidoMenu_Mostrar();

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
            {
                string nombre = dtresultado.Rows[i]["Titulo"].ToString();
                string valor = dtresultado.Rows[i]["IdMenu"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }
            ViewData["ContenidoMenu"] = li;

            return View();

        }

        public ActionResult ResultadoBusquedaContenidoUTP(int Menu = 0)
        {
            List<Contenido> lista = new List<Contenido>();
            DataTable dtResultado = ln.Contenido_ObtenerPorCodMenu(Menu);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido contenido = new Contenido();
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
                contenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                contenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                contenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();
                //contenido.Imagen = (byte[])dtResultado.Rows[i]["Imagen"];
                contenido.Imagen = dtResultado.Rows[0]["Imagen"] == DBNull.Value ? null : (byte[])dtResultado.Rows[0]["Imagen"];
                contenido.TituloMenu = dtResultado.Rows[i]["Menu"].ToString();
                lista.Add(contenido);
            }

            return PartialView("_ListaUTPContenido", lista);
        }

          
        public FileResult Imagen(int id)
        {
            const string alternativePicturePath = @"/img/sinimagen.jpg";

            List<Contenido> lista = new List<Contenido>();

            DataTable dtResultado = ln.Contenido_Mostrar_imagen();

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido contenido = new Contenido();
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
                contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();

                //06ENE Aldo Chocos: Validación de imagen null.
                if (dtResultado.Rows[i]["Imagen"] != null && dtResultado.Rows[i]["Imagen"] != DBNull.Value)
                    contenido.Imagen = (byte[])dtResultado.Rows[i]["Imagen"];

                //contenido.Imagen = Encoding.UTF8.GetBytes(dtResulatdo.Rows[i]["Imagen"].ToString());

                lista.Add(contenido);
            }

            Contenido producto = lista.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;
            if (producto.Titulo == "ABC")
            {
                int a = 0;
            }
            if (producto != null && producto.Imagen != null && producto.Imagen.Length > 1)
            {
                stream = new MemoryStream(producto.Imagen);
            }
            else
            {
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }


        public ActionResult Empresas()
        {

            //VistaEmpresaListaOpciones utp = new VistaEmpresaListaOpciones();
            VistaEmpresListarOfertas utp = new VistaEmpresListarOfertas();


            LNGeneral lngeneral = new LNGeneral();

            utp.ListaEstado = lngeneral.ObtenerListaValor(20);
            utp.Listasector = lngeneral.ObtenerListaValor(8);

            //Estado de la empresa
            List<SelectListItem> listItemsEstado = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.ListaEstado)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsEstado.Add(item);
            }

            //Sector empresarial
            List<SelectListItem> listItemSector = new List<SelectListItem>();
            foreach (ListaValor entidad in utp.Listasector)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemSector.Add(item);
            }



            //Lista de Combos

            ViewBag.ListaEstado = listItemsEstado;
            ViewBag.ListaSector = listItemSector;

            return View(utp);                 

        }
            
        public ActionResult BusquedaSimpleEmpresasActivas(VistaEmpresListarOfertas entidad)
        {

            List<EmpresaListaEmpresa> lista = lnUtp.Empresa_ObtenerPorNombre(entidad.PalabraClave == null ? "" : entidad.PalabraClave,
                                                            entidad.nroPaginaActual, Constantes.FILAS_POR_PAGINA);

            //Datos para la paginación.
            //Una ves traido la info de la bd, se llenan estos campos del objeto Paginacion
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            ViewBag.TipoBusqueda = "Simple";

            return PartialView("_ResultadoBusquedaEmpresas", lista);

        }


        public ActionResult BusquedaAvanzadaEmpresas(VistaEmpresListarOfertas entidad)
        {
            List<EmpresaListaEmpresa> lista = lnUtp.EmpresaBusquedaAvanzada(entidad.NombreComercial == null ? "" : entidad.NombreComercial,
                                                                            entidad.IdEstadoEmpresa == null ? "" : entidad.IdEstadoEmpresa,
                                                                            entidad.IdSector == null ? "" : entidad.IdSector,
                                                                            entidad.RazonSocial == null ? "" : entidad.RazonSocial,
                                                                            entidad.RUC == null ? "" : entidad.RUC,
                                                                            entidad.Ciudad == null ? "" : entidad.Ciudad,
                                                                            entidad.nroPaginaActual,
                                                                            Constantes.FILAS_POR_PAGINA);


            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            ViewBag.TipoBusqueda = "Avanzada";

            return PartialView("_ResultadoBusquedaEmpresas", lista);

        }

        public ActionResult Eventos()
        {
            return View();
        }
        public ActionResult BusquedaEventos(string SearchString, int nroPaginaActual = 1)
        //public ActionResult Eventos(string SearchString)
        {

            string palabraClave = SearchString == null ? "" : SearchString;
            List<VistaObtenerEventosUTP> listaEjemplo = new List<VistaObtenerEventosUTP>();

            DataTable dtResultado = lnUtp.UTP_ObtenerEventosObtenerBuscar(palabraClave, nroPaginaActual, Constantes.FILAS_POR_PAGINA);
            //DataTable dtResultado = lnUtp.UTP_ObtenerEventosObtenerBuscar(palabraClave);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaObtenerEventosUTP vista = new VistaObtenerEventosUTP();

                vista.IdEvento = Convert.ToInt32(fila["IdEvento"]);
                vista.NombreEvento = Convert.ToString(fila["NombreEvento"]);
                vista.LugarEvento = Convert.ToString(fila["LugarEvento"]);
                //vista.Expositor = Convert.ToString(fila["Expositor"]);
                vista.DireccionEvento = Convert.ToString(fila["DireccionEvento"]);
                vista.AsistentesEsperados = Convert.ToInt32(fila["AsistentesEsperados"]);
                vista.FechaEvento = Convert.ToString(fila["FechaEvento"]);
                vista.CantidadTotal = Convert.ToInt32(fila["CantidadTotal"]);

                listaEjemplo.Add(vista);
            }

            //Datos para la paginación.
            int cantidadTotal = listaEjemplo.Count() == 0 ? 0 : listaEjemplo[0].CantidadTotal;

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            return PartialView("_ListaUtpEvento", listaEjemplo);
            //return View(listaEjemplo);

        }

        public ActionResult Alumnos()
        {
     
            VistaAlumno utpAlumno = new VistaAlumno();

            LNGeneral lngeneral = new LNGeneral();

            //Sector empresarial

            //Busca Lista Sector Empresarial
            utpAlumno.ListaSectorEmpresarial = lngeneral.ObtenerListaValor(8);
            List<SelectListItem> listItemSector = new List<SelectListItem>();
            foreach (ListaValor entidad in utpAlumno.ListaSectorEmpresarial)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemSector.Add(item);
            }

            //Sexo

            //Busca Lista Sexo
            utpAlumno.ListaSexo = lngeneral.ObtenerListaValor(2);
            List<SelectListItem> listItemSexo = new List<SelectListItem>();
            foreach (ListaValor entidad in utpAlumno.ListaSexo)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemSexo.Add(item);
            }
            //Tipo de Estudio

            //Busca Lista Tipo de Estudio
            utpAlumno.ListaTipoEstudio = lngeneral.ObtenerListaValor(7);
            List<SelectListItem> listItemTipoEstudio = new List<SelectListItem>();
            foreach (ListaValor entidad in utpAlumno.ListaTipoEstudio)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemTipoEstudio.Add(item);
            }

            //Tipo Informacion Adicional

            //Busca Lista Tipo informacion Adicional
            utpAlumno.ListaInformacionAdicional = lngeneral.ObtenerListaValor(10);
            List<SelectListItem> listItemTipoInformacionAdicional = new List<SelectListItem>();
            foreach (ListaValor entidad in utpAlumno.ListaInformacionAdicional)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemTipoInformacionAdicional.Add(item);
            }

            //Estado de Estudio

            //Busca Estado de Estudio
            utpAlumno.ListaEstadoEstudio = lngeneral.ObtenerListaValor(43);
            List<SelectListItem> listItemEstadoEstudio = new List<SelectListItem>();
            foreach (ListaValor entidad in utpAlumno.ListaEstadoEstudio)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor;
                item.Value = entidad.IdListaValor.ToString();
                listItemEstadoEstudio.Add(item);
            }
              

            //Lista de Combos

   
            ViewBag.ListaSectorEmpresarial = listItemSector;
            ViewBag.ListaSexo = listItemSexo;
            ViewBag.ListaTipoEstudio = listItemTipoEstudio;
            ViewBag.ListaInformacionAdicional = listItemTipoInformacionAdicional;
            ViewBag.ListaEstadoEstudio = listItemEstadoEstudio;
            return View(utpAlumno);                 
                       

            //return View();
        }
    
        public ActionResult BusquedaAlumnos(VistaAlumno entidad)
        {
                   
            List<AlumnoUTP> lista = lnUtp.UTP_ObtenerUltimosAlumnos(entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.nroPaginaActual, Constantes.FILAS_POR_PAGINA);
                      
            
            //Datos para la paginación.
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            //ViewBag.TipoPaginacion = "Simple";
            return PartialView("_ListaUTPAlumnos", lista);
            //return View(listaEjemplo);

        }
        

        public ActionResult BusquedaAlumnosAvanzada(VistaAlumno entidad)
        {

            List<AlumnoUTP> lista = lnUtp.UTP_ObtenerUltimosAlumnosAvanzada(entidad.Carrera == null ? "" : entidad.Carrera,
                                                                            entidad.Ciclo == null ? "" : entidad.Ciclo,
                                                                            entidad.SectorEmpresarial == null ? "" : entidad.SectorEmpresarial,
                                                                            entidad.Alumno == null ? "" : entidad.Alumno,
                                                                            entidad.Sexo == null ? "" : entidad.Sexo,
                                                                            entidad.Distrito == null ? "" : entidad.Distrito,
                                                                            entidad.TipoEstudio == null ? "" : entidad.TipoEstudio,
                                                                            entidad.Conocimientos == null ? "" : entidad.Conocimientos,
                                                                            entidad.EstadoEstudio == null ? "" : entidad.EstadoEstudio,
                                                                            entidad.nroPaginaActual, 
                                                                            Constantes.FILAS_POR_PAGINA);


            //Datos para la paginación.
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            //Esto van en todas las paginas 
            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            ViewBag.TipoPaginacion = "Avanzada";
            return PartialView("_ListaUTPAlumnos", lista);
            //return View(listaEjemplo);

        }

        public ActionResult Ofertas()
        {
            VistaOferta oferta = new VistaOferta();
            LNGeneral lngeneral = new LNGeneral();
            oferta.ListaTipoCargo = lngeneral.ObtenerListaValor(9);

            //Tipo de Cargo
            List<SelectListItem> listItemsTipoCargo = new List<SelectListItem>();
            foreach (ListaValor entidad in oferta.ListaTipoCargo)
            {
                SelectListItem item = new SelectListItem();
                item.Text = entidad.Valor.ToUpper();
                item.Value = entidad.IdListaValor.ToString();
                listItemsTipoCargo.Add(item);
            }

            //Lista de Combos

            ViewBag.ListaTipoCargo = listItemsTipoCargo;

            return View(oferta);

        }

        public ActionResult BuscarOfertas(VistaOferta entidad)
        {

            List<OfertaUTP> lista = lnUtp.UTP_ObtenerOfertasporActivar(entidad.PalabraClave == null ? "" : entidad.PalabraClave, entidad.nroPaginaActual, Constantes.FILAS_POR_PAGINA);

            //Datos para la paginación.
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            ViewBag.TipoPaginacion = "Simple";
            return PartialView("_ListaUTPOfertas", lista);
        }

        public ActionResult UTP_ObtenerofertasAvanzada(VistaOferta entidad)
        {

            List<OfertaUTP> lista = lnUtp.UTP_ObtenerofertasAvanzada(entidad.CargoOfrecido == null ? "" : entidad.CargoOfrecido,
                                                                     entidad.NombreComercial == null ? "" : entidad.NombreComercial,
                                                                      entidad.IdTipoCargoutp == null ? "" : entidad.IdTipoCargoutp,
                                                                     entidad.nroPaginaActual,
                                                                     Constantes.FILAS_POR_PAGINA);


            //Datos para la paginación.
            int cantidadTotal = lista.Count() == 0 ? 0 : lista[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = entidad.nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;
            ViewBag.TipoPaginacion = "Avanzada";

            return PartialView("_ListaUTPOfertas", lista);
        }

        [HttpPost]
        public JsonResult ListarNombreEmpresa(string query)
        {
            //aqui muestro todas las empresas
            var resultado = lnUtp.Utp_ListaEmpresas();
            //aqui busco el nombre comercial                                                                               //obtengo el id         // muestro la empresa buscada
            var result = resultado.Where(s => s.NombreComercial.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdEmpresa, Label = c.NombreComercial }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarAlumnosNombreyCodigo(string query)
        {
            //aqui muestro todos los alumnos
            var resultado = lnUtp.Utp_ListarAlumnosNombreyCodigo();
            //aqui busco el Alumno                                                                             //obtengo el id         // muestro la empresa buscada
            var result = resultado.Where(s => s.Alumno.ToLower().Contains(query.ToLower())).Select(c => new { Value = c.IdAlumno, Label = c.Alumno }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListarRazonSocial(string query)
        {
            //aqui muestro todas las empresas
            var resultado = lnUtp.Utp_ListaEmpresas();
            //aqui busco el nombre comercial                                                                               //obtengo el id         // muestro la empresa buscada
            var result = resultado.Where(s => s.RazonSocial.ToLower().StartsWith(query.ToLower())).Select(c => new { Value = c.IdEmpresa, Label = c.RazonSocial }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public FileResult Imagen2(int id, string Menu)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";


            List<Contenido> lista = new List<Contenido>();


            int codMenu = Convert.ToInt32(Menu);

            if (Menu == null)
            {

                DataTable dtResultado = ln.Contenido_ObtenerPorCodMenu(0);


                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    Contenido contenido = new Contenido();
                    contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
                    contenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                    contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                    contenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                    contenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();

                    contenido.Imagen = (byte[])dtResultado.Rows[i]["Imagen"];

                    contenido.TituloMenu = dtResultado.Rows[i]["Menu"].ToString();


                    lista.Add(contenido);
                }


            }
            else
            {

                DataTable dtResultado = ln.Contenido_ObtenerPorCodMenu(codMenu);


                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    Contenido contenido = new Contenido();
                    contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
                    contenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                    contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                    contenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                    contenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();

                    contenido.Imagen = (byte[])dtResultado.Rows[i]["Imagen"];

                    contenido.TituloMenu = dtResultado.Rows[i]["Menu"].ToString();


                    lista.Add(contenido);
                }
            }



            Contenido producto = lista.Where(k => k.IdContenido == id).FirstOrDefault();

            MemoryStream stream;

            if (producto != null && producto.Imagen != null)
            {
                stream = new MemoryStream(producto.Imagen);
            }
            else
            {
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }

         [ValidateInput(false)]
        public ActionResult Portal_insertar()
        {

            DataTable dtresultado = ln.ContenidoMenu_Mostrar();

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
            {
                string nombre = dtresultado.Rows[i]["Titulo"].ToString();
                string valor = dtresultado.Rows[i]["IdMenu"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }
            ViewData["ContenidoMenu"] = li;
            return View();
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Portal_insertar([Bind(Include = "")] ContenidoVista contenidoHTML)
        {
            Contenido contenido = new Contenido();


            if (contenidoHTML.ImagenHtml != null)
            {

                byte[] uploadedFile = new byte[contenidoHTML.ImagenHtml.InputStream.Length];
                contenidoHTML.ImagenHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(contenidoHTML.ImagenHtml.InputStream.Length));
                contenidoHTML.ArchivoNombreOriginal = contenidoHTML.ImagenHtml.FileName;
                contenidoHTML.ArchivoMimeType = contenidoHTML.ImagenHtml.ContentType;
                contenidoHTML.Imagen = uploadedFile;


            }


            contenido.Titulo = contenidoHTML.Titulo;
            contenido.SubTitulo = contenidoHTML.SubTitulo;
            contenido.Descripcion = contenidoHTML.Descripcion;
            contenido.Imagen = contenidoHTML.Imagen;

            contenido.ArchivoMimeType = contenidoHTML.ArchivoMimeType;
            contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;
            contenido.EnPantallaPrincipal = contenidoHTML.EnPantallaPrincipal;
            contenido.Activo = contenidoHTML.Activo;
            contenido.Menu = contenidoHTML.Menu;


            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];


            contenido.CreadoPor = ticketUtp.Usuario;

            //contenido.CreadoPor = contenidoHTML.CreadoPor;


            if (ln.Contenido_insertar(contenido) == true)
            {
                ViewBag.Message = "Registro Insertado Correctamente";
                return RedirectToAction("Portal");
            }
            else
            {

                DataTable dtresultado = ln.ContenidoMenu_Mostrar();

                List<SelectListItem> li = new List<SelectListItem>();

                for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
                {
                    string nombre = dtresultado.Rows[i]["Titulo"].ToString();
                    string valor = dtresultado.Rows[i]["IdMenu"].ToString();



                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    li.Add(item);

                }
                ViewData["ContenidoMenu"] = li;

                ViewBag.Message = "Error al Guardar la informacion";
                return View(contenidoHTML);




                //ViewBag.Message = "Error al Guardar la informacion";
                //return View(contenido);
            }

        }

        public ActionResult Contenido_Eliminar(int id)
        {

            ln.Contenido_Eliminar(Convert.ToInt32(id));

            return RedirectToAction("Portal");

            
        }


          [ValidateInput(false)]
        public ActionResult Portal_Editar_Buscar(int id)
        {
            DataTable dtresultado = ln.ContenidoMenu_Mostrar();
            
            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
            {
                string nombre = dtresultado.Rows[i]["Titulo"].ToString();
                string valor = dtresultado.Rows[i]["IdMenu"].ToString();



                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }
            ViewData["ContenidoMenu"] = li;
            

            ContenidoVista contenido = new ContenidoVista();

    
            DataTable dtResultado = ln.ContenidoEDitar_Buscar(id);

            if (dtResultado.Rows.Count > 0)
            {
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[0]["IdContenido"]);
                contenido.Menu = dtResultado.Rows[0]["CodMenu"].ToString();
                contenido.Titulo = dtResultado.Rows[0]["Titulo"].ToString();
                contenido.SubTitulo = dtResultado.Rows[0]["SubTitulo"].ToString();
                contenido.Descripcion = dtResultado.Rows[0]["Descripcion"].ToString();
                contenido.ModificadoPor = dtResultado.Rows[0]["ModificadoPor"] == null ? "" : dtResultado.Rows[0]["ModificadoPor"].ToString();
                contenido.EnPantallaPrincipal = Convert.ToBoolean(dtResultado.Rows[0]["EnPantallaPrincipal"].ToString());
                contenido.Activo = Convert.ToBoolean(dtResultado.Rows[0]["Activo"] == DBNull.Value ? 0 : dtResultado.Rows[0]["Activo"]);
                contenido.ArchivoNombreOriginal = dtResultado.Rows[0]["ArchivoNombreOriginal"].ToString();
                contenido.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"] == DBNull.Value  ? null : dtResultado.Rows[0]["FechaCreacion"].ToString());
                contenido.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaModificacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaModificacion"].ToString());
                contenido.CreadoPor = dtResultado.Rows[0]["CreadoPor"] == null ? "" : dtResultado.Rows[0]["CreadoPor"].ToString();           

            }


            return View(contenido);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Portal_Editar_Buscar([Bind(Include = "")] ContenidoVista contenidoHTML)
        {
      
            Contenido contenido = new Contenido();

            if (contenidoHTML.ImagenHtml != null)
            {

                byte[] uploadedFile = new byte[contenidoHTML.ImagenHtml.InputStream.Length];
                contenidoHTML.ImagenHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(contenidoHTML.ImagenHtml.InputStream.Length));
                contenidoHTML.ArchivoNombreOriginal = contenidoHTML.ImagenHtml.FileName;
                contenidoHTML.ArchivoMimeType = contenidoHTML.ImagenHtml.ContentType;
                contenidoHTML.Imagen = uploadedFile;

                contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;

            }

            contenido.Titulo = contenidoHTML.Titulo;
            contenido.SubTitulo = contenidoHTML.SubTitulo;
            contenido.Descripcion = contenidoHTML.Descripcion;
            contenido.Imagen = contenidoHTML.Imagen;
            contenido.ArchivoMimeType = contenidoHTML.ArchivoMimeType;
            contenido.ArchivoNombreOriginal = contenidoHTML.ArchivoNombreOriginal;

            contenido.EnPantallaPrincipal = contenidoHTML.EnPantallaPrincipal;
            contenido.Activo = contenidoHTML.Activo;

            contenido.Menu= contenidoHTML.Menu;
            contenido.CreadoPor  = contenidoHTML.CreadoPor ;
            contenido.FechaCreacion  = contenidoHTML.FechaCreacion ;

            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];

            contenido.ModificadoPor = ticketUtp.Usuario;
            //contenido.ModificadoPor = contenidoHTML.ModificadoPor;
            contenido.IdContenido = contenidoHTML.IdContenido;
            
            //if (ModelState.IsValid)
            //{
            
            if (ln.Contenido_Actualizar(contenido) == true)
            {
                ViewBag.Message = "Datos Actualizado";
                return RedirectToAction("Portal");
            }
            else
            {
               
                DataTable dtresultado = ln.ContenidoMenu_Mostrar();

                List<SelectListItem> li = new List<SelectListItem>();

                for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
                {
                    string nombre = dtresultado.Rows[i]["Titulo"].ToString();
                    string valor = dtresultado.Rows[i]["IdMenu"].ToString();



                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    li.Add(item);

                }
                ViewData["ContenidoMenu"] = li;

                ViewBag.Message = "Error al Actualizar";
                return View(contenidoHTML);
            }

            //}
            
        }

        public ActionResult RemoverArchivo(int id)
        {

            var contenido = Portal_Editar_Buscar(id);

            bool esExitoso = ln.Contenido_RemoverImagen(id);

            if (esExitoso)
            {

                return RedirectToAction("Portal_Editar_Buscar", new { id = id });
            }

            return View(contenido);

        }

       
        //[HttpGet]
        //public ActionResult Convenios()
        //{
        //    List<VistaUTPListaConvenio> listaEjemplo = new List<VistaUTPListaConvenio>();
        //    return View("Convenios",listaEjemplo);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Convenios(string SearchString)
        {

            string palabraClave = SearchString == null ? "" : SearchString;

            List<VistaUTPListaConvenio> listaEjemplo = new List<VistaUTPListaConvenio>();

            DataTable dtResultado = lnUtp.UTP_ObtenerUltimosConvenios(palabraClave);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                VistaUTPListaConvenio vista = new VistaUTPListaConvenio();

                vista.IdConvenio = Convert.ToInt32(dtResultado.Rows[i]["IdConvenio"]);
                vista.Nombres = dtResultado.Rows[i]["Nombres"].ToString();
                vista.Apellidos = dtResultado.Rows[i]["Apellidos"].ToString();
                vista.Carrera = dtResultado.Rows[i]["Carrera"].ToString();
                vista.NombreComercial = dtResultado.Rows[i]["NombreComercial"].ToString();
                vista.TipoTrabajo = dtResultado.Rows[i]["TipoTrabajo"].ToString();
                vista.DuracionContrato = Convert.ToInt32(dtResultado.Rows[i]["DuracionContrato"]);
                vista.SalarioOfrecido = Convert.ToDecimal(dtResultado.Rows[i]["SalarioOfrecido"] == System.DBNull.Value ? null : dtResultado.Rows[i]["SalarioOfrecido"]);
                vista.AreaEmpresa = dtResultado.Rows[i]["AreaEmpresa"].ToString();
                vista.FechaIngreso = Convert.ToDateTime(dtResultado.Rows[i]["FechaIngreso"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaIngreso"]);

                listaEjemplo.Add(vista);
            }

            return View("Convenios", listaEjemplo);

        }
        //[ValidateAntiForgeryToken]
        public ActionResult _VistaNuevoConvenio()
        {
            //if (idConvenio != null)
            //{
            //    DataTable dtResultado = lnUtp.UTP_ObtenerConvenio(idConvenio);
            //}
            LNGeneral lngeneral = new LNGeneral();
            Convenio convenio = new Convenio();
            convenio.TipoTrabajo = "";
            convenio.FuenteConvenio = "";

            ViewBag.TipoTrabajo = new SelectList(lngeneral.ObtenerListaValor(29), "IdListaValor", "Valor");
            ViewBag.FuenteConvenio = new SelectList(lngeneral.ObtenerListaValor(51), "IdListaValor", "Valor");



            return PartialView("_VistaNuevoConvenio",convenio);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult NuevoConvenio(Convenio nconvenio)
        {
            //if (idConvenio != null)
            //{
            //    DataTable dtResultado = lnUtp.UTP_ObtenerConvenio(idConvenio);
            //}

            TicketUTP ticket = (TicketUTP)Session["TicketUTP"];
            nconvenio.CreadoPor = ticket.Usuario;
            lnUtp.UTP_ConvenioInsertar(nconvenio);


            LNGeneral lngeneral = new LNGeneral();
            Convenio convenio = new Convenio();
            convenio.TipoTrabajo = "";
            convenio.FuenteConvenio = "";

            //ViewBag.TipoTrabajo = new SelectList(lngeneral.ObtenerListaValor(29), "IdListaValor", "Valor");
            //ViewBag.FuenteConvenio = new SelectList(lngeneral.ObtenerListaValor(51), "IdListaValor", "Valor");

            return RedirectToAction("Convenios");
        }

        [HttpGet]
        public ActionResult Convenio(int idconvenio)
        {
            LNGeneral lngeneral = new LNGeneral();
            Convenio convenio = lnUtp.UTP_ObtenerConvenio(idconvenio);
            ViewBag.TipoTrabajo = new SelectList(lngeneral.ObtenerListaValor(29), "IdListaValor", "Valor",convenio.TipoTrabajo);
            ViewBag.FuenteConvenio = new SelectList(lngeneral.ObtenerListaValor(51), "IdListaValor", "Valor",convenio.FuenteConvenio);
            ViewBag.IdExperienciaCargo = new SelectList(convenio.Experiencias, "IdExperienciaCargo", "Experiencia",convenio.IdExperienciaCargo);
            return View(convenio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Convenio(Convenio convenio)
        {
            LNGeneral lngeneral = new LNGeneral();
            TicketUTP ticket = (TicketUTP)Session["TicketUTP"];
            convenio.ModificadoPor = ticket.Usuario;
            lnUtp.UTP_ConvenioActualizar(convenio);
            Convenio convenioa = lnUtp.UTP_ObtenerConvenio(convenio.IdConvenio);
            ViewBag.TipoTrabajo = new SelectList(lngeneral.ObtenerListaValor(29), "IdListaValor", "Valor", convenioa.TipoTrabajo);
            ViewBag.FuenteConvenio = new SelectList(lngeneral.ObtenerListaValor(51), "IdListaValor", "Valor", convenioa.FuenteConvenio);
            
            ViewBag.IdExperienciaCargo = new SelectList(convenioa.Experiencias, "IdExperienciaCargo", "Experiencia", convenioa.IdExperienciaCargo);
            return View(convenioa);
        }

        public ActionResult VerDetalleEmpresa(int id)
        {
            ViewBag.IdEmpresa = id;

            return View();
        }

        public PartialViewResult _VerDetalleEmpresaDatosGenerales(int id)
        {
            int idEmpresa = id;
            LNEmpresa lnEmpresa = new LNEmpresa();
            LNGeneral lnGeneral = new LNGeneral();
            LNUsuario lnUsuario = new LNUsuario();

            Empresa empresa = lnEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            ViewBag.EstadoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_EMPRESA), "IdListaValor", "Valor", empresa.EstadoIdListaValor);
            //Se obtienen sólo los usuarios activos:
            ViewBag.UsuarioEC = new SelectList(lnUsuario.ObtenerUsuariosPorTipo("USERUT"), "NombreUsuario", "NombreCompleto", empresa.UsuarioEC);

            return PartialView("_VerDetalleEmpresaDatosGenerales", empresa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public PartialViewResult _VerDetalleEmpresaDatosGeneralesEditar(Empresa empresa)
        {             
            LNUTP lnUTP = new LNUTP ();


            //Empresa objempresa=new Empresa ();
    
            
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];

            empresa.Usuario = ticketUtp.Usuario;
                   

            lnUtp.ActualizarEstadoYUsuarioEC(empresa);

            LNGeneral lnGeneral = new LNGeneral();
            LNUsuario lnUsuario = new LNUsuario();


            LNEmpresa lnEmpresa = new LNEmpresa();

            Empresa empresaActualizada = lnEmpresa.ObtenerDatosEmpresaPorId(empresa.IdEmpresa);

            ViewBag.EstadoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_EMPRESA), "IdListaValor", "Valor", empresaActualizada.EstadoIdListaValor);
            ViewBag.UsuarioEC = new SelectList(lnUsuario.ObtenerUsuariosPorTipo("USERUT"), "NombreUsuario", "NombreCompleto", empresaActualizada.UsuarioEC);


            return PartialView("_VerDetalleEmpresaDatosGenerales", empresaActualizada);
        }

        public ActionResult EmpresaUsuario()
        {
            return View();
        }
        public ActionResult EmpresaLocacion()
        {
            return View();
        }
 
       

      
 
        public ActionResult Sistema()
        {
            return View();
        }

       

        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult ListaValor()
        {
            return View();
        }
        public ActionResult Plantilla()
        {
            return View();
        }
        public ActionResult Reportes()
        {
            return View();
        }

        public ActionResult Evento_Editar(int? Id)
        {
            //Lista Estado Evento
            DataTable dtresultadoEstadoEvento = lnUtp.Evento_ListaEstadoEvento();

            List<SelectListItem> estadoEvento = new List<SelectListItem>();

            for (int i = 0; i <= dtresultadoEstadoEvento.Rows.Count - 1; i++)
            {
                string nombre = dtresultadoEstadoEvento.Rows[i]["Valor"].ToString();
                string valor = dtresultadoEstadoEvento.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                estadoEvento.Add(item);

            }
            ViewData["ListaEstadoEvento"] = estadoEvento;

            //------------------------------------------------------------

            //LISTA TIPO EVENTO

            DataTable dtresultadoTipoEvento = lnUtp.Evento_ListaTipoEvento();

            List<SelectListItem> TipoEvento = new List<SelectListItem>();

            for (int i = 0; i <= dtresultadoTipoEvento.Rows.Count - 1; i++)
            {
                string nombre = dtresultadoTipoEvento.Rows[i]["Valor"].ToString();
                string valor = dtresultadoTipoEvento.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                TipoEvento.Add(item);

            }
            ViewData["ListaTipoEvento"] = TipoEvento;

            //------------------------------------------------------------

            //LISTA EMPRESA

            DataTable dtresultadoEmpresa = lnUtp.EMPRESA_LISTAEMPRESA();

            List<SelectListItem> empresa = new List<SelectListItem>();

            for (int i = 0; i <= dtresultadoEmpresa.Rows.Count - 1; i++)
            {
                string nombre = dtresultadoEmpresa.Rows[i]["NombreComercial"].ToString();
                string valor = dtresultadoEmpresa.Rows[i]["IdEmpresa"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                empresa.Add(item);

            }
            ViewData["ListaEmpresa"] = empresa;

            Evento evento = new Evento();


            DataTable dtResultado = lnEventos.EVENTO_OBTENERPORID(Convert.ToInt32(Id));

            if (dtResultado.Rows.Count > 0)
            {

                evento.IdEvento             = Convert.ToInt32(dtResultado.Rows[0]["IdEvento"]);
                evento.NombreEvento         = Convert.ToString(dtResultado.Rows[0]["NombreEvento"]);
                evento.DescripcionEvento    = Convert.ToString(dtResultado.Rows[0]["DescripcionEvento"]);
                evento.FechaEvento          = Convert.ToDateTime(dtResultado.Rows[0]["FechaEvento"]);
                evento.FechaEventoTexto     = Convert.ToString(dtResultado.Rows[0]["FechaEventoTexto"]);
                evento.LugarEvento          = Convert.ToString(dtResultado.Rows[0]["LugarEvento"]);
                evento.DireccionRegion      = Convert.ToString(dtResultado.Rows[0]["DireccionRegion"]);
                evento.DireccionCiudad      = Convert.ToString(dtResultado.Rows[0]["DireccionCiudad"]);
                evento.DireccionDistrito    = Convert.ToString(dtResultado.Rows[0]["DireccionDistrito"]);
                evento.DireccionEvento      = Convert.ToString(dtResultado.Rows[0]["DireccionEvento"]);
                evento.AsistentesEsperados  = Convert.ToInt32(dtResultado.Rows[0]["AsistentesEsperados"]);
                evento.RegistraAlumnos      = Convert.ToBoolean(dtResultado.Rows[0]["RegistraAlumnos"]);
                evento.RegistraUsuariosEmpresa = Convert.ToBoolean(dtResultado.Rows[0]["RegistraUsuariosEmpresa"] == DBNull.Value ? 0 : dtResultado.Rows[0]["RegistraUsuariosEmpresa"]);
                evento.RegistraPublicoEnGeneral = Convert.ToBoolean(dtResultado.Rows[0]["RegistraPublicoEnGeneral"] == DBNull.Value ? 0 : dtResultado.Rows[0]["RegistraPublicoEnGeneral"]);
                evento.EstadoEvento         = Convert.ToString(dtResultado.Rows[0]["EstadoEvento"]);
                evento.TipoEvento           = Convert.ToString(dtResultado.Rows[0]["TipoEvento"]);
                evento.IdEmpresa            = Convert.ToInt32(dtResultado.Rows[0]["IdEmpresa"]);


                evento.CreadoPor = dtResultado.Rows[0]["CreadoPor"].ToString();
                evento.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaCreacion"]);
                evento.ModificadoPor = dtResultado.Rows[0]["ModificadoPor"].ToString();
                evento.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaModificacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaModificacion"]);
      
                
            }

                     
            return View(evento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Evento_Editar([Bind(Include = "")] Evento evento)
        {

            
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];


            evento.ModificadoPor = ticketUtp.Usuario;

            if (lnEventos.Evento_Actualizar(evento) == true)
            {

                ViewBag.Message = "Registro Actualizado Correctamente";
                return RedirectToAction("Eventos");
            }
            else
            {

                //Lista Estado Evento
                DataTable dtresultadoEstadoEvento = lnUtp.Evento_ListaEstadoEvento();

                List<SelectListItem> estadoEvento = new List<SelectListItem>();

                for (int i = 0; i <= dtresultadoEstadoEvento.Rows.Count - 1; i++)
                {
                    string nombre = dtresultadoEstadoEvento.Rows[i]["Valor"].ToString();
                    string valor = dtresultadoEstadoEvento.Rows[i]["IDListaValor"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    estadoEvento.Add(item);

                }
                ViewData["ListaEstadoEvento"] = estadoEvento;

                //------------------------------------------------------------

                //LISTA TIPO EVENTO

                DataTable dtresultadoTipoEvento = lnUtp.Evento_ListaTipoEvento();

                List<SelectListItem> TipoEvento = new List<SelectListItem>();

                for (int i = 0; i <= dtresultadoTipoEvento.Rows.Count - 1; i++)
                {
                    string nombre = dtresultadoTipoEvento.Rows[i]["Valor"].ToString();
                    string valor = dtresultadoTipoEvento.Rows[i]["IDListaValor"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    TipoEvento.Add(item);

                }
                ViewData["ListaTipoEvento"] = TipoEvento;

                //------------------------------------------------------------

                //LISTA EMPRESA

                DataTable dtresultadoEmpresa = lnUtp.EMPRESA_LISTAEMPRESA();

                List<SelectListItem> empresa = new List<SelectListItem>();

                for (int i = 0; i <= dtresultadoEmpresa.Rows.Count - 1; i++)
                {
                    string nombre = dtresultadoEmpresa.Rows[i]["NombreComercial"].ToString();
                    string valor = dtresultadoEmpresa.Rows[i]["IdEmpresa"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    empresa.Add(item);

                }
                ViewData["ListaEmpresa"] = empresa;


                return View(evento);

            }

        }

      

 
        public ActionResult Evento()
        {

            //Lista Estado Evento
            DataTable dtresultadoEstadoEvento = lnUtp.Evento_ListaEstadoEvento();

            List<SelectListItem> estadoEvento = new List<SelectListItem>();

            for (int i = 0; i <= dtresultadoEstadoEvento.Rows.Count - 1; i++)
            {
                string nombre = dtresultadoEstadoEvento.Rows[i]["Valor"].ToString();
                string valor = dtresultadoEstadoEvento.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                estadoEvento.Add(item);

            }
            ViewData["ListaEstadoEvento"] = estadoEvento;

            //------------------------------------------------------------

            //LISTA TIPO EVENTO

            DataTable dtresultadoTipoEvento = lnUtp.Evento_ListaTipoEvento();

            List<SelectListItem> TipoEvento = new List<SelectListItem>();

            for (int i = 0; i <= dtresultadoTipoEvento.Rows.Count - 1; i++)
            {
                string nombre = dtresultadoTipoEvento.Rows[i]["Valor"].ToString();
                string valor = dtresultadoTipoEvento.Rows[i]["IDListaValor"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                TipoEvento.Add(item);

            }
            ViewData["ListaTipoEvento"] = TipoEvento;

            //------------------------------------------------------------

            //LISTA EMPRESA

            DataTable dtresultadoEmpresa = lnUtp.EMPRESA_LISTAEMPRESA();

            List<SelectListItem> empresa = new List<SelectListItem>();

            for (int i = 0; i <= dtresultadoEmpresa.Rows.Count - 1; i++)
            {
                string nombre = dtresultadoEmpresa.Rows[i]["NombreComercial"].ToString();
                string valor = dtresultadoEmpresa.Rows[i]["IdEmpresa"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                empresa.Add(item);

            }
            ViewData["ListaEmpresa"] = empresa;



            return View();
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Evento([Bind(Include = "")] Evento evento)
        {

            

            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];


            evento.CreadoPor = ticketUtp.Usuario;

            if (lnEventos.Evento_insertar(evento) == true)
            {
            
                ViewBag.Message = "Registro Insertado Correctamente";
                return RedirectToAction("Eventos");
            }
            else
            {

                //Lista Estado Evento
                DataTable dtresultadoEstadoEvento = lnUtp.Evento_ListaEstadoEvento();

                List<SelectListItem> estadoEvento = new List<SelectListItem>();

                for (int i = 0; i <= dtresultadoEstadoEvento.Rows.Count - 1; i++)
                {
                    string nombre = dtresultadoEstadoEvento.Rows[i]["Valor"].ToString();
                    string valor = dtresultadoEstadoEvento.Rows[i]["IDListaValor"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    estadoEvento.Add(item);

                }
                ViewData["ListaEstadoEvento"] = estadoEvento;

                //------------------------------------------------------------

                //LISTA TIPO EVENTO

                DataTable dtresultadoTipoEvento = lnUtp.Evento_ListaTipoEvento();

                List<SelectListItem> TipoEvento = new List<SelectListItem>();

                for (int i = 0; i <= dtresultadoTipoEvento.Rows.Count - 1; i++)
                {
                    string nombre = dtresultadoTipoEvento.Rows[i]["Valor"].ToString();
                    string valor = dtresultadoTipoEvento.Rows[i]["IDListaValor"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    TipoEvento.Add(item);

                }
                ViewData["ListaTipoEvento"] = TipoEvento;

                //------------------------------------------------------------

                //LISTA EMPRESA

                DataTable dtresultadoEmpresa = lnUtp.EMPRESA_LISTAEMPRESA();

                List<SelectListItem> empresa = new List<SelectListItem>();

                for (int i = 0; i <= dtresultadoEmpresa.Rows.Count - 1; i++)
                {
                    string nombre = dtresultadoEmpresa.Rows[i]["NombreComercial"].ToString();
                    string valor = dtresultadoEmpresa.Rows[i]["IdEmpresa"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    empresa.Add(item);

                }
                ViewData["ListaEmpresa"] = empresa;


                return View(evento);

            }

        }




        public ActionResult verimagen()
        {

            List<Contenido> lista = new List<Contenido>();

            DataTable dtResulatdo = ln.Contenido_Mostrar_imagen();

            for (int i = 0; i <= dtResulatdo.Rows.Count - 1; i++)
            {
                Contenido  contenido = new Contenido();
                contenido.IdContenido = Convert.ToInt32(dtResulatdo.Rows[i]["IdContenido"]);
                contenido.Titulo = dtResulatdo.Rows[i]["Titulo"].ToString();



                contenido.Imagen = (byte[])dtResulatdo.Rows[i]["Imagen"];
           
                lista.Add(contenido);
            }

            List<ContenidoVista> contentModel = lista.Select(item => new ContenidoVista()
            {
                IdContenido = item.IdContenido,
                Titulo = item.Titulo,
                Imagen = item.Imagen,

            }).ToList();
            return View(contentModel);

            //return View();
           
        }
      
       

        public ActionResult VistaCabeceraUtp()
        {
      
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
         
            VistaPanelCabeceraUTP panel = new VistaPanelCabeceraUTP();

            panel = lnAutenticar.ObtenerPanelCabeceraUTP(ticketUtp.Usuario);

            return PartialView("_DatosUtp", panel);
        }
        public ActionResult VistaCabeceraUtpMiPortal()
        {

            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];

            VistaPanelCabeceraUTP panel = new VistaPanelCabeceraUTP();

            panel = lnAutenticar.ObtenerPanelCabeceraUTP(ticketUtp.Usuario);

            return PartialView("_DatosUtpPortal", panel);
        }

        public ActionResult VistaOfertasPendientes(int nroPaginaActual, int filasPorPagina = Constantes.FILAS_POR_PAGINA)
        {
            List<VistaOfertasPendientes> listaOfertasPendientes = new List<VistaOfertasPendientes>();

            DataTable dtResultado = lnUtp.OfertasObtenerPendientes(nroPaginaActual, filasPorPagina);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaOfertasPendientes vistaNueva = new VistaOfertasPendientes();

                vistaNueva.FechaPublicacion = Convert.ToDateTime(fila["FechaPublicacion"]);
                vistaNueva.NombreComercial = Convert.ToString(fila["NombreComercial"]);
                vistaNueva.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                vistaNueva.IdOferta = Convert.ToInt32(fila["IdOferta"]);
                vistaNueva.CantidadTotal = Convert.ToInt32(fila["CantidadTotal"]);

                listaOfertasPendientes.Add(vistaNueva);
            }

            //Datos para la paginación.
            int cantidadTotal = listaOfertasPendientes.Count() == 0 ? 0 : listaOfertasPendientes[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            return PartialView("_OfertasPendientes", listaOfertasPendientes);

        }



        public PartialViewResult _AdministrarActualizarImagenEventoUTP(int IdEvento)
        {
            
            VistaEvento vistaEvento = new VistaEvento();
            
            DataTable dtResultado = lnEventos.EVENTO_OBTENERPORID(Convert.ToInt32(IdEvento));

            if (dtResultado.Rows.Count > 0)
            {

                vistaEvento.IdEvento = Convert.ToInt32(dtResultado.Rows[0]["IdEvento"]);
                vistaEvento.NombreEvento = Convert.ToString(dtResultado.Rows[0]["NombreEvento"]);
                vistaEvento.DescripcionEvento = Convert.ToString(dtResultado.Rows[0]["DescripcionEvento"]);
                vistaEvento.FechaEvento = Convert.ToDateTime(dtResultado.Rows[0]["FechaEvento"]);
                vistaEvento.FechaEventoTexto = Convert.ToString(dtResultado.Rows[0]["FechaEventoTexto"]);
                vistaEvento.LugarEvento = Convert.ToString(dtResultado.Rows[0]["LugarEvento"]);
                vistaEvento.DireccionRegion = Convert.ToString(dtResultado.Rows[0]["DireccionRegion"]);
                vistaEvento.DireccionCiudad = Convert.ToString(dtResultado.Rows[0]["DireccionCiudad"]);
                vistaEvento.DireccionDistrito = Convert.ToString(dtResultado.Rows[0]["DireccionDistrito"]);
                vistaEvento.DireccionEvento = Convert.ToString(dtResultado.Rows[0]["DireccionEvento"]);
                vistaEvento.AsistentesEsperados = Convert.ToInt32(dtResultado.Rows[0]["AsistentesEsperados"]);
                vistaEvento.RegistraAlumnos = Convert.ToBoolean(dtResultado.Rows[0]["RegistraAlumnos"]);
                vistaEvento.RegistraUsuariosEmpresa = Convert.ToBoolean(dtResultado.Rows[0]["RegistraUsuariosEmpresa"] == DBNull.Value ? 0 : dtResultado.Rows[0]["RegistraUsuariosEmpresa"]);
                vistaEvento.RegistraPublicoEnGeneral = Convert.ToBoolean(dtResultado.Rows[0]["RegistraPublicoEnGeneral"] == DBNull.Value ? 0 : dtResultado.Rows[0]["RegistraPublicoEnGeneral"]);
                vistaEvento.EstadoEvento = Convert.ToString(dtResultado.Rows[0]["EstadoEvento"]);
                vistaEvento.TipoEvento = Convert.ToString(dtResultado.Rows[0]["TipoEvento"]);
                vistaEvento.IdEmpresa = Convert.ToInt32(dtResultado.Rows[0]["IdEmpresa"]);
                vistaEvento.ImagenEvento = dtResultado.Rows[0]["ImagenEvento"] == DBNull.Value ? null : (byte[])dtResultado.Rows[0]["ImagenEvento"];

            }

            
            //VistaEvento vistaEvento = new VistaEvento();
            //vistaEvento.LugarEvento = "demo";
            //vistaEvento.IdEvento = 10;
            //return PartialView("_AdministrarActualizarImagenEventoUTP", listaOfertasPendientes);

            return PartialView("_AdministrarActualizarImagenEventoUTP", vistaEvento);

            
        }


        public PartialViewResult _AdministrarActualizarImagenTickect(int IdEvento)
        {

            VistaEvento vistaEvento = new VistaEvento();

            DataTable dtResultado = lnEventos.EVENTO_OBTENERPORID(Convert.ToInt32(IdEvento));

            if (dtResultado.Rows.Count > 0)
            {

                vistaEvento.IdEvento = Convert.ToInt32(dtResultado.Rows[0]["IdEvento"]);
                vistaEvento.NombreEvento = Convert.ToString(dtResultado.Rows[0]["NombreEvento"]);
                vistaEvento.DescripcionEvento = Convert.ToString(dtResultado.Rows[0]["DescripcionEvento"]);
                vistaEvento.FechaEvento = Convert.ToDateTime(dtResultado.Rows[0]["FechaEvento"]);
                vistaEvento.FechaEventoTexto = Convert.ToString(dtResultado.Rows[0]["FechaEventoTexto"]);
                vistaEvento.LugarEvento = Convert.ToString(dtResultado.Rows[0]["LugarEvento"]);
                vistaEvento.DireccionRegion = Convert.ToString(dtResultado.Rows[0]["DireccionRegion"]);
                vistaEvento.DireccionCiudad = Convert.ToString(dtResultado.Rows[0]["DireccionCiudad"]);
                vistaEvento.DireccionDistrito = Convert.ToString(dtResultado.Rows[0]["DireccionDistrito"]);
                vistaEvento.DireccionEvento = Convert.ToString(dtResultado.Rows[0]["DireccionEvento"]);
                vistaEvento.AsistentesEsperados = Convert.ToInt32(dtResultado.Rows[0]["AsistentesEsperados"]);
                vistaEvento.RegistraAlumnos = Convert.ToBoolean(dtResultado.Rows[0]["RegistraAlumnos"]);
                vistaEvento.RegistraUsuariosEmpresa = Convert.ToBoolean(dtResultado.Rows[0]["RegistraUsuariosEmpresa"] == DBNull.Value ? 0 : dtResultado.Rows[0]["RegistraUsuariosEmpresa"]);
                vistaEvento.RegistraPublicoEnGeneral = Convert.ToBoolean(dtResultado.Rows[0]["RegistraPublicoEnGeneral"] == DBNull.Value ? 0 : dtResultado.Rows[0]["RegistraPublicoEnGeneral"]);
                vistaEvento.EstadoEvento = Convert.ToString(dtResultado.Rows[0]["EstadoEvento"]);
                vistaEvento.TipoEvento = Convert.ToString(dtResultado.Rows[0]["TipoEvento"]);
                vistaEvento.IdEmpresa = Convert.ToInt32(dtResultado.Rows[0]["IdEmpresa"]);
                vistaEvento.ImagenTicket = dtResultado.Rows[0]["ImagenTicket"] == DBNull.Value ? null : (byte[])dtResultado.Rows[0]["ImagenTicket"];

            }


            //VistaEvento vistaEvento = new VistaEvento();
            //vistaEvento.LugarEvento = "demo";
            //vistaEvento.IdEvento = 10;
            //return PartialView("_AdministrarActualizarImagenEventoUTP", listaOfertasPendientes);

            return PartialView("_AdministrarActualizarImagenTickect", vistaEvento);


        }




        public FileResult GetImagenEvento(int id)
        {
            
            const string alternativePicturePath = @"/img/sinimagen.jpg";
                  
            VistaEvento vistaEvento = new VistaEvento();

            DataTable dtResultado = lnEventos.EVENTO_OBTENERPORID(Convert.ToInt32(id));

            if (dtResultado.Rows.Count > 0)
            {

                vistaEvento.IdEvento = Convert.ToInt32(dtResultado.Rows[0]["IdEvento"]);
         
                vistaEvento.ImagenEvento = dtResultado.Rows[0]["ImagenEvento"] == DBNull.Value ? null : (byte[])dtResultado.Rows[0]["ImagenEvento"];

            }
                     
            
            MemoryStream stream;

            if (vistaEvento != null && vistaEvento.ImagenEvento != null)
            {
                stream = new MemoryStream(vistaEvento.ImagenEvento);
            }
            else
            {
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }

        public FileResult GetImagenEventoTickect(int id)
        {

            const string alternativePicturePath = @"/img/sinimagen.jpg";


            VistaEvento vistaEvento = new VistaEvento();

            DataTable dtResultado = lnEventos.EVENTO_OBTENERPORID(Convert.ToInt32(id));

            if (dtResultado.Rows.Count > 0)
            {

                vistaEvento.IdEvento = Convert.ToInt32(dtResultado.Rows[0]["IdEvento"]);

                vistaEvento.ImagenTicket = dtResultado.Rows[0]["ImagenTicket"] == DBNull.Value ? null : (byte[])dtResultado.Rows[0]["ImagenTicket"];

            }


            MemoryStream stream;

            if (vistaEvento != null && vistaEvento.ImagenTicket != null)
            {
                stream = new MemoryStream(vistaEvento.ImagenTicket);
            }
            else
            {
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult EVENTO_ACTUALIZAR_IMAGENEVENTO(VistaEvento EventoHTML)
        {
            Evento evento = new Evento();

            if (EventoHTML.ImagenEventoHtml != null)
            {

                byte[] uploadedFile = new byte[EventoHTML.ImagenEventoHtml.InputStream.Length];
                EventoHTML.ImagenEventoHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(EventoHTML.ImagenEventoHtml.InputStream.Length));
                EventoHTML.ArchivoNombreOriginalImagenEvento = EventoHTML.ImagenEventoHtml.FileName;
                EventoHTML.ArchivoMimeTypeImagenEvento = EventoHTML.ImagenEventoHtml.ContentType;
                EventoHTML.ImagenEvento = uploadedFile;

                evento.ArchivoNombreOriginalImagenEvento = EventoHTML.ArchivoNombreOriginalImagenEvento;
            }

            evento.ImagenEvento = EventoHTML.ImagenEvento;
            evento.ArchivoMimeTypeImagenEvento = EventoHTML.ArchivoMimeTypeImagenEvento;
            evento.ArchivoNombreOriginalImagenEvento = EventoHTML.ArchivoNombreOriginalImagenEvento;
            evento.IdEvento = EventoHTML.IdEvento;

            //if (ModelState.IsValid)
            //{

            if (lnEventos.EVENTO_ACTUALIZAR_IMAGENEVENTO(evento) == true)
            {
                ViewBag.Message = "Datos Actualizado";

                return RedirectToAction("Evento_Editar", "UTP", new { id =evento.IdEvento });
            }
            else
            {
                ViewBag.Message = "Error al Actualizar";
                return View(EventoHTML);
            }

        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult EVENTO_ACTUALIZAR_IMAGENTICKECT(VistaEvento EventoHTML)
        {
            Evento evento = new Evento();

            if (EventoHTML.ImagenEventoTicketHtml != null)
            {

                byte[] uploadedFile = new byte[EventoHTML.ImagenEventoTicketHtml.InputStream.Length];
                EventoHTML.ImagenEventoTicketHtml.InputStream.Read(uploadedFile, 0, Convert.ToInt32(EventoHTML.ImagenEventoTicketHtml.InputStream.Length));
                EventoHTML.ArchivoNombreOriginalImagenTicket = EventoHTML.ImagenEventoTicketHtml.FileName;
                EventoHTML.ArchivoMimeTypeImagenEventoTicket = EventoHTML.ImagenEventoTicketHtml.ContentType;
                EventoHTML.ImagenTicket = uploadedFile;

                evento.ArchivoNombreOriginalImagenTicket = EventoHTML.ArchivoNombreOriginalImagenTicket;
            }

            evento.ImagenTicket = EventoHTML.ImagenTicket;
            evento.ArchivoMimeTypeImagenEventoTicket = EventoHTML.ArchivoMimeTypeImagenEventoTicket;
            evento.ArchivoNombreOriginalImagenTicket = EventoHTML.ArchivoNombreOriginalImagenTicket;
            evento.IdEvento = EventoHTML.IdEvento;

            //if (ModelState.IsValid)
            //{

            if (lnEventos.EVENTO_ACTUALIZAR_IMAGENTICKECT(evento) == true)
            {
                ViewBag.Message = "Datos Actualizado";

                return RedirectToAction("Evento_Editar", "UTP", new { id = evento.IdEvento });
            }
            else
            {
                ViewBag.Message = "Error al Actualizar";
                return View(EventoHTML);
            }

        }


        public ActionResult VistaOfertasporActivar(int nroPaginaActual)
        {
            List<VistaOfertasPendientes> listaOfertasPendientes = new List<VistaOfertasPendientes>();

            DataTable dtResultado = lnUtp.OfertasObtenerPendientes(nroPaginaActual, Constantes.FILAS_POR_PAGINA);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaOfertasPendientes vistaNueva = new VistaOfertasPendientes();

                vistaNueva.FechaPublicacion = Convert.ToDateTime(fila["FechaPublicacion"]);
                vistaNueva.NombreComercial = Convert.ToString(fila["NombreComercial"]);
                vistaNueva.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                vistaNueva.IdOferta = Convert.ToInt32(fila["IdOferta"]);

                listaOfertasPendientes.Add(vistaNueva);
            }

            //Datos para la paginación.
            int cantidadTotal = listaOfertasPendientes.Count() == 0 ? 0 : listaOfertasPendientes[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;

            return PartialView("VistaOfertasporActivar", listaOfertasPendientes);

        }

        public ActionResult Vista_EmpresasPendientes(int nroPaginaActual)
        {
            List<VistaEmpresasPendientes> listaEmpresasPendientes = new List<VistaEmpresasPendientes>();

            DataTable dtResultado = lnUtp.EmpresaObtenerPendientes(nroPaginaActual, Constantes.FILAS_POR_PAGINA);

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaEmpresasPendientes vistaNueva = new VistaEmpresasPendientes();

                vistaNueva.FechaCreacion = Convert.ToDateTime(fila["FechaCreacion"]);
                vistaNueva.NombreComercial = Convert.ToString(fila["NombreComercial"]);

                vistaNueva.IdEmpresa = Convert.ToInt32(fila["IdEmpresa"]);
                vistaNueva.CantidadTotal = Convert.ToInt32(fila["CantidadTotal"]);

                listaEmpresasPendientes.Add(vistaNueva);
            }

            //Datos para la paginación.
            int cantidadTotal = listaEmpresasPendientes.Count() == 0 ? 0 : listaEmpresasPendientes[0].CantidadTotal;

            Paginacion paginacion = new Paginacion();
            paginacion.NroPaginaActual = nroPaginaActual;
            paginacion.CantidadTotalResultados = cantidadTotal;
            paginacion.FilasPorPagina = Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            paginacion.TotalPaginas = cantidadTotal / Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            int residuo = cantidadTotal % Constantes.FILAS_POR_PAGINA; // Constantes.FILAS_POR_PAGINA;
            if (residuo > 0) paginacion.TotalPaginas += 1;

            ViewBag.Paginacion = paginacion;


            return PartialView("_EmpresasPendientes", listaEmpresasPendientes);

        }

        public ActionResult VerDetalleOferta(int id)
        {
            ViewBag.IdOferta = id;

            //Se obtiene los datos de la empresa.
            Oferta oferta = lnoferta.ObtenerPorId(id);

            LNGeneral lnGeneral = new LNGeneral();

            ViewBag.EstadoOferta = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_OFERTA), "IdListaValor", "Valor", oferta.EstadoOferta);

            return View(oferta);
        }








        #region Vistas parciales de la Empresa

        [HttpGet] //Se indica explícitamente que es un Get
        public PartialViewResult _VerDetalleEmpresaUsuarios(int id)
        {
            int idEmpresa = id;

            //Se obtiene la información de la BD
            LNEmpresaUsuario lnEmpresaUsuario = new LNEmpresaUsuario ();
            List<VistaEmpresaUsuario> lista = lnEmpresaUsuario.ObtenerUsuariosPorIdEmpresa(id);

            return PartialView("_VerDetalleEmpresaUsuarios", lista);
        }

        [HttpGet] //Se indica explícitamente que es un Get
        public PartialViewResult _VerDetalleEmpresaUbicaciones(int id)
        {

            int idEmpresa = id;

            //Se obtiene la información de la BD
            LNEmpresaLocacion lnEmpresaLocacion = new LNEmpresaLocacion();
            List<VistaEmpresaLocacion> lista = lnEmpresaLocacion.ObtenerLocacionesPorIdEmpresa(id);

            return PartialView("_VerDetalleEmpresaUbicaciones", lista);



            //Se obtiene la información de la BD

            //return PartialView("_VerDetalleEmpresaUbicaciones");
        }

        [HttpGet] //Se indica explícitamente que es un Get
        public PartialViewResult _VerDetalleEmpresaOfertas(int id)
        {

            //Se obtiene la información de la BD

            int idEmpresa = id;

            //Se obtiene la información de la BD
            LNOferta lnempresaoferta = new LNOferta();
            List<VistaEmpresaOferta> lista = lnempresaoferta.ObtenerOfertasPorIdEmpresa(id);

            return PartialView("_VerDetalleEmpresaOfertas", lista);





            //return PartialView("_VerDetalleEmpresaOfertas");
        }

        [HttpGet] //Se indica explícitamente que es un Get
        public PartialViewResult _VerDetalleEmpresaMensajes(int id)
        {
            //Se obtiene la información de la BD

            return PartialView("_VerDetalleEmpresaMensajes");
        }

      
        #endregion

        public PartialViewResult _UsuariosUTPLista()
        {
            List<UTPUsuario> lista = lnUtp.ObtenerUsuariosUTP();

            return PartialView("_UsuariosUTPLista", lista);
            //return PartialView("_UsuariosUTPLista");
        }

        public PartialViewResult _ListavalorPadre()
        {
            List<Lista> lista = new List<Lista>();

            DataTable dtResultado = lnUtp.UTP_LISTAVALORPADRE();

            foreach (DataRow fila in dtResultado.Rows)
            {

                Lista objlista = new Lista();

                objlista.IDLista = Convert.ToInt32(fila["IDLista"]);
                objlista.NombreLista = Convert.ToString(fila["NombreLista"]);
                objlista.DescripcionLista = Convert.ToString(fila["DescripcionLista"]);
                objlista.Modificable = Convert.ToBoolean(fila["Modificable"]);

                lista.Add(objlista);
            }

            return PartialView("_ListavalorPadre", lista);

        }

      



        public ActionResult Lista(int? Id)
        {
            Lista lista = new Lista();
            DataTable dtResultado = lnUtp.UTP_BUSCARLISTAVALORPADRE(Convert.ToInt32(Id));
            if (dtResultado.Rows.Count > 0)
            {
                lista.IDLista = Convert.ToInt32(dtResultado.Rows[0]["IDLista"]);

            }
            return View(lista);

  
        }


        public ActionResult Vista_ListaValorHijo(int? Id)
        {
            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = lnUtp.UTP_LISTAVALORHIJO(Convert.ToInt32(Id));

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                ListaValor objListaValor = new ListaValor();
                objListaValor.IdLista = Convert.ToInt32(dtResultado.Rows[i]["IdLista"].ToString());
                objListaValor.IdListaValor = dtResultado.Rows[i]["IdListaValor"].ToString();
                objListaValor.Valor = dtResultado.Rows[i]["Valor"].ToString();
                objListaValor.DescripcionValor = dtResultado.Rows[i]["DescripcionValor"].ToString();
                objListaValor.Icono = dtResultado.Rows[i]["Icono"].ToString();
                objListaValor.Peso = Convert.ToInt32(dtResultado.Rows[i]["Peso"] == DBNull.Value ? 0 : dtResultado.Rows[i]["Peso"]);
                objListaValor.ValorUTP = dtResultado.Rows[i]["ValorUTP"].ToString();
                objListaValor.Padre = dtResultado.Rows[i]["Padre"].ToString();
                objListaValor.EstadoValor = dtResultado.Rows[i]["EstadoValor"].ToString();
                lista.Add(objListaValor);

            }
           
            return PartialView("Vista_ListaValorHijo", lista);
        }
        public ActionResult Vista_DatosdeListaValorPadre(int? Id)
        {
            Lista lista = new Lista();
            DataTable dtResultado = lnUtp.UTP_BUSCARLISTAVALORPADRE(Convert.ToInt32(Id));
            if (dtResultado.Rows.Count > 0)
            {
                lista.IDLista = Convert.ToInt32(dtResultado.Rows[0]["IDLista"]);
                lista.NombreLista = Convert.ToString(dtResultado.Rows[0]["NombreLista"]);
                lista.DescripcionLista = Convert.ToString(dtResultado.Rows[0]["DescripcionLista"]);
                lista.Modificable = Convert.ToBoolean(dtResultado.Rows[0]["Modificable"]);
                lista.Creadopor = Convert.ToString(dtResultado.Rows[0]["Creadopor"]);
                lista.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"]);
                lista.Modificadopor = Convert.ToString(dtResultado.Rows[0]["Modificadopor"]);
                lista.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaModificacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaModificacion"]);
            }

            return PartialView("Vista_DatosdeListaValorPadre", lista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public PartialViewResult Vista_DatosdeListaValorPadre(Lista objlista)
        {
            //Esto es para guardar el usuario de creacion
            TicketUTP ticket = (TicketUTP)Session["TicketUtp"];

            objlista.Modificadopor = ticket.Usuario;

            // logica que guarda los datos

            lnUtp.UTPACTUALIZAR_LISTAVALORPADRE(objlista);


            ////recorro los datos para mostrar en el partial

            //Lista coleccionDeLista = new Lista();

            //DataTable dtResultado = lnUtp.UTP_LISTAVALORPADRE();

            //if (dtResultado.Rows.Count > 0)
            //{
            //    coleccionDeLista.IDLista = Convert.ToInt32(dtResultado.Rows[0]["IDLista"]);
            //    coleccionDeLista.NombreLista = Convert.ToString(dtResultado.Rows[0]["NombreLista"]);
            //    coleccionDeLista.DescripcionLista = Convert.ToString(dtResultado.Rows[0]["DescripcionLista"]);
            //    coleccionDeLista.Modificable = Convert.ToBoolean(dtResultado.Rows[0]["Modificable"]);
            //    coleccionDeLista.Creadopor = Convert.ToString(dtResultado.Rows[0]["Creadopor"]);
            //    coleccionDeLista.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"]);
            //    coleccionDeLista.Modificadopor = Convert.ToString(dtResultado.Rows[0]["Modificadopor"]);
            //    coleccionDeLista.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaModificacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaModificacion"]);
            //}
            return PartialView("Vista_DatosdeListaValorPadre", objlista);

        }





        //get
        public PartialViewResult _ListaValorPadreInsertar()
        {

            Lista objlista = new Lista();
            return PartialView("_ListaValorPadreInsertar", objlista);

        }
 
        //set
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _ListaValorPadreInsertar(Lista objlista)
        {
            //Esto es para guardar el usuario de creacion
            TicketUTP ticket = (TicketUTP)Session["TicketUtp"];

            objlista.Creadopor = ticket.Usuario;

            // logica que guarda los datos

            lnUtp.UTPINSERTAR_LISTAVALORPADRE(objlista);


            //recorro los datos para mostrar en el partial

            List<Lista> coleccionDeLista = new List<Lista>();

            DataTable dtResultado = lnUtp.UTP_LISTAVALORPADRE();

            foreach (DataRow fila in dtResultado.Rows)
            {
                //Lista es una clase.
                Lista nuevalista = new Lista();

                nuevalista.IDLista = Convert.ToInt32(fila["IDLista"]);
                nuevalista.NombreLista = Convert.ToString(fila["NombreLista"]);
                nuevalista.DescripcionLista = Convert.ToString(fila["DescripcionLista"]);
                nuevalista.Modificable = Convert.ToBoolean(fila["Modificable"]);
                nuevalista.Creadopor = Convert.ToString(fila["Creadopor"]);
                nuevalista.FechaCreacion = Convert.ToDateTime(fila["FechaCreacion"]);
                nuevalista.Modificadopor = Convert.ToString(fila["Modificadopor"]);
                nuevalista.FechaModificacion = Convert.ToDateTime(fila["FechaModificacion"] == DBNull.Value ? null : fila["FechaModificacion"]);

                coleccionDeLista.Add(nuevalista);
            }

            return PartialView("_ListavalorPadre", coleccionDeLista);

        }


           //AQUÍ LLAMA EL MODAL --- ESTO ES GET
        public PartialViewResult _NuevoValor()
        {

            ListaValor objlista = new ListaValor();
         
            return PartialView("_NuevoValor", objlista);

        }
        //AQUÍ INSERTA LOS DATOS ---- ESTO ES SET
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _NuevoValor(ListaValor objlista)
        {
            if (ModelState.IsValid)
            {
                //Esto es para guardar el usuario de creacion
                TicketUTP ticket = (TicketUTP)Session["TicketUtp"];

                objlista.Creadopor = ticket.Usuario;

                // logica que guarda los datos

                lnUtp.UTPINSERTAR_LISTAVALORHIJO(objlista);


                ////recorro los datos para mostrar en el partial


                List<ListaValor> lista = new List<ListaValor>();

                DataTable dtResultado = lnUtp.UTP_LISTAVALORHIJO(Convert.ToInt32(objlista.IdLista));

                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    ListaValor objListaValor = new ListaValor();
                    objListaValor.IdLista = Convert.ToInt32(dtResultado.Rows[i]["IdLista"].ToString());
                    objListaValor.IdListaValor = dtResultado.Rows[i]["IdListaValor"].ToString();
                    objListaValor.Valor = dtResultado.Rows[i]["Valor"].ToString();
                    objListaValor.DescripcionValor = dtResultado.Rows[i]["DescripcionValor"].ToString();
                    objListaValor.Icono = dtResultado.Rows[i]["Icono"].ToString();
                    objListaValor.Peso = Convert.ToInt32(dtResultado.Rows[i]["Peso"] == DBNull.Value ? 0 : dtResultado.Rows[i]["Peso"]);
                    objListaValor.ValorUTP = dtResultado.Rows[i]["ValorUTP"].ToString();
                    objListaValor.Padre = dtResultado.Rows[i]["Padre"].ToString();
                    objListaValor.EstadoValor = dtResultado.Rows[i]["EstadoValor"].ToString();
                    lista.Add(objListaValor);

                }
                //
                return PartialView("Vista_ListaValorHijo", lista);
            //}
            //else
            //{
            //    var errors = ModelState.Select(x => x.Value.Errors)
            //               .Where(y => y.Count > 0)
            //               .ToList();

            //    int a = 0;
            }

            return PartialView("_NuevoValor", objlista);

        }

            //AQUI LLAMO EL MODAL A EDITAR Con datos--- ESTO ES GET
        public PartialViewResult _NuevoValorEditar(string id)
        {

            ListaValor objlista = new ListaValor();

            DataTable dtResultado = lnUtp.UTP_OBTENERVALORPADREEDITAR(Convert.ToString(id));
            
            if (dtResultado.Rows.Count > 0)
            {
                objlista.IdListaValor       = Convert.ToString(dtResultado.Rows[0]["IdListaValor"]);
                objlista.Valor              = Convert.ToString(dtResultado.Rows[0]["Valor"]);
                objlista.DescripcionValor   = Convert.ToString(dtResultado.Rows[0]["DescripcionValor"]);
                objlista.Icono              = Convert.ToString(dtResultado.Rows[0]["Icono"]);
                objlista.Peso               = Convert.ToInt32(dtResultado.Rows[0]["Peso"]);
                objlista.ValorUTP           = Convert.ToString(dtResultado.Rows[0]["ValorUTP"]);
                objlista.IdLista            = Convert.ToInt32(dtResultado.Rows[0]["IdLista"]);
                objlista.Padre              = Convert.ToString(dtResultado.Rows[0]["Padre"]);
                objlista.EstadoValor        = Convert.ToString(dtResultado.Rows[0]["EstadoValor"]);
                objlista.IdListaValorPadre = Convert.ToString(dtResultado.Rows[0]["IdListaValorPadre"]);
            }

            return PartialView("_NuevoValorEditar", objlista);
                      

        }

        //AQUÍ Actualizo LOS DATOS ---- ESTO ES SET
        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _NuevoValorEditar(ListaValor objlista)
        {
              if (ModelState.IsValid)
            {
            //Esto es para guardar el usuario de creacion
            TicketUTP ticket = (TicketUTP)Session["TicketUtp"];

            objlista.Modificadopor = ticket.Usuario;

            // logica que guarda los datos

            lnUtp.UTPACTUALIZAR_LISTAVALORHIJO(objlista);


            ////recorro los datos para mostrar en el partial


            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = lnUtp.UTP_LISTAVALORHIJO(Convert.ToInt32(objlista.IdLista));

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                ListaValor objListaValor = new ListaValor();
                objListaValor.IdLista = Convert.ToInt32(dtResultado.Rows[i]["IdLista"].ToString());
                objListaValor.IdListaValor = dtResultado.Rows[i]["IdListaValor"].ToString();
                objListaValor.Valor = dtResultado.Rows[i]["Valor"].ToString();
                objListaValor.DescripcionValor = dtResultado.Rows[i]["DescripcionValor"].ToString();
                objListaValor.Icono = dtResultado.Rows[i]["Icono"].ToString();
                objListaValor.Peso = Convert.ToInt32(dtResultado.Rows[i]["Peso"] == DBNull.Value ? 0 : dtResultado.Rows[i]["Peso"]);
                objListaValor.ValorUTP = dtResultado.Rows[i]["ValorUTP"].ToString();
                objListaValor.Padre = dtResultado.Rows[i]["Padre"].ToString();
                objListaValor.EstadoValor = dtResultado.Rows[i]["EstadoValor"].ToString();

                lista.Add(objListaValor);

            }

            return PartialView("Vista_ListaValorHijo", lista);
            }

              return PartialView("_NuevoValorEditar", objlista);


        }

        //Elimina datos de la lista de valor hijo
        public PartialViewResult EliminarVista_ListaValorHijo(string idListaValor,int idlista)
        {
            
            //ListaValor lista = new ListaValor();

            lnUtp.UTPELIMINAR_LISTAVALORHIJO(idListaValor);


            List<ListaValor> lista = new List<ListaValor>();

            DataTable dtResultado = lnUtp.UTP_LISTAVALORHIJO(Convert.ToInt32(idlista));

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                ListaValor objListaValor = new ListaValor();
                objListaValor.IdLista = Convert.ToInt32(dtResultado.Rows[i]["IdLista"].ToString());
                objListaValor.IdListaValor = dtResultado.Rows[i]["IdListaValor"].ToString();
                objListaValor.Valor = dtResultado.Rows[i]["Valor"].ToString();
                objListaValor.DescripcionValor = dtResultado.Rows[i]["DescripcionValor"].ToString();
                objListaValor.Icono = dtResultado.Rows[i]["Icono"].ToString();
                objListaValor.Peso = Convert.ToInt32(dtResultado.Rows[i]["Peso"] == DBNull.Value ? 0 : dtResultado.Rows[i]["Peso"]);
                objListaValor.ValorUTP = dtResultado.Rows[i]["ValorUTP"].ToString();
                objListaValor.Padre = dtResultado.Rows[i]["Padre"].ToString();
                objListaValor.EstadoValor = dtResultado.Rows[i]["EstadoValor"].ToString();
                lista.Add(objListaValor);

            }

            //return RedirectToAction("Vista_ListaValorHijo");
            return PartialView("Vista_ListaValorHijo", lista); 

        }


        public ActionResult VerDetalleAlumno(int? Id)
        {


            DataTable dtresultado = lnutpalumno.UtpAlumnoMenuMostrar();

            List<SelectListItem> li = new List<SelectListItem>();

            for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
            {
                string nombre = dtresultado.Rows[i]["EstadoAlumno"].ToString();
                string valor = dtresultado.Rows[i]["CodEstado"].ToString();

                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);

            }
            ViewData["UtpAlumnoMenuMostrar"] = li;

            
            UtpAlumnoDetalle alumno = new UtpAlumnoDetalle();


            DataTable dtResultado = lnutpalumno.AlumnoUTP_ObtenerDatosPorCodigo(Convert.ToInt32(Id));

            if (dtResultado.Rows.Count > 0)
            {

                alumno.IdAlumno = Convert.ToInt32(dtResultado.Rows[0]["IdAlumno"].ToString());
                alumno.Nombres = dtResultado.Rows[0]["Nombres"].ToString();
                alumno.Apellidos = dtResultado.Rows[0]["Apellidos"].ToString();
                alumno.Carrera = dtResultado.Rows[0]["Carrera"].ToString();
                alumno.CicloEquivalente = dtResultado.Rows[0]["CicloEquivalente"].ToString();
                alumno.NumeroDocumento = dtResultado.Rows[0]["NumeroDocumento"].ToString();
                alumno.TipoDocumento = dtResultado.Rows[0]["TipoDocumento"].ToString();
                alumno.CreadoPor = dtResultado.Rows[0]["CreadoPor"].ToString();
                alumno.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaCreacion"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaCreacion"]);
                alumno.ModificadoPor = dtResultado.Rows[0]["ModificadoPor"].ToString();
                alumno.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[0]["FechaModificacion"] == DBNull.Value ? null   : dtResultado.Rows[0]["FechaModificacion"]);
                alumno.CorreoElectronico = dtResultado.Rows[0]["CorreoElectronico"].ToString();
                alumno.FechaRegistro = dtResultado.Rows[0]["FechaRegistro"].ToString();
                alumno.CodEstadoAlumno = Convert.ToString(dtResultado.Rows[0]["CodEstado"]);
                alumno.Usuario = Convert.ToString(dtResultado.Rows[0]["Usuario"]);
                alumno.CodAlumnoUtp = Convert.ToString(dtResultado.Rows[0]["CodAlumnoUtp"]);
                alumno.FechaNacimiento = Convert.ToDateTime(dtResultado.Rows[0]["FechaNacimiento"] == DBNull.Value ? null : dtResultado.Rows[0]["FechaNacimiento"]);
                alumno.Direccion = Convert.ToString(dtResultado.Rows[0]["Direccion"] == DBNull.Value ? null : dtResultado.Rows[0]["Direccion"]);
                alumno.DireccionDistrito = Convert.ToString(dtResultado.Rows[0]["DireccionDistrito"] == DBNull.Value ? null : dtResultado.Rows[0]["DireccionDistrito"]);
                alumno.DireccionCiudad = Convert.ToString(dtResultado.Rows[0]["DireccionCiudad"] == DBNull.Value ? null : dtResultado.Rows[0]["DireccionCiudad"]);
                alumno.DireccionRegion = Convert.ToString(dtResultado.Rows[0]["DireccionRegion"] == DBNull.Value ? null : dtResultado.Rows[0]["DireccionRegion"]);
                alumno.CorreoElectronico2 = Convert.ToString(dtResultado.Rows[0]["CorreoElectronico2"] == DBNull.Value ? null : dtResultado.Rows[0]["CorreoElectronico2"]);
                alumno.TelefonoFijoCasa = Convert.ToString(dtResultado.Rows[0]["TelefonoFijoCasa"] == DBNull.Value ? null : dtResultado.Rows[0]["TelefonoFijoCasa"]);
                alumno.TelefonoCelular = Convert.ToString(dtResultado.Rows[0]["TelefonoCelular"] == DBNull.Value ? null : dtResultado.Rows[0]["TelefonoCelular"]);
                
            }
            return View(alumno);

            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerDetalleAlumno([Bind(Include = "")] UtpAlumnoDetalle alumno)
        {
            //if (ModelState.IsValid)
            //{


            if (lnutpalumno.UTPAlumnos_ActualizarEstadoAlumno(alumno))
            {
                ViewBag.Message = "Datos Actualizado";
                return RedirectToAction("Alumnos");    
            }

            else
            {
                DataTable dtresultado = lnutpalumno.UtpAlumnoMenuMostrar();

                List<SelectListItem> li = new List<SelectListItem>();

                for (int i = 0; i <= dtresultado.Rows.Count - 1; i++)
                {
                    string nombre = dtresultado.Rows[i]["EstadoAlumno"].ToString();
                    string valor = dtresultado.Rows[i]["CodEstado"].ToString();

                    SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                    li.Add(item);

                }
                ViewData["UtpAlumnoMenuMostrar"] = li;
                ViewBag.Message = "Error al Actualizar";
                return View(alumno);
            }

            //}


        }

        public ActionResult AlumnoUtp_obtenerEstudios(int? Id)
        {
            List<AlumnoEstudio> lista = new List<AlumnoEstudio>();
            DataTable dtResultado = lnutpalumno.AlumnoUtp_obtenerEstudios(Convert.ToInt32(Id));
                       
            
            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlumnoEstudio alumno = new AlumnoEstudio();
                alumno.IdAlumno         = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                alumno.Institucion = Convert.ToString(dtResultado.Rows[i]["Institucion"]);
                alumno.Estudio = Convert.ToString(dtResultado.Rows[i]["Estudio"]);
                alumno.TipoDeEstudio = Convert.ToString(dtResultado.Rows[i]["TipoEstudioValor"]);
                alumno.EstadoDelEstudio = Convert.ToString(dtResultado.Rows[i]["EstadoEstudioValor"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["EstadoEstudioValor"]);
                alumno.Observacion = Convert.ToString(dtResultado.Rows[i]["ObservacionValor"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["ObservacionValor"]);
                alumno.FechaInicioMes = Convert.ToInt32(dtResultado.Rows[i]["FechaInicioMes"]);
                alumno.FechaInicioAno = Convert.ToInt32(dtResultado.Rows[i]["FechaInicioAno"]);
                alumno.FechaFinMes = Convert.ToInt32(dtResultado.Rows[i]["FechaFinMes"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["FechaFinMes"]);
                alumno.FechaFinAno = Convert.ToInt32(dtResultado.Rows[i]["FechaFinAno"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["FechaFinAno"]);
                alumno.CicloEquivalente = Convert.ToInt32(dtResultado.Rows[i]["CicloEquivalente"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["CicloEquivalente"]);
                alumno.DatoUTP = Convert.ToBoolean(dtResultado.Rows[i]["DatoUTP"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["DatoUTP"]);
                alumno.Estado = Convert.ToString(dtResultado.Rows[i]["EstadoValor"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["EstadoValor"]);
                alumno.CreadoPor = Convert.ToString(dtResultado.Rows[i]["CreadoPor"]);
                alumno.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"]);
                alumno.ModificadoPor = Convert.ToString(dtResultado.Rows[i]["ModificadoPor"] == System.DBNull.Value ? 0 : dtResultado.Rows[i]["ModificadoPor"]);
                alumno.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaModificacion"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaModificacion"]);
                lista.Add(alumno);
            }

            return PartialView("_VistaObtenerEstudiosAlumnos", lista);
        }

        public ActionResult AlumnoUtp_obtenerExperiencia(int? Id)
        {
            List<AlumnoExperienciaCargo> lista = new List<AlumnoExperienciaCargo>();
            DataTable dtResultado = lnutpalumno.AlumnoUtp_obtenerExperiencia(Convert.ToInt32(Id));


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlumnoExperienciaCargo alumnoExperiencia = new AlumnoExperienciaCargo();
                alumnoExperiencia.IdExperienciaCargo = Convert.ToInt32(dtResultado.Rows[i]["IdExperienciaCargo"]);
                alumnoExperiencia.IdAlumno = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                alumnoExperiencia.NombreComercial = Convert.ToString(dtResultado.Rows[i]["NombreComercial"]);
                alumnoExperiencia.IdEmpresa = Convert.ToInt32(dtResultado.Rows[i]["IdEmpresa"]);
                alumnoExperiencia.SectorEmpresarial = Convert.ToString(dtResultado.Rows[i]["SectorEmpresarialValor"]);
                alumnoExperiencia.SectorEmpresarial2 = Convert.ToString(dtResultado.Rows[i]["SectorEmpresarialValor2"]);
                alumnoExperiencia.SectorEmpresarial3 = Convert.ToString(dtResultado.Rows[i]["SectorEmpresarialValor3"]);
                alumnoExperiencia.Pais = Convert.ToString(dtResultado.Rows[i]["Pais"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["Pais"]);
                alumnoExperiencia.Ciudad = Convert.ToString(dtResultado.Rows[i]["Ciudad"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["Ciudad"]);
                alumnoExperiencia.NombreCargo = Convert.ToString(dtResultado.Rows[i]["NombreCargo"]);
                alumnoExperiencia.FechaInicioCargoMes = Convert.ToInt32(dtResultado.Rows[i]["FechaInicioCargoMes"]);
                alumnoExperiencia.FechaInicioCargoAno = Convert.ToInt32(dtResultado.Rows[i]["FechaInicioCargoAno"]);
                alumnoExperiencia.FechaFinCargoMes = Convert.ToInt32(dtResultado.Rows[i]["FechaFinCargoMes"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaFinCargoMes"]);
                alumnoExperiencia.FechaFinCargoAno = Convert.ToInt32(dtResultado.Rows[i]["FechaFinCargoAno"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaFinCargoAno"]);
                alumnoExperiencia.TipoCargo = Convert.ToString(dtResultado.Rows[i]["TipoCargoValor"]);
                alumnoExperiencia.DescripcionCargo = Convert.ToString(dtResultado.Rows[i]["DescripcionCargo"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["DescripcionCargo"]);
                alumnoExperiencia.Estado = Convert.ToString(dtResultado.Rows[i]["EstadoValor"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["EstadoValor"]);
                alumnoExperiencia.CreadoPor = Convert.ToString(dtResultado.Rows[i]["CreadoPor"]);
                alumnoExperiencia.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"]);
                alumnoExperiencia.ModificadoPor = Convert.ToString(dtResultado.Rows[i]["ModificadoPor"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["ModificadoPor"]);
                alumnoExperiencia.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaModificacion"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaModificacion"]);
                

                lista.Add(alumnoExperiencia);
            }

            return PartialView("AlumnoUtp_obtenerExperiencia", lista);
        }


        public ActionResult AlumnoUtp_obtenerInformacionAdicional(int? Id)
        {
            List<AlumnoInformacionAdicional> lista = new List<AlumnoInformacionAdicional>();
            DataTable dtResultado = lnutpalumno.AlumnoUtp_obtenerInformacionAdicional(Convert.ToInt32(Id));


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlumnoInformacionAdicional alumno = new AlumnoInformacionAdicional();

                alumno.IdAlumno         = Convert.ToInt32(dtResultado.Rows[i]["IdAlumno"]);
                alumno.IdInformacionAdicional = Convert.ToInt32(dtResultado.Rows[i]["IdInformacionAdicional"]);
                alumno.DesTipoConocimiento             = Convert.ToString(dtResultado.Rows[i]["TipoConocimientoValor"]);
                alumno.Conocimiento     = Convert.ToString(dtResultado.Rows[i]["Conocimiento"]);
                alumno.DesNivelConocimiento = Convert.ToString(dtResultado.Rows[i]["NivelConocimientoValor"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["NivelConocimientoValor"]);
                alumno.FechaConocimientoDesdeMes = Convert.ToInt32(dtResultado.Rows[i]["FechaConocimientoDesdeMes"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaConocimientoDesdeMes"]);
                alumno.FechaConocimientoDesdeAno = Convert.ToInt32(dtResultado.Rows[i]["FechaConocimientoDesdeAno"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaConocimientoDesdeAno"]);
                alumno.FechaConocimientoHastaMes = Convert.ToInt32(dtResultado.Rows[i]["FechaConocimientoHastaMes"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaConocimientoHastaMes"]);
                alumno.FechaConocimientoHastaAno = Convert.ToInt32(dtResultado.Rows[i]["FechaConocimientoHastaAno"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaConocimientoHastaAno"]);
                alumno.NomPais = Convert.ToString(dtResultado.Rows[i]["PaisValor"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["PaisValor"]);
                alumno.Ciudad = Convert.ToString(dtResultado.Rows[i]["Ciudad"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["Ciudad"]);
                alumno.InstituciónDeEstudio = Convert.ToString(dtResultado.Rows[i]["InstituciónDeEstudio"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["InstituciónDeEstudio"]);
                alumno.AñosExperiencia = Convert.ToInt32(dtResultado.Rows[i]["AñosExperiencia"] == System.DBNull.Value ? null : dtResultado.Rows[i]["AñosExperiencia"]);
                alumno.Estado = Convert.ToString(dtResultado.Rows[i]["Estado"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["Estado"]);
                alumno.CreadoPor = Convert.ToString(dtResultado.Rows[i]["CreadoPor"]);
                alumno.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"]);
                alumno.ModificadoPor = Convert.ToString(dtResultado.Rows[i]["ModificadoPor"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["ModificadoPor"]);
                alumno.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaModificacion"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaModificacion"]);
                lista.Add(alumno);
            }

            return PartialView("AlumnoUtp_obtenerInformacionAdicional", lista);
        }

        public ActionResult AlumnoUtp_obtenerCV(string usuario)
        {
            List<AlertasCvAlumno> lista = new List<AlertasCvAlumno>();
            
            DataTable dtResultado = lnoferta.AlertaCvAlumno(usuario);
            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                AlertasCvAlumno alumno = new AlertasCvAlumno();
                alumno.IdCV = Convert.ToInt32(dtResultado.Rows[i]["IdCV"]);
                alumno.NombreCV = Convert.ToString(dtResultado.Rows[i]["NombreCV"]);
                alumno.NombrePlantilla = Convert.ToString(dtResultado.Rows[i]["NombrePlantilla"]);
                alumno.PorcentajeCV = Convert.ToInt32(dtResultado.Rows[i]["PorcentajeCV"]);
                alumno.CreadoPor = Convert.ToString(dtResultado.Rows[i]["CreadoPor"]);
                alumno.FechaCreacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaCreacion"]);
                alumno.ModificadoPor = Convert.ToString(dtResultado.Rows[i]["ModificadoPor"] == System.DBNull.Value ? "" : dtResultado.Rows[i]["ModificadoPor"]);
                alumno.FechaModificacion = Convert.ToDateTime(dtResultado.Rows[i]["FechaModificacion"] == System.DBNull.Value ? null : dtResultado.Rows[i]["FechaModificacion"]);
                lista.Add(alumno);
            }
            return PartialView("AlumnoUtp_obtenerCV", lista);
        }

        public ActionResult LogOut()
        {
            //FormsAuthentication.SignOut();
         Session["TicketUtp"]=null;
            return RedirectToAction("Index", "Home");
        }

    

        public FileResult GetImagenLogoEmpresa(int id)
        {

            const string alternativePicturePath = @"/img/sinimagen.jpg";
            LNEmpresa lnEmpresa = new LNEmpresa();
            Empresa empresa = lnEmpresa.ObtenerDatosEmpresaPorId(id);
           

            MemoryStream stream;

            if (empresa != null && empresa.LogoEmpresa != null)
            {
                stream = new MemoryStream(empresa.LogoEmpresa);
            }
            else
            {
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
            }

            return new FileStreamResult(stream, "image/jpeg");
        }

        public FileResult GetImagenAlumno(int idAlumno)
        {
            Alumno registroAlumno = new Alumno();
            registroAlumno = lnalumno.Alumno_ObtenerFoto(idAlumno);
            string MimeTypeCadena = registroAlumno.ArchivoMimeType;
            const string alternativePicturePath = @"/img/sinimagen.jpg";
            
            MemoryStream stream;

            if (registroAlumno.Foto != null)
            {
                stream = new MemoryStream(registroAlumno.Foto);
            }
            else
            {
                stream = new MemoryStream();

                var path = Server.MapPath(alternativePicturePath);
                var image = new System.Drawing.Bitmap(path);

                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);
                MimeTypeCadena = "image/jpeg";
            }

            return new FileStreamResult(stream, MimeTypeCadena);
        }

        //public ActionResult EventoInscritos()
        //{
        //    return View();
        //}

        public ActionResult EventoInscritos(int? Id)
        {
            List<EventoAsistente> listaevento = new List<EventoAsistente>();

            DataTable dtResultado = lnEventos.UTP_INSCRITOS_EVENTOS(Convert.ToInt32(Id));

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {

                EventoAsistente evento = new EventoAsistente();
                evento.FechaInscripcion = Convert.ToDateTime(dtResultado.Rows[i]["FechaInscripcion"] == DBNull.Value ? null : dtResultado.Rows[i]["FechaInscripcion"]);
                evento.IdEvento = Convert.ToInt32(dtResultado.Rows[i]["IdEvento"]);
                evento.Usuario = Convert.ToString(dtResultado.Rows[i]["Usuario"]);
                evento.Nombres = Convert.ToString(dtResultado.Rows[i]["Nombres"]);
                evento.Apellidos = Convert.ToString(dtResultado.Rows[i]["Apellidos"]);
                evento.Sexo = Convert.ToString(dtResultado.Rows[i]["Sexo"]);
                evento.ValorTipoDocumento = Convert.ToString(dtResultado.Rows[i]["ValorTipoDocumento"]);
                evento.NumeroDocumento = Convert.ToString(dtResultado.Rows[i]["NumeroDocumento"]);
                evento.ValorEstadoTicket = Convert.ToString(dtResultado.Rows[i]["ValorEstadoTicket"]);
                evento.FechaAsistencia = Convert.ToDateTime(dtResultado.Rows[i]["FechaAsistencia"] == DBNull.Value ? null : dtResultado.Rows[i]["FechaAsistencia"]);

                evento.NombreEvento = Convert.ToString(dtResultado.Rows[i]["NombreEvento"]);

                listaevento.Add(evento);
            }

            return View(listaevento);

        }


        #region Mantenimiento de usuarios

        
        /// <summary>
        /// Obtener la lista de usuarios UTP:
        /// </summary>
        /// <returns></returns>
    
       

        /// <summary>
        /// HttpGet del partialview para la creación de usuario UTP:
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _UsuariosUTPCrear()
        {
            
                 LNGeneral lnGeneral = new LNGeneral();

                 UTPUsuario utpUsuario = new UTPUsuario();

                 //Sexo, Roles y Estado
                 ViewBag.SexoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SEXO), "IdListaValor", "Valor");
                 ViewBag.RolIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ROL_USUARIO), "IdListaValor", "Valor");
                 ViewBag.EstadoUsuarioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_USUARIO), "IdListaValor", "Valor");

                 return PartialView("_UsuariosUTPCrear", utpUsuario);

        }


        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _UsuariosUTPCrear(UTPUsuario utpUsuario)
        {
     
 
            TicketUTP ticket = (TicketUTP)Session["TicketUtp"];

            utpUsuario.TipoUsuarioIdListaValor = "USERUT"; //Tipo usuario UTP
            utpUsuario.CreadoPor = ticket.Usuario;
            lnUtp.Insertar(utpUsuario);

            List<UTPUsuario> lista = lnUtp.ObtenerUsuariosUTP();
            return PartialView("_UsuariosUTPLista", lista);


        }
       




        /// <summary>
        /// HttpGet del partialview para la edición de usuario UTP:
        /// </summary>
        /// <returns></returns>
        public PartialViewResult _UsuariosUTPEditar(int id)
        {
            int idUTPUsuario = id;
            
            //1. Obtener al usuario UTP:
            LNUTP lnUTP = new LNUTP();
            LNGeneral lnGeneral = new LNGeneral();

            UTPUsuario utpUsuario = lnUtp.ObtenerUsuarioUTPPorId(idUTPUsuario);

            //2. Se cargan los combos: Sexo, Roles y Estado
            ViewBag.SexoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SEXO), "IdListaValor", "Valor", utpUsuario.SexoIdListaValor);
            ViewBag.RolIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ROL_USUARIO), "IdListaValor", "Valor", utpUsuario.RolIdListaValor);
            ViewBag.EstadoUsuarioIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_ESTADO_USUARIO), "IdListaValor", "Valor", utpUsuario.EstadoUsuarioIdListaValor);

            return PartialView("_UsuariosUTPEditar", utpUsuario);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public PartialViewResult _UsuariosUTPEditar(UTPUsuario utpUsuario)
        {
            TicketUTP ticket = (TicketUTP)Session["TicketUtp"];

            utpUsuario.ModificadoPor = ticket.Usuario;
            lnUtp.Actualizar(utpUsuario);

            List<UTPUsuario> lista = lnUtp.ObtenerUsuariosUTP();
            return PartialView("_UsuariosUTPLista", lista);
        }

        [HttpGet]
        public ActionResult UsuarioSistemaUTP_Exitencia(string Usuario)
        {
            int cantidad;
            cantidad = lnUtp.UsuarioSistemaUTP_Exitencia(Usuario);

            //No debe retornar vistas.
            return Content(cantidad.ToString());
        }


        #endregion

        public ActionResult BuscarDatosAlumno(int idAlumno)
        {
            //string descripocn = "";


            Convenio vista = new Convenio();

            DataTable dtResultado = lnUtp.Utp_BuscarDatosAlumno(idAlumno);

            if (dtResultado.Rows.Count > 0)
            {
                vista.IdAlumno = Convert.ToInt32(dtResultado.Rows[0]["IdAlumno"]);
                vista.Carrera = dtResultado.Rows[0]["Carrera"].ToString();
                vista.Ciclo = Convert.ToInt32(dtResultado.Rows[0]["Ciclo"] == System.DBNull.Value ? null : dtResultado.Rows[0]["Ciclo"]);
                vista.TelefonoFijoCasa = dtResultado.Rows[0]["TelefonoFijoCasa"].ToString();
                vista.TelefonoCelular = dtResultado.Rows[0]["TelefonoCelular"].ToString();
                
            }

            return Json(vista, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _EmpresaNuevaModal()
        {
            LNGeneral lnGeneral = new LNGeneral();
            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor", "PAIPER");
            ViewBag.NumeroEmpleadosIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_NRO_EMPLEADOS), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial2IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.SectorEmpresarial3IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor");
            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor");
            
            DataTable dtDepartamento = lnGeneral.Home_Departamento(Constantes.IDLISTA_Departamento);
            List<SelectListItem> li = new List<SelectListItem>();
            for (int i = 0; i <= dtDepartamento.Rows.Count - 1; i++)
            {
                string nombre = dtDepartamento.Rows[i]["Valor"].ToString();
                string valor = dtDepartamento.Rows[i]["IdListaValor"].ToString();
                SelectListItem item = new SelectListItem() { Text = nombre, Value = valor };

                li.Add(item);
            }
            ViewData["Departamento"] = li;



            return PartialView("_EmpresaNuevaModal");
        }
        [HttpPost]
        public ActionResult _EmpresaNuevaModal(VistaRegistroEmpresa empresa)
        {
            LNUsuario lnUsuario = new LNUsuario();
            StringBuilder mensajeDeError = new StringBuilder();
            if (lnUsuario.ValidarExistenciaEmpresa(empresa.PaisIdListaValor, empresa.IdentificadorTributario))
            {
                mensajeDeError.Append("La Empresa ya se encuentra registrada<br />");
            }
            if (mensajeDeError.ToString() == "")
            {
                LNEmpresa lnEmpresa = new LNEmpresa();
                //Empresa
                TicketUTP ticket = (TicketUTP)Session["TicketUTP"];
                empresa.CreadoPor = ticket.Usuario;
                
                empresa.EstadoIdListaValor = "EMPRNO"; //Estado de la empresa No Activa.
                //Ubicación
                empresa.EstadoLocacionIdListaValor = "LOSTNO"; //Estado NO ACTIVA. Se debe activar al momento que UTP active la cuenta. 


                if (empresa.PaisIdListaValor == "PAIPER")
                {
                    empresa.NombreLocacion = empresa.DireccionLocacion + ", " + empresa.TextDistrito + ", " + empresa.TextoCiudad + ", " + empresa.TextoDepartamento;

                }
                else
                {
                    empresa.NombreLocacion = empresa.DireccionLocacion + ", " + empresa.DireccionDistritoLocacion + ", " + empresa.DireccionCiudadLocacion + ", " + empresa.DireccionDepartamentoLocacion;
                }

                lnEmpresa.Insertar(empresa);


                

                //Si el registro fue exitoso redireccionar a página de resultado.
                TempData["GuardaRegistroExitoso"] = "La Empresa <strong>" + empresa.NombreComercial
                + "</strong>se ha registrado con éxito. ";
                //Aquí debería enviarse un correo
                //return PartialView();
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                //Variable temporal para poner el break
                int a = 0;
            }
            LNGeneral lnGeneral = new LNGeneral();
            ViewBag.PaisIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_PAIS), "IdListaValor", "Valor", empresa.PaisIdListaValor);
            ViewBag.SectorEmpresarial1IdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_SECTOR_EMPRESARIAL), "IdListaValor", "Valor", empresa.SectorEmpresarial1IdListaValor);
            ViewBag.TipoLocacionIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_LOCACION), "IdListaValor", "Valor", empresa.TipoLocacionIdListaValor);
            ViewBag.TipoDocumentoIdListaValor = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_TIPO_DOCUMENTO), "IdListaValor", "Valor", empresa.TipoDocumentoIdListaValor);


            ViewBag.DireccionDepartamentoLocacion = new SelectList(lnGeneral.ObtenerListaValor(Constantes.IDLISTA_Departamento), "IdListaValor", "Valor", empresa.DireccionDepartamentoLocacion);


            ViewBag.MensajeDeError = mensajeDeError;
            return RedirectToAction("Empresas");
        }

    }
}