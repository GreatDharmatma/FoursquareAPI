using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class VenueCategories : Response
    {
        public List<VenueCategory> categories = new List<VenueCategory>();

        public VenueCategories(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "response");
            foreach (object Obj in (object[])JSONDictionary["categories"])
            {
                categories.Add(new VenueCategory((Dictionary<string, object>)Obj));
            }
        }
    }
}
