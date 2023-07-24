using Serilog;
using Microsoft.Extensions.Hosting;
using EnglishAI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDBUploader.ConsoleApp;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.InitDatabase();
        services.AddSingleton<App>();
        services.AddHttpClient();
    })
    .UseSerilog()
    .Build();


