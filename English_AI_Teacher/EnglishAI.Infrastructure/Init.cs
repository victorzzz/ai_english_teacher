using EnglishAI.Application.Interfaces;
using EnglishAI.Infrastructure.AIAssistants;
using EnglishAI.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
