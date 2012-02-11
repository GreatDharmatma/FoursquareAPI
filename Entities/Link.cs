using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Link : Response
    {
        public Provider provider;
        public string url = "";
        public string linkedId = "";

        public Link(Dictionary<string, object> JSONDictionary)
            : base(JSONDictionary)
        {
            if (JSONDictionary.ContainsKey("provider"))
            {
                provider = new Provider(JSONDictionary);
            }
            url = Helpers.getDictionaryValue(JSONDictionary, "url");
            linkedId = Helpers.getDictionaryValue(JSONDictionary, "linkedId");
        }
    }
}
