using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Image
    {
        public string prefix = "";
        public string name = "";
        public List<string> sizes = new List<string>();
        private string JSON = "";

        public Image(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);

            prefix = JSONDictionary["prefix"].ToString();
            name = JSONDictionary["name"].ToString();

            foreach (object Size in ((object[])JSONDictionary["sizes"]))
            {
                sizes.Add(Size.ToString());
            }
        }
    }
}
