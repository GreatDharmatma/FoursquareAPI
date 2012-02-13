using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Link : Response
    {
        public string LinkedId { get; private set; }
        public string Url { get; private set; }
        public Provider Provider { get; private set; }

        public Link(Dictionary<string, object> jsonDictionary)
            : base(jsonDictionary)
        {
            if (jsonDictionary.ContainsKey("provider"))
                Provider = new Provider(jsonDictionary);
            Url = Helpers.GetDictionaryValue(jsonDictionary, "url");
            LinkedId = Helpers.GetDictionaryValue(jsonDictionary, "linkedId");
        }
    }
}
