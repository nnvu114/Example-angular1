using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.Service.Interfaces.Shared;
using Petronas.SampleApp.Service.Models.Shared;
using Microsoft.AspNet.Identity;
using Petronas.SampleApp.WebApi.Models;
using System.Net.Http;
using System.Net;
using Petronas.SampleApp.Common;
using System;
using Petronas.SampleApp.WebApi.Results;
using Petronas.SampleApp.WebApi.Attributes;

namespace Petronas.SampleApp.WebApi.Controllers
{
    /// <summary>
    /// Account management
    /// </summary>
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {
        private readonly IAuthService _service;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service">IAuthService</param>
        public AccountController(IAuthService service)
        {
            _service = service;
        }
        [Route("GetOrg")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrg()
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject(@"{'data': {'total': 2, 'data':[{'Id': 'ecb94227 - d5d2 - 4a14 - a70a - d7acfa483449','Name': 'Asean Bintulu Fertilizer Sdn Bhd'},{'Id': '3e12269d - 7fa6 - 480c - 8464 - b0770f9e5a3f','Name': 'Dewan Filharmonik PETRONAS'}]}}");
            return Ok(result);
        }
        // GET api/Account/Get
        /// <summary>
        /// Lấy dữ liệu theo username
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Get")]
        [HttpGet]
        [Authorize]
        //[ResponseType(typeof(ApplicationUser))]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        // POST api/Account/Register
        /// <summary>
        /// Đăng ký user
        /// </summary>
        /// <param name="userRegisterModel">userModel</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [AllowAnonymous]
        [Route("Register")]
        [ValidateModel]
        public async Task<IHttpActionResult> Register(UserRegisterModel userRegisterModel)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.Create(userRegisterModel);
                         
            var errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }
            else
            {
                var user = await _service.GetUserByUserName(userRegisterModel.UserName);

                string code = await this.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

                await this.UserManager.SendEmailAsync(user.Id, Common.Constants.EmailMassage.SubjectConfirmEmail, String.Format(Common.Constants.EmailMassage.BodyConfirmEmail, callbackUrl));

                return Ok();
            }           
        }

        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
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

        // PUT api/Account/Put
        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Put")]
        [HttpPut]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Put([FromBody]UserModel model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }

        // DELETE api/Account/Delete
        /// <summary>
        /// Remove UserProfile with username
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [Route("Delete")]
        [HttpDelete]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> Delete(string userName)
        {
            var result = await _service.Delete(userName);
            return Ok(result);
        }

        /// <summary>
        /// Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// handle forget password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        [ValidateModel]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {          
            var user = await _service.GetUserByEmail(model.Email);
            
            if (user == null || !(await _service.IsEmailConfirmed(((UserModel)user).Id)))
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, Common.Constants.ErrorMessage.NonConfirmedEmail));
            }             

            // Send an email with this link
            string code = await UserManager.GeneratePasswordResetTokenAsync(((UserModel)user).Id);

            await UserManager.SendEmailAsync(((UserModel)user).Id, Common.Constants.EmailMassage.SubjectResetPassword, Common.Constants.EmailMassage.BodyResetPassword + code);
           
            return Ok();
        }


        #region Helpers      
       
        #endregion
    }
}
