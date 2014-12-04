﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class EmpresaUsuario
    {
        public int IdEmpresaUsuario { get; set; }
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public ListaValor TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public ListaValor Sexo { get; set; }
        public string CorreoElectronico { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoAnexo { get; set; }
        public string TelefonoCelular { get; set; }
        public EmpresaLocacion EmpresaLocacion { get; set; }

        public EmpresaUsuario() 
        {
            Empresa = new Empresa();
            Usuario = new Usuario();
            TipoDocumento = new ListaValor();
            Sexo = new ListaValor();
        }

    }
}
