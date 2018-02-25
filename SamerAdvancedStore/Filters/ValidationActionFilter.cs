using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SamerAdvancedStore.Filters
{
    public class ValidationActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            var message = "";
            foreach (var error in modelState)
            {

                foreach (var singleerror in error.Value.Errors)
                {
                    message += singleerror.ErrorMessage+ " , ";
                }

            }
            if (!modelState.IsValid)
                actionContext.Response = actionContext.Request
                    .CreateErrorResponse(HttpStatusCode.BadRequest, message);
        }

    }

}