using System;
using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Mayorships : Response
    {
        int Count = 0;
        public List<Venue> MayorVenues = new List<Venue>();

        public Mayorships(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response:mayorships");
            string StrCount = Helpers.getDictionaryValue(JSONDictionary, "count");
            Int32.TryParse(StrCount, out Count);
            foreach (object Obj in (object[])JSONDictionary["items"])
            {
                MayorVenues.Add(new Venue((Dictionary<string, object>)Obj));
            }
        }
    }
}
