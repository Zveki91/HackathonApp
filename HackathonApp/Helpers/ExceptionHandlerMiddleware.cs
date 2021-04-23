using System;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HackathonApp.Dto.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HackathonApp.Helpers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor for middleware
        /// </summary>
        /// <param name="next">Middleware handler</param>
        /// <param name="logger">Logger</param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = ex switch
            {
                MyBadRequestException _ => HttpStatusCode.BadRequest,
                MyNotFoundException _ => HttpStatusCode.NotFound,
                MyUnauthorizedException _ => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError
            };

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            
            return context.Response.WriteAsync(result);
        }
    }
}