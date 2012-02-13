using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Source
    {
        internal string Json;

        public string Url { get; private set; }
        public string Name { get; private set; }

        public Source(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);
            Name = jsonDictionary["name"].ToString();
            Url = jsonDictionary["url"].ToString();
        }
    }
}
