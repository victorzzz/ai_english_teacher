using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.Models
{
    public record ApplicationEntityBase
    {
        public string Id { get; init; } = default!;
    }
}
