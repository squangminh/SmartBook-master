﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AdwardSoft.Web.Inside.Utilities
{
    public static class GlobalErrorHandling
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(new
                        {
                            Code = context.Response.StatusCode,
                            Message = contextFeature.Error.Message ?? "Internal Server Error."
                        }));
                    }
                });
            });
        }
    }
}
