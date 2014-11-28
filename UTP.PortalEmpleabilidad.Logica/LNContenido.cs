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


        public List<Contenido> Contenido_Mostrar()
        {
            List<Contenido> contenido = new List<Contenido>();

            DataTable dtResultado = ad.Contenido_Mostrar();


            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido listacontenido = new Contenido();
                listacontenido.IdContenido = Convert.ToInt32 (dtResultado.Rows[i]["IdContenido"]);
                listacontenido.Menu = dtResultado.Rows[i]["CodMenu"].ToString();
                listacontenido.Titulo = dtResultado.Rows[i]["Titulo"].ToString();
                listacontenido.SubTitulo = dtResultado.Rows[i]["SubTitulo"].ToString();
                listacontenido.Descripcion = dtResultado.Rows[i]["Descripcion"].ToString();

                listacontenido.Imagen = Encoding.UTF8 .GetBytes  (dtResultado.Rows[i]["Imagen"].ToString());
                
                
                //listacontenido.Imagen = dtResultado.Rows[i]["Imagen"].ToString();
              
                contenido.Add(listacontenido);
            }
            return contenido;
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
                //contenido.Imagen = dtResultado.Rows[i]["Imagen"].ToString();
                contenido.Imagen = Encoding.UTF8.GetBytes(dtResultado.Rows[i]["Imagen"].ToString());
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[i]["IdContenido"]);
   

                lista.Add(contenido);

            }


            return lista;
        }


        public Contenido ContenidoEDitar_Buscar(int Id)
        {
         
            Contenido contenido = new Contenido();

            DataTable dtResultado = ad.ContenidoEDitar_Buscar(Id);
            
            if (dtResultado.Rows.Count > 0)
            {
                contenido.IdContenido = Convert.ToInt32(dtResultado.Rows[0]["IdContenido"]);
                contenido.Menu = dtResultado.Rows[0]["CodMenu"].ToString();
                contenido.Titulo = dtResultado.Rows[0]["Titulo"].ToString();
                contenido.SubTitulo = dtResultado.Rows[0]["SubTitulo"].ToString();
                contenido.Descripcion = dtResultado.Rows[0]["Descripcion"].ToString();
                contenido.Imagen = Encoding.UTF8.GetBytes(dtResultado.Rows[0]["Imagen"].ToString());
                contenido.EnPantallaPrincipal = Convert.ToBoolean(dtResultado.Rows[0]["EnPantallaPrincipal"].ToString());
                contenido.ArchivoNombreOriginal = dtResultado.Rows[0]["ArchivoNombreOriginal"].ToString();
                //contenido.Imagen = dtResultado.Rows[0]["Imagen"].ToString();
                            
            }
            return contenido;

          
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
