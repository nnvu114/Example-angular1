using System.Web;
using System.Web.Http;
using Petronas.SampleApp.Model.NotMapping;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Http;

namespace Petronas.SampleApp.WebApi.Controllers
{
    /// <summary>
    /// Base API controller
    /// </summary>
    public class BaseApiController : ApiController
    {
        private ApplicationUser _userContext;

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
   
        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        /// <summary>
        /// Global UserContext
        /// </summary>
        public ApplicationUser UserContext
        {
            get
            {
                if (_userContext != null)
                {
                    return _userContext;
                }
                _userContext = UserManager.FindByName(User.Identity.Name);
                return _userContext;
            }
            set { _userContext = value; }
        }

        public IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}