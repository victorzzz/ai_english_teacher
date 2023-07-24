using EnglishAI.Application.Models.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Application.Interfaces;

public interface IAIAssistant
{
    Task<string> AskAsync(Session request);
}
