using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Links : Response
    {
        public int Count { get; private set; }
        public List<Link> VenueLink { get; private set; }

        public Links(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            VenueLink = new List<Link>();
            Count = 0;
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:links");
            if (jsonDictionary.ContainsKey("count"))
                Count = (int) jsonDictionary["count"];

            foreach (var obj in (object[]) jsonDictionary["items"])
                VenueLink.Add(new Link((Dictionary<string, object>) obj));
        }
    }
}
