using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTP.PortalEmpleabilidad.Logica
{
  public  class LNContenido
    {
      ADContenido ad = new ADContenido();


      //public List<Contenido> Contenido_ObtenerPorCodMenu(int codMenu)
      //{
      //    List<Contenido> contenido = new List<Contenido>();

      //    DataTable dtResultado = ad.Contenido_ObtenerPorCodMenu(codMenu);


      //    for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
      //    {
      //        Contenido listacontenido = new Contenido();
      //        listacontenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
      //        listacontenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
      //        listacontenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
      //        listacontenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
      //        listacontenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();

      //        listacontenido.Imagen = Encoding.UTF8.GetBytes(dtResultado.Rows[i]["Imagen"].ToString());

      //        listacontenido.TituloMenu = dtResultado.Rows[i]["Menu"].ToString();


      //        contenido.Add(listacontenido);
      //    }
      //    return contenido;
      //}

      public DataTable Contenido_ObtenerPorCodMenu(int codMenu)
      {

          return ad.Contenido_ObtenerPorCodMenu(codMenu);
      }


      public DataTable Contenido_Mostrar_imagen()
      {

          return ad.Contenido_Mostrar_imagen();
      }
    
        public DataTable ContenidoMenu_Mostrar()
        {
            return ad.ContenidoMenu_Mostrar();
        }

        public bool Contenido_insertar(Contenido  contenido)
        {


            if (ad.Contenido_Insertar  (contenido) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
      

        //muestra datos en el index solo lo que me instereza mostrar
        public List<Contenido> Contenido_BuscarIndex(string IdLista)
        {
            List<Contenido> lista = new List<Contenido>();


            DataTable dtResultado = ad.Contenido_BuscarIndex(IdLista);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido contenido     = new Contenido();

                contenido.Menu          = dtResultado.Rows[i]["CodMenu"].ToString();
                contenido.Titulo        = dtResultado.Rows[i]["Titulo"].ToString();
                contenido.SubTitulo     = dtResultado.Rows[i]["SubTitulo"].ToString();
                contenido.Descripcion   = dtResultado.Rows[i]["Descripcion"].ToString();

                contenido.Imagen = Encoding.UTF8.GetBytes (dtResultado.Rows[i]["Imagen"].ToString());
                //contenido.Imagen        = dtResultado.Rows[i]["Imagen"].ToString();

                contenido.IdContenido   = Convert.ToInt32 (dtResultado.Rows[i]["IdContenido"]);

                contenido.EnPantallaPrincipal = Convert.ToBoolean(dtResultado.Rows[i]["EnPantallaPrincipal"]);

                lista.Add(contenido);

            }


            return lista;
        }

          

        public List<Contenido> Contenido_BuscarNoticiasEventosOtros(string IdLista)
        {
            List<Contenido> lista = new List<Contenido>();


            DataTable dtResultado = ad.Contenido_BuscarNoticiasEventosOtros(IdLista);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido contenido = new Contenido();

                contenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                contenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                contenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                contenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();
                contenido.Imagen = (byte[])dtResultado.Rows[i]["Imagen"];
                //contenido.Imagen = Encoding.UTF8.GetBytes(dtResultado.Rows[i]["Imagen"].ToString());
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
   

                lista.Add(contenido);

            }


            return lista;
        }

        public DataTable ContenidoEDitar_Buscar(int Cod)
        {
            return ad.ContenidoEDitar_Buscar(Cod);
        }
      
        public bool Contenido_Actualizar(Contenido contenido)
        {


            if (ad.Contenido_Actualizar(contenido) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool Contenido_RemoverImagen(int id)
        {
            //return ad.Contenido_RemoverImagen(contenido);

            if (ad.Contenido_RemoverImagen(id) == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

      

      
    }
}
