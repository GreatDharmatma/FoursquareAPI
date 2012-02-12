using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Provider : Response
    {
        public string Id { get; private set; }

        public Provider(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            jsonDictionary = Helpers.ExtractDictionary(jsonDictionary, "provider");
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
        }
    }
}
