using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorSystem.Models
{
    public class subject
    {
        [Required(ErrorMessage ="select subject")]
        public string title { get; set; }
        [Required(ErrorMessage = "enter price")]
        public int price { get; set; }
        [Required(ErrorMessage = "select time")]
        public string timing { get; set; }
    }
}