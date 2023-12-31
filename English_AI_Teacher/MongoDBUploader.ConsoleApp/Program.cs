﻿using Serilog;  
using Microsoft.Extensions.Hosting;
using EnglishAI.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MongoDBUploader.ConsoleApp;
using Microsoft.Extensions.Logging;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddAutoMapper(typeof(AutoMapperProfileInfrastructure));
        services.InitDatabase();
        services.AddHttpClient();
        services.InitApp();
    })
    .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration))
    .Build();

using var scope = host.Services.CreateScope();

var logger = scope.ServiceProvider.GetRequiredService<ILogger<App>>();

logger.LogInformation("Starting application");

var app = scope.ServiceProvider.GetRequiredService<App>();

logger.LogInformation("Processing external sources ...");

//app.ProcessPhrasalVerbs(CancellationToken.None).Wait();
app.ProcessIrregularVerbs(CancellationToken.None).Wait();

logger.LogInformation("Processing external sources is finished");


