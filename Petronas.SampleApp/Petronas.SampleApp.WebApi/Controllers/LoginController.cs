using Petronas.SampleApp.Common;
using Petronas.SampleApp.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Petronas.SampleApp.WebApi.Controllers
{
    [RoutePrefix("api/users")]
    public class LoginController : BaseApiController
    {
        private SSOHelper _ssoHelper;

        public LoginController()
        {
            _ssoHelper = new SSOHelper();
        }

        [Route("login")]
        [HttpPost]
        public async Task<LoginResultModel> Login(UserLoginModel login)
        {
            LoginResultModel result = new LoginResultModel();

            var ssoToken = _ssoHelper.Login(login.Username, login.Password, "PETRONAS");
            if (ssoToken == null || ssoToken.Result == Guid.Empty)
            {
                result.Message = Common.Constants.ResponseMessage.UnSuccessLogin;
                result.Status = false;
            }
            else
            {
                result.Message = Common.Constants.ResponseMessage.Success;
                result.Status = true;
            }

            result.Token = ssoToken.Result;
            return result;
        }
    }
}