using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.UTP;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Empresa;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;
using UTPPrototipo.Models;
using UTPPrototipo.Models.ViewModels.Contenido;
using UTPPrototipo.Models.ViewModels.Cuenta;
using UTPPrototipo.Models.ViewModels.UTP;

namespace UTPPrototipo.Controllers
{
    public class UTPController : Controller
    {
        LNContenido ln = new LNContenido();
        LNAutenticarUsuario lnAutenticar = new LNAutenticarUsuario();
        LNUTP lnUtp = new LNUTP();
        LNEmpresaListaOferta lnEmpresa = new LNEmpresaListaOferta();
        LNOferta lnoferta = new LNOferta();
        // GET: UTP
        public ActionResult Index()
        {

            return View();
                       
     
        }
       
        public ActionResult Portal(string id, string Menu)
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
                     

            //List<Contenido> contenido = new List<Contenido>();


            //int codMenu = Convert.ToInt32(Menu);

            //if (Menu == null)
            //{
            //    contenido  = ln.Contenido_ObtenerPorCodMenu(0);
            //}
            //else
            //{
            //    contenido = ln.Contenido_ObtenerPorCodMenu(codMenu);
            //}
            
                      

            //return View(contenido);
            List<Contenido> lista = new List<Contenido>();

            try
            {
           

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
            }
            catch (Exception )
            {
        
                
            }
           

            List<ContenidoVista> contentModel = lista.Select(item => new ContenidoVista()
            {
                IdContenido = item.IdContenido,
                Menu =item.Menu,
                Titulo = item.Titulo,
                SubTitulo  = item.SubTitulo,
                Descripcion =item .Descripcion,
                Imagen = item.Imagen,
                TituloMenu =item .TituloMenu 

            }).ToList();
            return View(contentModel);

            //return View();
          
        }

        public ActionResult Empresas()
        {

            //VistaEmpresaListaOpciones utp = new VistaEmpresaListaOpciones();
            VistaEmpresListarOfertas utp = new VistaEmpresListarOfertas();


            LNGeneral lngeneral = new LNGeneral();

            utp.ListaEstado = lngeneral.ObtenerListaValor(5);
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
            ViewBag.ListaEstadoEstudio = listItemSector;

            return View(utp);                 

        }



        public ActionResult BusquedaSimpleEmpresasActivas(VistaEmpresListarOfertas entidad)
        {
            entidad.ListaBusqueda = lnUtp.Empresa_ObtenerPorNombre(entidad.PalabraClave==null ? "" :entidad.PalabraClave);


            return PartialView("_ResultadoBusquedaEmpresas", entidad.ListaBusqueda);
    
        }



        //public ActionResult prueba(string sortOrder, string currentFilter, string searchString, int? page)
        //{



        //    if (searchString != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewBag.CurrentFilter = searchString;

            

        //    List<VistaEmpresListarOfertas> listaEjemplo = new List<VistaEmpresListarOfertas>();
        

        //    if (!String.IsNullOrEmpty(searchString))
        //    {

        //        DataTable dtResultado = lnUtp.Empresa_ObtenerPorNombre(searchString);

        //        for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
        //        {
        //            VistaEmpresListarOfertas vista = new VistaEmpresListarOfertas();
        //            vista.NombreComercial = dtResultado.Rows[i]["Nombre"].ToString();
        //            vista.RazonSocial = dtResultado.Rows[i]["Razon"].ToString();
        //            vista.RUC = dtResultado.Rows[i]["RUC"].ToString();
        //            vista.Estado = dtResultado.Rows[i]["Estado"].ToString();
        //            vista.SectorEmpresarial = dtResultado.Rows[i]["SectorEmpresarial"].ToString();

        //            listaEjemplo.Add(vista);
        //        }

        //    }
        //    else
        //    {
        //        List<VistaEmpresListarOfertas> lista = new List<VistaEmpresListarOfertas>();

