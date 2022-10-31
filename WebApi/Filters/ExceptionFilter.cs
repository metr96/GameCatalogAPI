using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Games.BLL.Infrastructure;

namespace Games.Web.Filters;

public class ExceptionFilter : IActionFilter, IOrderedFilter
{
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    private static int ToStatusCode(Exception e)
        => e switch
        {
            DataConflictException => StatusCodes.Status409Conflict,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => 0
        };

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var exception = context.Exception;
        if (exception == null) return;

        var statusCode = ToStatusCode(exception);

        if (statusCode != 0)
        {
            context.Result = new ObjectResult(exception.Message)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}