using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using EnglishAI.Host;
using EnglishAI.Host.Shared;
using EnglishAI.Application.UIControllers;

namespace EnglishAI.Host.Pages
{
    public partial class VocabularyPhrasalVerbs
    {
        [Inject]
        public required VocabularyPhrasalVerbsController Controller { get; set; }
    }
}