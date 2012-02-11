using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class FourSquareSource
    {
        public string name = "";
        public string URL = "";
        private string JSON = "";

        public FourSquareSource(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            name = JSONDictionary["name"].ToString();
            URL = JSONDictionary["url"].ToString();
        }
    }
}
