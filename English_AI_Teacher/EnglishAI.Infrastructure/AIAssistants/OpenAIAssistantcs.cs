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
    public class OpenAIAssistantcs : IAIAssistant
    {
        private readonly Options.OpenAI _options;
        private readonly OpenAIAPI _openAIAPI;

        public OpenAIAssistantcs(IOptions<Options.OpenAI> options) 
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
