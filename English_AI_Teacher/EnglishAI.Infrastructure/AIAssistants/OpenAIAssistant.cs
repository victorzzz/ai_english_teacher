using EnglishAI.Application.Interfaces;
using EnglishAI.Application.Models.AI;
using Microsoft.Extensions.Options;
using OpenAI_API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.AIAssistants
{
    public class OpenAIAssistant : IAIAssistant
    {
        private readonly Options.OpenAIOptions _options;
        private readonly OpenAIAPI _openAIAPI;

        public OpenAIAssistant(IOptions<Options.OpenAIOptions> options) 
        { 
            _options = options.Value;
            _openAIAPI = new OpenAIAPI(_options.ApiKey);
        }

        public Task<string> AskAsync(Session request)
        {
            throw new NotImplementedException();
        }
    }
}
