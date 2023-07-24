using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.Options;

public record OpenAIOptions
{
    public static readonly string SectionName = "OpenAI";

    public string? ApiKey { get; init; }
}
