using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Petronas.SampleApp.Model.NotMapping;
using Petronas.SampleApp.WebApi.Attributes;
using Petronas.SampleApp.WebApi.Models;
using Petronas.SampleApp.WebApi.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Petronas.SampleApp.WebApi.Controllers.MasterData
{
    [RoutePrefix("api/MasterData/Role")]
    [Authorize(Roles = Common.Role.Admin)]
    public class RoleController : BaseApiController
    {
        public RoleController()
        {
        }

        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(Guid Id)
        {
            var role = await this.RoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                return Ok(role);
            }

            return NotFound();

        }

        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            var roles = this.RoleManager.Roles;

            return Ok(roles);
        }

        [Route("create")]
        [ValidateModel]
        public async Task<IHttpActionResult> Create(CreateRoleBindingModel model)
        {        
            var role = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = model.Name
            };

            var result = await this.RoleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            else
            {
                return Ok(result);
            }

            //Uri locationHeader = new Uri(Url.Link("GetRoleById", new { id = role.Id }));

            //return Created(locationHeader, role);

        }

        [Route("{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(Guid Id)
        {

            var role = await this.RoleManager.FindByIdAsync(Id);

            if (role != null)
            {
                IdentityResult result = await this.RoleManager.DeleteAsync(role);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();
            }

            return NotFound();

        }

        [Route("ManageUsersInRole")]
        public async Task<IHttpActionResult> ManageUsersInRole(UsersInRoleModel model)
        {
            var role = await this.RoleManager.FindByIdAsync(model.RoleId);

            if (role == null)
            {
                ModelState.AddModelError("", Common.Constants.ErrorMessage.RoleNotExist);
                return BadRequest(ModelState);
            }

            foreach (Guid userId in model.EnrolledUsers)
            {
                var appUser = await this.UserManager.FindByIdAsync(userId);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format(Common.Constants.ErrorMessage.UserNotFound, userId));
                    continue;
                }

                if (!this.UserManager.IsInRole(userId, role.Name))
                {
                    IdentityResult result = await this.UserManager.AddToRoleAsync(userId, role.Name);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", String.Format(Common.Constants.ErrorMessage.UserCannotAddToRole, userId));
                    }

                }
            }

            foreach (Guid userId in model.RemovedUsers)
            {
                var appUser = await this.UserManager.FindByIdAsync(userId);

                if (appUser == null)
                {
                    ModelState.AddModelError("", String.Format(Common.Constants.ErrorMessage.UserNotFound, userId));
                    continue;
                }

                IdentityResult result = await this.UserManager.RemoveFromRoleAsync(userId, role.Name);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", String.Format(Common.Constants.ErrorMessage.UserCannotRemoveFromRole, userId));
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
