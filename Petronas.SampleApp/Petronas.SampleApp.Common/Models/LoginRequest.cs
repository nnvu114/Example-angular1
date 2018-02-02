using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Attributes;

namespace Petronas.SampleApp.Common.Models
{
    [Validator(typeof(LoginRequestValidator))]
    public class LoginRequest
    {
        public string Id { get; set; }
        public string Domain { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string ApplicationId { get; set; }
        public string AccessToken { get; set; }
        public LoginClient Client { get; set; }
    }
    public class LoginClient
    {
        public string Ip;
        public string HostName;
        public string UserAgent;
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(request => request.UserId).NotEmpty().WithMessage("UserId cannot be empty.");
            RuleFor(request => request.Password).NotEmpty().WithMessage("Password cannot be empty.");
            RuleFor(request => request.ApplicationId).NotEmpty().WithMessage("ApplicationId cannot be empty.");
            RuleFor(request => request.Domain).NotEmpty().WithMessage("Domain cannot be empty.");
            RuleFor(request => request.Client.Ip).NotEmpty().WithMessage("Client ip cannot be empty.");
            RuleFor(request => request.Client.HostName).NotEmpty().WithMessage("Client host name cannot be empty.");
            RuleFor(request => request.Client.UserAgent).NotEmpty().WithMessage("Client user agent cannot be empty.");
        }
    }
}
