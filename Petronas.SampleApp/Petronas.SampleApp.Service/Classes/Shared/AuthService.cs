using System.Collections.Generic;
using System.Threading.Tasks;
using Petronas.SampleApp.Model.Classes.Shared;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.Repository.Interfaces;
using Petronas.SampleApp.Repository.Interfaces.Shared;
using Petronas.SampleApp.Service.Interfaces.Shared;
using Petronas.SampleApp.Service.Models.Shared;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Web;
using Petronas.SampleApp.Common;
using System;

namespace Petronas.SampleApp.Service.Classes.Shared
{
    public class AuthService : EntityService<RefreshToken>, IAuthService
    {
        readonly IUnitOfWork _unitOfWork;
        readonly IAuthRepository _repository;

        public AuthService(IUnitOfWork unitOfWork, IAuthRepository repository)
            : base(unitOfWork, repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;

        }

        public async Task<object> Get(string userName)
        {
            var user = await _repository.FindUser(userName);
            var result = new
            {
                user.Id,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.Email,
                user.IsActive
            };

            return result;
        }
        public async Task<object> Get()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            var user = await _repository.FindUser(userName);
            var roles = _repository.GetUserRole(user.Id.ToString());
            var result = new
            {
                user.Id,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.Email,
                user.IsActive,
                roles
            };

            return result;
        }

        public async Task<object> GetUserByEmail(string email)
        {
            var user = await _repository.FindUserByEmail(email);
            var result = new
            {
                user.Id,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.PhoneNumber,
                user.Email,
                user.IsActive
            };

            return result;
        }

        public async Task<bool> IsEmailConfirmed(Guid userId)
        {
            return await _repository.IsConfirmedUser(userId);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(Guid userId)
        {
            return await _repository.GeneratePasswordResetTokenAsync(userId);
        }

        public async Task SendEmailAsync(Guid userId, string subject, string body)
        {
            await _repository.SendEmailAsync(userId, subject, body);
        }

        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            return await _repository.FindUser(userName);
        }

        public Task<IdentityResult> Create(UserRegisterModel userRegisterModel)
        {
            return _repository.RegisterUser(userRegisterModel);
        }

        public async Task<IdentityResult> Update(UserModel model)
        {
            var obj = await _repository.FindUser(model.UserName);
            obj.Email = model.Email;
            obj.PhoneNumber = model.PhoneNumber;
            obj.FirstName = model.FirstName;
            obj.LastName = model.LastName;
            obj.ExpireDate = model.ExpireDate;
            obj.IsActive = model.IsActive;

            var result = await _repository.UpdateAsync(obj);
            return result;
        }

        public async Task<IdentityResult> Delete(string userName)
        {
            var obj = await _repository.FindUser(userName);
            var result = await _repository.DeleteAsync(obj);

            return result;
        }

        public void Create(List<RefreshToken> lstObj)
        {
            if (lstObj == null)
            {
                return;
            }

            foreach (var obj in lstObj)
            {
                _repository.Add(obj);
            }
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        /// <summary>
        /// Check current user whether admin or not?
        /// </summary>
        /// <returns></returns>
        public async Task<bool> isAdminUser()
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var userName = Thread.CurrentPrincipal.Identity.Name;
                ApplicationUser user = await _repository.FindUser(userName);
                var roles = _repository.GetUserRole(user.Id.ToString());

                if (roles != null && roles.Count > 0 && roles[0].ToString() == Role.Admin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }


    }
}