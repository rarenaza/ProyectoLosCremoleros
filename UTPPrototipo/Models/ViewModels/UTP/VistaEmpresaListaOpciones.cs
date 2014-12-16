using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Models.ViewModels.UTP
{
    public class VistaEmpresaListaOpciones
    {
        public List<ListaValor> ListaEstado { get; set; }
        public List<ListaValor> Listasector { get; set; }

        public string IdEstadoEmpresa { get; set; }

        public string IdSector { get; set; }
    }
}