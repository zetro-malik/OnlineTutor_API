using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineTutorSystem.Models
{
    public class SearchTutor
    {
        [Required(ErrorMessage ="Please Select City")]
        public String city { get; set; }
        [Required(ErrorMessage = "Please Select Class")]
        public String classID { get; set; }
        [Required(ErrorMessage = "Please Select Cubect")]
        public String Subject { get; set; }

    }
}