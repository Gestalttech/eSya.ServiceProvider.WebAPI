﻿using eSya.ServiceProvider.DL.Entities;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eSya.ServiceProvider.WebAPI.Utility
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class HttpAuthAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue("dbContextType", out var dbContextType);
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            eSyaEnterprise._connString = configuration.GetConnectionString(dbContextType + ":dbConn_eSyaEnterprise");

            await next();
        }
    }
}
