using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.UIControllers
{
    public class VocabularyPhrasalVerbsController : VocabularyControllerBase
    {
        public static IImmutableList<int> Frequency => new List<int> { 0, 1, 2, 3, 4, 5 }.ToImmutableList();

        public int SelectedFrequency { get; set; }
    }
}
