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
                listacontenido.Imagen = dtResultado.Rows[i]["Imagen"].ToString();
               
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


        public List<Contenido> Contenido_Buscar(string IdLista)
        {
            List<Contenido> lista = new List<Contenido>();


            DataTable dtResultado = ad.Contenido_Buscar(IdLista);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                Contenido contenido     = new Contenido();

                contenido.Menu       = dtResultado.Rows[i]["CodMenu"].ToString();
                contenido.Titulo        = dtResultado.Rows[i]["Titulo"].ToString();
                contenido.SubTitulo     = dtResultado.Rows[i]["SubTitulo"].ToString();
                contenido.Descripcion   = dtResultado.Rows[i]["Descripcion"].ToString();
                contenido.Imagen        = dtResultado.Rows[i]["Imagen"].ToString();
                lista.Add(contenido);

            }


            return lista;
        }

      
    }
}
