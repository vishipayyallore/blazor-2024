namespace TicketManagement.Api.Extensions
{
    internal static class StartUpPipelineExtensions
    {
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
    }
}