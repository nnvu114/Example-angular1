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
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {       
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            HttpContext ctx = HttpContext.Current;

            if (HttpContext.Current.Session["ID"] == null)
            {              
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, Common.Constants.ErrorMessage.SessionTimeoutMessage);
            }          
        }
    }
}