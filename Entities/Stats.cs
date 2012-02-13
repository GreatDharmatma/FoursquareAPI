using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Stats
    {
        internal string Json;

        public string CheckinsCount { get; private set; }
        public string UsersCount { get; private set; }
        public string TipCount { get; private set; }

        public Stats(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);

            CheckinsCount = Helpers.GetDictionaryValue(jsonDictionary, "checkinsCount");
            UsersCount = Helpers.GetDictionaryValue(jsonDictionary, "usersCount");
            TipCount = Helpers.GetDictionaryValue(jsonDictionary, "tipCount");
        }
    }
}
