using Api.Data.Context;
using Api.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Setup;

public static class ApplicationDatabases
{
    public static IServiceCollection AddData
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

        service.AddScoped<IUnitOfWork, UnitOfWork>();

        return service;
    }
    
    public static void MigrateDatabases
        (this IServiceProvider provider)
    {
        var context = provider.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
}