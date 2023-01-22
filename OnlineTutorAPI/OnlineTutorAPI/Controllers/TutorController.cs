using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineTutorAPI.Models;

namespace OnlineTutorAPI.Controllers
{
    public class TutorController : ApiController
    {

        public IHttpActionResult Get(string q)
        {
            TutorAccess obj = new TutorAccess();
            return Ok(obj.select(q));
        }
        public IHttpActionResult Post([FromBody] tutor t)
        {
            TutorAccess obj = new TutorAccess();
            if(obj.Insert(t))
            {
                return Ok("New Employee Added Successfully");
            }
            return NotFound();
        }
    }
}
