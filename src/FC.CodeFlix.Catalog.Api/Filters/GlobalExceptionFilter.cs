using FC.CodeFlix.Catalog.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FC.CodeFlix.Catalog.Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        public GlobalExceptionFilter(IHostEnvironment env)
       => _env = env;


        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails();
            var exception = context.Exception;
            if (_env.IsDevelopment())
                details.Extensions.Add("StackTrace",exception.StackTrace);

                if (exception is EntityValidationException)
                {
                    var validationException = exception as EntityValidationException;
                    details.Title = "One or more validation errors ocurred";
                    details.Status = StatusCodes.Status422UnprocessableEntity;
                    details.Type = "UnprocessableEntity";
                    details.Detail = validationException!.Message;
                }
                else
                {

                    details.Title = "An unexpected error ocurred";
                    details.Status = StatusCodes.Status422UnprocessableEntity;
                    details.Type = "Unexpected";
                    details.Detail = exception.Message;
                }
            context.HttpContext.Response.StatusCode = (int)details.Status;
            context.Result = new ObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
