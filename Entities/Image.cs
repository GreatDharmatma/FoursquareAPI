using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Image
    {
        internal string Json = "";

        public string Prefix { get; private set; }
        public string Name { get; private set; }
        public List<string> Sizes { get; private set; }

        public Image(Dictionary<string, object> jsonDictionary)
        {
            Sizes = new List<string>();
            Json = Helpers.JsonSerializer(jsonDictionary);

            Prefix = jsonDictionary["prefix"].ToString();
            Name = jsonDictionary["name"].ToString();

            foreach (var size in ((object[]) jsonDictionary["sizes"]))
                Sizes.Add(size.ToString());
        }
    }
}
