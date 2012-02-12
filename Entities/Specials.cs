using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Specials : Response
    {
        public List<Special> Special { get; private set; }

        public int Count { get; private set; }

        public Specials(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Special = new List<Special>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:specials");
            foreach (var obj in (object[]) jsonDictionary["items"])
                Special.Add(new Special((Dictionary<string, object>) obj));

            Count = jsonDictionary.ContainsKey("count") ? (int) jsonDictionary["count"] : Special.Count;
        }
    }
}
