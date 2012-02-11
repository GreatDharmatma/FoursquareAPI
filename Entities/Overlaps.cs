using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Overlaps
    {
        public string id = "";
        public string createdAt = "";
        public string type = "";
        public string timeZone = "";
        public User user;
        private string JSON = "";

        public Overlaps(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            id = JSONDictionary["id"].ToString();
            createdAt = JSONDictionary["createdAt"].ToString();
            type = JSONDictionary["type"].ToString();
            timeZone = JSONDictionary["timeZone"].ToString();
            user = new User((Dictionary<string, object>)JSONDictionary["user"]);
        }
    }
}
