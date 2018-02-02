using System;
using System.Collections.Generic;

namespace Petronas.SampleApp.WebApi.Models
{
    // Models returned by AccountController actions.

    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
    }

    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResultModel
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Guid Token { get; set; }
    }
}
