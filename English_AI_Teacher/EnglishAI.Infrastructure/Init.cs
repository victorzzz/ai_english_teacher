using EnglishAI.Application.Interfaces;
using EnglishAI.Infrastructure.AIAssistants;
using EnglishAI.Infrastructure.DBRepositories;
using EnglishAI.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure;

public static class Init
{
    public static void InitOpenAI(this IServiceCollection services)
    {
        services.AddOptions<OpenAIOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(OpenAIOptions.SectionName).Bind(settings);
            });

        services.AddSingleton<IAIAssistant, OpenAIAssistant>();
    }

    public static void InitDatabase(this IServiceCollection services)
    {
        services.AddOptions<MongoDBOptions>()
            .Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(MongoDBOptions.SectionName).Bind(settings);
            });

        services.AddSingleton<IMongoClient, MongoClient>(
            sp =>
            {
                var options = sp.GetRequiredService<IOptions<MongoDBOptions>>();
                var mongoClient = new MongoClient(options.Value.ConnectionString);

                return mongoClient;
            });

        services.AddSingleton(
            sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetRequiredService<IOptions<MongoDBOptions>>();
                var db = client.GetDatabase(options.Value.DatabaseName);

                return db;
            });

        services.AddScoped<IPhrasalVerbRepository, PhrasalVerbRepository>();
    }
}
