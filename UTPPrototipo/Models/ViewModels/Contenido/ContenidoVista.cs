﻿using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UTP.PortalEmpleabilidad.Modelo;

namespace UTPPrototipo.Models.ViewModels.Contenido
{
    public class ContenidoVista
    {
            [NotMapped] 
        public int id { get; set; }
        public int IdContenido { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
         
        public string Descripcion { get; set; }
      
        public HttpPostedFileBase ImagenHtml { get; set; }
        public string ArchivoNombreOriginal { get; set; }
        public string ArchivoMimeType { get; set; }
        //[Required]
        public  byte[] Imagen { get; set; }
            [Required(ErrorMessage = Constantes.MSJ_CAMPO_OBLIGATORIO)]
        public string Menu { get; set; }
             

        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }

        public bool EnPantallaPrincipal { get; set; }
        public bool Activo { get; set; }
        public string TituloMenu { get; set; }

        public string Pestana { get; set; }
    }
}