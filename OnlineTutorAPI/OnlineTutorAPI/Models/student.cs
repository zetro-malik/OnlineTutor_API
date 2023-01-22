using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTutorAPI.Models
{
    public class student
    {
        public int id { get; set; }
     

        public string fname { get; set; }

        public string lname { get; set; }


        public string gender { get; set; }


        public string email { get; set; }

        public string phone_no { get; set; }

        public string password { get; set; }

        public string confirmpassword { get; set; }

        public byte[] img { get; set; }
    }
}