using System.Reflection;
using Api.Setup;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// CORS setting
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
        policyBuilder
            .WithOrigins(builder.Configuration["Cors:Origins"] ?? "*")
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});


// Add services to the container.
builder.Services
    .AddDatabases()
    .AddRepositories()
    .AddValidatorsFromAssembly(typeof(Program).Assembly)
    .AddHandlers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.Services.MigrateDatabases();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API"); });
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.UseApplicationMiddlewares();

app.Run();
