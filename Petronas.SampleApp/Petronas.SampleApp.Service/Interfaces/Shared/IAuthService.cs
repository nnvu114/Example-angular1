using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Petronas.SampleApp.Model.Classes.Shared;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.Service.Models.Shared;
using Microsoft.AspNet.Identity;

namespace Petronas.SampleApp.Service.Interfaces.Shared
{
    public interface IAuthService : IEntityService<RefreshToken>, IDisposable
    {
        Task<object> Get(string userName);
        Task<object> Get();
        Task<IdentityResult> Create(UserRegisterModel userRegisterModel);
        //Task<IdentityResult> Create(UserModel model);
        Task<IdentityResult> Update(UserModel model);
        Task<IdentityResult> Delete(string userName);

        void Create(List<RefreshToken> lstObj);

        Task<bool> isAdminUser();
        Task<object> GetUserByEmail(string email);
        Task<bool> IsEmailConfirmed(Guid userId);
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        Task SendEmailAsync(Guid userId, string subject, string body);
        Task<ApplicationUser> GetUserByUserName(string userName);
    }
}