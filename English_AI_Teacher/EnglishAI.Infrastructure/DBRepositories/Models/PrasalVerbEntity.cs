using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.DBRepositories.Models;

public record PhrasalVerbEntity : EntityBase
{
    public string PrasalVerb { get; set; } = default!;

    public List<string> Descriptions { get; init; } = new List<string>();

    public List<string> Derivatives { get; init; } = new List<string>();

    public List<string> Examples { get; init; } = new List<string>();

    public int? Frequency { get; init; } = default;
}
