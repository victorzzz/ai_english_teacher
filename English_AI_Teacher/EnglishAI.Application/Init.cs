using EnglishAI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application
{
    public static class Init
    {
        public static IImmutableList<Type> GetUIControllers()
        {
            return ImmutableList.Create( 
                typeof(UIControllers.VocabularyPhrasalVerbsController)
                );

        }
    }
}
