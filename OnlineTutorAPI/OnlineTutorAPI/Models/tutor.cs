using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTutorAPI.Models
{
    public class tutor
    {
        public int id { get; set; }
       
        public string fname { get; set; }

        public string lname { get; set; }

        public string gender { get; set; }

  
        public String phone_no { get; set; }

        public string email { get; set; }


        public string city { get; set; }

        public string Class { get; set; }

        public string password { get; set; }

        public string confirmpassword { get; set; }

        public string Bio { get; set; }

        public byte[] img { get; set; }
    }
}