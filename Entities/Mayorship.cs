using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Mayorship
    {
        internal string Json = "";

        public string Type { get; private set; }
        public string Checkins { get; private set; }
        public User User { get; private set; }

        public Mayorship(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);

            Type = Helpers.GetDictionaryValue(jsonDictionary, "type");
            Checkins = Helpers.GetDictionaryValue(jsonDictionary, "count");

            if (jsonDictionary.ContainsKey("user"))
                User = new User((Dictionary<string, object>) jsonDictionary["user"]);
        }
    }
}

