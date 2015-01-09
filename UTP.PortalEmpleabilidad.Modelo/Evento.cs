using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
  public   class Evento
    {

      public int  IdEvento { get; set; }
      public string NombreEvento { get; set; }

      public string EstadoEvento { get; set; }
      
      public string ListaEstadoEvento {get;set;}
      public string ListaTipoEvento { get; set; }
      
      public string TipoEvento { get; set; }
      public int IdEmpresa { get; set; }
       public string DescripcionEvento { get; set; }
 
      public byte[] PosterGrandeEvento { get; set; }

	  public byte []PosterMedianoEvento{get;set;}
      public byte []PosterChicoEvento{get;set;}
       
      public DateTime  FechaEvento { get; set; }

      public string FechaEventoTexto { get; set; }

       public string DireccionEvento { get; set; }
      public string DireccionDistrito{get;set;}
	
      public string DireccionCiudad{get;set;}

       public string DireccionRegion{get;set;}
      public string DireccionPais{get;set;}
	
	 public int AsistentesEsperados { get; set; }

      public int AsistentesReales { get; set; }

     public byte []ImagenTicket{get;set;}

    public int  RegistraAlumnos{get; set;}

	public int  RegistraUsuariosEmpresa{get; set;}

    public int RegistraPublicoEnGeneral{get;set;}
	
   public string CreadoPor{get;set;}
   public DateTime FechaCreacion{get;set;}
   
   public string ModificadoPor{get;set;}

   public DateTime FechaModificacion{get;set;}
   public string LugarEvento { get; set; }

   public string NombreComercial { get; set; }

   public DateTime FechaInscripcion { get; set; }
   public string EstadoTicket { get; set; }
   public string ValorEstadoTicket { get; set; }
   public DateTime FechaAsistencia { get; set; }
    }
}
