using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.UIControllers;

public class VocabularyPhrasalVerbsController : VocabularyControllerBase
{
    public IImmutableList<int> Frequencies => ImmutableList.Create( 0, 1, 2, 3, 4, 5 );

    public int SelectedFrequency { get; set; }
}
