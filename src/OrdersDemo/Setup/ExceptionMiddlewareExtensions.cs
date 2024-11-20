using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrdersDemo.Domain.Exceptions;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace OrdersDemo.Setup;

public static class ExceptionMiddlewareExtensions
{
    public static async Task<ActionResult> ReturnErrorReponse(this HttpResponseMessage result)
    {
        var error = await result.FromJsonAsync<ProblemDetails>();

        var response = error?.Status switch
        {
            (int)HttpStatusCode.NotFound => new NotFoundObjectResult(error),
            (int)HttpStatusCode.GatewayTimeout => new ObjectResult(error) { StatusCode = error.Status },
            _ => new BadRequestObjectResult(error)
        };

        return response;
    }

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions()
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = async context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionObject is not null)
                {
                    var errorDetails = exceptionObject.Error switch
                    {
                        NotFoundException ex => GetProblemDetails(context, HttpStatusCode.NotFound, ex.Message),
                        AppException ex => GetProblemDetails(context, HttpStatusCode.BadRequest, ex.Message),
                        _ => GetProblemDetails(context, HttpStatusCode.BadRequest, "Internal error"),
                    };

                    context.Response.ContentType = "application/problem+json; charset=utf-8";
                    context.Response.StatusCode = errorDetails.Status ?? 400;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
                }
            }
        });
    }

    private static ProblemDetails GetProblemDetails(HttpContext context, HttpStatusCode status, string message)
    {
        var (title, rfcSection) = status switch
        {
            HttpStatusCode.NotFound => ("Not Found", "6.5.4"),
            HttpStatusCode.GatewayTimeout => ("Gateway Timeout", "6.6.5"),
            _ => ("Bad Request", "6.5.1")
        };

        var result = new ProblemDetails
        {
            Type = $"https://tools.ietf.org/html/rfc7231#section-{rfcSection}",
            Title = title,
            Status = (int)status,
        };

        result.Extensions.Add("traceId", Activity.Current?.Id ?? context?.TraceIdentifier);
        result.Extensions.Add("errors", new Dictionary<string, List<string>>()
        {
            [""] = new() { message }
        });

        return result;
    }
}
