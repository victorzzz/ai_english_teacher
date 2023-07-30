using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure.DBRepositories.Models;

public record PhrasalVerbEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; init; }

    public string PhrasalVerb { get; init; } = default!;

    public List<string> Descriptions { get; init; } = new List<string>();

    public List<string> Deriviates { get; init; } = new List<string>();

    public List<string> Examples { get; init; } = new List<string>();

    public List<string> Synonyms { get; init; } = new List<string>();

    public int? Frequency { get; init; }
}
