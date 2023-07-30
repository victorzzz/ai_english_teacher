using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.Models.DB;

public record PhrasalVerb : ApplicationEntityBase
{
    public string PrasavBerb { get; init; } = default!;

    public List<string> Descriptions { get; init; } = new List<string>();

    public List<string> Deriviates { get; init; } = new List<string>();

    public List<string> Examples { get; init; } = new List<string>();

    public int? Frequency { get; init; } = default;
}
