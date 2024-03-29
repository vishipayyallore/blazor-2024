﻿using Microsoft.EntityFrameworkCore;
using TicketManagement.Persistence;

namespace TicketManagement.Api.Extensions;

internal static class StartUpDatabaseExtensions
{
    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        try
        {
            TicketManagementDbContext? context = scope.ServiceProvider.GetService<TicketManagementDbContext>();
            if (context is not null)
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            //add logging here later on
            Console.WriteLine(ex.Message);
        }
    }
}