using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class VenueCategories : Response
    {
        public List<VenueCategory> Categories { get; private set; }

        public VenueCategories(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            Categories = new List<VenueCategory>();
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "response");
            foreach (var obj in (object[]) jsonDictionary["categories"])
                Categories.Add(new VenueCategory((Dictionary<string, object>) obj));
        }
    }
}
