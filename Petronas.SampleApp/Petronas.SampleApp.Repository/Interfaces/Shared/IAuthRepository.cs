using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petronas.SampleApp.Model.Classes;
using Petronas.SampleApp.Model.Classes.Shared;
using Petronas.SampleApp.Model.NotMapping;
using Microsoft.AspNet.Identity;

namespace Petronas.SampleApp.Repository.Interfaces.Shared
{
    public interface IAuthRepository : IRepository<RefreshToken>, IDisposable
    {
        Task<IdentityResult> RegisterUser(UserRegisterModel userRegisterModel);
        Task<IdentityResult> RegisterExternalUser(UserRegisterModel userRegisterModel, Guid companyId);

        Task<ApplicationUser> FindUser(string userName, string password);
        Task<ApplicationUser> FindUser(string userName);
        Task<ApplicationUser> FindUserByEmail(string email);

        Task<bool> IsConfirmedUser(Guid userId);
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);
        AuditUser FindClient(string clientId);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
        Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> UpdateAsync(ApplicationUser user);
        Task<IdentityResult> DeleteAsync(ApplicationUser user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        IList<string> GetUserRole(string userId);

        Task SendEmailAsync(Guid userId, string subject, string body);
    }
}