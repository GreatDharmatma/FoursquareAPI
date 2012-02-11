using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class PhotoSize
    {
        public string url = "";
        public string width = "";
        public string height = "";
        private string JSON = "";

        public PhotoSize(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            url = Helpers.getDictionaryValue(JSONDictionary, "url");
            width = Helpers.getDictionaryValue(JSONDictionary, "width");
            height = Helpers.getDictionaryValue(JSONDictionary, "height");
        }
    }
}
