using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class VenueCategory
    {
        public string id = "";
        public string name = "";
        public string pluralName = "";
        public string icon = "";
        public List<Category> categories = new List<Category>();
        private string JSON = "";

        public VenueCategory(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            id = Helpers.getDictionaryValue(JSONDictionary, "id");
            name = Helpers.getDictionaryValue(JSONDictionary, "name");
            pluralName = Helpers.getDictionaryValue(JSONDictionary, "pluralName");
            icon = Helpers.getDictionaryValue(JSONDictionary, "icon");
            if (JSONDictionary.ContainsKey("categories"))
            {
                foreach (object Obj in (object[])JSONDictionary["categories"])
                {
                    categories.Add(new Category((Dictionary<string, object>)Obj));
                }
            }
        }
    }
}
