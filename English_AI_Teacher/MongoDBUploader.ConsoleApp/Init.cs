using EnglishAI.Application.Interfaces;
using EnglishAI.Infrastructure.AIAssistants;
using EnglishAI.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDBUploader.ConsoleApp.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBUploader.ConsoleApp
{
    public static class Init
    {
        public static void InitApp(this IServiceCollection services)
        {
            services.AddOptions<AppOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(AppOptions.SectionName).Bind(settings);
                });

            services.AddScoped<App>();
        }
    }
}
