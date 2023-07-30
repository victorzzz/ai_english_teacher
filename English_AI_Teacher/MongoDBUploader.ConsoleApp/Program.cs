using Serilog;
using Microsoft.Extensions.Hosting;
using EnglishAI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDBUploader.ConsoleApp;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.InitDatabase();
        services.AddSingleton<App>();
        services.AddHttpClient();
    })
    .UseSerilog()
    .Build();

var logger = host.Services.GetRequiredService<ILogger<App>>();

logger.LogInformation("Starting application");

var app = host.Services.GetRequiredService<App>();

logger.LogInformation("Processing external sources");
app.ProcessPhrasalVerbs(CancellationToken.None).Wait();


