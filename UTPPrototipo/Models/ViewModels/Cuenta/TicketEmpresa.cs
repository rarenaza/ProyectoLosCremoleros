using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models.ViewModels.Cuenta
{
    public class TicketEmpresa
    {
        public string Idempresa { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string DNI { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoCelular { get; set; }
        public string TipoUsuario { get; set; }

    }
}