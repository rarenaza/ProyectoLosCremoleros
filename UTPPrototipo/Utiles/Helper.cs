using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UTP.PortalEmpleabilidad.Logica;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Utiles
{
    public static class Helper
    {
        public static List<SelectListItem> DevolverListaItems(List<ListaValor> lista)
        {
            List<SelectListItem> items= new List<SelectListItem>();

            foreach (var tipo in lista)
            {
                SelectListItem nuevoItem = new SelectListItem() { Text = tipo.Valor, Value = tipo.IdListaValor };
                items.Add(nuevoItem);
            }

            return items;
        }
    }
}