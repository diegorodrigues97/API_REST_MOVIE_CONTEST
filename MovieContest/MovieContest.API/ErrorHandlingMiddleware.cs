using Microsoft.AspNetCore.Http;
using MovieContest.Data;
using MovieContest.Domain;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MovieContest.API
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
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
            var code = HttpStatusCode.InternalServerError;
            string msg = null;

            if (ex is DomainException)
            {
                code = HttpStatusCode.BadRequest;
                msg = ex.Message;
            }
            else if (ex is DatabaseException)
            {
                code = HttpStatusCode.BadRequest;
                msg = "Ops, um erro inesperado aconteceu!";
            }
            else
            {
                code = HttpStatusCode.BadRequest;
                msg = "Ops, um erro inesperado aconteceu!";
            }

            context.Response.ContentType = "application/text";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(msg);
        }
    }
}
