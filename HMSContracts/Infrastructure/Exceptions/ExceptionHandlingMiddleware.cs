using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using static HMSContracts.Infrastructure.Exceptions.TypesOfExceptions;

namespace HMSContracts.Infrastructure.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex) 
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case ConflictException:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;

                case UnauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;

            }

            var response = new { message = exception.Message };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
