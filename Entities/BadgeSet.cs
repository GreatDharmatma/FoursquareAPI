using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;


namespace Brahmastra.FoursquareAPI.Entities
{
    public class BadgeSet
    {
        public string type = "";
        public string name = "";
        public Image image;
        public List<string> items = new List<string>();
        public List<BadgeSet> groups = new List<BadgeSet>();
        private string JSON = "";

        public BadgeSet(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            type = JSONDictionary["type"].ToString();
            name = JSONDictionary["name"].ToString();
            image = new Image((Dictionary<string, object>)JSONDictionary["image"]);

            foreach (object Obj in (object[])JSONDictionary["items"])
            {
                items.Add((string)Obj);
            }

            foreach (object Obj in (object[])JSONDictionary["groups"])
            {
                groups.Add(new BadgeSet((Dictionary<string, object>)Obj));
            }
        }
    }
}
