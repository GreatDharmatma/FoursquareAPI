using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Score
    {
        internal string Json;

        public int Goal { get; private set; }
        public int CheckinsCount { get; private set; }
        public int Max { get; private set; }
        public int Recent { get; private set; }

        public Score(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);

            Recent = (int)jsonDictionary["recent"];
            Max = (int)jsonDictionary["max"];
            CheckinsCount = (int)jsonDictionary["checkinsCount"];
            if (jsonDictionary.ContainsKey("goal"))
            {
                Goal = (int)jsonDictionary["goal"];
            }
        }
    }
}
