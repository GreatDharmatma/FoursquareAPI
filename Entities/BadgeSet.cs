using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;


namespace Brahmastra.FoursquareApi.Entities
{
    public class BadgeSet
    {
        internal string Json;

        public List<BadgeSet> Groups { get; private set; }
        public List<string> Items { get; private set; }
        public Image Image { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        
        public BadgeSet(Dictionary<string, object> jsonDictionary)
        {
            Items = new List<string>();
            Groups = new List<BadgeSet>();
            Json = Helpers.JsonSerializer(jsonDictionary);
            Type = jsonDictionary["type"].ToString();
            Name = jsonDictionary["name"].ToString();
            Image = new Image((Dictionary<string, object>)jsonDictionary["image"]);

            foreach (object obj in (object[])jsonDictionary["items"])
            {
                Items.Add((string)obj);
            }

            foreach (object obj in (object[])jsonDictionary["groups"])
            {
                Groups.Add(new BadgeSet((Dictionary<string, object>)obj));
            }
        }

        
    }
}
