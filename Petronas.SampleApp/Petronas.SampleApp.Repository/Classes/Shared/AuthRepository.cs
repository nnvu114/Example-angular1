using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Petronas.SampleApp.Model.Classes.Shared;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.Repository.Interfaces.Shared;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Petronas.SampleApp.Common.Models;
using Petronas.SampleApp.Common;
using Petronas.SampleApp.Common.Helpers;

namespace Petronas.SampleApp.Repository.Classes.Shared
{
    public class AuthRepository : Repository<RefreshToken>, IAuthRepository, IDisposable
    {
        private UserManager<ApplicationUser,Guid> _userManager;
        private RoleManager<ApplicationRole, Guid> _roleManager;

        public AuthRepository(DbContext context)
            : base(context)
        {
            _userManager = new UserManager<ApplicationUser,Guid>(new UserStore<ApplicationUser,ApplicationRole,Guid,ApplicationUserLogin,ApplicationUserRole,ApplicationUserClaim>(Context));
            _roleManager = new RoleManager<ApplicationRole, Guid>(new RoleStore<ApplicationRole,Guid,ApplicationUserRole>(Context));
        }

        // In this method we will create default User roles and Admin user for login    
        public async Task CreateDefaultRolesandUsers()
        {
            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!_roleManager.RoleExists(Role.Admin))
            {

                // first we create Admin rool    
                var role = new ApplicationRole();
                role.Name = Role.Admin;
                await CreateRole(role);

                //Here we create a Admin who will manage the system                   

                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = Role.Admin,
                    IsActive = true,
                    ExpireDate = DateTime.Now.AddYears(10),
                };

                var result = await _userManager.CreateAsync(user, Role.Admin);               

                //Add default User to Role Admin    
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user.Id, Role.Admin);
                }
            }         
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterModel userRegisterModel)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = userRegisterModel.UserName,
                Email = userRegisterModel.Email,
                IsActive = true,
                ExpireDate = DateTime.Now.AddYears(10),
            };

            var result = await _userManager.CreateAsync(user, CommonHelper.GetHash(userRegisterModel.Password));
            await _userManager.AddToRoleAsync(user.Id, Role.Admin);
            return result;
        }

        public async Task<IdentityResult> RegisterExternalUser(UserRegisterModel userRegisterModel, Guid companyId)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = userRegisterModel.UserName,
                Email = userRegisterModel.Email,
                IsActive = true,
                ExpireDate = DateTime.Now.AddYears(10),
                CompanyId = companyId
            };

            var result = await _userManager.CreateAsync(user, CommonHelper.GetHash(userRegisterModel.Password));

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(user.Id, Role.ExternalUser);
            }

            return result;
        }

        public async Task<IdentityResult> RegisterUser(UserRegisterModel userRegisterModel, string roleName)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = userRegisterModel.UserName,
                IsActive = true,
                ExpireDate = DateTime.Now.AddYears(10),
            };

            var result = await _userManager.CreateAsync(user, userRegisterModel.Password);
            await _userManager.AddToRoleAsync(user.Id, roleName);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmail(Guid userId, string code)
        {
            if (userId == null || code == null)
            {
                return new IdentityResult("NULL input information");
            }
            var result = await _userManager.ConfirmEmailAsync(userId, code);
            if (result.Succeeded)
            {
                return new IdentityResult("Confirmed Email");
            }

            return result;
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);
            return user;
        }

        public async Task<ApplicationUser> FindUser(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            return user;
        }

        public async Task<ApplicationUser> FindUserByEmail(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);

            return user;
        }       

        public async Task<bool> IsConfirmedUser(Guid userId)
        {
            return await _userManager.IsEmailConfirmedAsync(userId);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(userId);
        }

        public async Task SendEmailAsync(Guid userId, string subject, string body)
        {
            await _userManager.SendEmailAsync(userId, subject, body);
        }

        public AuditUser FindClient(string clientId)
        {
            var client = Context.AuditUsers.Find(Guid.Parse(clientId));

            return client;
        }
        public IList<string> GetUserRole(string userId)
        {
            return _userManager.GetRoles(Guid.Parse(userId));
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            //var existingToken = Context.RefreshTokens
            //    .SingleOrDefault(m => m.Subject == token.Subject
            //                          && m.ClientId == token.ClientId);
            try
            {
                var existingToken = Context.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).FirstOrDefault();

                if (existingToken != null)
                {
                    var result = await RemoveRefreshToken(existingToken);
                }

                Context.RefreshTokens.Add(token);

                return await Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await Context.RefreshTokens.FirstOrDefaultAsync(s => s.RefreshTokenId == refreshTokenId);

            if (refreshToken != null)
            {
                Context.RefreshTokens.Remove(refreshToken);
                return await Context.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            Context.RefreshTokens.Remove(refreshToken);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await Context.RefreshTokens.FirstOrDefaultAsync(s=>s.RefreshTokenId ==refreshTokenId);
            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return Context.RefreshTokens.ToList();
        }

        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(Guid.Parse(userId), login);
            
            return result;
        }

        #region Role Manager
        public async Task<IdentityResult> CreateRole(ApplicationRole role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }
        public async Task<IdentityResult> UpdateRole(ApplicationRole role)
        {
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }
        public async Task<IdentityResult> DeleteRole(ApplicationRole role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }
        public ResponseBaseModel GetRoles(int skip =0, int take = 10)
        {
            ResponseBaseModel result = new ResponseBaseModel();
            result.Data.total = _roleManager.Roles.Count();
            result.Data.data =  _roleManager.Roles.OrderBy(s => s.Name).Skip(0).Take(10).ToList();
            return result;
        }
        #endregion

        public void Dispose()
        {
            Context.Dispose();
            _userManager.Dispose();
            _roleManager.Dispose();
        }
    }
}