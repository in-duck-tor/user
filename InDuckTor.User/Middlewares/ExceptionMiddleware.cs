using InDuckTor.User.Domain;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace InDuckTor.User.WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        public ExceptionMiddleware()
        {
        }

        public async Task Invoke(HttpContext context)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null && contextFeature.Error != null)
            {
                context.Response.StatusCode = (int)GetErrorCode(contextFeature.Error);
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ProblemDetails()
                {
                    Status = context.Response.StatusCode,
                    Title = contextFeature.Error.Message
                }));
            }
        }

        private static HttpStatusCode GetErrorCode(Exception error)
        {
            return error switch
            {
                Errors.Forbidden => HttpStatusCode.Forbidden,
                Errors.NotFound => HttpStatusCode.NotFound,
                Errors.Conflict => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }
}
