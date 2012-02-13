using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
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
