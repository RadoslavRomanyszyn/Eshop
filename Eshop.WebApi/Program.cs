using Eshop.Persistence;
using Eshop.WebApi.Filters;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var assembly = typeof(Program).Assembly;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(SetupControllers).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EshopDbContext>(
    options => options.UseSqlServer(connectionString)
                      .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                      .EnableSensitiveDataLogging(true)
);

// MediatR & FluentValidation configuration
builder.Services.AddFluentValidation([assembly]);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

// CORS configuration
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// EF
CreateDbIfNotExist(app);

static void CreateDbIfNotExist(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<EshopDbContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devCorsPolicy);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SetupControllers(MvcOptions options)
{
    options.Filters.Add<GlobalExceptionFilter>();
}
