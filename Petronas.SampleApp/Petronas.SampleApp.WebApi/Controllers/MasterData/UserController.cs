using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Petronas.SampleApp.Common.Models;
using Petronas.SampleApp.Service.Interfaces.MasterData;
using Petronas.SampleApp.Service.Models.MasterData;
using Petronas.SampleApp.WebApi.Controllers;

namespace Petronas.SampleApp.WebApi.Controllers.MasterData
{
    /// <summary>
    /// Setup / Company Details
    /// </summary>
    //[Authorize]
    [RoutePrefix("api/MasterData/User")]
    [Authorize(Roles="Administrator")]
    public class UserController : BaseApiController
    {
        private readonly ICompanyService _service;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service"></param>
        public UserController(ICompanyService service)
        {
            _service = service;
        }

        // GET api/Company/Get
        /// <summary>
        /// Get data for Company
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        //[Route("Get")]
        [HttpGet]
        //[ResponseType(typeof(CompanyModel))]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        // POST api/Company/Post
        /// <summary>
        /// Create record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Post")]
        [HttpPost]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Post([FromBody]CompanyRegistrationModel model)
        {
            var result = await _service.Add(model);
            return Ok(result);
        }

        // PUT api/Company/Put
        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Put")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Put([FromBody]CompanyModel model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }

        // DELETE api/Company/Delete
        /// <summary>
        /// Delete record by id
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Delete")]
        [HttpDelete]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}