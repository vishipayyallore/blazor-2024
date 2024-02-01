using TicketManagement.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder
       .ConfigureServices()
       .ConfigurePipeline();

await app.ResetDatabaseAsync();

app.Run();
