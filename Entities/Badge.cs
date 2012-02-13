using System.Collections.Generic;
using Brahmastra.FoursquareApi.IO;

namespace Brahmastra.FoursquareApi.Entities
{
    public class Badge
    {
        internal string Json = "";

        public List<Checkin> Unlocks { get; private set; }
        public string Hint { get; private set; }
        public string Description { get; private set; }
        public string BadgeId { get; private set; }
        public string Name { get; private set; }
        public string Id { get; private set; }
        public Image Image { get; private set; }

        public Badge(Dictionary<string, object> jsonDictionary)
        {
            Unlocks = new List<Checkin>();
            Json = Helpers.JsonSerializer(jsonDictionary);
            Id = jsonDictionary["id"].ToString();
            BadgeId = jsonDictionary.ContainsKey("badgeID") ? jsonDictionary["badgeID"].ToString() : Id;
            Name = jsonDictionary["name"].ToString();
            Description = jsonDictionary.ContainsKey("description") ? jsonDictionary["description"].ToString() : "";
            Hint = jsonDictionary.ContainsKey("hint") ? jsonDictionary["hint"].ToString() : "";
            Image = new Image(((Dictionary<string, object>) jsonDictionary["image"]));
            foreach (object obj in (object[]) Helpers.ExtractDictionary(jsonDictionary, "response")["unlocks"])
            {
                var unlockCheckin =
                    (Dictionary<string, object>) ((object[]) ((Dictionary<string, object>) obj)["checkins"])[0];
                Unlocks.Add(new Checkin(unlockCheckin));
            }
        }
    }
}