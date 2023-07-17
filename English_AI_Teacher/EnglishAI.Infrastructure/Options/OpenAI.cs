using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.Options
{
    public record OpenAI
    {
        public string ApiKey { get; init; }
    }
}
