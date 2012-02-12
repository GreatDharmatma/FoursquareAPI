using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Location
    {
        internal string Json = "";

        public string Distance { get; private set; }
        public string Longitude { get; private set; }
        public string Latitude { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        public string CrossStreet { get; private set; }
        public string Address { get; private set; }

        public Location(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);

            Address = Helpers.GetDictionaryValue(jsonDictionary, "address");
            CrossStreet = Helpers.GetDictionaryValue(jsonDictionary, "crossStreet");
            City = Helpers.GetDictionaryValue(jsonDictionary, "city");
            State = Helpers.GetDictionaryValue(jsonDictionary, "state");
            PostalCode = Helpers.GetDictionaryValue(jsonDictionary, "postalCode");
            Country = Helpers.GetDictionaryValue(jsonDictionary, "country");
            Latitude = Helpers.GetDictionaryValue(jsonDictionary, "lat");
            Longitude = Helpers.GetDictionaryValue(jsonDictionary, "lng");
            Distance = Helpers.GetDictionaryValue(jsonDictionary, "distance");
        }
    }
}
