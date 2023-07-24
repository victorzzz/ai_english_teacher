using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.Options;

public record MongoDBOptions
{
    public static readonly string SectionName = "MongoDB";
    public string? ConnectionString { get; init; }
    public string? DatabaseName { get; init; }
}
