using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
  public   class Evento
    {
   
      public int  IdEvento { get; set; }

      [Required(ErrorMessage = "Ingrese Nombre de Evento")]
      
      public string NombreEvento { get; set; }
  
      [Required(ErrorMessage = "Seleccione Estado de Evento")]
      public string EstadoEvento { get; set; }    
      public string ValorEstadoEvento { get; set; }
      
      public string ListaEstadoEvento {get;set;}
      public string ListaTipoEvento { get; set; }
      public string ListaEmpresa { get; set; }
    
      [Required(ErrorMessage = "Seleccione Tipo de Evento")]
      public string TipoEvento { get; set; }
      public string ValorTipoEvento { get; set; }

      [Required(ErrorMessage = "Seleccione Empresa Auspiciadora")]
      public int IdEmpresa { get; set; }
       public string DescripcionEvento { get; set; }
 
      //public byte[] PosterGrandeEvento { get; set; }

      //public byte []PosterMedianoEvento{get;set;}
      //public byte []PosterChicoEvento{get;set;}
        [Required(ErrorMessage = "Seleccione Fecha de Evento")]
      public DateTime  FechaEvento { get; set; }

      public string FechaEventoTexto { get; set; }

       public string DireccionEvento { get; set; }
  
      [Required(ErrorMessage = "Ingrese Distrito")]
      public string DireccionDistrito{get;set;}
   
    [Required(ErrorMessage = "Ingrese Ciudad")]
      public string DireccionCiudad{get;set;}
      
      [Required(ErrorMessage = "Ingrese Departamento")]
       public string DireccionRegion{get;set;}
      public string DireccionPais{get;set;}
     
       [Required(ErrorMessage = "Ingrese Nro Asistente Esperado")]
	 public int AsistentesEsperados { get; set; }

      public int AsistentesReales { get; set; }
      
      public byte []ImagenEvento { get; set; }

      public string ArchivoNombreOriginalImagenEvento { get; set; }
      public string ArchivoMimeTypeImagenEvento { get; set; }

     public byte []ImagenTicket{get;set;}

     public string ArchivoNombreOriginalImagenTicket { get; set; }
     public string ArchivoMimeTypeImagenEventoTicket { get; set; }


     public bool  RegistraAlumnos { get; set; }

     public bool  RegistraUsuariosEmpresa { get; set; }

     public bool  RegistraPublicoEnGeneral { get; set; }
	
   public string CreadoPor{get;set;}
   public DateTime FechaCreacion{get;set;}
   
   public string ModificadoPor{get;set;}

   public DateTime FechaModificacion{get;set;}

   

       [Required(ErrorMessage = "Ingrese Lugar del Evento")]
   public string LugarEvento { get; set; }

   public string NombreComercial { get; set; }

   public DateTime FechaInscripcion { get; set; }
   public string EstadoTicket { get; set; }
   public string ValorEstadoTicket { get; set; }
   public DateTime FechaAsistencia { get; set; }
   public string Nombres { get; set; }
   public string Apellidos { get; set; }
   public string TipoDocumento { get; set; }
   public string ValorTipoDocumento { get; set; }
   public string NumeroDocumento { get; set; }
   public byte[] LogoEmpresa { get; set; }
   public string ValorDireccionPais { get; set; }
   public int IdEventoAsistente { get; set; }
    }
}
