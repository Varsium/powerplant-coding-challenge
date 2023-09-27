using System.ComponentModel.DataAnnotations;

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
            catch (ValidationException exc)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                //Todo Write body object of why it failed.
                _logger.LogError(exc, "This was thrown in the validationMiddleWare");
                return;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                _logger.LogError(ex, "This was thrown in the validationMiddleWare");
                throw;
            }

        }
    }
}
