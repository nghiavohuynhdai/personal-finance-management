using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Setup;

public static class ApplicationDatabases
{
    public static IServiceCollection AddDatabases
        (this IServiceCollection service)
    {
        service.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? "",
                builder =>
                {
                    builder.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "personal_finance_management");
                });
        });
        return service;
    }
    
    public static void MigrateDatabases
        (this IServiceProvider provider)
    {
        var context = provider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
}