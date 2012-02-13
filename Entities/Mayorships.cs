using System;
using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Mayorships : Response
    {
        private readonly int _count;

        public int Count { get { return _count; } }
        public List<Venue> MayorVenues1 { get; set; }

        public Mayorships(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            MayorVenues1 = new List<Venue>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response:mayorships");
            var strCount = Helpers.GetDictionaryValue(jsonDictionary, "count");
            Int32.TryParse(strCount, out _count);
            foreach (var obj in (object[]) jsonDictionary["items"])
                MayorVenues1.Add(new Venue((Dictionary<string, object>) obj));
        }
    }
}
