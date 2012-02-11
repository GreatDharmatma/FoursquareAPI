using System.Collections.Generic;
using Brahmastra.FoursquareAPI.IO;

namespace Brahmastra.FoursquareAPI.Entities
{
    public class Mayorship
    {
        public string Type = "";
        public string Checkins = "";
        public string DaysBehind = "";
        public string Message = "";
        public User User;
        public string ImageURL = "";
        private string JSON = "";

        public Mayorship(Dictionary<string, object> JSONDictionary)
        {
            JSON = Helpers.JSONSerializer(JSONDictionary);
            Type = Helpers.getDictionaryValue(JSONDictionary, "type");

            Checkins = Helpers.getDictionaryValue(JSONDictionary, "Checkins");
            DaysBehind = Helpers.getDictionaryValue(JSONDictionary, "DaysBehind");
            Message = Helpers.getDictionaryValue(JSONDictionary, "Message");
            ImageURL = Helpers.getDictionaryValue(JSONDictionary, "ImageURL");
            if (JSONDictionary.ContainsKey("user"))
            {
                User = new User((Dictionary<string, object>)JSONDictionary["user"]);
            }
        }

    }
}
