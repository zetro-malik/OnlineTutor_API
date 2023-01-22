using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTutorSystem.Models
{
    public class SearchedTutor
    {
        public string id { get; set; }
       
        public string fname { get; set; }

        public string lname { get; set; }
       
        public string gender { get; set; }
      
        public string city { get; set; }
        
        public string Class { get; set; }

        public string subject { get; set; }

        public string price { get; set; }

        public string time { get; set; }
        public string phone { get; set; }

        public string email { get; set; }

        public string Bio { get; set; }

     
        public byte[] img { get; set; }
    }
}