using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NorthWindAPI.Controllers
{
    public class VerbsData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [RoutePrefix("api/verbs")]
    public class VerbsController : ApiController
    {
        VerbsData data;

        public VerbsController()
        {
            data = new VerbsData { Id = 999, Name = "Manipal" };
        }

         // /api/verbs
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(data);
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            if (id == 10)
                return Ok(data);
            else
                return BadRequest();
        }

        //post -for insert operations
        //url /api/verbs
        //data is passed into the body

        [Route("")]
        public IHttpActionResult Post(VerbsData obj)
        {
            if (obj.Id == 0 || string.IsNullOrEmpty(obj.Name))
                return BadRequest();

            var newData = new VerbsData { Id = obj.Id, Name = obj.Name };
            return Ok(newData);
        }

        // url :/api/verbs/999
        //her you should set body also
        //update the row using put
        //here retrieving the object using id parameter and then replacing the
        //retrieved object 
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, VerbsData obj)
        {
            if (id == obj.Id)
                return Ok(obj);
            else
                return NotFound();
        }

        // url :/api/verbs/999
        //delete the row using delete
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (id == data.Id)
                return Ok();
            else
                return NotFound();
        }



    }
}
