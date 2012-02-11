using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Location
    {
        public string Address = "";
        public string CrossStreet = "";
        public string City = "";
        public string State = "";
        public string PostalCode = "";
        public string Country = "";
        public string Lat = "";
        public string Long = "";
        public string Distance = "";
        private string JSON = "";

        public Location(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);

            Address = Helpers.getDictionaryValue(JSONDictionary, "address");
            CrossStreet = Helpers.getDictionaryValue(JSONDictionary, "crossStreet");
            City = Helpers.getDictionaryValue(JSONDictionary, "city");
            State = Helpers.getDictionaryValue(JSONDictionary, "state");
            PostalCode = Helpers.getDictionaryValue(JSONDictionary, "postalCode");
            Country = Helpers.getDictionaryValue(JSONDictionary, "country");
            Lat = Helpers.getDictionaryValue(JSONDictionary, "lat");
            Long = Helpers.getDictionaryValue(JSONDictionary, "lng");
            Distance = Helpers.getDictionaryValue(JSONDictionary, "distance");
        }
    }
}
