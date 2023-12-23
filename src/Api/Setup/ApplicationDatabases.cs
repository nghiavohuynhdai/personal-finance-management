using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Setup;

public static class ApplicationDatabases
{
    public static IServiceCollection AddDatabases
        (this IServiceCollection service)
    {
        service.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "");
        });
        return service;
    }
}