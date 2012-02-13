using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Venues : Response
    {
        public List<Venue> Venue { get; set; }

        public int Count { get; private set; }

        public Venues(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Count = 0;
            Venue = new List<Venue>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            if (jsonDictionary["venues"].GetType() == typeof(System.Object[]))
            {
                foreach (var obj in (object[])jsonDictionary["venues"])
                {
                    Venue.Add(new Venue((Dictionary<string, object>)obj));
                }
                Count = Venue.Count;
            }
            else
            {
                jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "venues");
                if (jsonDictionary.ContainsKey("count"))
                    Count = (int) jsonDictionary["count"];
                foreach (var obj in (object[])jsonDictionary["items"])
                {
                    var venueHistoryObj = (Dictionary<string, object>)obj;
                    var beenHere = 0;
                    if (venueHistoryObj.ContainsKey("beenHere"))
                        beenHere = (int) venueHistoryObj["beenHere"];
                    //(Dictionary<string, object>) ((Dictionary<string, object>) obj)["venue"]
                    var venue = new Venue(Helpers.ExtractDictionary((Dictionary<string, object>) obj,"venue"))
                                    {BeenHere = beenHere};
                    Venue.Add(venue);
                }
            }
        }
    }
}
