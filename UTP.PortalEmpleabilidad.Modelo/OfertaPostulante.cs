using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTP.PortalEmpleabilidad.Modelo
{
    public class OfertaPostulante
    {
        public int IdOferta { get; set; }
        public int IdOfertaPostulante { get; set; }
        public Oferta Oferta { get; set; }
        public Alumno Alumno { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public ListaValor FaseOferta { get; set; }
        public int IdCV { get; set; }
        public byte[] DocumentoCV { get; set; }
        public int NivelDeMatch { get; set; }

        /// <summary>
        /// Columna de ayuda para mover a los postulantes entre fases. No es una columna en la tabla.
        /// </summary>
        public bool Seleccionado { get; set; }
        public string ModificadoPor { get; set; }
        public OfertaPostulante()
        {
            Oferta = new Oferta();
            Alumno = new Alumno();
            FaseOferta = new ListaValor();
        }

    }
}
