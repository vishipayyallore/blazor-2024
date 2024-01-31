using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Persistence.Repositories;

namespace TicketManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDbContext<GloboTicketDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TicketManagementConnectionString")));

        _ = services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

        _ = services.AddScoped<ICategoryRepository, CategoryRepository>();

        _ = services.AddScoped<IEventRepository, EventRepository>();

        _ = services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
