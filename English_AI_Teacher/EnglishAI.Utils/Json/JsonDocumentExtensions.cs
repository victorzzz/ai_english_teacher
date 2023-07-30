using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EnglishAI.Utils.Json
{
    public static class JsonDocumentExtensions
    {
        public static T? Deserialize<T>(this JsonElement jsonElement)
        {
            var json = jsonElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
