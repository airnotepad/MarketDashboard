using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

try
{

    var builder = WebApplication.CreateBuilder(args);
    
    // Add services to the container.
    builder.Host.UseSerilog();

    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    var app = builder.Build();
    Log.Information("Starting web host");

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<IApplicationDbContext>();

            await ApplicationDbContextSeed.SeedSampleDataAsync(context);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "An error occurred while migrating or seeding the database.");

            return;
        }
    }

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return;
}
finally
{
    Log.CloseAndFlush();
}