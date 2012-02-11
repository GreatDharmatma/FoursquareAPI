using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Venues : Response
    {
        public int count = 0;
        public List<Venue> venues = new List<Venue>();

        public Venues(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response");
            if (JSONDictionary["venues"].GetType() == typeof(System.Object[]))
            {
                foreach (object Obj in (object[])JSONDictionary["venues"])
                {
                    venues.Add(new Venue((Dictionary<string, object>)Obj));
                }
                count = venues.Count;
            }
            else
            {
                JSONDictionary = Helpers.extractDictionary(JSONDictionary, "venues");
                if (JSONDictionary.ContainsKey("count"))
                {
                    count = (int)JSONDictionary["count"];
                }
                foreach (object Obj in (object[])JSONDictionary["items"])
                {
                    Dictionary<string, object> VenueHistoryObj = (Dictionary<string, object>)Obj;
                    int beenHere = 0;
                    if (VenueHistoryObj.ContainsKey("beenHere"))
                    {
                        beenHere = (int)VenueHistoryObj["beenHere"];
                    }

                    Venue Venue = new Venue((Dictionary<string, object>)((Dictionary<string, object>)Obj)["venue"]);
                    Venue.beenHere = beenHere;
                    venues.Add(Venue);
                }
            }
        }
    }
}
