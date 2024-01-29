using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Application.Contracts.Infrastructure;
using TicketManagement.Application.Models.Mail;
using TicketManagement.Infrastructure.Mail;

namespace TicketManagement.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

        _ = services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
