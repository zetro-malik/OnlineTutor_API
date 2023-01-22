using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTutorAPI.Models;

namespace OnlineTutorAPI.Controllers
{
    public class StudentController : ApiController
    {
        

        public IHttpActionResult Get(string q)
        {
            StudentAccess obj = new StudentAccess();
           
            return Ok(obj.select(q));
        }





        public IHttpActionResult Post([FromBody] student std)
        {
            StudentAccess obj = new StudentAccess();
            if (obj.Insert(std))
            {
                return Ok("New Employee Added Successfully");
            }
            return NotFound();
        }
    }
}
