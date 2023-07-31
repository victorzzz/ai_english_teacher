using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EnglishAI.Infrastructure.DBRepositories.Models;

public record IrregularVerbEntity : EntityBase
{
    public string BaseForm { get; set; } = default!;

    public List<string> PastTenses { get; init; } = new List<string>();

    public List<string> PastParticiples { get; init; } = new List<string>();

    public List<string> Descriptions { get; init; } = new List<string>();
}
