using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectBaseVue_Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectBaseVue_API.Utilities
{
    public class SimpleAuthorize : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                DataEntities db = new DataEntities();



            }
            catch (Exception ex)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }


    }
}
