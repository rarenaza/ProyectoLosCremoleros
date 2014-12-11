using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Datos;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNOfertaEmpresa
    {
        ADOferta adOferta = new ADOferta();
                
        public void Insertar(Oferta oferta)
        {
            try
            {
                if (oferta.Funciones == null) oferta.Funciones = string.Empty;
                if (oferta.Competencias == null) oferta.Competencias = string.Empty;
                if (oferta.AreaEmpresa == null) oferta.AreaEmpresa = string.Empty;
                if (oferta.TipoCargoIdListaValor == null) oferta.TipoCargoIdListaValor = string.Empty;
                if (oferta.TipoContratoIdListaValor == null) oferta.TipoContratoIdListaValor = string.Empty;
                if (oferta.Horario == null) oferta.Horario = string.Empty;

                adOferta.Insertar(oferta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }



    }
}
