using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UTPPrototipo.Models
{
    public class Temporal
    {
        public int Id { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}