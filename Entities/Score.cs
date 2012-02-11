using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Score
    {
        public int recent = 0;
        public int max = 0;
        public int checkinsCount = 0;
        public int goal = 0;
        private string JSON = "";

        public Score(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            recent = (int)JSONDictionary["recent"];
            max = (int)JSONDictionary["max"];
            checkinsCount = (int)JSONDictionary["checkinsCount"];
            if (JSONDictionary.ContainsKey("goal"))
            {
                goal = (int)JSONDictionary["goal"];
            }
        }
    }
}
