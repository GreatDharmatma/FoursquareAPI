using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class PhotoSize
    {
        internal string Json = "";

        public string Url { get; private set; }
        public string Width { get; private set; }
        public string Height { get; private set; }

        public PhotoSize(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);
            Url = Helpers.GetDictionaryValue(jsonDictionary, "url");
            Width = Helpers.GetDictionaryValue(jsonDictionary, "width");
            Height = Helpers.GetDictionaryValue(jsonDictionary, "height");
        }
    }
}
