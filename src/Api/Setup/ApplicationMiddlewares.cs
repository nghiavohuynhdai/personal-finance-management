using Api.Middlewares;

namespace Api.Setup;

public static class ApplicationMiddlewares
{
    public static void UseApplicationMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}