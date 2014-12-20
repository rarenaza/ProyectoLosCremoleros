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
    public class LNPlantillaCV
    {
        ADPlantilla pcv = new ADPlantilla();
        public List<PlantillaCV> MostrarPlantillaCV()
        {
            List<PlantillaCV> listaPlantillaCV = new List<PlantillaCV>();

            DataTable dtResultado = pcv.MostrarPlantillaCV();

            if (dtResultado.Rows.Count > 0)
            {
                for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
                {
                    PlantillaCV plantillacv = new PlantillaCV();
                    plantillacv.IdPlantillaCV = int.Parse(dtResultado.Rows[i]["IdPlantillaCV"].ToString());
                    plantillacv.Plantilla = dtResultado.Rows[i]["PlantillaCV"].ToString();
                    plantillacv.DescripcionPlantilla =dtResultado.Rows[i]["DescripcionPlantilla"].ToString();
                    listaPlantillaCV.Add(plantillacv);
                }


            }

            return listaPlantillaCV;
        }

        public DataSet ObtenerDatosParaPlantilla(int IdCV)
        { 
            ADPlantilla adPlantilla = new ADPlantilla ();

            return adPlantilla.ObtenerDatosParaPlantilla(IdCV);
        }
    }
}
