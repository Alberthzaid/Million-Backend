
using System.Reflection;
using MongoDB.Driver;
using RealState.Application.Interfaces;
using RealState.Application.Mappers;
using RealState.Application.Services;
using RealState.Application.UnitOfWork;
using RealState.Domain.Interfaces;
using RealState.Infrastructure;

namespace RealState.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Mapper and Scopes
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();       
        });
        
        //Scopes
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPropertyService, PropertyService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IImagePropertyService, ImagePropertyService>();
        
        // DB Context
        string? connectionString = configuration.GetConnectionString("MongoDb");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Connection string 'MongoDb' is not configured in appsettings.json.");

        string? databaseName = configuration["MongoSettings:Database"];
        if (string.IsNullOrWhiteSpace(databaseName))
            throw new InvalidOperationException("Database name is not configured in MongoSettings section.");

        services.AddSingleton<IMongoClient>(sp =>
            new MongoClient(connectionString));
        services.AddSingleton(sp =>
            sp.GetRequiredService<IMongoClient>().GetDatabase(databaseName)); 

        services.AddScoped<MongoDbContext>();
    }
    
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()       
                    .AllowAnyHeader()); 
        });
}