using TicketManagement.Application;
using TicketManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Persistence;

namespace TicketManagement.Api.Extensions;

public static class StartUpExtensions
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

        _ = builder.Services.AddSwaggerGen();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

        _ = app.UseCors("open");

        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseHttpsRedirection();
        _ = app.MapControllers();

        return app;
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        try
        {
            GloboTicketDbContext? context = scope.ServiceProvider.GetService<GloboTicketDbContext>();
            if (context is not null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            //add logging here later on
        }
    }
}
