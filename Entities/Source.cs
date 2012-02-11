using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Source
    {
        public string name = "";
        public string URL = "";
        private string JSON = "";

        public Source(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            name = JSONDictionary["name"].ToString();
            URL = JSONDictionary["url"].ToString();
        }
    }
}
