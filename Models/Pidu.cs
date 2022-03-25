using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    public class Pidu
    {
        public int PiduId { set; get; }
        [Required(ErrorMessage = "On vaja sisesta oma nime!!!")]
        public string Pidu_nimi { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Paev { get; set; }
    }
}