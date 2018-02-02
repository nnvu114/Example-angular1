using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Petronas.SampleApp.Common.Models;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Service.Interfaces.MasterData;
using Petronas.SampleApp.Service.Interfaces.Shared;
using Petronas.SampleApp.Service.Models.MasterData;
using Petronas.SampleApp.WebApi.Attributes;
using Petronas.SampleApp.WebApi.Controllers;
using Petronas.SampleApp.WebApi.Models;

namespace Petronas.SampleApp.WebApi.Areas.Setup
{
    /// <summary>
    /// Setup / Company Details
    /// </summary>
    //[Authorize]
    [RoutePrefix("api/Company")]
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _service;
        private readonly IAuthService _authorService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service"></param>
        public CompanyController(ICompanyService service, IAuthService authorService)
        {
            _service = service;
            _authorService = authorService;
        }

        // GET api/Company/Get
        /// <summary>
        /// Get data for Company
        /// <input>PageInfo model</input>
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Get")]
        [HttpGet]
        [ResponseType(typeof(ResponseBaseModel))]
        public async Task<IHttpActionResult> Get([FromUri]PageInfo model = null)
        {
           
            PageInfo request = new PageInfo(); ;
            Request.RequestUri.TryReadQueryAs(out request);
            var result = _service.Search(request);
            return Ok(result);
        }
        [Route("GetById")]
        [HttpGet]
        //[ResponseType(typeof(CompanyModel))]
        public async Task<IHttpActionResult> GetById([FromUri]Guid Id)
        {
            var result = await _service.GetById(Id);
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
        [ValidateModel]
        public async Task<IHttpActionResult> Post([FromBody]CompanyRegistrationModel model)
        {
            var result = await _service.Add(model);

            if (result.statusCode != 0)
            {
                // Send email to confirm account
                var user = await _authorService.GetUserByUserName(model.UserRegisterInfo.UserName);

                string code = await this.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                var callbackUrl = new Uri(Url.Link("ConfirmCompanyEmail", new { userId = user.Id, code = code }));

                await this.UserManager.SendEmailAsync(user.Id, Common.Constants.EmailMassage.SubjectConfirmEmail, String.Format(Common.Constants.EmailMassage.BodyConfirmEmail, callbackUrl));               
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmCompanyEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(Guid userId, string code = "")
        {
            if (userId == null || Guid.Empty == userId || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", Common.Constants.ErrorMessage.MissedFields);
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.UserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
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