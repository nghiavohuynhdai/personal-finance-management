using System.Net;
using Api.Common;
using Api.Exceptions;

namespace Api.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "Service is unavailable";

        switch (ex)
        {
            case BadRequestException badEx:
                code = HttpStatusCode.BadRequest;
                message = badEx.Message;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsJsonAsync(ResultResponse<object>.Init(null, message));
    }
}