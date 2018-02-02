using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Petronas.SampleApp.WebApi.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errors = string.Empty;
                var states = actionContext.ModelState;

                foreach (var state in states)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                        {
                            errors += error.ErrorMessage + ";";
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(errors))
                {
                    errors = errors.TrimEnd(';');
                }
             
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}