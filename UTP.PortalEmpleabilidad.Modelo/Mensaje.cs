﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class Mensaje
    {
        public int IdMensaje { get; set; }
        public string DeUsuario { get; set; }
        public string DeUsuarioCorreoElectronico { get; set; }
        public string ParaUsuario { get; set; }
        public string ParaUsuarioCorreoElectronico { get; set; }
        public int IdOferta { get; set; }
        public Oferta Oferta { get; set; }
        public int IdEvento { get; set; }
        public Evento Evento { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Asunto { get; set; }
        public string MensajeTexto { get; set; }
        public string EstadoMensaje { get; set; }
        public DateTime FechaLectura { get; set; }
        public int IdMensajePadre { get; set; }
    }
}