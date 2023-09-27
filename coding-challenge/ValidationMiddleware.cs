using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace coding_challenge
{
    public class ValidationMiddleware : IMiddleware
    {
        private readonly ILogger<ValidationMiddleware> _logger;
        public ValidationMiddleware(ILogger<ValidationMiddleware> logger)
        {
            _logger = logger;
        }

        //TODO FINE TUNE THE MIDDLEWARE!
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails();

            switch (ex)
            {
                case BadHttpRequestException _:
                    problemDetails.Title = "Bad Request";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Detail = "Bad Request";
                    break;

                case UnauthorizedAccessException _:
                    problemDetails.Title = "Unauthorized";
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Detail = "Unauthorized";
                    break;

                case  FluentValidation.ValidationException _:
                    problemDetails.Title = "BadRequest";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Detail = ex.Message;
                    break;
                default:
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    problemDetails.Detail = ex.Message + ex.StackTrace;
                    break;
            }

            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)problemDetails.Status;
            var result = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(result);
        }
    }
}