        //        lista = lnEmpresa.ObtenerEmpresaListaOfertas();

        //        return View(lista);
        //    }
        
        //    return View(listaEjemplo); 
 

        //}

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

        public ActionResult Alumnos()
        {
            return View();
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

            Empresa empresa = lnEmpresa.ObtenerDatosEmpresaPorId(idEmpresa);

            return PartialView("_VerDetalleEmpresaDatosGenerales", empresa);
        }

        public ActionResult EmpresaUsuario()
        {
            return View();
        }
        public ActionResult EmpresaLocacion()
        {
            return View();
        }
        public ActionResult Oferta()
        {
            return View();
        }
        public ActionResult Sistema()
        {
            return View();
        }
        public ActionResult Reportes()
        {
            return View();
        }
        public ActionResult Eventos()
        {
            return View();
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
      
        public FileResult Imagen(int id)
        {
            const string alternativePicturePath = @"/Content/Images/question_mark.jpg";

            List<Contenido> lista = new List<Contenido>();

            DataTable dtResulatdo = ln.Contenido_Mostrar_imagen();

            for (int i = 0; i <= dtResulatdo.Rows.Count - 1; i++)
            {
                Contenido contenido = new Contenido();
                contenido.IdContenido = Convert.ToInt32(dtResulatdo.Rows[i]["IdContenido"]);
                contenido.Titulo = dtResulatdo.Rows[i]["Titulo"].ToString();
                contenido.Imagen = (byte[])dtResulatdo.Rows[i]["Imagen"];

                //contenido.Imagen = Encoding.UTF8.GetBytes(dtResulatdo.Rows[i]["Imagen"].ToString());

                lista.Add(contenido);
            }

            Contenido producto = lista.Where(k => k.IdContenido== id).FirstOrDefault();

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

        public ActionResult VistaCabeceraUtp()
        {
      
            TicketUTP ticketUtp = (TicketUTP)Session["TicketUtp"];
         
            VistaPanelCabeceraUTP panel = new VistaPanelCabeceraUTP();

            panel = lnAutenticar.ObtenerPanelCabeceraUTP(ticketUtp.Usuario);

            return PartialView("_DatosUtp", panel);
        }


        public ActionResult VistaOfertasPendientes()
        {
            List<VistaOfertasPendientes> listaOfertasPendientes = new List<VistaOfertasPendientes>();

            DataTable dtResultado = lnUtp.OfertasObtenerPendientes();

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaOfertasPendientes vistaNueva = new VistaOfertasPendientes();

                vistaNueva.FechaPublicacion = Convert.ToDateTime(fila["FechaPublicacion"]);
                vistaNueva.NombreComercial = Convert.ToString(fila["NombreComercial"]);
                vistaNueva.CargoOfrecido = Convert.ToString(fila["CargoOfrecido"]);
                vistaNueva.IdOferta = Convert.ToInt32(fila["IdOferta"]);

                listaOfertasPendientes.Add(vistaNueva);
            }

            return PartialView("_OfertasPendientes", listaOfertasPendientes);

        }

        public ActionResult Vista_EmpresasPendientes()
        {
            List<VistaEmpresasPendientes> listaEmpresasPendientes = new List<VistaEmpresasPendientes>();

            DataTable dtResultado = lnUtp.EmpresaObtenerPendientes();

            foreach (DataRow fila in dtResultado.Rows)
            {
                VistaEmpresasPendientes vistaNueva = new VistaEmpresasPendientes();

                vistaNueva.FechaCreacion = Convert.ToDateTime(fila["FechaCreacion"]);
                vistaNueva.NombreComercial = Convert.ToString(fila["NombreComercial"]);

                vistaNueva.IdEmpresa = Convert.ToInt32(fila["IdEmpresa"]);

                listaEmpresasPendientes.Add(vistaNueva);
            }

            return PartialView("_EmpresasPendientes", listaEmpresasPendientes);

        }

       
    }
}