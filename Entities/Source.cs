using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
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
