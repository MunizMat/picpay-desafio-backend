using Microsoft.AspNetCore.Diagnostics;

namespace Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        var (statusCode, title) = MapExceptionToProblem(exception);

        await Results.Problem(
            title: title,
            statusCode: statusCode
            ).ExecuteAsync(httpContext);

        return true;
    }

    private static (int statusCode, string title) MapExceptionToProblem(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (401, exception.Message),
            ArgumentException => (400, exception.Message),
            _ => (500, "Internal server error"),
        };
    }
}
