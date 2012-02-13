using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class VenueCategory
    {
        internal string Json = "";

        public List<Category> Categories { get; private set; }
        public string PluralName { get; private set; }
        public string Icon { get; private set; }
        public string Name { get; private set; }
        public string Id { get; private set; }
        
        public VenueCategory(Dictionary<string, object> jsonDictionary)
        {
            Categories = new List<Category>();
            Json = Helpers.JsonSerializer(jsonDictionary);
            Id = Helpers.GetDictionaryValue(jsonDictionary, "id");
            Name = Helpers.GetDictionaryValue(jsonDictionary, "name");
            PluralName = Helpers.GetDictionaryValue(jsonDictionary, "pluralName");
            Icon = Helpers.GetDictionaryValue(jsonDictionary, "icon");
            if (jsonDictionary.ContainsKey("categories"))
                foreach (var obj in (object[]) jsonDictionary["categories"])
                    Categories.Add(new Category((Dictionary<string, object>) obj));
        }
    }
}
