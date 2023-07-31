using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnglishAI.Application.Models.DB;

public record IrregularVerb : ApplicationEntityBase
{
    public string BaseForm { get; set; } = default!;

    [JsonPropertyName("2")]
    public List<string> PastTenses { get; init; } = new List<string>();

    [JsonPropertyName("3")]
    public List<string> PastParticiples { get; init; } = new List<string>();

    [JsonPropertyName("description")]
    public List<string> Descriptions { get; init; } = new List<string>();
}
