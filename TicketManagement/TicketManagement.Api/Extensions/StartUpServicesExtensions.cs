using TicketManagement.Application;
using TicketManagement.Infrastructure;
using TicketManagement.Persistence;

namespace TicketManagement.Api.Extensions;

internal static class StartUpServicesExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        _ = builder.Services.AddApplicationServices();
        _ = builder.Services.AddInfrastructureServices(builder.Configuration);
        _ = builder.Services.AddPersistenceServices(builder.Configuration);

        _ = builder.Services.AddControllers();

        _ = builder.Services.AddCors(
            options => options.AddPolicy(
                "open",
                policy => policy.WithOrigins([builder.Configuration["ApiUrl"] ?? "https://localhost:7020",
                    builder.Configuration["BlazorUrl"] ?? "https://localhost:7080"])
                .AllowAnyMethod()
                .SetIsOriginAllowed(pol => true)
                .AllowAnyHeader()
                .AllowCredentials()));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen();

        return builder.Build();
    }
}