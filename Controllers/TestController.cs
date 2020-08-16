using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NorthWindAPI.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : ApiController
    {

        [Route("")]
        public string Get()
        {
            return DateTime.Now.ToString();
        }


        [HttpGet] //Qualify methods which donot use name like get,put etc
        [Route("{addDays:int}")] //define a root with a customized paramter //url :/api/test/10
        public string NextDate(int addDays )
        {
            return DateTime.Now.AddDays(addDays).ToString();
        }

        //suports the HttpResponseMessage class
        
        [HttpGet]
        [Route("custommsg/{page:int}")]
        //url: /api/test/custommsg/9
        public HttpResponseMessage GenerateCustomMeassage(int page)
        {
            var response = Request.CreateResponse();
            response.Content = new StringContent("this is a custom response message to be sent to user");
            if (page > 10)
                response.StatusCode = HttpStatusCode.BadRequest;
            else
                response.StatusCode = HttpStatusCode.OK;
            return response;
            
        }
             
        [HttpGet]
        [Route("preferred/{page:int}")] //arl :/api/test/preferred/10
        public IHttpActionResult PreferredPractice(int page=10)
        {
            if (page > 999)
                return BadRequest();
            else
                return Ok(page);
        }
            






    }
}
