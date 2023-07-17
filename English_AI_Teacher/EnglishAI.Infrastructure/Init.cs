using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure
{
    public static class Init
    {
        public static void InitOpenAI(this IServiceCollection services)
        {
            // add OpenAI options
            services.AddOptions<Options.OpenAI>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("OpenAI").Bind(settings);
                });
        }

        public static void InitDatabase(this IServiceCollection services)
        {

        }
    }
}
