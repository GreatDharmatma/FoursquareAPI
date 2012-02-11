using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Provider : Response
    {
        public string id = "";

        public Provider(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            JSONDictionary = Helpers.extractDictionary(JSONDictionary, "provider");
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
        }
    }
}
