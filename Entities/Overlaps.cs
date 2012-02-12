using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Overlaps
    {
        internal string Json = "";

        public string Id { get; private set; }
        public string CreatedAt { get; private set; }
        public string Type { get; private set; }
        public string TimeZone { get; private set; }
        public User User { get; private set; }

        public Overlaps(Dictionary<string, object> jsonDictionary)
        {
            Json = Helpers.JsonSerializer(jsonDictionary);
            Id = jsonDictionary["id"].ToString();
            CreatedAt = jsonDictionary["createdAt"].ToString();
            Type = jsonDictionary["type"].ToString();
            TimeZone = jsonDictionary["timeZone"].ToString();
            User = new User((Dictionary<string, object>)jsonDictionary["user"]);
        }
    }
}
