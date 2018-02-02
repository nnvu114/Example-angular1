using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Petronas.SampleApp.WebApi.Controllers
{
    [Authorize]
    [RoutePrefix("api/Test/Values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("GetAll")]
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<string> GetAll()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values
        [Authorize]
        [Route("GetAllWithAuthorize")]
        [HttpGet]
        public IEnumerable<string> GetAllWithAuthorize()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
